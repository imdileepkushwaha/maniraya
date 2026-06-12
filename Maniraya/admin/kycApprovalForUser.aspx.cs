using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_kycApprovalForUser : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();

    private DataTable KycData
    {
        get { return ViewState["KycData"] as DataTable; }
        set { ViewState["KycData"] = value; }
    }

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
        loaduser(true);
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        if (KycData != null)
        {
            BindGrid();
        }
    }

    void loaduser(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

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
        DataTable dt = objUser.getUserReport(objUser);
        KycData = dt;
        BindGrid();
    }

    void BindGrid()
    {
        DataTable dt = KycData;
        if (dt == null)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            return;
        }

        int pageSize = GetPageSize();
        if (pageSize <= 0 || ddlRecordFilter.SelectedItem.Text == "All")
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = dt.Rows.Count > 0 ? dt.Rows.Count : 10;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;

            if (dt.Rows.Count > 0)
            {
                int totalPages = (int)Math.Ceiling(dt.Rows.Count / (double)pageSize);
                if (GridView1.PageIndex >= totalPages)
                {
                    GridView1.PageIndex = Math.Max(0, totalPages - 1);
                }
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    int GetPageSize()
    {
        int pageSize;
        if (int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }

        return 10;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
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
            loaduser(false);
        }

        else if (e.CommandName == "approve_pan")
        {
            objUser.changeKYCStatus("PAN", "Approve", userid);
            Message.Show("PAN Card Approved");
            loaduser(false);
        }

        else if (e.CommandName == "approve_cheque")
        {
            objUser.changeKYCStatus("Cheque", "Approve", userid);
            Message.Show("Cancel Cheque/Passbook Approved");
            loaduser(false);
        }

        else if (e.CommandName == "approve_aadhaar")
        {
            objUser.changeKYCStatus("Aadhaar", "Approve", userid);
            Message.Show("Aadhaar Card Approved");
            loaduser(false);
        }

        else if (e.CommandName == "approve_GST")
        {
            objUser.changeKYCStatus("GST", "Approve", userid);
            Message.Show("GST Approved");
            loaduser(false);
        }


        else if (e.CommandName == "reject_signup")
        {
            objUser.changeKYCStatus("Sign Up", "Reject", userid);
            Message.Show("Sign Up Rejected");
            loaduser(false);
        }

        else if (e.CommandName == "reject_pan")
        {
            objUser.changeKYCStatus("PAN", "Reject", userid);
            Message.Show("PAN Card Rejected");
            loaduser(false);
        }

        else if (e.CommandName == "reject_cheque")
        {
            objUser.changeKYCStatus("Cheque", "Reject", userid);
            Message.Show("Cancel Cheque/Passbook Rejected");
            loaduser(false);
        }

        else if (e.CommandName == "reject_aadhaar")
        {
            objUser.changeKYCStatus("Aadhaar", "Reject", userid);
            Message.Show("Aadhaar Card Rejected");
            loaduser(false);
        }

        else if (e.CommandName == "reject_GST")
        {
            objUser.changeKYCStatus("GST", "Reject", userid);
            Message.Show("GST Rejected");
            loaduser(false);
        }

    }

    protected void lnkedit_click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string userid = btn.CommandArgument.ToString();
        Response.Redirect("UploadKYC.aspx?ID=" + userid);

    }
}