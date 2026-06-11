using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_EmailSetting : System.Web.UI.Page
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
                GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";
                Session["GClass"] = "expand";
                Session["GridView1"] = GridView1.ClientID;
                //Attribute to hide column in Phone.

                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";
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
        dt = objsetting.GetEmailSettings();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditUser")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            HiddenField1.Value = ((Label)row.FindControl("LblId")).Text;
            LblEditId.Text = HiddenField1.Value;
            fillform();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        if (e.CommandName == "Delete1")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            HiddenField1.Value = ((Label)row.FindControl("LblId")).Text;
            int str = Convert.ToInt32(HiddenField1.Value);
            objsetting.DeleteEmailSetting_ById(str);
            fillgrid();
        }
    }
    void fillform()
    {
        if (LblEditId.Text != "0")
        {
            ButSubmit.Text = "Update";
        }
        else if (LblEditId.Text == "0")
        {
            ButSubmit.Text = "Submit";
        }
        DataTable dt = new DataTable();
        int i = Convert.ToInt32(LblEditId.Text);
        dt = objsetting.GetEmailSetting_ById(i);
        txt_EmailFrom.Text = dt.Rows[0]["FromEmail"].ToString();
        Txt_EmailUserId.Text = dt.Rows[0]["EmailUserId"].ToString();
        txt_HostName.Text = dt.Rows[0]["HostName"].ToString();
        txt_Password.Text = dt.Rows[0]["Password"].ToString();
        txt_Port.Text = dt.Rows[0]["Port"].ToString();
    }
    public void clear()
    {
        LblEditId.Text = "0";
        txt_EmailFrom.Text = "";
        Txt_EmailUserId.Text = "";
        txt_HostName.Text = "";
        txt_Password.Text = "";
        txt_Port.Text = "";
    }
    protected void ButSubmit_Click(object sender, EventArgs e)
    {
        if (txt_EmailFrom.Text != "" && Txt_EmailUserId.Text != "" && txt_HostName.Text != "" && txt_Password.Text != "" && txt_Port.Text != "")
        {
            objsetting.Update_EmailSettings(txt_EmailFrom.Text, txt_Password.Text, txt_HostName.Text, Convert.ToInt32(txt_Port.Text), Txt_EmailUserId.Text, LblEditId.Text);
            clear();

            string popupScript = "alert('Data Updated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Closepopup();", true);

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
}