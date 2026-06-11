using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_APISMS : System.Web.UI.Page
{
    public string LoginId = "";
    public string UserId = "";
    clsAPI objapi = new clsAPI();
    public string str = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            fillgrid();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                Session["GClass"] = "expand";
                Session["GridView1"] = GridView1.ClientID;
                //Attribute to hide column in Phone.
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

    }

    public string Checknull(string str)
    {
        if (str == "")
            return "Admin";
        else
            return str;
    }
    void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = objapi.GetSmsApi(LoginId);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            HiddenField1.Value = ((Label)gvrow.FindControl("LblId")).Text;
            LinkButton LinkStatus = (LinkButton)gvrow.FindControl("LnkEnable");
            string str = HiddenField1.Value;
            DataTable dt1 = new DataTable();
            dt1 = objapi.GetSmsApi_ById(Convert.ToInt32(str));
            string boolval = dt1.Rows[0]["Status"].ToString();
            string defaultt = dt1.Rows[0]["DefaultType"].ToString();
            if (boolval == "True")
            {
               LinkStatus.CssClass = "btn btn-danger btn-round tooltips fa fa-times";
                LinkStatus.ToolTip = "API Deactive";
            }
            else
            {
               LinkStatus.CssClass = "btn btn-success btn-round tooltips fa fa-check-square-o";
                LinkStatus.ToolTip = "API Active";
            }
            if (defaultt == "True" || defaultt == "1")
            {
                LinkStatus.Visible = false;
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
        Label lblid = (Label)row.FindControl("LblId");
        LinkButton LinkStatus = (LinkButton)row.FindControl("LnkEnable");
        hdn_Id1.Value = HiddenField1.Value;
        string str = lblid.Text;

        if (e.CommandName == "EditUser")
        {
            lblapiId.Text = str;
            fillform();
            ButSubmit.Text = "Update";
        }
        if (e.CommandName == "EnableUser")
        {
            DataTable dt1 = new DataTable();
            dt1 = objapi.GetSmsApi_ById(Convert.ToInt32(str));
            string boolval = dt1.Rows[0]["Status"].ToString();
            if (boolval == "True")
            {
                objapi.UpdateStatus("0", LoginId, str);
                string popupScript = "alert('API Deactivated Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                if (LoginId != "1")
                {
                    UserId = LoginId;
                    DataTable dt2 = new DataTable();
                    dt2 = objapi.GetSmsApi_true(LoginId);
                    if (dt2.Rows.Count > 0)
                    {
                        //ImageError.Src = "../lib/img/loading/error.gif";
                        // P1.InnerText = "Active API Already Exists for this User, You want to Replace this as Active API ???????....!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                    }
                    else
                    {
                        objapi.UpdateStatus("1", LoginId, str);
                        string popupScript = "alert('API Activated Successfully');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                }
                else
                {
                    UserId = dt1.Rows[0]["UserId"].ToString();
                    DataTable dt2 = new DataTable();
                    dt2 = objapi.GetSmsApi_true(dt1.Rows[0]["UserId"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        //ImageError.Src = "../lib/img/loading/error.gif";
                        // P1.InnerText = "Active API Already Exists for this User, You want to Replace this as Active API ???????....!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                    }
                    else
                    {
                        objapi.UpdateStatus("1", LoginId, str);
                        string popupScript = "alert('API Activated Successfully');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                }
            }

        }
        if (e.CommandName == "SubOk")
        {
            objapi.UpdateStatus_final("1", LoginId, str, UserId);
            string popupScript = "alert('API Activated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        fillgrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:100px");
        }
    }
    
    void clear()
    {
        chk_Default.Checked = false;
        TxtName.Text = TxtHomePageUrl.Text = "";
        lblapiId.Text = "0";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clear();
        ButSubmit.Text = "Submit";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void ButSubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            if (TxtHomePageUrl.Text != "" && TxtName.Text != "")
            {
                string Userid = "admin";
                string id = "admin";

                int DefaultType = 0;
                if (chk_Default.Checked == true)
                    DefaultType = 1;
                if (chk_Default.Checked == false)
                    DefaultType = 0;
                if (lblapiId.Text != "0")
                {
                    string result = objapi.UpdateSmsApi(TxtName.Text, TxtHomePageUrl.Text, LoginId, lblapiId.Text, Userid, DefaultType);
                    if (result == "1")
                    {
                        clear();
                        string popupScript = "alert('Data Updated Successfully');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                    {
                        string popupScript = "alert( '" + result + "');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                }
                else if (lblapiId.Text == "0")
                {
                    string result = objapi.InsertSmsApi(TxtName.Text, TxtHomePageUrl.Text, LoginId, Userid, DefaultType);
                    if (result == "1")
                    {
                        clear();
                        string popupScript = "alert('Data Saved Successfully');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                    {
                        string popupScript = "alert('" + result + "');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                }

                else
                {
                    string popupScript = "alert('Enter Valid Mobile No');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
            }
            else
            {
                string popupScript = "alert('Enter All Values');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        catch (Exception ex)
        {

        }
        fillgrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Closepopup();", true);
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    void fillform()
    {
        DataTable dt = new DataTable();
        int i = Convert.ToInt32(lblapiId.Text);
        dt = objapi.GetSmsApi_ById(i);
        TxtName.Text = dt.Rows[0]["Name"].ToString();
        TxtHomePageUrl.Text = dt.Rows[0]["Url"].ToString();
        if (dt.Rows[0]["DefaultType"].ToString() == "0")
            chk_Default.Checked = false;
        if (dt.Rows[0]["DefaultType"].ToString() == "1")
            chk_Default.Checked = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

    }
}