using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_UserReport : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsplan cPlan = new clsplan();
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                //loadcountry();
                loadstate();
                loadcountryedit();

                bindPlans();
                bindSponsorList();

                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    protected void bindPlans()
    {
        DataTable dt = new DataTable();

        try
        {
            dt = cPlan.getPlanAll();

            if (dt.Rows.Count > 0)
            {
                ddlPackage.DataSource = dt;
                ddlPackage.DataTextField = "PlanName";
                ddlPackage.DataValueField = "Id";
                ddlPackage.DataBind();

                ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
        }
    }

    protected void bindSponsorList()
    {
        DataTable dt = new DataTable();

        try
        {
            dt = objUser.getUserList();

            if (dt.Rows.Count > 0)
            {
                ddlSponsor.DataSource = dt;
                ddlSponsor.DataTextField = "UserId";
                ddlSponsor.DataValueField = "UserId";
                ddlSponsor.DataBind();

                ddlSponsor.Items.Insert(0, new ListItem("Select Sponsor", "0"));
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
        }
    }

    //void loadcountry()
    //{
    //    ddcountry.Items.Clear();
    //    DataTable dt = new DataTable();
    //    dt = objState.getCountry();
    //    ddcountry.DataSource = dt;
    //    ddcountry.DataTextField = "CountryName";
    //    ddcountry.DataValueField = "CountryID";
    //    ddcountry.DataBind();
    //    ListItem li = new ListItem("Select Country", "0");
    //    ddcountry.Items.Insert(0, li);
    //}
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        //objState.CountryId = ddcountry.SelectedValue.ToString();
        objState.CountryId = "1";
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
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lbluser = (Label)GridView1.Rows[index].FindControl("lbluserid");

            Response.Redirect("UserEdit.aspx?userId=" + lbluser.Text);

            //objUser.UserId = lbluser.Text;
            //DataTable dt = new DataTable();
            //dt = objUser.getUserDetail(objUser);
            //if (dt.Rows.Count > 0)
            //{
            //    ViewState["UID"] = objUser.UserId;
            //    txtnameedit.Text = dt.Rows[0]["username"].ToString();
            //    txtmobileedit.Text = dt.Rows[0]["mobile"].ToString();
            //    txtemailedit.Text = dt.Rows[0]["email"].ToString();
            //    ddgenderedit.SelectedValue = dt.Rows[0]["gender"].ToString();
            //    txtaddressedit.Text = dt.Rows[0]["address"].ToString();
            //    ddcountryedit.SelectedValue = dt.Rows[0]["countryid"].ToString();
            //    loadstateedit();
            //    ddstateedit.SelectedValue = dt.Rows[0]["stateid"].ToString();
            //    loadcityedit();
            //    ddcityedit.SelectedValue = dt.Rows[0]["cityid"].ToString();
            //    //loadareaedit();
            //    ddareaedit.Text = dt.Rows[0]["areaname"].ToString();
            //    txtpincodeedit.Text = dt.Rows[0]["pincode"].ToString();
            //    txtdateofbirthedit.Text = Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString()).ToString("dd/MM/yyyy");
            //}
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        if (e.CommandName == "changeStatus")
        {
            try
            {
                objUser.UserId = e.CommandArgument.ToString();

                if (objUser.changeUserStatus(objUser) > 0)
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

        if (e.CommandName == "epin")
        {
            try
            {
                objUser.UserId = e.CommandArgument.ToString();

                if (objUser.changeUserEPinStatus(objUser) > 0)
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
        string userId = (txtuserid.Text ?? string.Empty).Trim();
        string userName = (txtname.Text ?? string.Empty).Trim();
        string mobile = (txtmobile.Text ?? string.Empty).Trim();
        string email = (txtemail.Text ?? string.Empty).Trim();
        string cityId = ddcity.SelectedValue;
        string pincode = (txtPinCode.Text ?? string.Empty).Trim();
        string stateId = ddstate.SelectedValue;
        string planId = ddlPackage.SelectedValue;
        string sponserId = ddlSponsor.SelectedValue;
        string panNumber = (txtPanNumber.Text ?? string.Empty).Trim();

        // Name / User ID / Mobile / PAN search should not be limited to today's date range.
        bool identitySearch =
            !string.IsNullOrWhiteSpace(userName) ||
            !string.IsNullOrWhiteSpace(userId) ||
            !string.IsNullOrWhiteSpace(mobile) ||
            !string.IsNullOrWhiteSpace(panNumber);

        DateTime fromDate = DateTime.MinValue;
        DateTime toDate = DateTime.MinValue;
        if (!identitySearch && !string.IsNullOrWhiteSpace(txtfromdate.Text))
        {
            fromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        if (!identitySearch && !string.IsNullOrWhiteSpace(txttodate.Text))
        {
            toDate = Message.GetIndianDate(txttodate.Text).AddDays(1);
        }

        string noOfRows = "";
        if (ddlRecordFilter.SelectedItem.Text == "All")
            noOfRows = "";
        else if (ddlRecordFilter.SelectedItem.Text == "25")
            noOfRows = "top 25";
        else if (ddlRecordFilter.SelectedItem.Text == "50")
            noOfRows = "top 50";
        else if (ddlRecordFilter.SelectedItem.Text == "100")
            noOfRows = "top 100";
        else if (ddlRecordFilter.SelectedItem.Text == "500")
            noOfRows = "top 500";

        DataTable dt = GetUserReportData(noOfRows, userId, userName, mobile, email, cityId, pincode, stateId, planId, sponserId, panNumber, fromDate, toDate);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    DataTable GetUserReportData(
        string noOfRows,
        string userId,
        string userName,
        string mobile,
        string email,
        string cityId,
        string pincode,
        string stateId,
        string planId,
        string sponserId,
        string panNumber,
        DateTime fromDate,
        DateTime toDate)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append(@"SELECT ").Append(noOfRows).Append(@"
    ud.status, ud.userid, ud.username, ud.Mobile, ud.Email, ud.Gender, ud.Address, cm.CityName, ud.MentionDate, ld.password,
    ISNULL(ud.balanceamount, 0) AS balanceamount, ISNULL(ud.utilityBalance, 0) AS utilityBalance, ld.status AS activeStatus,
    (CASE WHEN ud.SignUpImgStatus IS NOT NULL THEN ud.SignUpFormImage ELSE NULL END) SignUpFormImage,
    (CASE WHEN ud.SignUpImgStatus = 0 THEN 'Pending' WHEN ud.SignUpImgStatus = 1 THEN 'Approved' WHEN ud.SignUpImgStatus = 2 THEN 'Rejected' END) SignUpImgStatuss,
    (CASE WHEN ud.PanImgStatus IS NOT NULL THEN ud.PanImage ELSE NULL END) PanImage,
    (CASE WHEN ud.PanImgStatus = 0 THEN 'Pending' WHEN ud.PanImgStatus = 1 THEN 'Approved' WHEN ud.PanImgStatus = 2 THEN 'Rejected' END) PanImgStatuss,
    (CASE WHEN ud.ChequeImgStatus IS NOT NULL THEN ud.CancelCheque ELSE NULL END) CancelCheque,
    (CASE WHEN ud.ChequeImgStatus = 0 THEN 'Pending' WHEN ud.ChequeImgStatus = 1 THEN 'Approved' WHEN ud.ChequeImgStatus = 2 THEN 'Rejected' END) ChequeImgStatuss,
    (CASE WHEN ud.AadharImgStatus IS NOT NULL THEN ud.AadharImage ELSE NULL END) AadharImage,
    (CASE WHEN ud.AadharImgStatus IS NOT NULL THEN ud.AadharImageBack ELSE NULL END) AadharImageBack,
    (CASE WHEN ud.AadharImgStatus = 0 THEN 'Pending' WHEN ud.AadharImgStatus = 1 THEN 'Approved' WHEN ud.AadharImgStatus = 2 THEN 'Rejected' END) AadharImgStatuss,
    epin.planId, plm.PlanName AS packageName, ud.SponserId, ISNULL(ud1.userName, 'Company') sponserName, ud.PanNumber, sm.stateName, ud.Pincode, ud.epinGenerationStatus,
    CASE WHEN ISNULL(ud.GSTimage, '') = '' THEN 'img/default.png' ELSE '../ProductImage/' + ud.GSTimage END AS GSTimage,
    ud.gstnumber, ISNULL(ud.IsGSTDeductedOfUnverified, 0) AS IsGSTDeductedOfUnverifie,
    (CASE WHEN ud.IsGstApplicable = 0 THEN 'Pending' WHEN ud.IsGstApplicable = 1 THEN 'Approved' WHEN ud.IsGstApplicable = 2 THEN 'Rejected' END) IsGstApplicable
FROM userdetail ud WITH (NOLOCK)
LEFT JOIN citymaster cm WITH (NOLOCK) ON ud.Cityid = cm.CityId
LEFT JOIN statemaster sm WITH (NOLOCK) ON sm.stateId = cm.stateId
LEFT JOIN Logindetail ld WITH (NOLOCK) ON ud.userid = ld.username
LEFT JOIN EPinMaster epin WITH (NOLOCK) ON epin.UsedUserId = ud.userID
LEFT JOIN PlanMaster plm WITH (NOLOCK) ON plm.id = epin.planId
LEFT JOIN userdetail ud1 WITH (NOLOCK) ON ud.sponserId = ud1.userId
WHERE 1 = 1");

        if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
        {
            sql.Append(" AND ud.MentionDate > '").Append(fromDate.ToString("yyyy-MM-dd HH:mm:ss")).Append("'");
            sql.Append(" AND ud.MentionDate < '").Append(toDate.ToString("yyyy-MM-dd HH:mm:ss")).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(userName))
        {
            sql.Append(" AND ud.username LIKE '%").Append(SqlEscape(userName)).Append("%'");
        }
        if (!string.IsNullOrWhiteSpace(userId))
        {
            sql.Append(" AND LTRIM(RTRIM(ud.UserId)) = '").Append(SqlEscape(userId)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(mobile))
        {
            sql.Append(" AND LTRIM(RTRIM(ud.mobile)) = '").Append(SqlEscape(mobile)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            sql.Append(" AND ud.email LIKE '%").Append(SqlEscape(email)).Append("%'");
        }
        if (!string.IsNullOrWhiteSpace(cityId) && cityId != "0")
        {
            sql.Append(" AND ud.cityid = '").Append(SqlEscape(cityId)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(pincode))
        {
            sql.Append(" AND ud.Pincode = '").Append(SqlEscape(pincode)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(stateId) && stateId != "0")
        {
            sql.Append(" AND cm.stateId = '").Append(SqlEscape(stateId)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(planId) && planId != "0")
        {
            sql.Append(" AND epin.planId = '").Append(SqlEscape(planId)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(sponserId) && sponserId != "0")
        {
            sql.Append(" AND ud.SponserId = '").Append(SqlEscape(sponserId)).Append("'");
        }
        if (!string.IsNullOrWhiteSpace(panNumber))
        {
            sql.Append(" AND ud.PanNumber = '").Append(SqlEscape(panNumber)).Append("'");
        }

        sql.Append(" ORDER BY ud.MentionDate DESC");

        DataTable dt = new DataTable();
        try
        {
            ObjData.StartConnection();
            try
            {
                dt = ObjData.RunDataTable(sql.ToString());
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
            dt = new DataTable();
        }

        return dt ?? new DataTable();
    }

    static string SqlEscape(string value)
    {
        return (value ?? string.Empty).Replace("'", "''");
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
        string res = objUser.Update_UserProfile(objUser);
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
            string popupScript = "alert('unknown error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void btneditbank_click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string userid = btn.CommandArgument.ToString();
        Response.Redirect("userbankdetail.aspx?Id=" + userid);

    }

    
}