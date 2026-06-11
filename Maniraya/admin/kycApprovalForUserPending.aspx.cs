using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using DataTier;

public partial class kycApprovalForUserPending : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcountry();
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
        objUser.UserName = string.Empty;
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
        objUser.UserId = txtname.Text;
        DataTable dt = new DataTable();
        dt = getUserReport(objUser);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public DataTable getUserReport(clsUser objUser)
    {
        //string str_query = "SELECT ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password,isnull(ud.balanceamount,0) as balanceamount FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId left join Logindetail ld on ud.userid=ld.username where 1=1 ";
        string str_query = @"SELECT ud.status,ud.Pannumber,ud.adharnumber,ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,isnull(ud.utilityBalance,0) as utilityBalance,ld.status as activeStatus, 
                                (case when ud.SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when ud.SignUpImgStatus=0 then 'Pending' when ud.SignUpImgStatus=1 then 'Approved' when ud.SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss, 
                                (case when ud.PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when ud.PanImgStatus=0 then 'Pending' when ud.PanImgStatus=1 then 'Approved' when ud.PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ud.ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ud.ChequeImgStatus=0 then 'Pending' when ud.ChequeImgStatus=1 then 'Approved' when ud.ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when ud.AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when ud.AadharImgStatus=0 then 'Pending' when ud.AadharImgStatus=1 then 'Approved' when ud.AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss,  
                                epin.planId,plm.PlanName as packageName,ud.SponserId,isnull(ud1.userName,'Company')sponserName,ud.PanNumber,sm.stateName,ud.Pincode,ud.epinGenerationStatus,CASE WHEN isnull(ud.GSTimage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.GSTimage END AS GSTimage,ud.gstnumber,isnull(ud.IsGSTDeductedOfUnverified,0) as IsGSTDeductedOfUnverifie,
(case when ud.IsGstApplicable=0 then 'Pending' when ud.IsGstApplicable=1 then 'Approved' when ud.IsGstApplicable=2 then 'Rejected' end)IsGstApplicable 
                                FROM userdetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId inner join statemaster sm on sm.stateId=cm.stateId 
                                left join Logindetail ld on ud.userid=ld.username left join EPinMaster epin on epin.UsedUserId=ud.userID 
                                left join PlanMaster plm on plm.id=epin.planId left join userdetail ud1 on ud.sponserId=ud1.userId 
                                where 1=1 ";

        if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
        {
            str_query += "  and ud.MentionDate  > '" + objUser.FromDate + "'   and ud.MentionDate   < '" + objUser.ToDate + "' ";
        }
        //if (objUser.UserName != "")
        if (!string.IsNullOrEmpty(objUser.UserName))
        {
            str_query += "  and ud.username = '" + objUser.UserName + "' ";
        }
        //if (objUser.UserId != "")
        if (!string.IsNullOrEmpty(objUser.UserId))
        {
            str_query += "  and ud.UserId = '" + objUser.UserId + "' ";
        }
        //if (objUser.Mobile != "")
        if (!string.IsNullOrEmpty(objUser.Mobile))
        {
            str_query += "  and ud.mobile = '" + objUser.Mobile + "' ";
        }
        //if (objUser.Email != "")
        if (!string.IsNullOrEmpty(objUser.Email))
        {
            str_query += "  and ud.email = '" + objUser.Email + "' ";
        }
        if (objUser.CityId != "0" && !string.IsNullOrEmpty(objUser.CityId))
        {
            str_query += "  and ud.cityid = '" + objUser.CityId + "' ";
        }
        if (!string.IsNullOrEmpty(objUser.Pincode))
        {
            str_query += "  and ud.Pincode = '" + objUser.Pincode + "' ";
        }

        if (!string.IsNullOrEmpty(objUser.StateId) && objUser.StateId != "0")
        {
            str_query += "  and cm.stateId = '" + objUser.StateId + "' ";
        }
        if (!string.IsNullOrEmpty(objUser.plan_Id) && objUser.plan_Id != "0")
        {
            str_query += "  and epin.planId = '" + objUser.plan_Id + "' ";
        }
        if (!string.IsNullOrEmpty(objUser.SponserId) && objUser.SponserId != "0")
        {
            str_query += "  and ud.SponserId = '" + objUser.SponserId + "' ";
        }
        if (!string.IsNullOrEmpty(objUser.PanCardNo))
        {
            str_query += "  and ud.PanNumber = '" + objUser.PanCardNo + "' ";
        }

        str_query += "  and ud.PanImgStatus = 0 or ud.ChequeImgStatus = 0 or ud.AadharImgStatus=0 ";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
        ImageButton imgBtn;

        string userid = e.CommandArgument.ToString();

        if (e.CommandName == "openSignUpImg")
        {
            imgBtn = (ImageButton)gvr.FindControl("imgSignUpForm");
            ImageLarge.ImageUrl = imgBtn.ImageUrl;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }

        else if (e.CommandName == "openPANImg")
        {
            imgBtn = (ImageButton)gvr.FindControl("imgPANCard");
            ImageLarge.ImageUrl = imgBtn.ImageUrl;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }

        else if (e.CommandName == "opengstImg")
        {
            imgBtn = (ImageButton)gvr.FindControl("imgGSTCard");
            ImageLarge.ImageUrl = imgBtn.ImageUrl;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }

        else if (e.CommandName == "openChequeImg")
        {
            imgBtn = (ImageButton)gvr.FindControl("imgCheque");
            ImageLarge.ImageUrl = imgBtn.ImageUrl;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }

        else if (e.CommandName == "openAadhaarImg")
        {
            imgBtn = (ImageButton)gvr.FindControl("imgAadhaar");
            ImageLarge.ImageUrl = imgBtn.ImageUrl;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }

        else if (e.CommandName == "openAadhaarImgBack")
        {
            imgBtn = (ImageButton)gvr.FindControl("imgAadhaarBack");
            ImageLarge.ImageUrl = imgBtn.ImageUrl;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }



        else if (e.CommandName == "approve_signup")
        {
            objUser.changeKYCStatus("Sign Up", "Approve", userid);
            Message.Show("Sign Up Approved");
            loaduser();
        }

        else if (e.CommandName == "approve_pan")
        {
            objUser.changeKYCStatus("PAN", "Approve", userid);
            Message.Show("PAN Card Approved");
            loaduser();
        }

        else if (e.CommandName == "approve_cheque")
        {
            objUser.changeKYCStatus("Cheque", "Approve", userid);
            Message.Show("Cancel Cheque/Passbook Approved");
            loaduser();
        }

        else if (e.CommandName == "approve_aadhaar")
        {
            objUser.changeKYCStatus("Aadhaar", "Approve", userid);
            Message.Show("Aadhaar Card Approved");
            loaduser();
        }

        else if (e.CommandName == "approve_GST")
        {
            objUser.changeKYCStatus("GST", "Approve", userid);
            Message.Show("GST Approved");
            loaduser();
        }


        else if (e.CommandName == "reject_signup")
        {
            objUser.changeKYCStatus("Sign Up", "Reject", userid);
            Message.Show("Sign Up Rejected");
            loaduser();
        }

        else if (e.CommandName == "reject_pan")
        {
            objUser.changeKYCStatus("PAN", "Reject", userid);
            Message.Show("PAN Card Rejected");
            loaduser();
        }

        else if (e.CommandName == "reject_cheque")
        {
            objUser.changeKYCStatus("Cheque", "Reject", userid);
            Message.Show("Cancel Cheque/Passbook Rejected");
            loaduser();
        }

        else if (e.CommandName == "reject_aadhaar")
        {
            objUser.changeKYCStatus("Aadhaar", "Reject", userid);
            Message.Show("Aadhaar Card Rejected");
            loaduser();
        }

        else if (e.CommandName == "reject_GST")
        {
            objUser.changeKYCStatus("GST", "Reject", userid);
            Message.Show("GST Rejected");
            loaduser();
        }

    }

    protected void lnkedit_click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string userid = btn.CommandArgument.ToString();
        Response.Redirect("UploadKYC.aspx?ID=" + userid);

    }
}