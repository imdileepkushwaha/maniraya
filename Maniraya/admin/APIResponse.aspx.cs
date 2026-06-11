using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARA_StringHunt;
using BusinessLogicTier;

public partial class APIResponse : System.Web.UI.Page
{
    clsRecharge objrecharge = new clsRecharge();
    clsUser objU = new clsUser();
    public void SaveUrl(string test)
    {

        objrecharge.InsertCallBackURL(test);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.Browser.Browser != "Unknown")
        //{
        //    Response.Write("Invalid Request....!");
        //    return;
        //}
        //System.IO.StreamReader str = new System.IO.StreamReader(Page.Request.InputStream);
        //string test = str.ReadToEnd();
        SaveUrl(Request.Url.AbsoluteUri.ToString());
        if (!IsPostBack)
        {
            string res = "";
            string status = (string)Request.QueryString["status"];
            string type = "";
            if (status != null)
            {
                if (status.ToUpper() == "FAILED" || status.ToUpper() == "FAILURE")
                {
                    status = "Failed";
                    type = "RechargeFailed";
                }

                else if (status.ToUpper() == "PENDING")
                {
                    status = "Pending";
                    return;
                }
                else if (status.ToUpper() == "SUCCESS")
                {
                    status = "Success";
                    type = "RechargeSuccess";
                }
                else if (status.ToUpper() == "REQUEST SENT")
                {
                    status = "Request Sent";
                    type = "RechargeSuccess";
                }
                else if (status.ToUpper() == "REFUND")
                {
                    status = "Refund";
                    type = "RechargeFailed";
                }
                else
                {
                    return;
                }
                try
                {
                    string msg = "";
                    string LiveiD = "";
                    string TID = "";
                    if (Request.QueryString["msg"] != null)
                    {
                        msg = (string)Request.QueryString["msg"];
                        if (msg == null)
                        {
                            msg = "";
                        }
                    }
                    else
                    {
                        msg = "";
                    }
                    if (Request.QueryString["opid"] != null)
                    {
                        LiveiD = (string)Request.QueryString["opid"];
                        if (LiveiD == null)
                        {
                            LiveiD = "";
                        }
                    }
                    else
                    {
                        if (Request.QueryString["operator_ref"] != null)
                        {
                            LiveiD = (string)Request.QueryString["operator_ref"];
                            if (LiveiD == null)
                            {
                                LiveiD = "";
                            }
                        }
                    }
                    if (Request.QueryString["agentid"] != null)
                    {
                        TID = (string)Request.QueryString["agentid"];
                        if (TID == null)
                        {
                            TID = "";
                        }
                    }
                    else
                    {
                        if (Request.QueryString["client_id"] != null)
                        {
                            TID = (string)Request.QueryString["client_id"];
                            if (TID == null)
                            {
                                TID = "";
                            }
                        }
                    }
                    objrecharge.UpdateCallBackResponse(TID, LiveiD, status, msg);
                    res = "Final";
                    DataTable Dtrr=objrecharge.CheckReponsefromSMS(TID);
                    if (Dtrr.Rows.Count > 0)
                    {
                        if (Dtrr.Rows[0][0].ToString() == "1")
                        {
                            if (Dtrr.Rows[0]["rechargestatus"].ToString().ToUpper() == "SUCCESS")
                            {
                                if (Dtrr.Rows[0]["Successstatus"].ToString()=="1")
                                {
                                    string url = objU.smstemplate("", Dtrr.Rows[0]["Mobile"].ToString(), "", "", "ARSENPAY", "www.arsenpay.in", Dtrr.Rows[0]["RechargeMobile"].ToString(), Dtrr.Rows[0]["Rechargeamount"].ToString(), Dtrr.Rows[0]["balanceamount"].ToString(), "", Dtrr.Rows[0]["operatorname"].ToString(), Dtrr.Rows[0]["transactionid"].ToString(), Dtrr.Rows[0]["LiveID"].ToString(), "rechargesuccess");
                                    string Result = url.CallURL();
                                    objU.Insert_SendSMS(Dtrr.Rows[0]["Mobile"].ToString(), Result, url);
                                }
                            }
                            if (Dtrr.Rows[0]["rechargestatus"].ToString().ToUpper() == "FAILED")
                            {

                                if (Dtrr.Rows[0]["failedstatus"].ToString()=="1")
                                {
                                    string url = objU.smstemplate("", Dtrr.Rows[0]["Mobile"].ToString(), "", "", "ARSENPAY", "www.arsenpay.in", Dtrr.Rows[0]["RechargeMobile"].ToString(), Dtrr.Rows[0]["Rechargeamount"].ToString(), Dtrr.Rows[0]["balanceamount"].ToString(), "", Dtrr.Rows[0]["operatorname"].ToString(), Dtrr.Rows[0]["transactionid"].ToString(), Dtrr.Rows[0]["LiveID"].ToString(), "rechargefailed");
                                    string Result = url.CallURL();
                                    objU.Insert_SendSMS(Dtrr.Rows[0]["Mobile"].ToString(), Result, url);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res = "err";
                }
                Response.Write(res);
            }
        }
    }
    public string IpAddress()
    {
        string strIpAddress;
        strIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (strIpAddress == null)
        {
            strIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return strIpAddress;
    }

}