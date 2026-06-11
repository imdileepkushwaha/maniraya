using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SubAdminReport : System.Web.UI.Page
{

    clsState objState = new clsState();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();
                loadcountryedit();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
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
        public DataTable getUserDetail(string userid)
        {
            string str_query = "SELECT ud.*,cm.stateid,sm.countryid,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage FROM subadmindetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid where ud.UserId = '" + userid + "' ";
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public int changeFranchiseeStatus(string userid)
        {
            string sql = @"update Logindetail set status=(case status when 0 then 1 when 1 then 0 end) where Username='" + userid + "'";

            ObjData.StartConnection();
            try
            {
                return ObjData.RunInsUpDelQueryNew(sql);
            }
            catch
            {
                throw;
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        public DataTable getUserReport(string UserId, DateTime FromDate, DateTime ToDate, string UserName, string Mobile, string Email, string CityId)
        {
            string str_query = @"SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,ld.status as activeStatus, 
                                (case when SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when SignUpImgStatus=0 then 'Pending' when SignUpImgStatus=1 then 'Approved' when SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss,  
                                (case when PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when PanImgStatus=0 then 'Pending' when PanImgStatus=1 then 'Approved' when PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ChequeImgStatus=0 then 'Pending' when ChequeImgStatus=1 then 'Approved' when ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss,
                                (case when AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when AadharImgStatus=0 then 'Pending' when AadharImgStatus=1 then 'Approved' when AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss
                                 FROM subadmindetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId 
                                left join Logindetail ld on ud.userid=ld.username where 1=1 ";

            if (FromDate != DateTime.MinValue && ToDate != DateTime.MinValue)
            {
                str_query += "  and ud.MentionDate  >= '" + FromDate + "'   and ud.MentionDate   <= '" + ToDate + "' ";
            }
            //if (objUser.UserName != "")
            if (!string.IsNullOrEmpty(UserName))
            {
                str_query += "  and ud.username = '" + UserName + "' ";
            }
            //if (objUser.UserId != "")
            if (!string.IsNullOrEmpty(UserId))
            {
                str_query += "  and ud.UserId = '" + UserId + "' ";
            }
            //if (objUser.Mobile != "")
            if (!string.IsNullOrEmpty(Mobile))
            {
                str_query += "  and ud.mobile = '" + Mobile + "' ";
            }
            //if (objUser.Email != "")
            if (!string.IsNullOrEmpty(Email))
            {
                str_query += "  and ud.email = '" + Email + "' ";
            }
            if (CityId != "0" && !string.IsNullOrEmpty(CityId))
            {
                str_query += "  and ud.cityid = '" + CityId + "' ";
            }

            str_query += " order by ud.MentionDate  desc";

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(str_query);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public string Update_UserProfile(string UserName, string Email, DateTime DateOfBirth, string Gender, string Mobile, string Address, string CityId, string AreaName, string Pincode, string AccHolderName, string AccNo, string IFSCCode, string BankName, string BranchName, string PanCardNo, string NomineeName, string NomineeRelation, string UserId)
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
                string str = "SP_chekUser";
                SqlParameter[] parameter = {              
                   
                    new SqlParameter("@Email",Email), 
                    new SqlParameter("@Mobile",Mobile),                    
                  new SqlParameter("@UserId",UserId),     
                    
                };
                res = ObjData.RunInsUpDelQueryTransProcScalar(str, tr, parameter);
                if (res == "1")
                {
                    s2 = "update subadmindetail  set username='" + UserName + "', email='" + Email + "',dateofbirth='" + DateOfBirth.ToString("yyyy/MM/dd") + "',gender='" + Gender + "' ,mobile='" + Mobile + "', address='" + Address + "', cityid='" + CityId + "',areaName='" + AreaName + "' ,pincode='" + Pincode + "',AccountHolderName='" + AccHolderName + "',AccountNo='" +AccNo + "',IFSCCode='" + IFSCCode + "',BankName='" + BankName + "',BranchName='" + BranchName + "',PanNumber='" + PanCardNo + "',NomineeName='" + NomineeName + "',NomineeRelation='" + NomineeRelation + "'  where UserId='" + UserId + "'   ";
                    ObjData.RunInsUpDelQueryTrans(s2, tr);
                    res = "t";
                    tr.Commit();
                }
                else
                {


                }
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
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }
    void loadarea()
    {
        ddarea.Items.Clear();
        DataTable dt = new DataTable();
        objState.CityId = ddcity.SelectedValue.ToString();
        dt = objState.getArea(objState);

        ddarea.DataSource = dt;
        ddarea.DataTextField = "areaName";
        ddarea.DataValueField = "areaID";
        ddarea.DataBind();
        ListItem li = new ListItem("Select Area", "0");
        ddarea.Items.Insert(0, li);
    }
    void loadcountryedit()
    {
        ddcountryedit.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountryedit.DataSource = dt;
        ddcountryedit.DataTextField = "CountryName";
        ddcountryedit.DataValueField = "CountryID";
        ddcountryedit.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountryedit.Items.Insert(0, li);
    }
    void loadstateedit()
    {
        ddstateedit.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountryedit.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstateedit.DataSource = dt;
        ddstateedit.DataTextField = "StateName";
        ddstateedit.DataValueField = "StateID";
        ddstateedit.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstateedit.Items.Insert(0, li);
    }
    void loadcityedit()
    {
        ddcityedit.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstateedit.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddcityedit.DataSource = dt;
        ddcityedit.DataTextField = "CityName";
        ddcityedit.DataValueField = "CityID";
        ddcityedit.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcityedit.Items.Insert(0, li);
    }
    void loadareaedit()
    {
        //ddareaedit.Items.Clear();
        //DataTable dt = new DataTable();
        //objState.CityId = ddcityedit.SelectedValue.ToString();
        //dt = objState.getArea(objState);

        //ddareaedit.DataSource = dt;
        //ddareaedit.DataTextField = "areaName";
        //ddareaedit.DataValueField = "areaID";
        //ddareaedit.DataBind();
        //ListItem li = new ListItem("Select Area", "0");
        //ddareaedit.Items.Insert(0, li);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        subadmin objUser = new subadmin();
        if (e.CommandName == "edt")
        {
           
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lbluser = (Label)GridView1.Rows[index].FindControl("lbluserid");
            objUser.UserId = lbluser.Text;
            DataTable dt = new DataTable();
            dt = objUser.getUserDetail(objUser.UserId);
            if (dt.Rows.Count > 0)
            {
                ViewState["UID"] = objUser.UserId;
                txtnameedit.Text = dt.Rows[0]["username"].ToString();
                txtmobileedit.Text = dt.Rows[0]["mobile"].ToString();
                txtemailedit.Text = dt.Rows[0]["email"].ToString();
                ddgenderedit.SelectedValue = dt.Rows[0]["gender"].ToString();
                txtaddressedit.Text = dt.Rows[0]["address"].ToString();
                ddcountryedit.SelectedValue = dt.Rows[0]["countryid"].ToString();
                loadstateedit();
                ddstateedit.SelectedValue = dt.Rows[0]["stateid"].ToString();
                loadcityedit();
                ddcityedit.SelectedValue = dt.Rows[0]["cityid"].ToString();
                //loadareaedit();
                ddareaedit.Text = dt.Rows[0]["areaname"].ToString();
                txtpincodeedit.Text = dt.Rows[0]["pincode"].ToString();
                txtdateofbirthedit.Text = Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString()).ToString("dd/MMM/yyyy");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        if (e.CommandName == "changeStatus")
        {
            try
            {
                objUser.UserId = e.CommandArgument.ToString();

                if (objUser.changeFranchiseeStatus(objUser.UserId) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Status Changed Successfully...!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
            }
            finally
            {
                loaduser();
              
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
    protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadarea();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {
        subadmin objUser = new subadmin();
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.CityId = ddcity.SelectedValue.ToString();
        objUser.AreaName = ddarea.SelectedValue.ToString();
        if (txtfromdate.Text != "")
        {
            objUser.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objUser.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objUser.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objUser.ToDate = DateTime.MinValue;
        }
        objUser.UserId = "";
        DataTable dt = new DataTable();
        dt = objUser.getUserReport(objUser.UserId, objUser.FromDate, objUser.ToDate, objUser.UserName, objUser.Mobile, objUser.Email, objUser.CityId);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
    protected void ddcountryedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstateedit();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void ddstateedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcityedit();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void ddcityedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadareaedit();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        subadmin objUser = new subadmin();
        objUser.UserId = ViewState["UID"].ToString();
        objUser.UserName = txtnameedit.Text;
        objUser.Mobile = txtmobileedit.Text;
        objUser.Email = txtemailedit.Text;
        objUser.Gender = ddgenderedit.SelectedValue.ToString();
        objUser.Address = txtaddressedit.Text;
        objUser.CityId = ddcityedit.SelectedValue.ToString();
        objUser.CountryId = ddcountryedit.SelectedValue.ToString();
        objUser.StateId = ddstateedit.SelectedValue.ToString();
        objUser.AreaName = ddareaedit.Text;
        objUser.Pincode = txtpincodeedit.Text;
        objUser.DateOfBirth = Message.GetIndianDate(txtdateofbirthedit.Text);
        string res = objUser.Update_UserProfile(objUser.UserName, objUser.Email, objUser.DateOfBirth,objUser.Gender, objUser.Mobile,objUser.Address,objUser.CityId,objUser.AreaName,objUser.Pincode,objUser.AccHolderName,objUser.AccNo,objUser.IFSCCode,objUser.BankName,objUser.BranchName,objUser.PanCardNo,objUser.NomineeName,objUser.NomineeRelation,objUser.UserId);
        if (res == "t")
        {
            loaduser();
            string popupScript = "alert('User Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else
        {
            
            string popupScript = "alert(' mobile number and email id sholud be unique !!!');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}