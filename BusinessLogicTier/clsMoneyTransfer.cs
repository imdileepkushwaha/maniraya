using DataTier;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace BusinessLogicTier
{
    public class clsMoneyTransfer
    {
        private Data ObjData = new Data();

        private string apiBaseUrl = ConfigurationManager.AppSettings["apiBaseUrl"];

        private string UMobileNo = ConfigurationManager.AppSettings["UMobileNo"];

        private string Password = ConfigurationManager.AppSettings["Password"];

        public string UserId
        {
            get;
            set;
        }

        public string AmountTransferTo
        {
            get;
            set;
        }

        public System.DateTime FromDate
        {
            get;
            set;
        }

        public System.DateTime ToDate
        {
            get;
            set;
        }
        public class GetSenderinfo
        {
            public string senderName { get; set; }
            public string senderMobile { get; set; }
            public double availbleLimit { get; set; }
            public double totalLimit { get; set; }
            public int kycStatus { get; set; }
            public string referenceID { get; set; }
            public bool isOTPRequired { get; set; }
            public bool isSenderNotExists { get; set; }
            public bool isEKYCAvailable { get; set; }
            public bool isActive { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
            public string note { get; set; }
        }
        public class Beneficiary
        {
            public string mobileNo { get; set; }
            public string beneName { get; set; }
            public string ifsc { get; set; }
            public string accountNo { get; set; }
            public string bankName { get; set; }
            public int bankID { get; set; }
            public string beneID { get; set; }
            public bool isVerified { get; set; }
            public int transMode { get; set; }
            public bool impsStatus { get; set; }
            public bool neftStatus { get; set; }
        }

        public class BeneficiaryList
        {
            public List<Beneficiary> beneficiaries { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
        }
        public class CreateSender
        {
            public string referenceID { get; set; }
            public bool isOTPRequired { get; set; }
            public bool isOTPResendAvailble { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
            public bool isOTpRequired { get; set; }
            public string note { get; set; }
        }
        public class VerifySendar
        {
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
        }
        public class ResendSenderOTP
        {
            public string referenceID { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
        }
        public class CreateBeneficiary
        {
            public string beneID { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
            public bool isOTPRequired { get; set; }
        }
        public class DeleteBeneficiary
        {
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
            public bool isOTPRequired { get; set; }
        }
        public class Verifyaccount
        {
            public int status { get; set; }
            public string beneName { get; set; }
            public string rpid { get; set; }
            public string liveID { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
        }
        public class AccountTransfer
        {
            public int status { get; set; }
            public string beneName { get; set; }
            public string rpid { get; set; }
            public string liveID { get; set; }
            public int statuscode { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
        }
        public class BankList
        {
            public List<object> Banks { get; set; }
            public List<object> States { get; set; }
            public List<object> Cities { get; set; }
            public List<object> KYCDocs { get; set; }
        }

        public class Bankdata
        {
            public int statuscode { get; set; }
            public string msg { get; set; }
            public int errorcode { get; set; }
            public BankList [] Data { get; set; }
        }
        public string Get_Outlet(string userid)
        {
            string H = "123";
            try
            {
                DataTable dtTransId = GenerateTransId();
                H = dtTransId.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {

            }
            return H;
        }
        public string CallGetDepositorApi(string SenderMobileNo = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "GetSenderInfo";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string CallGetDepositorApiNew(string SenderMobileNo = "", string UserId = "", string OutletId = "")
        {
            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "GetSender";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));

           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, "");            
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;
           
        }

        public string CallGetBeniListApi(string SenderMobileNo = "", string UserId = "")
        {

           

            string apiUrl = this.apiBaseUrl + "GetBeniList";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }
        public string CallGetBeniListApiNew(string SenderMobileNo = "", string UserId = "", String SessionKey = "", string OutletId = "")
        {


            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""ReferenceID"": """ + SessionKey + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "GetBeneficiary";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, "");
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       
        }

        public string CallDeleteBeneficiaryApi(string SenderMobileNo = "", string RecipientId = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "DeleteBeneficiary";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("RecipientId", RecipientId);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string CallDeleteBeneficiaryApiNew(string SenderMobileNo = "", string BeneID = "",string OTP="", string UserId = "", String SessionKey = "", string OutletId = "")
        {
            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""BeneID"": """ + BeneID + @""",""OTP"": """ + OTP + @""",""ReferenceID"": """ + SessionKey + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "DeleteBeneficiary";
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, "");
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       
            //string apiUrl = this.apiBaseUrl + "DeleteBeneficiary";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("RecipientId", RecipientId);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public DataSet GetCommissionModule1New()
        {
            DataSet dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "GetCommissionModule1New";
                SqlParameter[] parameter = new SqlParameter[0];
                dt = this.ObjData.RunDataSetProcedure(s2, parameter);
            }
            catch (System.Exception ex_2E)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable dd(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }

                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }
        public string CallBank()
        {

            var client = new RestClient("https://roundpay.net/API/Resource/Get");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"userID\":13547,\r\n  \"token\": \"fb7e6b52ef62ad1cca4779848b6f68b9\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string _output = response.Content;
            return _output;

        }

        public string CallVerifyBeneficiaryApi(string SenderMobileNo = "", string BeneID = "", string BeneMobile = "", string BeneAccountNumber = "", string OTP="", string BankAccount = "", string BackCode = "", string UserId = "", String SessionKey = "", string OutletId = "")
        {
           
            string apiUrl = apiBaseUrl + "VerifyBeneficiary";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", UMobileNo);
            postParams.Add("password", Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("BankAccount", BankAccount);
            postParams.Add("BackCode", BackCode);
            string Values = ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
         
        }

        public string CallVerifyBeneficiaryApiNew(string SenderMobileNo = "", string BeneMobile = "", string BeneAccountNumber = "", string BankName = "", string IFSC = "", string BankID = "", string TransMode = "", string ReferenceID="", string UserId = "", String SessionKey = "", string OutletId = "")
        {
            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""BeneMobile"": """ + BeneMobile + @""",""BeneAccountNumber"": """ + BeneAccountNumber + @""",""BankName"": """ + BankName + @""",""IFSC"": """ + IFSC + @""",""BankID"": """ + BankID + @""",""TransMode"": """ + TransMode + @""",""ReferenceID"": """ + ReferenceID + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "VerifyAccount";
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, ReferenceID);
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       
            //string apiUrl = apiBaseUrl + "VerifyBeneficiary";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("UMobileNo", UMobileNo);
            //postParams.Add("password", Password);
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("BankAccount", BankAccount);
            //postParams.Add("BackCode", BackCode);
            //string Values = ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));

        }

        public DataTable Insert_REQUEST_RESPONSE(string username, string password, string RequestUrl, string Request, string Response, string UserId, string TransId)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_InsertRequestResponse";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@username", username),
					new SqlParameter("@password", password),
					new SqlParameter("@TransId", TransId),
					new SqlParameter("@RequestUrl", RequestUrl),
					new SqlParameter("@Request", Request),
					new SqlParameter("@Response", Response),
					new SqlParameter("@UserId", UserId)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_9F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public void Insert_WALLET(string UserId, string MobileNo, string Name)
        {
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_InsertWalletMaster";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@MobileNo", MobileNo),
					new SqlParameter("@Name", Name),
					new SqlParameter("@UserId", UserId)
				};
                DataTable dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_5F)
            {
            }
            this.ObjData.EndConnection();
        }

        public DataTable insertDMRCommission1(DataTable GridData)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "insertDMRCommission1New";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@GridData", GridData)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_41)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public DataTable updateDMRCommissionModule1(DataTable GridData)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "updateDMRCommissionModule1New";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@GridData", GridData)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_41)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public DataTable GetCharges(int UserId, decimal Amount)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_GetSurCharge";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@UserId", UserId),
					new SqlParameter("@Amount", Amount)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_5A)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public DataTable GetCharges(string UserId, decimal Amount)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_GetSurCharge";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@UserId", UserId),
                    new SqlParameter("@Amount", Amount)
                };
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_55)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public DataTable ValidateMRTransfer(string UserId, int SenderId, string SenderMobileNo, int BeneficiaryId, string AccountNo, decimal Amount, string TransferType, string ReferenceId, string RequestUrl, string mentionby,string INRAmount)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                if (ReferenceId.Trim() != "")
                {
                    string s2 = "spValidateMRTransfer";
                    SqlParameter[] parameter = new SqlParameter[]
					{
						new SqlParameter("@UserId", UserId),
						new SqlParameter("@SenderId", SenderId),
						new SqlParameter("@SenderMobileNo", SenderMobileNo),
						new SqlParameter("@BeneficiaryId", BeneficiaryId),
						new SqlParameter("@AccountNo", AccountNo),
						new SqlParameter("@Amount", Amount),
						new SqlParameter("@TransferType", TransferType),
						new SqlParameter("@ReferenceId", ReferenceId),
						new SqlParameter("@RequestUrl", RequestUrl),
						new SqlParameter("@MentionBy", mentionby),
                        new SqlParameter("@INRAmount", INRAmount)
					};
                    dt = this.ObjData.RunDataTableProcedure(s2, parameter);
                }
                else
                {
                    dt = null;
                }
            }
            catch (System.Exception ex_105)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public void UpdateMRTransferResponse(string UserId, string BeneficiaryId, string AccountNo, string PreviousTransId, string Response, string Type, string Oprater_Id, string VenderID, string mentionby)
        {
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateMRTransferResponse";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@UserId", UserId),
					new SqlParameter("@BeneficiaryId", BeneficiaryId),
					new SqlParameter("@AccountNo", AccountNo),
					new SqlParameter("@PreviousTransId", PreviousTransId),
					new SqlParameter("@Response", Response),
					new SqlParameter("@Type", Type),
					new SqlParameter("@Oprater_Id", Oprater_Id),
					new SqlParameter("@VenderID", VenderID),
					new SqlParameter("@MentionBy", mentionby)
				};
                DataTable dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_C0)
            {
            }
            this.ObjData.EndConnection();
        }
        public DataTable ValidateBeneficiary(string UserId, int BeneficiaryId, string AccountNo, string Sendermobileno)
        {
           
            DataTable dt = new DataTable();
            try
            {
                this.ObjData.StartConnection();
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@UserId", UserId);
                param[1] = new SqlParameter("@BeneficiaryId", BeneficiaryId);
                param[2] = new SqlParameter("@AccountNo", AccountNo);
                param[3] = new SqlParameter("@Sendermobileno", Sendermobileno);
                dt = this.ObjData.RunDataTableProcedure("sp_ValidateBeneficiaryTransaction", param);
                return dt;
            }
            catch
            {
                return dt;

            }
        }

        public DataTable DeValidateBeneficiary(string UserId, int BeneficiaryId, string AccountNo, string PreviousTransId, int type, string Response)
        {
           
            DataTable dt = new DataTable();
            try
            {
                this.ObjData.StartConnection();
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@UserId", UserId);
                param[1] = new SqlParameter("@BeneficiaryId", BeneficiaryId);
                param[2] = new SqlParameter("@AccountNo", AccountNo);
                param[3] = new SqlParameter("@PreviousTransId", PreviousTransId);
                param[4] = new SqlParameter("@type", type);
                param[5] = new SqlParameter("@Response", Response);

                dt = this.ObjData.RunDataTableProcedure("sp_DeValidateBeneficiaryTransaction", param);
                return dt;
               
            }
            catch
            {
                return dt;

            }
        }

        public DataTable GenerateTransId()
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_GenerateTransId";
                SqlParameter[] parameter = new SqlParameter[0];
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_2E)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public string GetMoneyTransferUrl(string SenderMobileNo = "", string BankAccount = "", string Amount = "", string Recipientid = "", string Channel = "", string IMEI = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "SendMoney";
            string Values = clsMoneyTransfer.ConstructQueryString(new NameValueCollection
			{
				{
					"UMobileNo",
					this.UMobileNo
				},
				{
					"password",
					this.Password
				},
				{
					"SenderMobileNo",
					SenderMobileNo
				},
				{
					"BankAccount",
					BankAccount
				},
				{
					"Amount",
					Amount
				},
				{
					"Recipientid",
					Recipientid
				},
				{
					"Channel",
					Channel
				},
				{
					"IMEI",
					IMEI
				}
			});
            return apiUrl + '?' + Values;
        }

        public string GetMoneyTransferUrlNew(string SenderMobileNo = "", string BeneID = "", string Amount = "", string BeneName = "", string BeneMobile = "", string BeneAccountNumber = "", string BankName = "", string IFSC = "", string BankID = "", string TransMode = "", string ReferenceID = "", string UserId = "", String SessionKey = "", string OutletId = "")
        {
            string apiUrl = this.apiBaseUrl + "AccountTransfer";
            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""BeneID"": """ + BeneID + @""",""Amount"": """ + Amount + @""",""BeneName"": """ + BeneName + @""",""BeneMobile"": """ + BeneMobile + @""",""BeneAccountNumber"": """ + BeneAccountNumber + @""",""BankName"": """ + BankName + @""",""IFSC"": """ + IFSC + @""",""BankID"": """ + BankID + @""",""TransMode"": """ + TransMode + @""",""ReferenceID"": """ + ReferenceID + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            return apiUrl + '?' + json;
        }

        public string CallMoneyTransferApi(string SenderMobileNo = "", string BankAccount = "", string Amount = "", string Recipientid = "", string Channel = "", string IMEI = "", string UserId = "", string TransId = "")
        {
            string apiUrl = this.apiBaseUrl + "SendMoney";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("BankAccount", BankAccount);
            postParams.Add("Amount", Amount);
            postParams.Add("Recipientid", Recipientid);
            postParams.Add("Channel", Channel);
            postParams.Add("IMEI", IMEI);
        
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, TransId);
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string CallMoneyTransferApiNew(string SenderMobileNo = "", string BeneID = "", string Amount = "", string BeneName = "", string BeneMobile = "", string BeneAccountNumber = "", string BankName = "", string IFSC = "", string BankID = "", string TransMode = "", string ReferenceID = "", string UserId = "", String SessionKey = "", string OutletId = "",String IP="")
        {
            string apiUrl = this.apiBaseUrl + "AccountTransfer";
            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""BeneID"": """ + BeneID + @""",""Amount"": """ + Amount + @""",""BeneName"": """ + BeneName + @""",""BeneMobile"": """ + BeneMobile + @""",""BeneAccountNumber"": """ + BeneAccountNumber + @""",""BankName"": """ + BankName + @""",""IFSC"": """ + IFSC + @""",""BankID"": """ + BankID + @""",""TransMode"": """ + TransMode + @""",""APIRequestID"": """ + ReferenceID + @""",""ReferenceID"": """ + ReferenceID + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("BankAccount", BankAccount);
            //postParams.Add("Amount", Amount);
            //postParams.Add("Recipientid", Recipientid);
            //postParams.Add("Channel", Channel);
            //postParams.Add("IMEI", IMEI);

            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, TransId);
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, ReferenceID);
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       


        }

        public string GetRefundUrl(string SenderMobileNo = "", string TransactionID = "", string IMEI = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "Refund";
            string Values = clsMoneyTransfer.ConstructQueryString(new NameValueCollection
			{
				{
					"UMobileNo",
					this.UMobileNo
				},
				{
					"Password",
					this.Password
				},
				{
					"SenderMobileNo",
					SenderMobileNo
				},
				{
					"TransactionID",
					TransactionID
				},
				{
					"IMEI",
					IMEI
				}
			});
            return apiUrl + '?' + Values;
        }

        public string CallRefundApi(string SenderMobileNo = "", string TransactionID = "", string IMEI = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "Refund";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("TransactionID", TransactionID);
            postParams.Add("IMEI", IMEI);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string VerifyRefund(string SenderMobileNo = "", string OTP = "", string TransactionID = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "VerifyRefund";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("TransactionID", TransactionID);
            postParams.Add("OTP", OTP);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string ResendOtp(string SenderMobileNo = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "ResendOtp";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string ResendOtpNew(string SenderMobileNo = "", string UserId = "", String SessionKey = "", string OutletId = "")
        {
            string json = @"{""SenderMobile"": """ + SenderMobileNo + @""",""ReferenceID"": """ + SessionKey + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "SenderResendOTP";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("SenderName", SenderName);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, "");
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       
        }

        public string VerifySender(string SenderMobileNo = "", string OTP = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "VerifySender";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("OTP", OTP);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }
        public string VerifySenderNew(string SenderMobileNo = "", string OTP = "", string UserId = "", String SessionKey = "", string OutletId="")
        {

            string json = @"{""OTP"": """ + OTP + @""",""SenderMobile"": """ + SenderMobileNo + @""",""ReferenceID"": """ + SessionKey + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "VerifySender";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("SenderName", SenderName);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, "");
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       

           
        }

        public void Update_REQUEST_RESPONSE(int Id, string Response)
        {
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateRequestResponse";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@Id", Id),
					new SqlParameter("@Response", Response)
				};
                DataTable dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_55)
            {
            }
            this.ObjData.EndConnection();
        }

        public DataTable GetBankList(int BankId, string BankName)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_GetBankList";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@BankId", BankId),
					new SqlParameter("@BankName", BankName)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_55)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public string CallAddBeneficiariesApi(string SenderMobileNo = "", string name = "", string RMobileNo = "", string BankAccount = "", string BankCode_IFSC = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "AddBeneficiary";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("name", name);
            postParams.Add("RMobileNo", RMobileNo);
            postParams.Add("BankAccount", BankAccount);
            postParams.Add("BankCode_IFSC", BankCode_IFSC);
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("password", this.Password);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string CallAddBeneficiariesApiNew(string SenderMobileNo = "", string BeneName = "", string BeneMobile = "", string BeneAccountNumber = "", string BankName = "", string IFSC = "", String BankID = "", string TransMode = "", string APIRequestID="", string UserId = "", string SessionKey = "", string OutletId = "")
        {
            string json = @"{""BeneName"": """ + BeneName + @""",""BeneMobile"": """ + BeneMobile + @""",""BeneAccountNumber"": """ + BeneAccountNumber + @""",""BankName"": """ + BankName + @""",""IFSC"": """ + IFSC + @""",""BankID"": """ + BankID + @""",""TransMode"": """ + TransMode + @""",""APIRequestID"": """ + APIRequestID + @""",""SenderMobile"": """ + SenderMobileNo + @""",""ReferenceID"": """ + SessionKey + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "AddBeneficiary";

           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, APIRequestID);
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;      
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("name", name);
            //postParams.Add("RMobileNo", RMobileNo);
            //postParams.Add("BankAccount", BankAccount);
            //postParams.Add("BankCode_IFSC", BankCode_IFSC);
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("password", this.Password);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public DataTable CheckBeneficiary(string BeneficiaryName, string AccountNo, string IFSC, string SenderMobileNo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] array = new SqlParameter[]
				{
					new SqlParameter("@BeneficiaryName", BeneficiaryName),
					new SqlParameter("@AccountNo", AccountNo),
					new SqlParameter("@IFSC", IFSC),
					new SqlParameter("@SenderMobileNo", SenderMobileNo)
				};
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        public string CallAddDepositorApi(string SenderMobileNo = "", string SenderName = "", string UserId = "")
        {
            string apiUrl = this.apiBaseUrl + "CreateSender";
            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("UMobileNo", this.UMobileNo);
            postParams.Add("Password", this.Password);
            postParams.Add("SenderMobileNo", SenderMobileNo);
            postParams.Add("SenderName", SenderName);
            string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
        }

        public string CallAddDepositorApiNew(string SenderMobileNo = "", string FirstName = "", string LastName = "", string Pincode = "", string Address = "", string DOB = "", string OTP = "", string UserId = "", string SessionKey = "", string OutletId = "")
        {
            string json = @"{""FirstName"": """ + FirstName + @""",""LastName"": """ + LastName + @""",""Pincode"": """ + Pincode + @""",""Address"": """ + Address + @""",""DOB"": """ + DOB + @""",""OTP"": """ + OTP + @""",""SenderMobile"": """ + SenderMobileNo + @""",""ReferenceID"": """ + SessionKey + @""",""SPKey"": ""DMT"",""UserID"": """ + UMobileNo + @""",""Token"": """ + Password + @""",""OutletID"": """ + OutletId + @"""}";
            string apiUrl = this.apiBaseUrl + "CreateSender";
            //NameValueCollection postParams = new NameValueCollection();
            //postParams.Add("UMobileNo", this.UMobileNo);
            //postParams.Add("Password", this.Password);
            //postParams.Add("SenderMobileNo", SenderMobileNo);
            //postParams.Add("SenderName", SenderName);
            //string Values = clsMoneyTransfer.ConstructQueryString(postParams);
            //DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, Values, "", UserId, "");
            //return this.postData(apiUrl, postParams, System.Convert.ToInt32(dt.Rows[0][0].ToString()));
           // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string httpUrl = apiUrl;
            RestClient restClient = new RestClient(httpUrl);
            RestRequest restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Authorization", BasicAuthrozation);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            DataTable dt = this.Insert_REQUEST_RESPONSE(this.UMobileNo, this.Password, apiUrl, json, "", UserId, "");
            IRestResponse result = restClient.Execute(restRequest);
            string _output = result.Content;
            Update_REQUEST_RESPONSE(Convert.ToInt32(dt.Rows[0][0].ToString()), _output);
            return _output;       
        }

        public void Insert_BENEFICIARY_MASTER(string SenderMobileNo, string BeneficiaryName, string AccountNo, string Ifsc, string UserId, int ValidateStatus, int BankId, string BeneficiaryId)
        {
            DataTable dt = new DataTable();
            try
            {
                string s2 = "spInsertBENEFICIARY_MASTER";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@SenderMobileNo", SenderMobileNo),
					new SqlParameter("@BeneficiaryName", BeneficiaryName),
					new SqlParameter("@AccountNo", AccountNo),
					new SqlParameter("@BankId", BankId),
					new SqlParameter("@UserId", UserId),
					new SqlParameter("@ValidateStatus", ValidateStatus),
					new SqlParameter("@Ifsc", Ifsc),
					new SqlParameter("@BeneficiaryId", BeneficiaryId)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch
            {
            }
        }

        public string postData(string destinationUrl, NameValueCollection postParams, int Id)
        {
            string result;
            using (WebClient client = new WebClient())
            {
                byte[] response = client.UploadValues(destinationUrl, postParams);
                string responseString = System.Text.Encoding.Default.GetString(response);
                this.Update_REQUEST_RESPONSE(Id, responseString);
                result = responseString;
            }
            return result;
        }

        public DataSet ReadDataFromJson(string jsonString)
        {
            XmlDocument xd = new XmlDocument();
            jsonString = "{\"rootNode\":{" + jsonString.Trim().TrimStart(new char[]
			{
				'{'
			}).TrimEnd(new char[]
			{
				'}'
			}) + "}}";
            xd = JsonConvert.DeserializeXmlNode(jsonString);
            DataSet result = new DataSet();
            result.ReadXml(new XmlNodeReader(xd));
            return result;
        }

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            XmlTextReader reader = null;
            DataSet result;
            try
            {
                DataSet xmlDS = new DataSet();
                System.IO.StringReader stream = new System.IO.StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                result = xmlDS;
            }
            catch
            {
                result = null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return result;
        }

        public static string ConstructQueryString(NameValueCollection parameters)
        {
            System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>();
            foreach (string name in parameters)
            {
                items.Add(name + "=" + HttpUtility.UrlEncode(parameters[name]));
            }
            return string.Join("&amp;", items.ToArray());
        }

        public DataTable getMoneyTransferReport(clsMoneyTransfer objmoneytransfer)
        {
            string str_query = "SELECT * FROM tbl_MRTransaction where  1=1 ";
            if (objmoneytransfer.FromDate != System.DateTime.MinValue && objmoneytransfer.ToDate != System.DateTime.MinValue)
            {
                object obj = str_query;
                str_query = string.Concat(new object[]
				{
					obj,
					"  and convert(DATE, creatd_date)   >= '",
					objmoneytransfer.FromDate,
					"'   and convert(DATE, creatd_date)    <= '",
					objmoneytransfer.ToDate,
					"' "
				});
            }
            if (objmoneytransfer.UserId != "")
            {
                str_query = str_query + "  and User_Id = '" + objmoneytransfer.UserId + "' ";
            }
            if (objmoneytransfer.AmountTransferTo != "")
            {
                str_query = str_query + "  and AmountTransfer_To = '" + objmoneytransfer.AmountTransferTo + "' ";
            }
            str_query += " order by creatd_date  desc";
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                dt = this.ObjData.RunDataTable(str_query);
            }
            catch (System.Exception ex_119)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

    }
}
