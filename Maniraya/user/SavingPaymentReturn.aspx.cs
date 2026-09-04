using System;
using System.Web;
using System.Web.UI;

public partial class user_SavingPaymentReturn : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            string orderId = Convert.ToString(Request.QueryString["order_id"]);
            string returnUrl = ResolveUrl("~/user/SavingPaymentReturn.aspx");
            if (!string.IsNullOrWhiteSpace(orderId))
            {
                returnUrl += "?order_id=" + HttpUtility.UrlEncode(orderId);
            }
            Session["returnUrl"] = returnUrl;
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (IsPostBack)
        {
            return;
        }

        BindStatus();
    }

    void BindStatus()
    {
        string orderId = Convert.ToString(Request.QueryString["order_id"]).Trim();
        litOrderId.Text = string.IsNullOrWhiteSpace(orderId) ? "-" : orderId;

        if (string.IsNullOrWhiteSpace(orderId))
        {
            SetState("pending", "Payment not found", "Order ID is missing. If money was deducted, wait a minute and check order history.", "Missing", "-");
            return;
        }

        CashfreeHelper.PaymentRow row = CashfreeHelper.GetPayment(orderId);
        if (row == null)
        {
            SetState("pending", "Payment not found", "We could not find this Cashfree order. If money was deducted, contact support with the Order ID.", "Missing", "-");
            return;
        }

        if (!string.Equals(row.UserId, Convert.ToString(Session["userid"]), StringComparison.OrdinalIgnoreCase))
        {
            SetState("failed", "Access denied", "This payment does not belong to the logged-in user.", "Denied", "-");
            return;
        }

        string status = CashfreeHelper.RefreshAndComplete(orderId);
        row = CashfreeHelper.GetPayment(orderId) ?? row;
        string amountText = "₹ " + row.Amount.ToString("N2");
        string paymentId = string.IsNullOrWhiteSpace(row.CfPaymentId) ? "-" : row.CfPaymentId;

        if (status == "paid")
        {
            if (string.Equals(row.PlanType, "Installment", StringComparison.OrdinalIgnoreCase))
            {
                string extra = row.SavingResult == "f"
                    ? "Payment is successful, but this installment is already in process. Admin will review it."
                    : "Payment is successful. Your installment request is submitted. Admin will verify and approve it, same as UTR or cash.";
                SetInstallmentReturnLinks(row.InstallmentId);
                SetState("paid", "Installment payment successful", extra, "Paid", amountText, paymentId);
                return;
            }

            string coupon = CashfreeHelper.GetApprovedCoupon(row.UserId, row.CfPaymentId);
            string extra;
            if (!string.IsNullOrWhiteSpace(coupon))
            {
                extra = "Your saving account is active. Coupon " + coupon + " has been generated.";
            }
            else if (row.SavingResult == "f")
            {
                extra = "Payment is successful, but another saving request is already pending. Admin will review it.";
            }
            else
            {
                extra = "Payment is successful. Your coupon and saving activation are being completed.";
            }

            SetState("paid", "Payment successful", extra, "Paid", amountText, paymentId);
            return;
        }

        if (status == "failed")
        {
            if (string.Equals(row.PlanType, "Installment", StringComparison.OrdinalIgnoreCase))
            {
                SetInstallmentReturnLinks(row.InstallmentId);
            }
            SetState("failed", "Payment failed", "Cashfree could not complete this payment. You can try again from the purchase page.", "Failed", amountText, paymentId);
            return;
        }

        if (status == "dropped")
        {
            if (string.Equals(row.PlanType, "Installment", StringComparison.OrdinalIgnoreCase))
            {
                SetInstallmentReturnLinks(row.InstallmentId);
            }
            SetState("failed", "Payment cancelled", "The checkout was closed before payment completed. You can try again.", "Dropped", amountText, paymentId);
            return;
        }

        SetState("pending", "Payment is processing", "If you already paid, wait a few seconds and refresh. The webhook will confirm the payment automatically.", "Pending", amountText, paymentId);
    }

    void SetInstallmentReturnLinks(int installmentId)
    {
        string coupon = CashfreeHelper.GetInstallmentCoupon(installmentId);
        string installmentUrl = string.IsNullOrWhiteSpace(coupon)
            ? "SavingProductInstallmentList.aspx"
            : ("SavingProductInstallmentDetail.aspx?oid=" + HttpUtility.UrlEncode(coupon));
        lnkPrimaryAction.HRef = installmentUrl;
        litPrimaryAction.Text = "Installment Details";
        lnkTryAgain.HRef = installmentUrl;
    }

    void SetState(string tone, string title, string message, string status, string amount, string paymentId = "-")
    {
        divReturnHero.Attributes["class"] = "cf-return-hero is-" + tone;
        iReturnIcon.Attributes["class"] = tone == "paid" ? "fa fa-check" : (tone == "failed" ? "fa fa-times" : "fa fa-clock");
        litReturnTitle.Text = title;
        litReturnMessage.Text = message;
        litStatus.Text = status;
        litAmount.Text = amount;
        litPaymentId.Text = paymentId;
    }
}
