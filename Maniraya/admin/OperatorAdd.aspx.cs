using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_OperatorAdd : System.Web.UI.Page
{
    clsOperator objoperator = new clsOperator();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loaddata();
                fillDdl();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    public void fillDdl()
    {
        DataTable dt = objoperator.GetOperatorCodeType();
        DdlAddType.DataSource = dt;
        DdlAddType.DataTextField = "Type";
        DdlAddType.DataValueField = "TypeId";
        DdlAddType.DataBind();
        DdlAddType.Items.Insert(0, new ListItem("Select Operator Type", "0"));
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objoperator.GetAllOperator();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
    protected void ButSubmit_Click(object sender, EventArgs e)
    {
        string result = "";
        if (Convert.ToInt32(LblEditId.Text) == 0)
        {
            result = objoperator.InsertOrEditOperator("-1", TxtOperator.Text, TxtAddOPID.Text, DdlAddType.SelectedValue, TxtAddLength.Text, TxtAddMinimum.Text, TxtAddMaximum.Text, TxtAddDisplayName.Text, TxtAddDisplayNote.Text, TxtAccountDisplay.Text, TxtLocationDisplay.Text, Session["useradmin"].ToString(), txt_Startswith.Text, txt_ReasonDisabled.Text,DDLstatus.SelectedValue);
        }
        else
        {
            result = objoperator.InsertOrEditOperator(LblEditId.Text, TxtOperator.Text, TxtAddOPID.Text, DdlAddType.SelectedValue, TxtAddLength.Text, TxtAddMinimum.Text, TxtAddMaximum.Text, TxtAddDisplayName.Text, TxtAddDisplayNote.Text, TxtAccountDisplay.Text, TxtLocationDisplay.Text, Session["useradmin"].ToString(), txt_Startswith.Text, txt_ReasonDisabled.Text,DDLstatus.SelectedValue);
        }
        if (result == "insered")
        {
            string popupScript = "alert('Operator Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            //diverror.Visible = true;
            //LblErrorLable.InnerText = "";
        }
        else if (result == "updated")
        {
            string popupScript = "alert('Operator Updated Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            //diverror.Visible = true;
            //LblErrorLable.InnerText = "";
        }
        else {
            string popupScript = "alert('" + result + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        LblEditId.Text = "0";
        ButSubmit.Text = "Save";

        loaddata();
        TxtOperator.Text = TxtAddOPID.Text = TxtAddLength.Text = TxtAddMinimum.Text = TxtAddMaximum.Text = TxtAddDisplayName.Text = TxtAddDisplayNote.Text = TxtAccountDisplay.Text = TxtLocationDisplay.Text = txt_Startswith.Text = txt_ReasonDisabled.Text = "";
        DdlAddType.SelectedValue = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Closepopup();", true);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        TxtOperator.Text = TxtAddOPID.Text = TxtAddLength.Text = TxtAddMinimum.Text = TxtAddMaximum.Text = TxtAddDisplayName.Text = TxtAddDisplayNote.Text = TxtAccountDisplay.Text = TxtLocationDisplay.Text = txt_Startswith.Text = txt_ReasonDisabled.Text = "";
        DdlAddType.SelectedValue = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditUser")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            LblEditId.Text = ((Label)row.FindControl("LblId")).Text;
            DataTable dt = objoperator.GetSelectedOperator(LblEditId.Text);
            if (dt.Rows.Count > 0)
            {
                LblEditId.Text = dt.Rows[0]["id"].ToString();
                TxtOperator.Text = dt.Rows[0]["Operator"].ToString();
                TxtAddOPID.Text = dt.Rows[0]["OPID"].ToString();
                TxtAddLength.Text = dt.Rows[0]["Length"].ToString();
                TxtAddMinimum.Text = dt.Rows[0]["Minimum"].ToString();
                TxtAddMaximum.Text = dt.Rows[0]["Maximum"].ToString();
                TxtAddDisplayName.Text = dt.Rows[0]["DisplayName"].ToString();
                TxtAddDisplayNote.Text = dt.Rows[0]["DisplayNote"].ToString();
                TxtAccountDisplay.Text = dt.Rows[0]["AccountDisplay"].ToString();
                TxtLocationDisplay.Text = dt.Rows[0]["LocationDisplay"].ToString();
                txt_Startswith.Text = dt.Rows[0]["StartsWith"].ToString();
                txt_ReasonDisabled.Text = dt.Rows[0]["DisabledReasion"].ToString();
                DdlAddType.SelectedValue = dt.Rows[0]["type"].ToString();
                DDLstatus.SelectedValue = dt.Rows[0]["showstatus"].ToString();
                ButSubmit.Text = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

            }
        }
    }
}