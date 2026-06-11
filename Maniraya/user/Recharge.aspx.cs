using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Xml.Serialization;
using System.IO;
//using WebReference;
//using InternationalWebReference;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;
using System.Globalization;
using System.Web.Security;
using Newtonsoft.Json;
using System.Web.Services;
using BusinessLogicTier;

public partial class admin_Recharge : System.Web.UI.Page
{
    public string LoginId = "";
    public string WhiteLabelId = "";
    clsRecharge objrecharge = new clsRecharge();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            LoginId = (string)Session["userid"];
            if (LoginId == "" || LoginId == null)
            {
                Response.Redirect("logout.aspx");
            }
           
                GetIp();
                fillgrid(LoginId);
                UpdateBal(LoginId);
                if ((string)Session["status1"] != null)
                {
                    if ((string)Session["status1"] != "")
                    {
                        errormsg((string)Session["status1"], (string)Session["Msg"]);
                        Session["status1"] = "";
                    }
                }
                if (!IsPostBack)
                {
                    if (Session["chktrans"] != null)
                    {
                    fillOperator();
                    rdpre.Checked = true;
                    string popupScript = "getOpOption(this,'ctl00_contentpageData_txtoptional1','ctl00_contentpageData_txtoptional2','ctl00_contentpageData_txtoptional3','ctl00_contentpageData_TxtStd');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

                    getLastTenTransactions();
                    Session.Remove("chktrans");
                    }
                    else
                    {
                        Session["pg"] = "recharge";
                        Response.Redirect("TransactionPass.aspx");
                    }
                }
                
           
        }
        catch (Exception ex)
        {
            while (ex != null)
            {
                Console.WriteLine(ex.Message);
                ex = ex.InnerException;
            }
        }
       // rdpre.Checked = true;
    }
    public void UpdateBal(string UserId)
    {
        DataTable dt=new DataTable();
        objuser.UserId = UserId;
        dt = objuser.getUserDetail(objuser);
        if (dt.Rows.Count > 0)
        {
            lblwalletbalance.Text = dt.Rows[0]["balanceamount"].ToString();
            Lblcashwallet.Text = dt.Rows[0]["Utilitybalance"].ToString();
        }
    }
    private string GetSocialImage(DataTable dt)
    {
        string pageid = "1";
        string returnPath = "";
        //string searchExpression = "PageId = '" + pageid + "'";
        //DataRow[] foundRows = dt.Select(searchExpression);
        //if (foundRows.Length > 0)
        //{
        //    dt = foundRows.CopyToDataTable();

        //    returnPath = "<li><a  target='_blank' href='" + dt.Rows[0]["Text5"].ToString() + "'><i class='fa fa-facebook'></i></a></li>";
        //    returnPath += "<li><a  target='_blank' href='" + dt.Rows[0]["Text6"].ToString() + "'><i class='fa fa-twitter'></i></a></li>";
        //    returnPath += "<li><a  target='_blank' href='" + dt.Rows[0]["Text8"].ToString() + "'><i class='fa fa-linkedin'></i></a></li>";
        //    returnPath += "<li><a  target='_blank' href='" + dt.Rows[0]["Text7"].ToString() + "'><i class='fa fa-google-plus'></i></a></li>";
        //}
        return returnPath;
    }
    /*********************  Start Recharge Functions ****************/
    public void WebsiteSetting(string host)
    {
        //WhiteLabelMaster obal = new WhiteLabelMaster();
        //System.Data.DataTable dt = obal.GetSellerWebsiteDetail(host);
        //string searchExpression = "PageId = 1";
        //DataRow[] foundRows = dt.Select(searchExpression);
        //dt = foundRows.CopyToDataTable();
        //if (dt.Rows.Count > 0)
        //{

        //    lblFooter.InnerText = dt.Rows[0]["FooterTitle"].ToString();

        //    WhiteLabelId = dt.Rows[0]["UserId"].ToString();

        //    LblNo.InnerHtml = dt.Rows[0]["CustomerCare"].ToString();
        //    DivCustome.InnerHtml = dt.Rows[0]["CareEmail"].ToString();

        //}
        //Ul4.InnerHtml = GetSocialImage(dt);
    }
    protected void rdpre_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void DdlDTHOpertor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnDtHRecharge_Click(object sender, EventArgs e)
    {

        Session["status1"] = "";
        Session["Msg"] = "";
        string ss = DdlOpertor.SelectedValue;
        string rechargetype = "";
        string message = objrecharge.checkvalidation(TxtAmount, TxtMobileNo, DdlOpertor);

        if ((string)Session["username"] == "" || (string)Session["username"] == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (message != "success")
        {
            Message.Show("Find Error");
            return;
        }

        rechargetype = "Panel";
        string STD = "DTH";
        string Liveid = "";
        string TransactionID, Error = "";
        decimal UserBalance;
        string Status = objrecharge.Recharge(TxtCardNo.Text.Trim(), DdlDTHOpertor.SelectedValue, TxtDTHAmount.Text, STD, rechargetype, (string)Session["username"], out TransactionID, out UserBalance, out Error, GetIp(), out Liveid);
        errormsg(Status, Error);
        Session["status1"] = Status;
        Session["Msg"] = Error;
        TxtAmount.Text = "";
        TxtMobileNo.Text = "";
        DdlOpertor.SelectedIndex = 0;
        fillgrid(LoginId);
        errormsg((string)Session["status1"], (string)Session["Msg"]);
        // Response.Redirect("Recharge.aspx");
    }
    public string GetIp()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
    public void fillimage()
    {

        //string path = "userimages/" + (string)Session["WhiteLabelId"] + "/" + Path.GetFileNameWithoutExtension(Request.PhysicalPath);
        ////UCWlsilder.path = path;
        ////UCWlsilder.SilderAlig = 1;
        //DivBanner.Style.Add("background", "url(../" + path + "banner.jpg)no-repeat 0px 0px;");

        //DtnumberDetail = obal.fillNumberList();
    }
    public void fillgrid(string UserId)
    {
        GridViewLast10.DataSource = objrecharge.LastRechargeRecode(UserId);
        GridViewLast10.DataBind();
    }
    public void fillOperator()
    {
        DataTable dt = objrecharge.GetAllOpertorSelectedByUsernew("1");
        DataRow[] MobileOpertor = dt.Select("Type = 1");
        if (MobileOpertor.Length > 0)
        {
            DdlOpertor.DataSource = MobileOpertor.CopyToDataTable();
            DdlOpertor.DataValueField = "Id";
            DdlOpertor.DataTextField = "Operator";
            DdlOpertor.DataBind();
        }
        DataRow[] PostpaidOpertor = dt.Select("Type = 6");
        if (PostpaidOpertor.Length > 0)
        {
            DdlOpertorPostPaid.DataSource = PostpaidOpertor.CopyToDataTable();
            DdlOpertorPostPaid.DataValueField = "Id";
            DdlOpertorPostPaid.DataTextField = "Operator";
            DdlOpertorPostPaid.DataBind();
        }
        DataRow[] Landline = dt.Select("Type = 3");
        if (Landline.Length > 0)
        {
            dthLandline.DataSource = Landline.CopyToDataTable();
            dthLandline.DataValueField = "Id";
            dthLandline.DataTextField = "Operator";
            dthLandline.DataBind();


        }
        DataRow[] DTHOpertor = dt.Select("Type = 2");
        if (DTHOpertor.Length > 0)
        {
            for (int i = 0; i < DTHOpertor.Length; i++)
            {
                DdlDTHOpertor.Items.Insert(i, new ListItem(DTHOpertor[i]["Operator"].ToString(), DTHOpertor[i]["Id"].ToString() + "__" + DTHOpertor[i]["DisplayNote"].ToString().Trim() + "__" + DTHOpertor[i]["AccountDisplay"].ToString().Trim()));
            }

        }
        DataRow[] ELECTRICITY = dt.Select("Type = 5");
        if (ELECTRICITY.Length > 0)
        {
            for (int i = 0; i < ELECTRICITY.Length; i++)
            {
                DdlELECTRICITY.Items.Insert(i, new ListItem(ELECTRICITY[i]["Operator"].ToString(), ELECTRICITY[i]["Id"].ToString() + "__" + ELECTRICITY[i]["DisplayNote"].ToString().Trim() + "__" + ELECTRICITY[i]["AccountDisplay"].ToString().Trim()));
            }

        }
        DataRow[] gas = dt.Select("Type = 9");
        if (gas.Length > 0)
        {
            for (int i = 0; i < gas.Length; i++)
            {
                Ddlgus.Items.Insert(i, new ListItem(gas[i]["Operator"].ToString(), gas[i]["Id"].ToString() + "__" + gas[i]["DisplayNote"].ToString().Trim() + "__" + gas[i]["AccountDisplay"].ToString().Trim()));
            }

        }

    }
    protected void ButRecharge_Click(object sender, EventArgs e)
    {
        string ss = DdlOpertor.SelectedValue;
        string rechargetype = "Panel";
        string STD = "";
        string Liveid = "";
        string opertor = "";
        string amount = "";
        string rechrgeNo = "";
        string Stdcode = "";
        string Option1 = "";
        string Option2 = "";
        string Option3 = "";
        string Option4 = "";
        string DisplayValue1 = "";
        string DisplayValue2 = "";
        string DisplayValue3 = "";
        string DisplayValue4 = "";

        if (txtflag.Text == "1")
        {
            if (rdpre.Checked == true)
            {
                STD = "PrePaid";
                string[] errmsg = DdlOpertor.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
                opertor = errmsg[0].ToString().Trim();
            }
            else
            {
                STD = "PostPaid";
                string[] errmsg = DdlOpertorPostPaid.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
                opertor = errmsg[0].ToString().Trim();
            }
            amount = TxtAmount.Text.Trim();
            rechrgeNo = TxtMobileNo.Text.Trim();

            objrecharge.OperatorCode = opertor;
            objrecharge.RechargeMobile = TxtMobileNo.Text;
            objrecharge.RechargeAmount = Convert.ToDecimal(amount);
            string popupScript = "theFunction('limobile','tab1');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        if (txtflag.Text == "2")
        {
            STD = "DTH";
            if (DdlDTHOpertor.SelectedValue != "")
            {
                string[] errmsg = DdlDTHOpertor.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
                opertor = errmsg[0].ToString().Trim();
            }
            amount = TxtDTHAmount.Text.Trim();
            rechrgeNo = TxtCardNo.Text.Trim();
            objrecharge.OperatorCode = opertor;
            objrecharge.RechargeMobile = TxtCardNo.Text;
            objrecharge.RechargeAmount = Convert.ToDecimal(amount);
            string popupScript = "theFunction('lidth','tab2');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        if (txtflag.Text == "3")
        {
            STD = "Landline";
            if (dthLandline.SelectedValue != "")
            {
                string[] errmsg = dthLandline.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
                opertor = errmsg[0].ToString().Trim();
                Option1 = errmsg[1].ToString().Trim();
                Option2 = errmsg[2].ToString().Trim();
                Option3 = errmsg[3].ToString().Trim();
                Option4 = errmsg[4].ToString().Trim();
                DisplayValue1 = txtoptional1.Text;
                DisplayValue2 = txtoptional2.Text;
                DisplayValue3 = txtoptional3.Text;
                DisplayValue4 = TxtStd.Text;
            }
           
           
            amount = TxtElAmt.Text.Trim();
            rechrgeNo = TxtLandLine.Text.Trim();
            objrecharge.OperatorCode = opertor;
            objrecharge.RechargeMobile = TxtLandLine.Text;
            objrecharge.RechargeAmount = Convert.ToDecimal(amount);
            //objrecharge.STDCode = Stdcode;
            string popupScript = "theFunction('lilandline','tab3');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        if (txtflag.Text == "4")
        {

            string[] ELECTRICITY = DdlELECTRICITY.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
            Option1 = TxtElecttrcityOption1.Text.Trim();
            Option2 = TxtElecttrcityOption2.Text.Trim();
            Option3 = TxtElecttrcityOption3.Text.Trim();
            Option4 = TxtElecttrcityOption4.Text.Trim();

            objrecharge.OperatorCode = ELECTRICITY[0];
            amount = txtElecttrcityAmt.Text.Trim();
            rechrgeNo = txtCustomerNo.Text.Trim();
            objrecharge.RechargeMobile = rechrgeNo;
            objrecharge.RechargeAmount = Convert.ToDecimal(amount);

        }
        if (txtflag.Text == "5")
        {
            string[] gus = Ddlgus.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
            Option1 = TxtGusOption1.Text.Trim();
            Option2 = TxtGusOption2.Text.Trim();
            Option3 = TxtGusOption3.Text.Trim();
            Option4 = TxtGusOption4.Text.Trim();

            objrecharge.OperatorCode = gus[0];
            amount = txtGusAmt.Text.Trim();
            rechrgeNo = txtGusNo.Text.Trim();
            objrecharge.RechargeMobile = rechrgeNo;
            objrecharge.RechargeAmount = Convert.ToDecimal(amount);
                     

        }
        objrecharge.Option1 = Option1;
        objrecharge.Option2 = Option2;
        objrecharge.Option3 = Option3;
        objrecharge.Option4 = Option4;
        objrecharge.DispalyValue1 = DisplayValue1;
        objrecharge.DispalyValue2 = DisplayValue2;
        objrecharge.DispalyValue3 = DisplayValue3;
        objrecharge.DispalyValue4 = DisplayValue4;
        objrecharge.RechargeType = STD;
        objrecharge.UserMobile = Session["userid"].ToString();
        objrecharge.IPAddress = GetIp();
        objrecharge.EntryBy = Session["userid"].ToString();
        objrecharge.RechargeFrom = "PANEL";
        DataTable dtres = objrecharge.RechargeNew(objrecharge);
        if (dtres.Rows[0][0].ToString() == "1")
        {
            Message.Show("Request Sent...!!!");
            TxtMobileNo.Text = "";
            TxtAmount.Text = "";
            UpdateBal(Session["userid"].ToString());
            
        }
        else
            if (dtres.Rows[0][0].ToString() == "-1")
            {
                Message.Show(dtres.Rows[0][1].ToString());
            }
            else
            {
                Message.Show("Unknown Error Occurred....!!!");
            }
        Message.Show(dtres.Rows[0][0].ToString());
    }
    //protected void ButRecharge_Click(object sender, EventArgs e)
    //{

    //    Session["status"] = "";
    //    Session["Msg"] = "";
    //    string ss = DdlOpertor.SelectedValue;
    //    string rechargetype = "Panel";
    //    string STD = "";
    //    string Liveid = "";
    //    string opertor = "";
    //    string amount = "";
    //    string rechrgeNo = "";
    //    try
    //    {
    //        if (txtflag.Text == "1")
    //        {
    //            if (rdpre.Checked == true)
    //            {
    //                STD = "PrePaid";
    //                opertor = DdlOpertor.SelectedValue;
    //            }
    //            else
    //            {
    //                STD = "PostPaid";
    //                opertor = DdlOpertorPostPaid.SelectedValue;
    //            }
    //            amount = TxtAmount.Text.Trim();
    //            rechrgeNo = TxtMobileNo.Text.Trim();
    //        }
    //        if (txtflag.Text == "2")
    //        {
    //            STD = "DTH";
    //            if (DdlDTHOpertor.SelectedValue != "")
    //            {
    //                string[] errmsg = DdlDTHOpertor.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
    //                opertor = errmsg[0].ToString().Trim();
    //            }
    //            amount = TxtDTHAmount.Text.Trim();
    //            rechrgeNo = TxtCardNo.Text.Trim();
    //        }
    //        if (txtflag.Text == "1" || txtflag.Text == "2")
    //        {
    //            string message = objrecharge.checkvalidation(amount, rechrgeNo, opertor);

    //            if ((string)Session["username"] == "" || (string)Session["username"] == null)
    //            {
    //                Response.Redirect("logout.aspx");
    //            }
    //            if (message != "success")
    //            {
    //                Message.Show(message);
    //                return;
    //            }

    //            string TransactionID, Error = "";
    //            decimal UserBalance;
    //            string Status = objrecharge.Recharge(rechrgeNo, opertor, amount, STD, rechargetype, (string)Session["ID"], out TransactionID, out UserBalance, out Error, GetIp(), out Liveid);
    //            errormsg(Status, Error);
    //            Session["status"] = Status;
    //            Session["Msg"] = Error;
    //            if (txtflag.Text == "3")
    //            {
    //                STD = "DTH";
    //                opertor = dthLandline.SelectedValue;
    //                amount = TxtElAmt.Text.Trim();
    //                rechrgeNo = TxtLandLine.Text.Trim();
    //                STD = "LandLine";
    //            }
    //        }


    //        else if (txtflag.Text == "3")
    //        {
    //            Session["status"] = "";
    //            Session["Msg"] = "";
    //            if (string.IsNullOrEmpty(TxtStd.Text) == false)
    //            {
    //                if (dthLandline.SelectedValue != "")
    //                {
    //                    string[] errmsg = dthLandline.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
    //                    opertor = errmsg[0].ToString().Trim();
    //                }
    //                if (IsNumeric(opertor) != true)
    //                {
    //                    Session["status"] = "0";
    //                    Session["Msg"] = "Select Operator Code ...." + opertor;
    //                    Response.Redirect("Wl_Recharge.aspx");
    //                }
    //                rechrgeNo = TxtLandLine.Text;
    //                amount = TxtElAmt.Text;
    //            }
    //            string message = objrecharge.checkvalidation(amount, rechrgeNo, opertor);
    //            if ((string)Session["username"] == "" || (string)Session["username"] == null)
    //            {
    //                Response.Redirect("logout.aspx");
    //            }
    //            if (message != "success")
    //            {

    //                Message.Show( message);
    //                return;
    //            }
    //            rechargetype = "Panel";
    //            string TransactionID, Error = "";
    //            decimal UserBalance;
    //            string Status = objrecharge.Recharge1(rechrgeNo, opertor, amount, STD, rechargetype, (string)Session["username"], out TransactionID, out UserBalance, out Error, GetIp(), out Liveid, TxtStd.Text, txtLandLine1.Text);
    //            errormsg(Status, Error);
    //            Session["status"] = Status;
    //            Session["Msg"] = Error;
    //        }
    //        else if (txtflag.Text == "4")
    //        {

    //            Session["status"] = "";
    //            Session["Msg"] = "";
    //            //if (string.IsNullOrEmpty(TxtStd.Text) == true)
    //            //{
    //            if (DdlELECTRICITY.SelectedValue != "")
    //            {
    //                string[] errmsg = DdlELECTRICITY.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
    //                opertor = errmsg[0].ToString().Trim();

    //            }
    //            if (IsNumeric(opertor) != true)
    //            {
    //                Session["status"] = "0";
    //                Session["Msg"] = "Select Operator Code ...." + opertor;
    //                Response.Redirect("Recharge.aspx");
    //            }
    //            rechrgeNo = txtCustomerNo.Text.Trim();
    //            amount = txtElecttrcityAmt.Text.Trim();
    //            //}


    //            string message = objrecharge.checkvalidation(amount, rechrgeNo, opertor);

    //            if ((string)Session["username"] == "" || (string)Session["username"] == null)
    //            {
    //                Response.Redirect("logout.aspx");
    //            }
    //            if (message != "success")
    //            {

    //             Message.Show( message);
    //                return;
    //            }
    //            rechargetype = "Panel";
    //            STD = TxtElecttrcityOption1.Text + "," + TxtElecttrcityOption2.Text + "," + TxtElecttrcityOption3.Text + "," + TxtElecttrcityOption4.Text;
    //            string TransactionID, Error = "";
    //            decimal UserBalance;
    //            string Status = objrecharge.Recharge1(rechrgeNo, opertor, amount, STD, rechargetype, (string)Session["username"], out TransactionID, out UserBalance, out Error, GetIp(), out Liveid, TxtElecttrcityOption1.Text, TxtElecttrcityOption2.Text, TxtElecttrcityOption3.Text, TxtElecttrcityOption4.Text);
    //            errormsg(Status, Error);
    //            Session["status"] = Status;
    //            Session["Msg"] = Error;
    //        }
    //        else if (txtflag.Text == "5")
    //        {
    //            Session["status"] = "";
    //            Session["Msg"] = "";
    //            //if (string.IsNullOrEmpty(TxtStd.Text) == true)
    //            //{
    //            if (Ddlgus.SelectedValue != "")
    //            {
    //                string[] errmsg = Ddlgus.SelectedValue.Split(new string[] { "__" }, StringSplitOptions.None);
    //                opertor = errmsg[0].ToString().Trim();

    //            }
    //            if (IsNumeric(opertor) != true)
    //            {
    //                Session["status"] = "0";
    //                Session["Msg"] = "Select Operator Code ...." + opertor;
    //                Response.Redirect("WL_Recharge.aspx");
    //            }
    //            rechrgeNo = txtGusNo.Text.Trim();
    //            amount = txtGusAmt.Text.Trim();
    //            //}


    //            string message = objrecharge.checkvalidation(amount, rechrgeNo, opertor);

    //            if ((string)Session["username"] == "" || (string)Session["username"] == null)
    //            {
    //                Response.Redirect("logout.aspx");
    //            }
    //            if (message != "success")
    //            {

    //               Message.Show( message);
    //                return;
    //            }
    //            rechargetype = "Panel";
    //            STD = TxtGusOption1.Text + "," + TxtGusOption2.Text + "," + TxtGusOption3.Text + "," + TxtGusOption4.Text;
    //            string TransactionID, Error = "";
    //            decimal UserBalance;
    //            string Status = objrecharge.Recharge1(rechrgeNo, opertor, amount, STD, rechargetype, (string)Session["username"], out TransactionID, out UserBalance, out Error, GetIp(), out Liveid, TxtGusOption1.Text, TxtGusOption2.Text, TxtGusOption3.Text, TxtGusOption4.Text);
    //            errormsg(Status, Error);
    //            Session["status"] = Status;
    //            Session["Msg"] = Error;
    //        }
    //        else
    //        {
    //            Session["status"] = "0";
    //            Session["Msg"] = "Select Recharge Type ";
    //        }
    //        opertor = "";
    //        rechrgeNo = "";
    //        amount = "";
    //    }
    //    catch
    //    {
    //        Session["status"] = "1";
    //        Session["Msg"] = "";

    //        opertor = "";
    //        rechrgeNo = "";
    //        amount = "";

    //    }
    //    finally
    //    {
    //        opertor = "";
    //        rechrgeNo = "";
    //        amount = "";
    //        Response.Redirect("Recharge.aspx");
    //    }
    //    // Response.Redirect("Hotel_Main.aspx"); 
    //}
    public void errormsg(string Status, string Error)
    {
        if (Status == "0")
        {
            Message.Show("Failed");
        }
        if (Status == "1")
        {
            Message.Show("Request Accepted " + Error + " success");
        }
        {
            if (Status == "2")
                Message.Show("Success " + Error + " success");
        }
        if (Status == "3")
        {
            Message.Show("Request Accepted " + Error + " success");
        }
    }
    public static bool IsNumeric(object Expression)
    {
        double retNum;

        bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }
    public string CheckType(string Status)
    {
        if (Status == "0")
            return "Failed";
        else if (Status == "1")
            return "Request Accepted";
        else if (Status == "2")
            return "Success";
        else if (Status == "3")
            return "Request Accepted";
        else
            return "";

    }

    protected void getLastTenTransactions()
    {
        try
        {
            objrecharge.UserMobile = Session["userid"].ToString();

            DataTable dt = new DataTable();

            dt = objrecharge.getRechargeReport(objrecharge);

            dt = dt.AsEnumerable().Take(10).CopyToDataTable();

            grdTransReport.DataSource = dt;
            grdTransReport.DataBind();
        }
        catch
        {

        }
    }

    protected void grdTransReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            if (lblstatus.Text == "Request Sent")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-info";
            }
            else
                if (lblstatus.Text == "Pending")
                {
                    lblstatus.CssClass = "label label-info";
                }
                else
                    if (lblstatus.Text == "Success")
                    {
                        lblstatus.CssClass = "label label-success";
                    }
                    else
                        if (lblstatus.Text == "Dispute")
                        {
                            lblstatus.CssClass = "label badge-info";
                        }
                        else
                            if (lblstatus.Text == "Failed")
                            {
                                lblstatus.CssClass = "label label-danger";
                            }
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        getLastTenTransactions();
    }
   
}