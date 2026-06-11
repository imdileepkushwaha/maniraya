using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public partial class user_MoneyTransfer : System.Web.UI.Page
{
    clsMoneyTransfer objmoneytransfer = new clsMoneyTransfer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           Session["userid"] = "SW000001";
           
            if (Session["userid"] != null)
            {
               // string ff1 = objmoneytransfer.CallBank();
             //   DataSet Ds = JsonConvert.DeserializeObject<DataSet>(ff);
                //clsMoneyTransfer.Bankdata Root = JsonConvert.DeserializeObject<clsMoneyTransfer.Bankdata>(ff);
               // DataTable Dt = ConvertJSONToDataTable(JsonConvert.SerializeObject(Root.Data[0]));
                loaddollar();
                loadlogin();
                getoutlet();
                
            }
        }
    }
    public DataTable getbankdata()
    {
           string ff = objmoneytransfer.CallBank();
           // DataSet data = JsonConvert.DeserializeObject<DataSet>(ff);
            ff = ff.Replace(@"{""statuscode"":1,""msg"":""Transaction Successful"",""errorcode"":200,""data"":","");
            ff = ff.Substring(0, ff.IndexOf("]") + 1);
            ff = ff + "}";
            DataTable Dt = objmoneytransfer.dd(ff);
            return Dt;
    }
    void loaddollar()
    {
        clsDownload objD = new clsDownload();
        DataTable dt = new DataTable();
        dt = objD.Dollarcharge();
        ViewState["dollar"] = dt.Rows[0]["INR"].ToString();
    }
    public DataTable getbeneficiarydata(string Response)
    {
       
        // DataSet data = JsonConvert.DeserializeObject<DataSet>(ff);       
        Response = Response.Substring(0, Response.IndexOf("]") + 1);
        Response = Response + "}";
        DataTable Dt = objmoneytransfer.dd(Response);
        return Dt;
    }
    public void getoutlet()
    {

        string ff = objmoneytransfer.Get_Outlet(Session["userid"].ToString());
        Session["Sessionkey"] = ff;
        Session["OutletID"] = "11558";
    }
    void loadlogin()
    {
        if (Session["SenderMobileNo"] == null)
        {
            divLogin.Visible = true;
            divWelcome.Visible = false;
            divCreate.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        else
        {
            divLogin.Visible = false;
            divCreate.Visible = false;
            divWelcome.Visible = true;
            loadbalance();
            string Response = objmoneytransfer.CallGetDepositorApiNew(Session["SenderMobileNo"].ToString(), Session["userid"].ToString(), Session["OutletID"].ToString());
            clsMoneyTransfer.GetSenderinfo Root = JsonConvert.DeserializeObject<clsMoneyTransfer.GetSenderinfo>(Response);
            if (Root.statuscode != null)
            {
                if (Root.statuscode == 1 && Root.isSenderNotExists==false)
                {
                    UpdateBal(Session["userid"].ToString());
                    divLogin.Visible = false;
                    divWelcome.Visible = true;
                    lblsendername.Text = Root.senderName;
                    lblkyc.Text = Root.kycStatus.ToString();
                    lbllimit.Text = Math.Round(Convert.ToDecimal(Root.totalLimit.ToString())/Convert.ToDecimal(ViewState["dollar"].ToString()),0).ToString();
                    lblused.Text = Math.Round(Convert.ToDecimal(Root.availbleLimit.ToString()) / Convert.ToDecimal(ViewState["dollar"].ToString()), 0).ToString();
                    lblcurrency.Text = "$";
                    lblsendername2.Text = Root.senderName;
                    Session["RemainingLimit"] = Root.availbleLimit.ToString();

                    string ResponseBeniList = objmoneytransfer.CallGetBeniListApiNew(Session["SenderMobileNo"].ToString(), Session["userid"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
                    clsMoneyTransfer.BeneficiaryList RootList = JsonConvert.DeserializeObject<clsMoneyTransfer.BeneficiaryList>(ResponseBeniList);
                    if (Root.statuscode != null)
                    {
                        if (Root.statuscode == 1)
                        {
                            try
                            {
                               // DataTable Dt = ConvertJSONToDataTable(RootList.beneficiaries.ToString());// JsonConvert.DeserializeObject<DataSet>(RootList.beneficiaries);
                                DataTable Dt = getbeneficiarydata(ResponseBeniList);
                                if (Dt != null && Dt.Rows.Count > 0)
                                {
                                    GridView1.DataSource = Dt;
                                    GridView1.DataBind();
                                }
                                else
                                {
                                    GridView1.DataSource = null;
                                    GridView1.DataBind();
                                }
                            }
                            catch
                            {

                            }
                          
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                    else
                    {

                    }

                }
                else if (Root.statuscode == 1 && Root.isSenderNotExists == true)
               
                {
                    txtmobilecreate.Text = txtmobilelogin.Text;
                    lblloginerror.Text =Root.message;
                    divLogin.Visible = false;
                    divWelcome.Visible = false;
                    divCreate.Visible = true;
                    Session["SenderMobileNo"] = null;
                }
                else if (Root.statuscode == -1)
                {
                    string popupScript = "alert('" + Root.message + "');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    divLogin.Visible = true;
                    divWelcome.Visible = false;
                    divCreate.Visible = false;
                }
            }
            else
            {

            }
            //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);
            //if (ds.Tables["TABLE"].Rows.Count > 0)
            //{
            //    if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
            //    {
            //        UpdateBal(Session["userid"].ToString());
            //        divLogin.Visible = false;
            //        divWelcome.Visible = true;
            //        lblsendername.Text = ds.Tables["TABLE"].Rows[0]["NAME"].ToString();
            //        lblkyc.Text = ds.Tables["TABLE"].Rows[0]["KYC"].ToString();
            //        lbllimit.Text =  ds.Tables["TABLE"].Rows[0]["USED"].ToString();
            //        lblused.Text =  ds.Tables["TABLE"].Rows[0]["REMAINING"].ToString();
            //        lblcurrency.Text =   ds.Tables["TABLE"].Rows[0]["CURRENCY"].ToString();
            //        lblsendername2.Text = ds.Tables["TABLE"].Rows[0]["NAME"].ToString();
            //        Session["RemainingLimit"] = ds.Tables["TABLE"].Rows[0]["REMAINING"].ToString();

            //        string ResponseBeniList = objmoneytransfer.CallGetBeniListApi(Session["SenderMobileNo"].ToString());
            //        if (ResponseBeniList.Contains("<?"))
            //        {
            //            ResponseBeniList = ResponseBeniList.Substring(ResponseBeniList.IndexOf("?>") + 2).Trim();
            //        }
            //        DataSet dsBeniLis = objmoneytransfer.ConvertXMLToDataSet(ResponseBeniList);

            //        if (dsBeniLis.Tables["TABLE"].Rows.Count > 0)
            //        {
            //            if (dsBeniLis.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
            //            {
            //                if (dsBeniLis.Tables["BENEFICIARY"].Rows[0]["RESPONSESTATUS"].ToString() == "23")
            //                {
            //                    GridView1.DataSource = dsBeniLis.Tables["BENEFICIARY"];
            //                    GridView1.DataBind();
            //                }
            //                else
            //                {
            //                    GridView1.DataSource = null;
            //                    GridView1.DataBind();
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString() == "Sender not found....Please Create Sender or verify sender !!")
            //        {
            //            txtmobilecreate.Text = txtmobilelogin.Text;
            //            lblloginerror.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
            //            divLogin.Visible = false;
            //            divWelcome.Visible = false;
            //            divCreate.Visible = true;
            //            Session["SenderMobileNo"] = null;
            //        }
            //        else
            //        {
            //            string popupScript = "alert('Error Occurred');";
            //            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            //            divLogin.Visible = true;
            //            divWelcome.Visible = false;
            //            divCreate.Visible = false;
            //        }
            //    }
            //}
            //else
            //{
              
            //}

        }
    }
    void loadbalance()
    {
        clsAccount objaccount = new clsAccount();
        objaccount.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = objaccount.getUserWalletBalance(objaccount);
        if (dt.Rows.Count > 0)
        {
            lbluserbalance.Text = dt.Rows[0][0].ToString();
        }
        else
        {
            lbluserbalance.Text = "0.00";
        }
    }

   

    
    public void UpdateBal(string UserId)
    {
        clsUser objuser = new clsUser();
        DataTable dt = new DataTable();
        objuser.UserId = UserId;
        dt = objuser.getUserDetail(objuser);
        if (dt.Rows.Count > 0)
        {
            lbluserbalance.Text = dt.Rows[0]["balanceamount"].ToString();
           // lbluserbalance.Text = dt.Rows[0]["UtilityBalance"].ToString();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Session["SenderMobileNo"] = txtmobilelogin.Text;
        loadlogin();
    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["SenderMobileNo"] = null;

        loadlogin();
    }
    void BindTransferType()
    {
        try
        {
            ddtransfertype.Items.Clear();
            ListItem lstimps = new ListItem("IMPS", "1");
            ddtransfertype.Items.Add(lstimps);
        //    DataTable dtBank = new DataTable();
        //dtBank = objmoneytransfer.GetBankList(0, Session["Bank"].ToString());

        //    if (dtBank.Rows.Count > 0)
        //    {
        //        ListItem lstimps = new ListItem("IMPS", "2");
        //        ListItem lstneft = new ListItem("NEFT", "1");



        //        if (dtBank.Rows[0]["IMPS"].ToString() == "Yes")
        //        {
        //            ddtransfertype.Items.Add(lstimps);
        //        }
        //        else if (dtBank.Rows[0]["IMPS"].ToString() == "No")
        //        {
        //        }
        //        else
        //        {
        //            //ddlTransferType.Items.Add(lstimps);
        //        }
        //        if (dtBank.Rows[0]["NEFT"].ToString() == "Yes")
        //        {
        //            ddtransfertype.Items.Add(lstneft);
        //        }
        //        else if (dtBank.Rows[0]["NEFT"].ToString() == "No")
        //        {
        //        }
        //        else
        //        {
        //            ddtransfertype.Items.Add(lstneft);
        //        }
        //        if (dtBank.Rows[0]["NEFT"].ToString() == "No" && dtBank.Rows[0]["IMPS"].ToString() == "Yes")
        //        {
        //            divErrorMR.Visible = false;
        //            lblmsg.Text = " (Only IMPS is available)";
        //        }
        //        else if (dtBank.Rows[0]["NEFT"].ToString() == "Yes" && dtBank.Rows[0]["IMPS"].ToString() == "No")
        //        {
        //            divErrorMR.Visible = false;
        //            lblmsg.Text = " (Only NEFT is available)";
        //        }
        //        else if (dtBank.Rows[0]["NEFT"].ToString() == "No" && dtBank.Rows[0]["IMPS"].ToString() == "No")
        //        {
        //            divErrorMR.Visible = false;
        //            lblmsg.Text = " (Only NEFT is available)";
        //        }
        //        else
        //        {
        //            divErrorMR.Visible = false;
        //            lblmsg.Text = " (Both NEFT/IMPS is available)";

        //        }

           // }
        }
        catch (Exception ex)
        { }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        Label lblBankId = (Label)gvr.FindControl("lblBankId");
        LinkButton LnkValidate = (LinkButton)gvr.FindControl("LnkValidate");
        Label lblDMRValidationCharge = (Label)gvr.FindControl("lblDMRValidationCharge");
        //LnkValidate.Attributes.Add("onclick", "javascript:return confirm ('Are you sure you want to Validate this Beneficiary.<b/> Validation Charges are ₹ " + DataBinder.Eval(e.row.DataItem, "YourDbField") + " ?');");
        string BeneficiaryId = gvr.Cells[0].Text.Trim();
        string BeneficiaryName = gvr.Cells[1].Text.Trim();
        string Bank = gvr.Cells[2].Text.Trim();
        string AccountNo = gvr.Cells[3].Text.Trim();
        string IFSC = gvr.Cells[4].Text.Trim();


        TextBox txt_BenificiaryMobileNO = (TextBox)gvr.FindControl("txt_BenificiaryMobileNO");
        ViewState["BeneficiaryId"] = BeneficiaryId;
        ViewState["BeneficiaryName"] = BeneficiaryName;
        ViewState["AccountNo"] = AccountNo;
        ViewState["IFSC"] = IFSC;

        if (e.CommandName == "LnkPay")
        {

            Session["BeneficiaryId"] = BeneficiaryId;
            Session["BeneficiaryName"] = BeneficiaryName;
            Session["AccountNo"] = AccountNo;
            Session["IFSC"] = IFSC;
            Session["Bank"] = Bank;
            BindTransferType();
            txtbeneficiarynameMR.Text = BeneficiaryName;
            txtbeneficiaryaccountMR.Text = AccountNo;
            txtamount.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalMR();", true);
        }
        if (e.CommandName == "LnkDelete")
        {
            string Response = objmoneytransfer.CallDeleteBeneficiaryApiNew(Session["SenderMobileNo"].ToString().Trim(), BeneficiaryId,"", Session["UserID"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
            clsMoneyTransfer.DeleteBeneficiary Root = JsonConvert.DeserializeObject<clsMoneyTransfer.DeleteBeneficiary>(Response);
            if (Root.statuscode != null)
            {
                if (Root.statuscode == 1)
                {
                    string ResponseBeniList = objmoneytransfer.CallGetBeniListApiNew(Session["SenderMobileNo"].ToString(), Session["userid"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
                    clsMoneyTransfer.BeneficiaryList RootList = JsonConvert.DeserializeObject<clsMoneyTransfer.BeneficiaryList>(ResponseBeniList);
                    if (Root.statuscode != null)
                    {
                        if (Root.statuscode == 1)
                        {
                            try
                            {
                                DataTable Dt = ConvertJSONToDataTable(RootList.beneficiaries.ToString());// JsonConvert.DeserializeObject<DataSet>(RootList.beneficiaries);
                                if (Dt != null && Dt.Rows.Count > 0)
                                {
                                    GridView1.DataSource = Dt;
                                    GridView1.DataBind();
                                }
                                else
                                {
                                    GridView1.DataSource = null;
                                    GridView1.DataBind();
                                }
                            }
                            catch
                            {
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                            }
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                    else
                    {

                    }
                }

            }
        //    if (Response.Contains("<?"))
        //    {
        //        Response = Response.Substring(Response.IndexOf("?>") + 2).Trim();
        //    }
        //    DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);


        //    if (ds.Tables["TABLE"].Rows.Count > 0)
        //    {

        //        if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
        //        {
        //            //LblMsg.Text = "Beneficiary Deleted Successfully";
        //            //Error.Visible = true;
        //            //Error.Attributes["class"] = "btn btn-success btn-round span6";

        //            string ResponseBeniList = objmoneytransfer.CallGetBeniListApi(Session["SenderMobileNo"].ToString().Trim());
        //            if (ResponseBeniList.Contains("<?"))
        //            {
        //                ResponseBeniList = ResponseBeniList.Substring(ResponseBeniList.IndexOf("?>") + 2).Trim();
        //            }
        //            DataSet dsBeniLis = objmoneytransfer.ConvertXMLToDataSet(ResponseBeniList);

        //            if (dsBeniLis.Tables["TABLE"].Rows.Count > 0)
        //            {
        //                if (dsBeniLis.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
        //                {
        //                    if (dsBeniLis.Tables["BENEFICIARY"].Rows.Count > 0)
        //                    {
        //                        GridView1.DataSource = dsBeniLis.Tables["BENEFICIARY"];
        //                        GridView1.DataBind();
        //                    }
        //                }
        //                else
        //                {
        //                    GridView1.DataSource = null;
        //                    GridView1.DataBind();
        //                }
        //            }
        //        }
        //    }
        }
    }


    protected void btnAddBeneficiary_Click(object sender, EventArgs e)
    {
        loadbank();
        txtsendermobileno.Text = Session["SenderMobileNo"].ToString();
        txtifscsticky.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalBeneficiary();", true);
    }
    void loadbank()
    {
        ddbank.Items.Clear();
        DataTable dtBank = new DataTable();
       // dtBank = objmoneytransfer.GetBankList(0, "");
        dtBank = getbankdata();
        
        if (dtBank.Rows.Count > 0)
        {
            ddbank.DataSource = dtBank;
            ddbank.DataValueField = "id";
            ddbank.DataTextField = "bankName";
            ddbank.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddbank.Items.Insert(0, li);
        }
        else
        {
            ddbank.Items.Clear();
            ListItem li = new ListItem("-Select-", "0");
            ddbank.Items.Insert(0, li);

        }
    }
    protected void btnSubmitBeneficiary_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        //if (txtifscsticky.Text.Trim() == "")
        //    dt = objmoneytransfer.CheckBeneficiary(txtbeneficiaryname.Text.Trim(), txtbenaccountno.Text.Trim(), txtifsccode.Text.Trim(), Session["SenderMobileNo"].ToString());
        //else
        //    dt = objmoneytransfer.CheckBeneficiary(txtbeneficiaryname.Text.Trim(), txtbenaccountno.Text.Trim(), (txtifscsticky.Text.Trim() + txtifsccode.Text.Trim()), Session["SenderMobileNo"].ToString());
        //if (dt.Rows.Count > 0)
        //{
        //    //LblMsg.Text = "Beneficiary Already Exists";
        //    //Error.Visible = true;
        //    //Error.Attributes["class"] = "btn btn-danger btn-round span6";
        //}
        //else
        //{
        string Response = string.Empty;
        DataTable Dt1= objmoneytransfer.GenerateTransId();
       // if (txtifscsticky.Text.Trim() == "")
        Response = objmoneytransfer.CallAddBeneficiariesApiNew(Session["SenderMobileNo"].ToString(), txtbeneficiaryname.Text.Trim(), txtbenegiciarymobile.Text.Trim(), txtbenaccountno.Text.Trim(),ddbank.SelectedItem.Text,txtifscsticky.Text.Trim()+ txtifsccode.Text.Trim(), ddbank.SelectedValue, "2", Dt1.Rows[0][0].ToString(), Session["userid"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
        clsMoneyTransfer.CreateBeneficiary Root = JsonConvert.DeserializeObject<clsMoneyTransfer.CreateBeneficiary>(Response);
        if (Root.statuscode != null)
        {
            if (Root.statuscode == 1)
            {
                if (txtifscsticky.Text.Trim() == "")
                    objmoneytransfer.Insert_BENEFICIARY_MASTER(Session["SenderMobileNo"].ToString(), txtbeneficiaryname.Text.Trim(), txtbenaccountno.Text.Trim(), txtifsccode.Text.Trim(), Session["UserID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Convert.ToInt32(ddbank.SelectedValue), Root.beneID);
                else
                    objmoneytransfer.Insert_BENEFICIARY_MASTER(Session["SenderMobileNo"].ToString(), txtbeneficiaryname.Text.Trim(), txtbenaccountno.Text.Trim(), (txtifscsticky.Text.Trim() + txtifsccode.Text.Trim()), Session["UserID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Convert.ToInt32(ddbank.SelectedValue), Root.beneID);
                //LblMsg.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
                //Error.Visible = true;
                //Error.Attributes["class"] = "btn btn-success btn-round span6";
                Session["ValidateStatus"] = "0";
                loadlogin();
            }
            else
            {

                string popupScript = "alert('" + Root.message + "');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript2 = "ClosepopupBeneficiary();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            }

        }
      //  else
        ///    Response = objmoneytransfer.CallAddBeneficiariesApi(Session["SenderMobileNo"].ToString(), txtbeneficiaryname.Text.Trim(), txtbenegiciarymobile.Text.Trim(), txtbenaccountno.Text.Trim(), (txtifscsticky.Text.Trim() + txtifsccode.Text.Trim()), Session["userid"].ToString());
        //if (Response.Contains("<?"))
        //{
        //    Response = Response.Substring(Response.IndexOf("?>") + 2).Trim();
        //}
        //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);

        //if (ds.Tables["TABLE"].Rows.Count > 0)
        //{
        //    if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
        //    {
        //        if (txtifscsticky.Text.Trim() == "")
        //            objmoneytransfer.Insert_BENEFICIARY_MASTER(Session["SenderMobileNo"].ToString(), txtbeneficiaryname.Text.Trim(), txtbenaccountno.Text.Trim(), txtifsccode.Text.Trim(), Session["UserID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Convert.ToInt32(ddbank.SelectedValue), ds.Tables["TABLE"].Rows[0]["RECIPIENTID"].ToString());
        //        else
        //            objmoneytransfer.Insert_BENEFICIARY_MASTER(Session["SenderMobileNo"].ToString(), txtbeneficiaryname.Text.Trim(), txtbenaccountno.Text.Trim(), (txtifscsticky.Text.Trim() + txtifsccode.Text.Trim()), Session["UserID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Convert.ToInt32(ddbank.SelectedValue), ds.Tables["TABLE"].Rows[0]["RECIPIENTID"].ToString());
        //        //LblMsg.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
        //        //Error.Visible = true;
        //        //Error.Attributes["class"] = "btn btn-success btn-round span6";
        //        Session["ValidateStatus"] = "0";
        //        loadlogin();
        //    }
        //    else
        //    {
               
        //        string popupScript = "alert('" + ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString() + "');";
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //        string popupScript2 = "ClosepopupBeneficiary();";
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);

        //    }
        //}
        ////}
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupBeneficiary();", true);
    }
    protected void ddbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalBeneficiary();", true);
        Session["ValidateStatus"] = "0";
        txtifscsticky.Text = "";
        DataTable dtBanklist = new DataTable();
        dtBanklist = getbankdata();
       // dtBanklist = objmoneytransfer.GetBankList(Convert.ToInt32(ddbank.SelectedValue), "");

        if (dtBanklist.Rows.Count > 0 && Convert.ToInt32(ddbank.SelectedValue) != 0)
        {
            foreach (DataRow Dr in dtBanklist.Rows)
            {
                if (Dr["id"].ToString().Trim() == ddbank.SelectedValue.ToString().Trim())
                {
                    if (Dr["ifsc"].ToString().Length > 3)
                    {
                        txtifscsticky.Visible = false;
                      //  txtifscsticky.Text = Dr["ifsc"].ToString().Substring(0, 4);
                        txtifsccode.Text = Dr["ifsc"].ToString();
                      //  txtifsccode.Text = txtifsccode.Text.Replace(txtifscsticky.Text, "");
                    }
                    else
                    {
                        txtifscsticky.Visible = false;
                        txtifsccode.Text = Dr["ifsc"].ToString();
                    }
                   
                }
            }
           // var name = dtBanklist.AsEnumerable().Where(row => Convert.ToInt32(row["Id"]) == Convert.ToInt32(ddbank.SelectedValue)).Select(row => row.Field<string>("ifsc"));
           // txtifscsticky.Visible = false;
            //if (name.ToString() != "")
            //{
            //   // txtifscsticky.Text = dtBanklist.Rows[0]["ShortCode"].ToString();
            //    txtifsccode.Text = name.ToString();
            //}
           
           // txtifsccode.Attributes.Add("OnKeyPress", "onlyNumbers(this,event,true,false)");
            //txtbenaccountno.MaxLength = Convert.ToInt32(dtBanklist.Rows[0]["AccNolimit"].ToString());
            //if (dtBanklist.Rows[0]["ShortCode"].ToString() != "")
            //{
            //    txtifscsticky.Visible = true;
            //    txtifscsticky.Text = dtBanklist.Rows[0]["ShortCode"].ToString();
            //    txtifsccode.MaxLength = 7;
            //    txtifsccode.Attributes.Add("OnKeyPress", "onlyNumbers(this,event,true,false)");
            //}
            //else
            //{
            //    txtifscsticky.Visible = false;
            //    txtifsccode.CssClass = "span12";
            //    txtifsccode.MaxLength = 11;
            //}
        }
        else
        {
            txtifscsticky.Visible = false;
            txtifsccode.MaxLength = 11;
        }
    }
    protected void btncloseBen_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupBeneficiary();", true);
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string Response = objmoneytransfer.CallAddDepositorApiNew(txtmobilecreate.Text.Trim(), txtnamecreate.Text.Trim(), TxtLastname.Text.Trim(), "394107", "SURAT", "07 Nov 1986","", Session["UserID"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
        clsMoneyTransfer.CreateSender Root = JsonConvert.DeserializeObject<clsMoneyTransfer.CreateSender>(Response);
        if (Root.statuscode != null)
        {
            if (Root.statuscode == 1)
            {
                lblmsgotp.Text = Root.message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
                divoptsuccess.Visible = true;
                divotperror.Visible = false;
            }
            else
            {
                lblmsgotperror.Text = Root.message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
                divoptsuccess.Visible = false;
                divotperror.Visible = true;
            }

        }
        //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);
        //if (ds.Tables["TABLE"].Rows.Count > 0)
        //{
        //    if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
        //    {
        //        //if (ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString() == "Verification pending : !! To verify Account..Fill Otp and Verify..Or Reset To Create New Account.")
        //        //{

        //        lblmsgotp.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
        //        //}
        //        //else
        //        //{
        //        //    //Label1.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
        //        //}
        //        divoptsuccess.Visible = true;
        //        divotperror.Visible = false;
        //    }
        //    else
        //    {
        //        lblmsgotperror.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
        //        divoptsuccess.Visible = false;
        //        divotperror.Visible = true;
        //    }
        //}
        //else
        //{
        //    //LblMsg.Text = ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString();
        //    //Error.Visible = true;
        //}
    }
    protected void btncancelcreate_Click(object sender, EventArgs e)
    {
        divLogin.Visible = true;
        divWelcome.Visible = false;
        divCreate.Visible = false;
    }
    protected void btnOTPSubmit_Click(object sender, EventArgs e)
    {
        string Response = string.Empty;


        Response = objmoneytransfer.VerifySenderNew(txtmobilecreate.Text.Trim(), txtotp.Text.Trim(), Session["userid"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
        clsMoneyTransfer.VerifySendar Root = JsonConvert.DeserializeObject<clsMoneyTransfer.VerifySendar>(Response);
        if (Root.statuscode != null)
        {
            if (Root.statuscode == 1)
            {
                objmoneytransfer.Insert_WALLET(Session["userid"].ToString(), txtmobilecreate.Text.Trim(), txtnamecreate.Text.Trim());
                divLogin.Visible = false;
                divCreate.Visible = false;
                divWelcome.Visible = true;
                txtmobilelogin.Text = txtmobilecreate.Text;
                Session["SenderMobileNo"] = txtmobilelogin.Text;
                loadlogin();
                txtotp.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupOTP();", true);
            }
            else
            {
                lblmsgotperror.Text = Root.message;
                divoptsuccess.Visible = false;
                divotperror.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
                return;
            }

        }
        //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);

        //if (ds.Tables["TABLE"].Rows.Count > 0)
        //{
        //    if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
        //    {
        //        objmoneytransfer.Insert_WALLET(Session["userid"].ToString(), txtmobilecreate.Text.Trim(), txtnamecreate.Text.Trim());
        //        divLogin.Visible = false;
        //        divCreate.Visible = false;
        //        divWelcome.Visible = true;
        //        txtmobilelogin.Text = txtmobilecreate.Text;
        //        Session["SenderMobileNo"] = txtmobilelogin.Text;
        //        loadlogin();
        //        txtotp.Text = "";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupOTP();", true);
        //    }
        //    else
        //    {
        //        lblmsgotperror.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
        //        divoptsuccess.Visible = false;
        //        divotperror.Visible = true;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
        //        return;
        //    }


        //}
    }
    protected void btnCloseOTP_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupOTP();", true);
    }
    protected void btnResend_Click(object sender, EventArgs e)
    {
            string Response = string.Empty;
            //lblOTPMsg.Text = "";
            //OTPErrorText.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);

            Response = objmoneytransfer.ResendOtpNew(txtmobilecreate.Text.Trim(), Session["userid"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
            clsMoneyTransfer.ResendSenderOTP Root = JsonConvert.DeserializeObject<clsMoneyTransfer.ResendSenderOTP>(Response);
            if (Root.statuscode != null)
            {
                if (Root.statuscode == 1)
                {

                    divoptsuccess.Visible = true;
                    divotperror.Visible = false;
                    lblmsgotp.Text = "OTP Resend Successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
                    txtotp.Text = "";
                    return;
                }
                else
                {
                    divoptsuccess.Visible = false;
                    divotperror.Visible = true;
                    lblmsgotperror.Text = Root.message;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
                    return;
                }

            }
            //if (Response.Contains("<?"))
            //{
            //    Response = Response.Substring(Response.IndexOf("?>") + 2).Trim();
            //}
            //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);

            //if (ds.Tables["TABLE"].Rows.Count > 0)
            //{
            //    if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
            //    {
            //        divoptsuccess.Visible = true;
            //        divotperror.Visible = false;
            //        lblmsgotp.Text = "OTP Resend Successfully";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
            //        txtotp.Text = "";
            //        return;
            //    }
            //    else
            //    {
            //        divoptsuccess.Visible = false;
            //        divotperror.Visible = true;
            //        lblmsgotperror.Text = ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString();
                  
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalOTP();", true);
            //        return;
            //    }

            //}
    }
    protected void btnCloseMoneyTransfer_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupMR();", true);
    }
    protected void btnSubmitMoneyTranfer_Click(object sender, EventArgs e)
    {
      
        if (( Convert.ToDecimal(txtamount.Text) > Convert.ToDecimal( Session["RemainingLimit"].ToString())) && ddtransfertype.SelectedItem.ToString().ToLower() == "neft")
        {
           
            lblerrorMR.Text = "Amount can not be more than your Remaining limit.";
            divErrorMR.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalMR();", true);
            return;
        }

        if (( Convert.ToDecimal(txtamount.Text) >  Convert.ToDecimal(Session["RemainingLimit"].ToString())) && ddtransfertype.SelectedItem.ToString().ToLower() == "imps")
        {
            lblerrorMR.Text = "Amount can not be more than your Remaining limit.";
            divErrorMR.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalMR();", true);
            return;
        }
        DataTable dtcharges = new DataTable();
        dtcharges = objmoneytransfer.GetCharges( Session["userid"].ToString(), Convert.ToDecimal( txtamount.Text.Trim()));
        if (dtcharges.Rows.Count > 0)
        {
            if (dtcharges.Rows[0]["Message"].ToString() == "Success")
            {
                lblsendermobileconfirm.Text = Session["SenderMobileNo"].ToString();
                lbltransfertypeConfirm.Text = ddtransfertype.SelectedItem.ToString();
                lblbeneficiarynameConfirm.Text = Session["BeneficiaryName"].ToString();
                lblbeneficiaryaccnoConfirm.Text = Session["AccountNo"].ToString();
                lblamountConfirm.Text = txtamount.Text.Trim();
                lblcharges.Text = dtcharges.Rows[0]["CommAmt"].ToString();
                lbltotalamount.Text = dtcharges.Rows[0]["DeductionAmt"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalMRConfirm();", true);
            }
            else
            {
                lblerrorMR.Text  = dtcharges.Rows[0]["Message"].ToString();
                divErrorMR.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalMR();", true);
            }

        }
    }
    Random _random = new Random();
    public int RandomNumber(int min, int max)
    {
        return _random.Next(min, max);
    }  
    protected void btnSubmitConfirmMR_Click(object sender, EventArgs e)
    {
        DataTable dtTransId = objmoneytransfer.GenerateTransId();
        if (dtTransId.Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            string url = objmoneytransfer.GetMoneyTransferUrlNew(Session["SenderMobileNo"].ToString().Trim(), Session["BeneficiaryId"].ToString().Trim(), txtamount.Text.Trim(), Session["BeneficiaryName"].ToString(), Session["SenderMobileNo"].ToString(), Session["AccountNo"].ToString().Trim(), Session["Bank"].ToString(), Session["IFSC"].ToString(), ddbank.SelectedValue, ddtransfertype.SelectedValue, dtTransId.Rows[0][0].ToString().Trim(), Session["UserID"].ToString().Trim(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
            Double INRamount = Convert.ToDouble(ViewState["dollar"].ToString()) * Convert.ToDouble(txtamount.Text.Trim());
            Int32 RR = RandomNumber(10000, 100000);
            dt = objmoneytransfer.ValidateMRTransfer(Session["userid"].ToString(), 0, Session["SenderMobileNo"].ToString(),
              Convert.ToInt32(Session["BeneficiaryId"].ToString()), Session["AccountNo"].ToString(), Convert.ToDecimal(txtamount.Text), ddtransfertype.SelectedValue, dtTransId.Rows[0][0].ToString(), url, Session["userid"].ToString(),INRamount.ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "1")
                {
                    string Response = objmoneytransfer.CallMoneyTransferApiNew(Session["SenderMobileNo"].ToString().Trim(), Session["BeneficiaryId"].ToString().Trim(), INRamount.ToString(), Session["BeneficiaryName"].ToString(), Session["SenderMobileNo"].ToString(), Session["AccountNo"].ToString().Trim(), Session["Bank"].ToString(), Session["IFSC"].ToString(), "0", ddtransfertype.SelectedValue, RR.ToString(), Session["UserID"].ToString().Trim(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString(), GetLocalIPAddress());
                    clsMoneyTransfer.AccountTransfer Root = JsonConvert.DeserializeObject<clsMoneyTransfer.AccountTransfer>(Response);
                    try
                    {
                        if (Root.statuscode != null)
                        {
                            if (Root.statuscode == 1)
                            {
                                if (Root.status == 2)
                                {
                                    objmoneytransfer.UpdateMRTransferResponse(Session["userid"].ToString(), Session["BeneficiaryId"].ToString(), Session["AccountNo"].ToString(),
                      dtTransId.Rows[0][0].ToString(), Response, Root.status.ToString(), Root.liveID, Root.rpid, Session["userid"].ToString());
                                    txtamount.Text = "";
                                   
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + Root.message + "');", true);
                                }
                                else if (Root.status == 3)
                                {
                                    if (Root.message.ToLower().Contains("insufficient balance") == true)
                                    {
                                        objmoneytransfer.UpdateMRTransferResponse(Session["userid"].ToString(), Session["BeneficiaryId"].ToString(), Session["AccountNo"].ToString(),
                dtTransId.Rows[0][0].ToString(), Response, Root.status.ToString(), Root.liveID, Root.rpid, Session["userid"].ToString());
                                        txtamount.Text = "";

                                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('Technical Issue');", true);
                                    }
                                    else
                                    {
                                        objmoneytransfer.UpdateMRTransferResponse(Session["userid"].ToString(), Session["BeneficiaryId"].ToString(), Session["AccountNo"].ToString(),
                  dtTransId.Rows[0][0].ToString(), Response, Root.status.ToString(), Root.liveID, Root.rpid, Session["userid"].ToString());
                                        txtamount.Text = "";

                                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + Root.message + "');", true);
                                    }
                                  
                                }
                                else
                                {
                                    objmoneytransfer.UpdateMRTransferResponse(Session["userid"].ToString(), Session["BeneficiaryId"].ToString(), Session["AccountNo"].ToString(),
                   dtTransId.Rows[0][0].ToString(), Response, "1", Root.liveID, Root.rpid, Session["userid"].ToString());
                                    txtamount.Text = "";
                                   
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + Root.message + "');", true);
                                }
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupMRConfirm();", true);
                            }
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + dt.Rows[0][1].ToString() + "');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupMRConfirm();", true);

                    }
                   
                    //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);
                    //if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
                    //{
                    //    if (ds.Tables["DMR"].Rows[0]["MESSAGE"].ToString().ToLower().Contains("insufficient balance") == true)
                    //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('Technical Issue');", true);
                    //    else
                    //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + ds.Tables["DMR"].Rows[0]["MESSAGE"].ToString() + "');", true);

                    //}
                    //else
                    //{
                    //    if (ds.Tables["DMR"].Rows[0]["MESSAGE"].ToString().ToLower().Contains("insufficient balance") == true)
                    //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('Technical Issue');", true);
                    //    else
                    //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + ds.Tables["DMR"].Rows[0]["MESSAGE"].ToString() + "');", true);
                    //    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + ds.Tables["DMR"].Rows[0]["MESSAGE"].ToString() + "');", true);

                    //}
                   // if (ds.Tables["DMR"].Rows[0]["bank_ref_num"].ToString() == "")
                   //     objmoneytransfer.UpdateMRTransferResponse(Session["userid"].ToString(), Session["BeneficiaryId"].ToString(), Session["AccountNo"].ToString(),
                   //dtTransId.Rows[0][0].ToString(), Response, ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString(), ds.Tables["DMR"].Rows[0]["tid"].ToString(), ds.Tables["DMR"].Rows[0]["tid"].ToString(), Session["userid"].ToString());
                   // else
                   //     objmoneytransfer.UpdateMRTransferResponse(Session["userid"].ToString(), Session["BeneficiaryId"].ToString(), Session["AccountNo"].ToString(),
                   //     dtTransId.Rows[0][0].ToString(), Response, ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString(), ds.Tables["DMR"].Rows[0]["bank_ref_num"].ToString(), ds.Tables["DMR"].Rows[0]["tid"].ToString(), Session["userid"].ToString());
                   // txtamount.Text="";
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupMRConfirm();", true);
                }
                else if (dt.Rows[0][0].ToString() == "-1")
                {
                    //if (dt.Rows[0][1].ToString().Contains("Insufficient balance") == true)
                    //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('Technical Issue');", true);
                    //else
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + dt.Rows[0][1].ToString() + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupMRConfirm();", true);
                    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + dt.Rows[0][1].ToString() + "');", true);

                }

            }

            loadlogin();
        }
        loadbalance();
      
    }
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }
    protected void btnCloseConfirmMR_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosepopupMRConfirm();", true);
    }
    protected void btnverify_Click(object sender, EventArgs e)
    {
       
        if (txtbenaccountno.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('Fill Account No');", true);
             return;
        }

        //if (txtifscsticky.Text.Trim() == "")
        //{
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('Fill IFSC Code');", true);
          
        //    return;
        //}
        DataTable dtValidate = new DataTable();
        dtValidate = objmoneytransfer.ValidateBeneficiary(Session["userid"].ToString(), 0, txtbenaccountno.Text.Trim(), Session["SenderMobileNo"].ToString());
        if (dtValidate.Rows.Count > 0)
        {
            if (dtValidate.Rows[0][0].ToString() == "Success")
            {
                string Response = string.Empty;
               // if (txtifsccode.Text.Trim() != "")
                Response = objmoneytransfer.CallVerifyBeneficiaryApiNew(txtsendermobileno.Text.Trim(), txtbenegiciarymobile.Text, txtbenaccountno.Text.Trim(), ddbank.Text, (txtifscsticky.Text.Trim() + txtifsccode.Text.Trim()), ddbank.SelectedValue, "2", dtValidate.Rows[0]["TransID"].ToString(), Session["userid"].ToString(), Session["Sessionkey"].ToString(), Session["OutletID"].ToString());
               // else
                  //  Response = objmoneytransfer.CallVerifyBeneficiaryApi(txtsendermobileno.Text.Trim(), txtbenaccountno.Text.Trim(), txtifscsticky.Text.Trim(), Session["userid"].ToString());
                //if (Response.Contains("<?"))
                //{
                //    Response = Response.Substring(Response.IndexOf("?>") + 2).Trim();
                //}
                //DataSet ds = objmoneytransfer.ConvertXMLToDataSet(Response);
                clsMoneyTransfer.Verifyaccount Root = JsonConvert.DeserializeObject<clsMoneyTransfer.Verifyaccount>(Response);
                if (Root.statuscode != null)
                {
                    if (Root.statuscode == 1)
                    {
                        Session["ValidateStatus"] = "1";
                        objmoneytransfer.DeValidateBeneficiary(Session["userid"].ToString(), 0, txtbenaccountno.Text.Trim(), dtValidate.Rows[0]["TransID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Response);
                        txtbeneficiaryname.Text = Root.beneName;
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + Root.beneName + " Account Validated Successfully');", true);
                        return;
                    }
                    else
                    {
                        Session["ValidateStatus"] = "0";
                        objmoneytransfer.DeValidateBeneficiary(Session["userid"].ToString(), 0, txtbenaccountno.Text.Trim(), dtValidate.Rows[0]["TransID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Response);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + Root.message + "');", true);
                        return;
                    }

                }
                else
                {
                    Session["ValidateStatus"] = "0";
                    objmoneytransfer.DeValidateBeneficiary(Session["userid"].ToString(), 0, txtbenaccountno.Text.Trim(), dtValidate.Rows[0]["TransID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Response);
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + Root.message + "');", true);
                    return;
                }

                //if (ds.Tables["TABLE"].Rows.Count > 0)
                //{
                //    if (ds.Tables["TABLE"].Rows[0]["RESPONSESTATUS"].ToString() == "1")
                //    {
                //        Session["ValidateStatus"] = "1";
                //        objmoneytransfer.DeValidateBeneficiary( Session["userid"].ToString(), 0, txtbenaccountno.Text.Trim(), dtValidate.Rows[0]["TransID"].ToString(), Convert.ToInt32( Session["ValidateStatus"].ToString()), Response);
                //        txtbeneficiaryname.Text = ds.Tables["TABLE"].Rows[0]["RECIPIENTNAME"].ToString();
                //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + ds.Tables["TABLE"].Rows[0]["RECIPIENTNAME"].ToString() + " Account Validated Successfully');", true);
                //        return;

                //    }
                //    else
                //    {
                //        Session["ValidateStatus"] = "0";
                //        objmoneytransfer.DeValidateBeneficiary(Session["userid"].ToString(), 0, txtbenaccountno.Text.Trim(), dtValidate.Rows[0]["TransID"].ToString(), Convert.ToInt32(Session["ValidateStatus"].ToString()), Response);
                //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + ds.Tables["TABLE"].Rows[0]["MESSAGE"].ToString()+"');", true);
                //        return;
                //    }
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myalert", "alert('" + dtValidate.Rows[0][0].ToString() + "');", true);
              
            }
        }
    }
    private DataTable ConvertJSONToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        //strip out bad characters
        string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

        //hold column names
        List<string> dtColumns = new List<string>();

        //get columns
        foreach (string jp in jsonParts)
        {
            //only loop thru once to get column names
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                }

            }
            break; // TODO: might not be correct. Was : Exit For
        }

        //build dt
        foreach (string c in dtColumns)
        {
            dt.Columns.Add(c);
        }
        //get table data
        foreach (string jp in jsonParts)
        {
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string v = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[n] = v;
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            dt.Rows.Add(nr);
        }
        return dt;
    }
}