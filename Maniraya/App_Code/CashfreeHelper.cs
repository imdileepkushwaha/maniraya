using DataTier;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

public static class CashfreeHelper
{
    public const string ApiVersion = "2023-08-01";
    const string SandboxBase = "https://sandbox.cashfree.com/pg";
    const string ProductionBase = "https://api.cashfree.com/pg";

    public class CreateOrderResult
    {
        public bool Ok;
        public string OrderId;
        public string PaymentSessionId;
        public string CfOrderId;
        public string ErrorMessage;
        public string Mode;
    }

    public class PaymentRow
    {
        public string OrderId;
        public string UserId;
        public decimal Amount;
        public string Status;
        public string CfPaymentId;
        public bool SavingInserted;
        public string SavingResult;
        public DateTime EntryDate;
        public string PlanType;
        public int Quantity;
        public string ShippingType;
    }

    public static bool IsConfigured
    {
        get
        {
            return !string.IsNullOrWhiteSpace(AppId) && !string.IsNullOrWhiteSpace(SecretKey);
        }
    }

    public static string AppId
    {
        get { return GetSetting("CashfreeAppId"); }
    }

    public static string SecretKey
    {
        get { return GetSetting("CashfreeSecretKey"); }
    }

    public static bool IsSandbox
    {
        get
        {
            string env = GetSetting("CashfreeEnvironment", "sandbox");
            return !string.Equals(env, "production", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(env, "prod", StringComparison.OrdinalIgnoreCase);
        }
    }

    public static string CheckoutMode
    {
        get { return IsSandbox ? "sandbox" : "production"; }
    }

    public static string ApiBaseUrl
    {
        get { return IsSandbox ? SandboxBase : ProductionBase; }
    }

    public static void EnsureSchema()
    {
        RunNonQuery(@"
IF OBJECT_ID('dbo.SavingCashfreePayment', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SavingCashfreePayment
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        OrderId NVARCHAR(60) NOT NULL,
        UserId NVARCHAR(50) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        CustomerName NVARCHAR(200) NULL,
        CustomerPhone NVARCHAR(20) NULL,
        CustomerEmail NVARCHAR(200) NULL,
        PaymentSessionId NVARCHAR(200) NULL,
        CfOrderId NVARCHAR(100) NULL,
        CfPaymentId NVARCHAR(100) NULL,
        Status NVARCHAR(30) NOT NULL CONSTRAINT DF_SavingCashfreePayment_Status DEFAULT ('Pending'),
        SavingInserted BIT NOT NULL CONSTRAINT DF_SavingCashfreePayment_Inserted DEFAULT (0),
        SavingResult NVARCHAR(20) NULL,
        RawPayload NVARCHAR(MAX) NULL,
        EntryDate DATETIME NOT NULL CONSTRAINT DF_SavingCashfreePayment_Entry DEFAULT (GETDATE()),
        PaidDate DATETIME NULL
    );
    CREATE UNIQUE INDEX UX_SavingCashfreePayment_OrderId ON dbo.SavingCashfreePayment (OrderId);
END
IF COL_LENGTH('SavingCashfreePayment', 'PlanType') IS NULL
BEGIN
    ALTER TABLE dbo.SavingCashfreePayment ADD PlanType NVARCHAR(20) NULL;
END
IF COL_LENGTH('SavingCashfreePayment', 'Quantity') IS NULL
BEGIN
    ALTER TABLE dbo.SavingCashfreePayment ADD Quantity INT NULL;
END
IF COL_LENGTH('SavingCashfreePayment', 'ShippingType') IS NULL
BEGIN
    ALTER TABLE dbo.SavingCashfreePayment ADD ShippingType NVARCHAR(50) NULL;
END");
    }

    public static CreateOrderResult StartSavingPurchase(string userId, string userName, string mobile, string email, decimal amount, HttpRequest request)
    {
        return StartSavingPurchase(userId, userName, mobile, email, amount, request, "First", 1);
    }

    public static CreateOrderResult StartSavingPurchase(string userId, string userName, string mobile, string email, decimal amount, HttpRequest request, string purchaseType, int quantity)
    {
        return StartSavingPurchase(userId, userName, mobile, email, amount, request, purchaseType, quantity, string.Empty);
    }

    public static CreateOrderResult StartSavingPurchase(string userId, string userName, string mobile, string email, decimal amount, HttpRequest request, string purchaseType, int quantity, string shippingType)
    {
        CreateOrderResult result = new CreateOrderResult { Mode = CheckoutMode };
        if (!IsConfigured)
        {
            result.ErrorMessage = "Cashfree App ID / Secret is not configured in Web.config.";
            return result;
        }

        if (amount <= 0)
        {
            result.ErrorMessage = "Invalid payable amount.";
            return result;
        }

        if (quantity <= 0)
        {
            quantity = 1;
        }

        purchaseType = string.Equals(purchaseType, "Bulk", StringComparison.OrdinalIgnoreCase) ? "Bulk" : "First";

        string phone = NormalizePhone(mobile);
        if (string.IsNullOrWhiteSpace(phone))
        {
            result.ErrorMessage = "Please update a valid 10-digit mobile number in your profile before paying online.";
            return result;
        }

        EnsureSchema();

        string orderId = NewOrderId();
        string customerId = SanitizeCustomerId(userId);
        string customerEmail = string.IsNullOrWhiteSpace(email) ? (customerId + "@mpremium.in") : email.Trim();
        string returnUrl = BuildReturnUrl(request);
        string notifyUrl = BuildNotifyUrl(request);

        JObject body = new JObject();
        body["order_id"] = orderId;
        body["order_amount"] = amount;
        body["order_currency"] = "INR";
        body["order_note"] = purchaseType == "Bulk" ? "Saving Bulk Purchase" : "Saving First Purchase";
        body["customer_details"] = new JObject
        {
            { "customer_id", customerId },
            { "customer_name", string.IsNullOrWhiteSpace(userName) ? customerId : userName.Trim() },
            { "customer_email", customerEmail },
            { "customer_phone", phone }
        };
        body["order_meta"] = new JObject
        {
            { "return_url", returnUrl },
            { "notify_url", notifyUrl }
        };

        string responseText;
        int statusCode;
        if (!TryApi("POST", "/orders", body.ToString(), out responseText, out statusCode))
        {
            result.ErrorMessage = ExtractApiError(responseText, "Unable to create Cashfree order.");
            return result;
        }

        JObject json = ParseObject(responseText);
        string sessionId = JsonText(json, "payment_session_id");
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            result.ErrorMessage = ExtractApiError(responseText, "Cashfree did not return a payment session.");
            return result;
        }

        InsertPending(orderId, userId, amount, userName, phone, customerEmail, sessionId, JsonText(json, "cf_order_id"), purchaseType, quantity, shippingType);

        result.Ok = true;
        result.OrderId = orderId;
        result.PaymentSessionId = sessionId;
        result.CfOrderId = JsonText(json, "cf_order_id");
        return result;
    }

    public static PaymentRow GetPayment(string orderId)
    {
        EnsureSchema();
        orderId = (orderId ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return null;
        }

        DataTable dt = RunSelect(
            "SELECT TOP 1 OrderId, UserId, Amount, Status, CfPaymentId, SavingInserted, SavingResult, EntryDate, " +
            "ISNULL(PlanType, 'First') AS PlanType, ISNULL(Quantity, 1) AS Quantity, ISNULL(ShippingType, '') AS ShippingType " +
            "FROM SavingCashfreePayment WITH (NOLOCK) WHERE OrderId = '" + Escape(orderId) + "'");
        if (dt == null || dt.Rows.Count == 0)
        {
            return null;
        }

        DataRow row = dt.Rows[0];
        int quantity = 1;
        int.TryParse(Convert.ToString(row["Quantity"]), out quantity);
        if (quantity <= 0)
        {
            quantity = 1;
        }

        return new PaymentRow
        {
            OrderId = Convert.ToString(row["OrderId"]),
            UserId = Convert.ToString(row["UserId"]),
            Amount = ToDecimal(row["Amount"]),
            Status = Convert.ToString(row["Status"]),
            CfPaymentId = Convert.ToString(row["CfPaymentId"]),
            SavingInserted = ToBool(row["SavingInserted"]),
            SavingResult = Convert.ToString(row["SavingResult"]),
            EntryDate = row["EntryDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EntryDate"]),
            PlanType = Convert.ToString(row["PlanType"]),
            Quantity = quantity,
            ShippingType = Convert.ToString(row["ShippingType"])
        };
    }

