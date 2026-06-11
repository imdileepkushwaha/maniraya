using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DataTier;
public partial class admin_SubAdmin : System.Web.UI.Page
{

 
    clsState objState = new clsState();    
    clsEPin objepin = new clsEPin();
    Clsmail objmail = new Clsmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();

                FillYears();
                FillMonths();
                FillDays();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li1 = new ListItem("Other", "111111");
        ddcity.Items.Insert(0, li1);
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }
    public void FillMonths()
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("ID");
        Dt.Columns.Add("Value");
        DataRow DR;

        for (int i = 1; i <= 12; i++)
        {
            DR = Dt.NewRow();
            DR["ID"] = i;
            DR["Value"] = ChkMonth(i.ToString());
            Dt.Rows.Add(DR);
        }
        ddlMonth.DataSource = Dt;
        ddlMonth.DataTextField = "Value";
        ddlMonth.DataValueField = "ID";
        ddlMonth.DataBind();
        ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString(); // Set current month as selected
    }
    public void FillYears()
    {
        //Fill Years
        for (int i = (DateTime.Now.Year - 50); i <= DateTime.Now.Year; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
        ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;  //set current year as selected
    }
    public void FillDays()
    {
        //Clear Days
        ddlDay.Items.Clear();
        //getting numbner of days in selected month & year
        int noofdays = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

        //Fill days
        for (int i = 1; i <= noofdays; i++)
        {
            ddlDay.Items.Add(i.ToString());
        }
        ddlDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
    }
    protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddcity.SelectedIndex != 0)
        {
            if (ddcity.SelectedValue == "111111")
            {
                otherPnl.Visible = true;
            }
            else
            {
                otherPnl.Visible = false;
            }
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDays();
    }
    public string ChkMonth(string Code)
    {
        string Operator = "";
        switch (Code)
        {
            case "1":
                Operator = "Jan";
                break;
            case "2":
                Operator = "Feb";
                break;
            case "3":
                Operator = "Mar";
                break;
            case "4":
                Operator = "Apr";
                break;
            case "5":
                Operator = "May";
                break;
            case "6":
                Operator = "Jun";
                break;
            case "7":
                Operator = "Jul";
                break;
            case "8":
                Operator = "Aug";
                break;
            case "9":
                Operator = "Sep";
                break;
            case "10":
                Operator = "Oct";
                break;
            case "11":
                Operator = "Nov";
                break;
            case "12":
                Operator = "Dec";
                break;
        }

        return Operator;
    }

    public bool chk_passlength() {

        if (txtuserpassword.Text.Length == 6)
        {

            return true;
        }
        else {

            lblpasslenwarn.Text = "Password Should be Of SIX Digit";
            lblpasslenwarn.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    
    
    }
    public class subadmin
    {
        Data ObjData = new Data();
        public string username { get; set; }
        public string password { get; set; }
        public string newpassword { get; set; }
        public string UserId { get; set; }
        public string SponserId { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthnew { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Password { get; set; }
        public string MentionBy { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string CityId { get; set; }
        public string EpinNo { get; set; }
        public string StandingPosition { get; set; }
        public string AreaName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string TransferUserId { get; set; }
        public int NoOfEpin { get; set; }
        public string ParentUserId { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string AccHolderName { get; set; }
        public string AccNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string PanCardNo { get; set; }
        public decimal pinamount { get; set; }
        public string RegType { get; set; }
        public string Photo { get; set; }
        public string Signupform { get; set; }
        public string PanImage { get; set; }
        public string CancelCheck { get; set; }
        public string Addressproof { get; set; }
        public string AddressproofBack { get; set; }
        public string AdhaarNo { get; set; }
        public string OtherCity { get; set; }
        public string ipaddress { get; set; }
        public string OTP { get; set; }
        public string Remark
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        public string CallResultStatus
        {
            get;
            set;
        }

        public string PickedBy
        {
            get;
            set;
        }
        public string CallStatus
        {
            get;
            set;
        }


        public string outletName { get; set; }
        public string gstNo { get; set; }
        public string gstImg { get; set; }

        public string queryType { get; set; }
        public int requestId { get; set; }
        public string prevMobileNo { get; set; }
        public string requestStatus { get; set; }

        public string Insert_User_subadmin(subadmin objUser)
        {
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "sp_add_subadmin";
                SqlParameter[] parameter = {              
                    new SqlParameter("@SponserId",objUser.SponserId), 
                    new SqlParameter("@username",objUser.UserName), 
                    new SqlParameter("@DateofBirth",objUser.DateOfBirthnew), 
                    new SqlParameter("@Gender",objUser.Gender), 
                    new SqlParameter("@Email",objUser.Email), 
                    new SqlParameter("@Mobile",objUser.Mobile), 
                    new SqlParameter("@Address",objUser.Address),
                    new SqlParameter("@CityId",objUser.CityId), 
                    new SqlParameter("@AreaName",objUser.AreaName), 
                    new SqlParameter("@Pincode",objUser.Pincode), 
                    new SqlParameter("@Password",objUser.Password), 
                    new SqlParameter("@MentionBy",objUser.MentionBy),
                    new SqlParameter("@EPinNo",objUser.EpinNo),
                     new SqlParameter("@StandingPosition",objUser.StandingPosition),
                      new SqlParameter("@RegType",objUser.RegType),
                      new SqlParameter("@OtherCity",objUser.OtherCity),
                      new SqlParameter("@outletName",objUser.outletName),
                      new SqlParameter("@panNumber",objUser.PanCardNo),
                      new SqlParameter("@panImg",objUser.PanImage),
                      new SqlParameter("@GST_Number",objUser.gstNo),
                      new SqlParameter("@GST_Img",objUser.gstImg)
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
                //string url = string.Concat(new string[]
                //{
                //    "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=turab.prince@gmail.com:9153152863&senderID=ARSENR&receipientno=",
                //    objUser.Mobile,
                //    "&dcs=0&msgtxt=Congrats! Your Mobile Number Successfully Registered as SubAdmin with ARENPAY.User Id:",
                //    res,
                //    ", and password:",
                //    objUser.Password,
                //    " Login to www.arsenpay.in. The User Id and Password should not be disclosed to any one",
                //    "&state=4"
                //});
                //string Result = url.CallURL();
                //Insert_SendSMS(objUser.Mobile, Result, url);
                tr.Commit();

            }
            catch (Exception ex)
            {
                res = "0";
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        subadmin objUser = new subadmin();
        objUser.RegType = "free";
        objUser.StandingPosition = "1";
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.Gender = ddgender.SelectedValue.ToString();
        objUser.Address = txtaddress.Text;
        objUser.CityId = ddcity.SelectedValue.ToString();
        objUser.CountryId = ddcountry.SelectedValue.ToString();
        objUser.StateId = ddstate.SelectedValue.ToString();
        objUser.AreaName = txtareaname.Text;
        objUser.Pincode = txtpincode.Text;
        objUser.DateOfBirthnew = ddlDay.SelectedValue.ToString() + "/" + ddlMonth.SelectedItem.ToString() + "/" + ddlYear.SelectedItem.ToString();
        objUser.Password = txtuserpassword.Text;   
        objUser.MentionBy = Session["useradmin"].ToString();
        objUser.SponserId = "0";
        objUser.EpinNo = "0";
        objUser.OtherCity = TxtOtherCity.Text;



        //objUser.outletName = txtOutletName.Text;
        //objUser.PanCardNo = txtPANNo.Text;
        objUser.PanImage = Convert.ToString(ViewState["PAN_Img"]);

        //objuser.gstno = txtgstno.text;
        objUser.gstImg = Convert.ToString(ViewState["GST_Img"]);

        string res = "";
        if (chk_passlength()) {
            res = objUser.Insert_User_subadmin(objUser);
        }
       
        if (res =="f")
        {
            string popupScript = "alert('Mobile No Already Exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
            if (res == "0")
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                //objmail.sendmail(txtname.Text, txtuserpassword.Text, res, txtemail.Text);
                if (chk_passlength())
                {
                    lblpasslenwarn.Text = "";
                    string popupScript = "alert('SubAdmin Added Successfully, SubAdminId is " + res + "');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    txtname.Text = txtmobile.Text = txtemail.Text = txtaddress.Text = txtuserpassword.Text = txtconfirmpassword.Text = txtpincode.Text = txtareaname.Text = "";

                    ddcountry.SelectedValue = "0";
                    loadstate();
                    loadcity();
                }
                else {
                }
               
            }

    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }

    //public string UploadPAN()
    //{
    //    string Imagename = "";
    //    if (filePAN.HasFile)
    //    {
    //        string RandomNumber = DateTime.Now.Ticks.ToString();
    //        string fileName = Path.GetFileName(filePAN.PostedFile.FileName);
    //        Imagename = RandomNumber + fileName;
    //        filePAN.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

    //    }
    //    return Imagename;
    //}

    //public string UploadGST()
    //{
    //    string Imagename = "";
    //    if (fileGST.HasFile)
    //    {
    //        string RandomNumber = DateTime.Now.Ticks.ToString();
    //        string fileName = Path.GetFileName(fileGST.PostedFile.FileName);
    //        Imagename = RandomNumber + fileName;
    //        fileGST.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

    //    }
    //    return Imagename;
    //}

    //protected void btnPANUPload_Click(object sender, EventArgs e)
    //{
    //    if (!filePAN.HasFile)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Select PAN File')", true);
    //        return;
    //    }

    //    ViewState["PAN_Img"] = UploadPAN();

    //    imgPAN.ImageUrl = "~/ProductImage/" + ViewState["PAN_Img"];
    //}

    //protected void btnGSTUpload_Click(object sender, EventArgs e)
    //{
    //    if (!fileGST.HasFile)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Select GST File')", true);
    //        return;
    //    }

   //  ViewState["GST_Img"] = UploadGST();

    //    imgGST.ImageUrl = "~/ProductImage/" + ViewState["GST_Img"];
    //}

    //protected void imgPAN_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImagePANLarge.ImageUrl = "~/ProductImage/" + ViewState["PAN_Img"].ToString();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
    //}

    //protected void imgGST_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageGSTLarge.ImageUrl = "~/ProductImage/" + ViewState["GST_Img"].ToString();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
    //}
}