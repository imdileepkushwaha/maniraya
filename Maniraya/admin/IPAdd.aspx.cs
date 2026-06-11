using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;

public partial class admin_IPAdd : System.Web.UI.Page
{
    clsSetting objsetting = new clsSetting();
    public string LoginId = "";
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
                GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";
                Session["GClass"] = "expand";
                Session["GridView1"] = GridView1.ClientID;
                //Attribute to hide column in Phone.
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
    public string chknull(string str)
    {
        if (str == "")
            return "Admin";
        else
            return str;
    }
    void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = objsetting.GetIP();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            HiddenField1.Value = ((Label)gvrow.FindControl("LblId")).Text;
            LinkButton LinkStatus = (LinkButton)gvrow.FindControl("LnkEnable");
            string str = HiddenField1.Value;
            DataTable dt1 = new DataTable();
            dt1 = objsetting.GetIP_ById(Convert.ToInt32(str));
            string boolval = dt1.Rows[0]["Status"].ToString();
            if (boolval == "True")
            {
                LinkStatus.CssClass = "btn btn-danger btn-round tooltips fa fa-times";
                LinkStatus.ToolTip = "User Deactive";
            }
            else
            {
                LinkStatus.CssClass = "btn btn-success btn-round tooltips fa fa-check-square-o";
                LinkStatus.ToolTip = "User Active";
            }
        }
    }
    void fillform()
    {
        DataTable dt = new DataTable();
        int i = Convert.ToInt32(LblEditId.Text);
        dt = objsetting.GetIP_ById(i);
        txt_IP.Text = dt.Rows[0]["IP"].ToString();
        if (dt.Rows[0]["UserId"].ToString() == "1")
            txt_UserId.Text = "1";
        else
            txt_UserId.Text = dt.Rows[0]["MobileNo"].ToString();
        ddl_Type.SelectedValue = dt.Rows[0]["IPType"].ToString();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditUser")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label LblId = (Label)row.FindControl("LblId");
            string str = LblId.Text;
            LblEditId.Text = str;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            if (LblEditId.Text != "0")
            {
                fillform();
                ButSubmit.Text = "Update";
            }
        }
        if (e.CommandName == "EnableUser")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            HiddenField1.Value = ((Label)row.FindControl("LblId")).Text;
            LinkButton LinkStatus = (LinkButton)row.FindControl("LnkEnable");
            string str = HiddenField1.Value;
            DataTable dt1 = new DataTable();
            dt1 = objsetting.GetIP_ById(Convert.ToInt32(str));
            string boolval = dt1.Rows[0]["Status"].ToString();
            if (boolval == "True")
            {
                objsetting.UpdateStatus("0", LoginId, str);
            }
            else
            {
                objsetting.UpdateStatus("1", LoginId, str);
            }
            fillgrid();
        }
        if (e.CommandName == "delete1")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            objsetting.Delete_ById(((Label)row.FindControl("LblId")).Text);
            fillgrid();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txt_UserId.Text = txt_IP.Text = "";
        ddl_Type.SelectedValue = "0";
        LblEditId.Text = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }

    protected void ButSubmit_Click(object sender, EventArgs e)
    {
        if (LblEditId.Text != "0")
        {
            objsetting.Update_IP(txt_IP.Text, ddl_Type.SelectedValue, "admin", (string)Session["useradmin"], Convert.ToInt32(LblEditId.Text));
            clear();

            string popupScript = "alert('Data Updated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (LblEditId.Text == "0")
        {
            objsetting.Insert_IP(txt_IP.Text, ddl_Type.SelectedValue, "admin",(string)Session["useradmin"]);
            clear();
            string popupScript = "alert('Data Saved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Closepopup();", true);
    }
    protected void ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Type.SelectedValue == "1")
        {
            txt_UserId.ReadOnly = true;
            txt_UserId.Text = "1";
        }
        if (ddl_Type.SelectedValue == "2")
        {
            txt_UserId.ReadOnly = false;
            txt_UserId.Text = "";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }

    void clear()
    {
        txt_IP.Text = "";
        txt_UserId.Text = "";
        ddl_Type.SelectedValue = "0";
        LblEditId.Text = "0";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Closepopup();", true);
    }
}