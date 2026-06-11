using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessLogicTier;
using System.Data;
using Newtonsoft.Json;
using ARA_StringHunt;

/// <summary>
/// Summary description for SecureService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SecureService : System.Web.Services.WebService {

    public SecureService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    protected void PrintJson(String jsonstr)
    {
        try
        {
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Write(jsonstr);
            HttpContext.Current.Response.End();
        }
        catch { }
    }

    [WebMethod]
    public void Login(String _UserId, String _UPassword, String _DeviceId)
    {
        PrintJson(new clsSecureService().Login(_UserId, _UPassword, _DeviceId));
    }
    [WebMethod]
    public void GetUserDetail(String _UserId, String _UPassword)
    {
        PrintJson(new clsSecureService().GetUserDetail(_UserId, _UPassword));
    }
    [WebMethod]
    public void GetUserBalance(String _UserId, String _UPassword)
    {
        PrintJson(new clsSecureService().GetUserBalance(_UserId, _UPassword));
    }
    [WebMethod]
    public void GetNews(String _UserId, String _UPassword)
    {
        PrintJson(new clsSecureService().GetNews(_UserId, _UPassword));
    }
    [WebMethod]
    public void GetDownlineCount(String _UserId, String _UPassword)
    {
        PrintJson(new clsSecureService().GetUserDownlineCount(_UserId, _UPassword));
    }
    [WebMethod]
    public void GetDownlineReport(String _UserId, String _UPassword)
    {
        PrintJson(new clsSecureService().getDownlineReport(_UserId, _UPassword));
    }
    [WebMethod]
    public void GetBankList(String _UserId)
    {
        PrintJson(new clsSecureService().GeBankList(_UserId));
    }
    [WebMethod]
    public void GetCountryList(String _UserId)
    {
        PrintJson(new clsSecureService().GeCountryList(_UserId));
    }
    [WebMethod]
    public void GetStateList(String _UserId, String _CountryId)
    {
        PrintJson(new clsSecureService().GeStateList(_UserId, _CountryId));
    }
    [WebMethod]
    public void GetCityList(String _UserId, String _StateId)
    {
        PrintJson(new clsSecureService().GeCityList(_UserId, _StateId));
    }
    [WebMethod]
    public void UpdateUserDetail(String UserId, String UPassword, String username, String mobile, String email, String gender, String address, String cityid, String stateid, String countryid, String areaname, String pincode, String dateofbirth, String mentionby, String nomineename, String nomineerelation, String accountholdername, String accountno, String ifsccode, String panno, String bankname, String branchname)
    {
        PrintJson(new clsSecureService().UpdateUserDetail(UserId, UPassword, username, mobile, email, gender, address, cityid, stateid, countryid, areaname, pincode, dateofbirth, mentionby, nomineename, nomineerelation, accountholdername, accountno, ifsccode, panno, bankname, branchname));
    }
    [WebMethod]
    public void UserAdd(String userid, String password, String username, String mobile, String email, String gender, String address, String cityid, String areaname, String pincode, String dateofbirth, String Newpassword, String epinno)
    {
        PrintJson(new clsSecureService().UserAdd(userid, password, username, mobile, email, gender, address, cityid, areaname, pincode, dateofbirth, Newpassword, epinno));
    }
    [WebMethod]
    public void GenerateEPin(String UserId, String UPassword, String NoOfEpin, String amount)
    {
        PrintJson(new clsSecureService().GenerateEPin(UserId, UPassword, NoOfEpin, amount));
    }
    [WebMethod]
    public void EPinTransfer(String UserId, String UPassword, String NoOfEpin, String TransferUserId)
    {
        PrintJson(new clsSecureService().EPinTrasnfer(UserId, UPassword, NoOfEpin, TransferUserId));
    }
    [WebMethod]
    public void getEPinReport(String _UserId, String UPassword, String EPinStatus, String GenerateUserId, String UsedUserId, String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().getEPinReport(_UserId, UPassword, EPinStatus, GenerateUserId, UsedUserId, FromDate, ToDate));
    }
    [WebMethod]
    public void getEPinForReg(String _UserId, String UPassword)
    {
        PrintJson(new clsSecureService().getEPinForReg(_UserId, UPassword));
    }
    [WebMethod]
    public void getDirectIncomeReport(String _UserId, String UPassword, String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().getLevelIncomeReport(_UserId, UPassword, FromDate, ToDate));
    }
     [WebMethod]
    public void getpurchaseIncomeReport(String _UserId, String UPassword, String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().getpurchaseIncomeReport(_UserId, UPassword, FromDate, ToDate));
    }
    [WebMethod]
    public void getTransactionReport(String _UserId, String UPassword, String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().getTransactionReport(_UserId, UPassword, FromDate, ToDate));
    }
    [WebMethod]
    public void GetCompanyBankAccountList(String _UserId, String _UPassword)
    {
        PrintJson(new clsSecureService().GetCompanyBankAccount(_UserId, _UPassword));
    }
    [WebMethod]
    public void GetCompanyBankAccountDetail(String _UserId, String _UPassword, String id)
    {
        PrintJson(new clsSecureService().GetCompanyBankAccountDetail(_UserId, _UPassword, id));
    }
    [WebMethod]
    public void FundRequestAdd(String UserId, String UPassword, String AccountId, String paymentMode, String amount, String Transactionid, String ChequeNo, String MobileNoInBank, String Remark)
    {
        PrintJson(new clsSecureService().FundRequestAdd(UserId, UPassword, AccountId, paymentMode, amount, Transactionid, Remark));
    }
    [WebMethod]
    public void FundRequestReport(String _UserId, String UPassword,String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().FundRequestReport(_UserId, UPassword, FromDate, ToDate));
    }
    [WebMethod]
    public void WithdrawlRequestAdd(String UserId, String UPassword,String amount,string balanceamount)
    {
        PrintJson(new clsSecureService().withdrawlRequestAdd(UserId, UPassword, amount, balanceamount));
    }
    [WebMethod]
    public void WithdrawlRequestReport(String _UserId, String UPassword, String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().withdrwlRequestReport(_UserId, UPassword, FromDate, ToDate));
    }
    [WebMethod]
    public void getProductForPurchase(String _UserId, String UPassword, int Pageindex, int Pagesize)
    {
        PrintJson(new clsSecureService().getProductReportForPurchse(_UserId, UPassword, Pageindex, Pagesize));
    }
    [WebMethod]
    public void SubmitProductForPurchase(String _UserId, String UPassword, Decimal TotalAmount, string Productarray)
    {
        PrintJson(new clsSecureService().SubmitProductReportForPurchse(_UserId, UPassword, TotalAmount, Productarray));
    }
    [WebMethod]
    public void getProductPurchaseReport(String _UserId, String UPassword, String FromDate, String ToDate)
    {
        PrintJson(new clsSecureService().getPurchaseProductReport(_UserId, UPassword, FromDate, ToDate));
    }
   
}