    public static string GetCashfreeOrderStatus(string orderId)
    {
        string responseText;
        int statusCode;
        if (!TryApi("GET", "/orders/" + Uri.EscapeDataString(orderId ?? string.Empty), null, out responseText, out statusCode))
        {
            return string.Empty;
        }

        return JsonText(ParseObject(responseText), "order_status");
    }

    public static string GetApprovedCoupon(string userId, string transactionId)
    {
        string txn = Escape((transactionId ?? string.Empty).Trim());
        string uid = Escape((userId ?? string.Empty).Trim());
        if (string.IsNullOrWhiteSpace(uid))
        {
            return string.Empty;
        }

        string sql = "SELECT TOP 1 LTRIM(RTRIM(ISNULL(couponcode,''))) AS couponcode FROM SavingAccountDetail WITH (NOLOCK) " +
            "WHERE UserId = '" + uid + "' AND UPPER(LTRIM(RTRIM(ISNULL(Status,'')))) = 'APPROVED' " +
            "AND ISNULL(LTRIM(RTRIM(couponcode)), '') <> ''";
        if (!string.IsNullOrWhiteSpace(txn))
        {
            sql += " AND UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId,'')))) = UPPER('" + txn + "')";
        }

        sql += " ORDER BY id DESC";
        DataTable dt = RunSelect(sql);
        if (dt == null || dt.Rows.Count == 0)
        {
            return string.Empty;
        }

        return Convert.ToString(dt.Rows[0]["couponcode"]).Trim();
    }

