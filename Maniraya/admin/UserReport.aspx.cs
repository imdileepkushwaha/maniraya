using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_UserReport : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsplan cPlan = new clsplan();

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
        objUser.UserId = txtuserid.Text;
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.CityId = ddcity.SelectedValue.ToString();
        objUser.AreaName= ddarea.SelectedValue.ToString();
        objUser.Pincode = txtPinCode.Text;

        objUser.StateId = ddstate.SelectedValue;
        objUser.plan_Id = ddlPackage.SelectedValue;
        objUser.SponserId = ddlSponsor.SelectedValue;
        objUser.PanCardNo = txtPanNumber.Text;

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
            objUser.ToDate = Message.GetIndianDate(txttodate.Text).AddDays(1);
        }
        else
        {
            objUser.ToDate = DateTime.MinValue;
        }
        objUser.Email = "";

        string noOfRows = "";
        if (ddlRecordFilter.SelectedItem.Text == "All")
            noOfRows = "";

       //else if (ddlRecordFilter.SelectedItem.Text == "5")
        //    noOfRows = "top 5";

        else if (ddlRecordFilter.SelectedItem.Text == "25")
            noOfRows = "top 25";

        else if (ddlRecordFilter.SelectedItem.Text == "50")
            noOfRows = "top 50";

        else if (ddlRecordFilter.SelectedItem.Text == "100")
            noOfRows = "top 100";

        else if (ddlRecordFilter.SelectedItem.Text == "500")
            noOfRows = "top 500";

        DataTable dt = new DataTable();
        dt = objUser.getUserReportPage(objUser, noOfRows);
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