    public static bool VerifyWebhookSignature(string timestamp, string rawBody, string signature)
    {
        if (string.IsNullOrWhiteSpace(SecretKey) || string.IsNullOrWhiteSpace(timestamp) || string.IsNullOrWhiteSpace(signature))
        {
            return false;
        }

        string signedPayload = timestamp + (rawBody ?? string.Empty);
        byte[] hash;
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey)))
        {
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signedPayload));
        }

        string computed = Convert.ToBase64String(hash);
        return SlowEquals(computed, signature.Trim());
    }

    public static string HandleWebhook(string rawBody)
    {
        EnsureSchema();
        JObject payload = ParseObject(rawBody);
        if (payload == null)
        {
            return "invalid";
        }

        string type = JsonText(payload, "type");
        JObject data = payload["data"] as JObject ?? new JObject();
        JObject order = data["order"] as JObject ?? payload["order"] as JObject ?? new JObject();
        JObject payment = data["payment"] as JObject ?? payload["payment"] as JObject ?? new JObject();

        string orderId = FirstText(order, "order_id", payload, "order_id");
        string paymentStatus = FirstText(payment, "payment_status", order, "order_status");
        string cfPaymentId = FirstText(payment, "cf_payment_id", payment, "payment_id");

        if (string.IsNullOrWhiteSpace(orderId))
        {
            return "missing-order";
        }

        string localStatus = "Pending";
        if (IsSuccessEvent(type, paymentStatus))
        {
            localStatus = "Paid";
        }
        else if (IsDroppedEvent(type, paymentStatus))
        {
            localStatus = "Dropped";
        }
        else if (IsFailedEvent(type, paymentStatus))
        {
            localStatus = "Failed";
        }

        UpdatePaymentStatus(orderId, localStatus, cfPaymentId, rawBody);

        if (localStatus == "Paid")
        {
            CompleteSavingPurchase(orderId, cfPaymentId);
        }

        return localStatus.ToLowerInvariant();
    }

    public static string RefreshAndComplete(string orderId)
    {
        PaymentRow row = GetPayment(orderId);
        if (row == null)
        {
            return "missing";
        }

        if (row.SavingInserted)
        {
            EnsureSavingApproved(row);
            return "paid";
        }

        string paymentId;
        string paymentOutcome = GetCashfreePaymentOutcome(orderId, out paymentId);
        if (!string.IsNullOrWhiteSpace(paymentId))
        {
            row.CfPaymentId = paymentId;
        }

        string cfStatus = GetCashfreeOrderStatus(orderId);
        if (string.Equals(cfStatus, "PAID", StringComparison.OrdinalIgnoreCase)
            || string.Equals(paymentOutcome, "paid", StringComparison.OrdinalIgnoreCase)
            || string.Equals(row.Status, "Paid", StringComparison.OrdinalIgnoreCase))
        {
            UpdatePaymentStatus(orderId, "Paid", row.CfPaymentId, null);
            CompleteSavingPurchase(orderId, row.CfPaymentId);
            return "paid";
        }

        if (string.Equals(paymentOutcome, "failed", StringComparison.OrdinalIgnoreCase)
            || string.Equals(cfStatus, "EXPIRED", StringComparison.OrdinalIgnoreCase)
            || string.Equals(cfStatus, "TERMINATED", StringComparison.OrdinalIgnoreCase)
            || string.Equals(row.Status, "Failed", StringComparison.OrdinalIgnoreCase))
        {
            if (!string.Equals(row.Status, "Paid", StringComparison.OrdinalIgnoreCase))
            {
                UpdatePaymentStatus(orderId, "Failed", row.CfPaymentId, null);
            }
            return "failed";
        }

        if (string.Equals(paymentOutcome, "dropped", StringComparison.OrdinalIgnoreCase)
            || string.Equals(row.Status, "Dropped", StringComparison.OrdinalIgnoreCase))
        {
            if (!string.Equals(row.Status, "Paid", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(row.Status, "Failed", StringComparison.OrdinalIgnoreCase))
            {
                UpdatePaymentStatus(orderId, "Dropped", row.CfPaymentId, null);
            }
            return "dropped";
        }

        return "pending";
    }

    public static string BuildReturnUrl(HttpRequest request)
    {
        return PublicBaseUrl(request) + "/user/SavingPaymentReturn.aspx?order_id={order_id}";
    }

    public static string BuildNotifyUrl(HttpRequest request)
    {
        return PublicBaseUrl(request) + "/user/CashfreeWebhook.ashx";
    }

    static void CompleteSavingPurchase(string orderId, string cfPaymentId)
    {
        PaymentRow row = GetPayment(orderId);
        if (row == null)
        {
            return;
        }

        if (row.SavingInserted)
        {
            EnsureSavingApproved(row);
            return;
        }

        string txnId = string.IsNullOrWhiteSpace(cfPaymentId) ? ("CF-" + orderId) : cfPaymentId.Trim();
        string res;
        if (string.Equals(row.PlanType, "Bulk", StringComparison.OrdinalIgnoreCase))
        {
            res = InsertBulkSavingAccount(row.UserId, row.Amount, txnId);
        }
        else
        {
            decimal unitAmount = row.Quantity > 1 ? decimal.Round(row.Amount / row.Quantity, 2) : row.Amount;
            res = InsertSavingAccount(row.UserId, unitAmount, txnId, orderId, row.Quantity);
        }
        bool inserted = res == "t" || res == "u";
        if (inserted)
        {
            ApplyPurchaseMeta(row.UserId, txnId, "Online", row.ShippingType);
            string approveRes = AutoApproveSavingPurchase(row.UserId, txnId);
            if (!string.IsNullOrWhiteSpace(approveRes) && approveRes != "0")
            {
                res = res + "/" + approveRes;
            }
        }

        RunNonQuery(
            "UPDATE SavingCashfreePayment SET SavingInserted = " + (inserted ? "1" : "0") +
            ", SavingResult = '" + Escape(res) + "'" +
            ", PaidDate = ISNULL(PaidDate, GETDATE())" +
            " WHERE OrderId = '" + Escape(orderId) + "'");
    }

    static void EnsureSavingApproved(PaymentRow row)
    {
        if (row == null)
        {
            return;
        }

        string txnId = string.IsNullOrWhiteSpace(row.CfPaymentId) ? ("CF-" + row.OrderId) : row.CfPaymentId.Trim();
        string approveRes = AutoApproveSavingPurchase(row.UserId, txnId);
        if (string.IsNullOrWhiteSpace(approveRes) || approveRes == "0")
        {
            return;
        }

        RunNonQuery(
            "UPDATE SavingCashfreePayment SET SavingResult = '" + Escape((row.SavingResult ?? "t") + "/" + approveRes) + "'" +
            " WHERE OrderId = '" + Escape(row.OrderId) + "'" +
            " AND (SavingResult IS NULL OR SavingResult NOT LIKE '%/' + '" + Escape(approveRes) + "')");
    }

    public static string RepairPaidPendingPurchase(string userId)
    {
        PaymentRow row = GetLatestPaidPayment(userId);
        if (row == null)
        {
            return "missing";
        }

        EnsureSavingApproved(row);
        return string.IsNullOrWhiteSpace(GetApprovedCoupon(userId, row.CfPaymentId)) ? "pending" : "paid";
    }

    static PaymentRow GetLatestPaidPayment(string userId)
    {
        EnsureSchema();
        DataTable dt = RunSelect(
            "SELECT TOP 1 OrderId FROM SavingCashfreePayment WITH (NOLOCK) " +
            "WHERE UserId = '" + Escape(userId ?? string.Empty) + "' " +
            "AND UPPER(LTRIM(RTRIM(ISNULL(Status,'')))) = 'PAID' " +
            "ORDER BY Id DESC");
        if (dt == null || dt.Rows.Count == 0)
        {
            return null;
        }

        return GetPayment(Convert.ToString(dt.Rows[0]["OrderId"]));
    }

    static string AutoApproveSavingPurchase(string userId, string transactionId)
    {
        DataTable pending = GetPendingSavingAccounts(userId, transactionId);
        if (pending == null || pending.Rows.Count == 0)
        {
            return "t";
        }

        string last = "0";
        for (int i = 0; i < pending.Rows.Count; i++)
        {
            string id = Convert.ToString(pending.Rows[i]["id"]);
            string savingOrderId = Convert.ToString(pending.Rows[i]["OrderId"]);
            last = SavingProductHelper.ExecuteScalarProc("sp_approveSavingAccountDetail", new[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@Approveby", "cashfree-online"),
                new SqlParameter("@Remark", "Auto approved after Cashfree payment success")
            });

            if ((last ?? string.Empty).Trim() != "t")
            {
                continue;
            }

            try
            {
                SavingProductHelper.ExecuteScalarProc("sp_markSavingBulkPrepaid", new[]
                {
                    new SqlParameter("@id", id)
                });
            }
            catch
            {
            }

            try
            {
                string waStatus;
                ChatwayWhatsAppHelper.TrySendInvoiceAfterApprove(
                    userId,
                    savingOrderId,
                    ChatwayWhatsAppHelper.InvoiceMessageType.FirstPurchase,
                    out waStatus);
            }
            catch
            {
            }
        }

        return string.IsNullOrWhiteSpace(last) ? "0" : last.Trim();
    }

    static DataTable GetPendingSavingAccounts(string userId, string transactionId)
    {
        string uid = Escape((userId ?? string.Empty).Trim());
        string txn = Escape((transactionId ?? string.Empty).Trim());
        if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(txn))
        {
            return new DataTable();
        }

        return RunSelect(
            "SELECT id, OrderId FROM SavingAccountDetail WITH (NOLOCK) " +
            "WHERE UserId = '" + uid + "' " +
            "AND UPPER(LTRIM(RTRIM(ISNULL(Status,'')))) IN ('PENDING','0') " +
            "AND UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId,'')))) = UPPER('" + txn + "') " +
            "ORDER BY id");
    }

    static string GetCashfreePaymentOutcome(string orderId, out string cfPaymentId)
    {
        cfPaymentId = string.Empty;
        string responseText;
        int statusCode;
        if (!TryApi("GET", "/orders/" + Uri.EscapeDataString(orderId ?? string.Empty) + "/payments", null, out responseText, out statusCode))
        {
            return string.Empty;
        }

        JArray payments = ParseArray(responseText);
        if (payments == null || payments.Count == 0)
        {
            return string.Empty;
        }

        string latestFailedId = string.Empty;
        string latestDroppedId = string.Empty;
        for (int i = 0; i < payments.Count; i++)
        {
            JObject payment = payments[i] as JObject;
            if (payment == null)
            {
                continue;
            }

            string paymentStatus = FirstText(payment, "payment_status", payment, "payment_message");
            string paymentId = FirstText(payment, "cf_payment_id", payment, "payment_id");
            if (IsSuccessEvent(string.Empty, paymentStatus))
            {
                cfPaymentId = paymentId;
                return "paid";
            }

            if (IsDroppedEvent(string.Empty, paymentStatus) && string.IsNullOrWhiteSpace(latestDroppedId))
            {
                latestDroppedId = paymentId;
            }
            else if (IsFailedEvent(string.Empty, paymentStatus) && string.IsNullOrWhiteSpace(latestFailedId))
            {
                latestFailedId = paymentId;
            }
        }

        if (!string.IsNullOrWhiteSpace(latestFailedId))
        {
            cfPaymentId = latestFailedId;
            return "failed";
        }

        if (!string.IsNullOrWhiteSpace(latestDroppedId))
        {
            cfPaymentId = latestDroppedId;
            return "dropped";
        }

        return string.Empty;
    }

    static JArray ParseArray(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new JArray();
        }

        try
        {
            JToken token = JToken.Parse(json);
            JArray array = token as JArray;
            if (array != null)
            {
                return array;
            }

            JObject obj = token as JObject;
            if (obj == null)
            {
                return new JArray();
            }

            JArray nested = obj["payments"] as JArray;
            if (nested != null)
            {
                return nested;
            }

            JArray data = obj["data"] as JArray;
            if (data != null)
            {
                return data;
            }

            return new JArray();
        }
        catch
        {
            return new JArray();
        }
    }

    static string InsertBulkSavingAccount(string userId, decimal amount, string transactionId)
    {
        string orderId = Guid.NewGuid().ToString("N").Substring(0, 8);
        string res = SavingProductHelper.InsertBulkSavingAccount(
            orderId,
            userId,
            amount,
            transactionId,
            "cashfree-online",
            userId);

        if ((res ?? string.Empty).Trim() == "t")
        {
            SavingProductHelper.SetAccountPlanType(orderId, "Bulk18");
        }

        return string.IsNullOrWhiteSpace(res) ? "0" : res.Trim();
    }

    static string InsertSavingAccount(string userId, decimal amount, string transactionId, string fallbackOrderId, int quantity)
    {
        if (quantity <= 0)
        {
            quantity = 1;
        }

        Data objData = new Data();
        SqlTransaction tr = null;
        string res = "0";
        try
        {
            SqlConnection cn = objData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            SqlParameter[] parameter = {
                new SqlParameter("@OrderId", Guid.NewGuid().ToString("N").Substring(0, 8)),
                new SqlParameter("@UserId", userId ?? string.Empty),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@OnlineTransactionId", transactionId ?? fallbackOrderId),
                new SqlParameter("@ImageName", "cashfree-online"),
                new SqlParameter("@EntryBy", userId ?? string.Empty),
                new SqlParameter("@quantity", quantity)
            };
            res = objData.RunInsUpDelQueryTransProcScalar("sp_add_SavingAccountDetail", tr, parameter);
            tr.Commit();
        }
        catch
        {
            res = "0";
            if (tr != null)
            {
                try { tr.Rollback(); }
                catch { }
            }
        }
        finally
        {
            try { objData.EndConnection(); }
            catch { }
            if (tr != null)
            {
                tr.Dispose();
            }
        }

        return string.IsNullOrWhiteSpace(res) ? "0" : res.Trim();
    }

    static void ApplyPurchaseMeta(string userId, string transactionId, string paymentMethod, string shippingType)
    {
        DataTable accounts = GetPendingSavingAccounts(userId, transactionId);
        if (accounts == null || accounts.Rows.Count == 0)
        {
            accounts = RunSelect(
                "SELECT TOP 5 OrderId FROM SavingAccountDetail WITH (NOLOCK) " +
                "WHERE UserId = '" + Escape(userId ?? string.Empty) + "' " +
                "AND UPPER(LTRIM(RTRIM(ISNULL(OnlineTransactionId,'')))) = UPPER('" + Escape(transactionId ?? string.Empty) + "') " +
                "ORDER BY id DESC");
        }

        if (accounts == null)
        {
            return;
        }

        for (int i = 0; i < accounts.Rows.Count; i++)
        {
            string savingOrderId = Convert.ToString(accounts.Rows[i]["OrderId"]);
            if (!string.IsNullOrWhiteSpace(savingOrderId))
            {
                SavingProductHelper.UpdatePurchaseMeta(savingOrderId, paymentMethod, shippingType);
            }
        }
    }

    static void InsertPending(string orderId, string userId, decimal amount, string name, string phone, string email, string sessionId, string cfOrderId, string planType, int quantity, string shippingType)
    {
        RunNonQuery(
            "INSERT INTO SavingCashfreePayment (OrderId, UserId, Amount, CustomerName, CustomerPhone, CustomerEmail, PaymentSessionId, CfOrderId, Status, PlanType, Quantity, ShippingType) VALUES (" +
            "'" + Escape(orderId) + "'," +
            "'" + Escape(userId) + "'," +
            amount.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," +
            "'" + Escape(name) + "'," +
            "'" + Escape(phone) + "'," +
            "'" + Escape(email) + "'," +
            "'" + Escape(sessionId) + "'," +
            "'" + Escape(cfOrderId) + "'," +
            "'Pending'," +
            "'" + Escape(planType) + "'," +
            quantity + "," +
            "'" + Escape(shippingType ?? string.Empty) + "')");
    }

    static void UpdatePaymentStatus(string orderId, string status, string cfPaymentId, string rawPayload)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE SavingCashfreePayment SET Status = '").Append(Escape(status)).Append("'");
        if (!string.IsNullOrWhiteSpace(cfPaymentId))
        {
            sql.Append(", CfPaymentId = '").Append(Escape(cfPaymentId)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(rawPayload))
        {
            sql.Append(", RawPayload = '").Append(Escape(rawPayload)).Append("'");
        }
        if (status == "Paid")
        {
            sql.Append(", PaidDate = ISNULL(PaidDate, GETDATE())");
        }
        sql.Append(" WHERE OrderId = '").Append(Escape(orderId)).Append("'");
        sql.Append(" AND UPPER(LTRIM(RTRIM(ISNULL(Status,'')))) <> 'PAID'");
        RunNonQuery(sql.ToString());

        if (status == "Paid")
        {
            RunNonQuery(
                "UPDATE SavingCashfreePayment SET Status = 'Paid', PaidDate = ISNULL(PaidDate, GETDATE())" +
                (string.IsNullOrWhiteSpace(cfPaymentId) ? string.Empty : ", CfPaymentId = '" + Escape(cfPaymentId) + "'") +
                " WHERE OrderId = '" + Escape(orderId) + "'");
        }
    }

    static bool TryApi(string method, string path, string jsonBody, out string responseText, out int statusCode)
    {
        responseText = string.Empty;
        statusCode = 0;
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + path);
            request.Method = method;
            request.Accept = "application/json";
            request.Timeout = 45000;
            request.ReadWriteTimeout = 45000;
            request.Headers.Add("x-client-id", AppId);
            request.Headers.Add("x-client-secret", SecretKey);
            request.Headers.Add("x-api-version", ApiVersion);

            if (!string.IsNullOrWhiteSpace(jsonBody) && !string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(jsonBody);
                request.ContentType = "application/json";
                request.ContentLength = bytes.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
            {
                statusCode = (int)response.StatusCode;
                responseText = reader.ReadToEnd();
            }

            return statusCode >= 200 && statusCode < 300;
        }
        catch (WebException ex)
        {
            try
            {
                HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                if (errorResponse != null)
                {
                    statusCode = (int)errorResponse.StatusCode;
                    using (Stream stream = errorResponse.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream ?? Stream.Null, Encoding.UTF8))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                else
                {
                    responseText = ex.Message;
                }
            }
            catch
            {
                responseText = ex.Message;
            }
            return false;
        }
        catch (Exception ex)
        {
            responseText = ex.Message;
            return false;
        }
    }

    static string PublicBaseUrl(HttpRequest request)
    {
        string configured = GetSetting("CashfreePublicBaseUrl", "https://mpremium.in").Trim().TrimEnd('/');
        string useHost = GetSetting("CashfreeUseRequestHost", "false");
        bool fromRequest = string.Equals(useHost, "true", StringComparison.OrdinalIgnoreCase)
            || string.Equals(useHost, "1", StringComparison.OrdinalIgnoreCase);
        if (fromRequest && request != null && request.Url != null)
        {
            return request.Url.GetLeftPart(UriPartial.Authority);
        }

        return string.IsNullOrWhiteSpace(configured) ? "https://mpremium.in" : configured;
    }

    static string NewOrderId()
    {
        return "SAV" + DateTime.Now.ToString("yyMMddHHmmssfff") + new Random().Next(100, 999);
    }

    static string SanitizeCustomerId(string userId)
    {
        string value = Regex.Replace(userId ?? string.Empty, "[^A-Za-z0-9_-]", "_");
        if (string.IsNullOrWhiteSpace(value))
        {
            value = "USER";
        }
        if (value.Length > 45)
        {
            value = value.Substring(0, 45);
        }
        return value;
    }

    static string NormalizePhone(string mobile)
    {
        if (string.IsNullOrWhiteSpace(mobile))
        {
            return string.Empty;
        }

        string digits = Regex.Replace(mobile, @"\D", string.Empty);
        if (digits.Length >= 12 && digits.StartsWith("91"))
        {
            digits = digits.Substring(digits.Length - 10);
        }
        else if (digits.Length > 10)
        {
            digits = digits.Substring(digits.Length - 10);
        }

        return digits.Length == 10 ? digits : string.Empty;
    }

    static bool IsSuccessEvent(string type, string paymentStatus)
    {
        return ContainsAny(type, "PAYMENT_SUCCESS", "SUCCESS_PAYMENT")
            || string.Equals(paymentStatus, "SUCCESS", StringComparison.OrdinalIgnoreCase)
            || string.Equals(paymentStatus, "PAID", StringComparison.OrdinalIgnoreCase);
    }

    static bool IsFailedEvent(string type, string paymentStatus)
    {
        return ContainsAny(type, "PAYMENT_FAILED", "FAILED_PAYMENT")
            || string.Equals(paymentStatus, "FAILED", StringComparison.OrdinalIgnoreCase)
            || string.Equals(paymentStatus, "FAILURE", StringComparison.OrdinalIgnoreCase);
    }

    static bool IsDroppedEvent(string type, string paymentStatus)
    {
        return ContainsAny(type, "USER_DROPPED", "DROPPED")
            || string.Equals(paymentStatus, "USER_DROPPED", StringComparison.OrdinalIgnoreCase)
            || string.Equals(paymentStatus, "CANCELLED", StringComparison.OrdinalIgnoreCase);
    }

    static bool ContainsAny(string value, params string[] tokens)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        for (int i = 0; i < tokens.Length; i++)
        {
            if (value.IndexOf(tokens[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
        }

        return false;
    }

    static string ExtractApiError(string responseText, string fallback)
    {
        JObject json = ParseObject(responseText);
        string message = FirstText(json, "message", json, "error_description");
        return string.IsNullOrWhiteSpace(message) ? fallback : message;
    }

    static JObject ParseObject(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new JObject();
        }

        try
        {
            return JObject.Parse(json) ?? new JObject();
        }
        catch
        {
            return new JObject();
        }
    }

    static string JsonText(JObject obj, string key)
    {
        if (obj == null || obj[key] == null || obj[key].Type == JTokenType.Null)
        {
            return string.Empty;
        }

        return Convert.ToString(obj[key]).Trim();
    }

    static string FirstText(JObject first, string firstKey, JObject second, string secondKey)
    {
        string value = JsonText(first, firstKey);
        return string.IsNullOrWhiteSpace(value) ? JsonText(second, secondKey) : value;
    }

    static string GetSetting(string key, string fallback = "")
    {
        try
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        }
        catch
        {
            return fallback;
        }
    }

    static string Escape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
    }

    static decimal ToDecimal(object value)
    {
        decimal amount;
        return decimal.TryParse(Convert.ToString(value), out amount) ? amount : 0m;
    }

    static bool ToBool(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return false;
        }

        if (value is bool)
        {
            return (bool)value;
        }

        return Convert.ToString(value) == "1" || string.Equals(Convert.ToString(value), "true", StringComparison.OrdinalIgnoreCase);
    }

    static bool SlowEquals(string a, string b)
    {
        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        int result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result |= a[i] ^ b[i];
        }
        return result == 0;
    }

    static DataTable RunSelect(string sql)
    {
        Data objData = new Data();
        DataTable dt = new DataTable();
        try
        {
            objData.StartConnection();
            try
            {
                dt = objData.RunDataTable(sql);
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    static bool RunNonQuery(string sql)
    {
        Data objData = new Data();
        try
        {
            objData.StartConnection();
            try
            {
                objData.RunInsUpDelQuery(sql);
                return true;
            }
            finally
            {
                objData.EndConnection();
            }
        }
        catch
        {
            return false;
        }
    }
}
