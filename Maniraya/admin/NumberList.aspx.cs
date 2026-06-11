using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NumberList : System.Web.UI.Page
{
    clsRecharge objrecharge = new clsRecharge();
    clsState objState = new clsState();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadoperator();             
                loadcircle();
                loadstate();
                loadcircleEDIT();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadoperator()
    {
        DdlOpertor.Items.Clear();
        DataTable dt = objrecharge.GetAllOpertorSelectedByTYpe1("1");
        foreach (DataRow Dr in dt.Rows)
        {
            Dr["Opid"] = Dr["Opid"].ToString().Trim();
        }
        DdlOpertor.DataSource = dt;
            DdlOpertor.DataValueField = "opId";
            DdlOpertor.DataTextField = "Operator";
            DdlOpertor.DataBind();
        
        ListItem li = new ListItem("Select Operator", "0");
        DdlOpertor.Items.Insert(0, li);

        ddloperatoredit.DataSource = dt;
        ddloperatoredit.DataValueField = "opId";
        ddloperatoredit.DataTextField = "Operator";
        ddloperatoredit.DataBind();
        ddloperatoredit.Items.Insert(0, li);
       
    }
  
    void loadcircle()
    {
        
        ddlstcircle.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCircle();
        ddlstcircle.DataSource = dt;
        ddlstcircle.DataTextField = "Circle";
        ddlstcircle.DataValueField = "IReffCircle";
        ddlstcircle.DataBind();
        ListItem li = new ListItem("Select Circle", "0");
        ddlstcircle.Items.Insert(0, li);

    }
    void loadcircleEDIT()
    {
        DataTable dt = new DataTable();
        dt = objState.getCircle();
        ddlstcircledit.Items.Clear();
        ddlstcircledit.DataSource = dt;
        ddlstcircledit.DataTextField = "Circle";
        ddlstcircledit.DataValueField = "IReffCircle";
        ddlstcircledit.DataBind();
        ListItem li = new ListItem("Select Circle", "0");
        ddlstcircledit.Items.Insert(0, li);



    }
    void loadstate()
    {
        DataTable dt = new DataTable();
        dt = objState.getNumberList();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string NUmber = TxtNumberListEdit.Text;
        string Operator = ddloperatoredit.SelectedItem.Text;
        string OPID = ddloperatoredit.SelectedValue;
        string corcle = ddlstcircledit.SelectedItem.Text;
        string circlecode = ddlstcircledit.SelectedValue.Trim();
        string id = lblstateid.Text;
        string res = objState.Insert_NumberList(NUmber, Operator, OPID, corcle, circlecode, id, "update");
        if (res == "t")
        {
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
           
            string popupScript = "alert('record Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            loadstate();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string NUmber = TxtNumber.Text;
        string Operator = DdlOpertor.SelectedItem.Text;
        string OPID = DdlOpertor.SelectedValue;
        string corcle = ddlstcircle.SelectedItem.Text;
        string circlecode = ddlstcircle.SelectedValue.Trim();
        string res = objState.Insert_NumberList(NUmber, Operator, OPID, corcle, circlecode,"0","insert");
        if (res == "t")
        {
            string popupScript = "alert('record Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            TxtNumber.Text = ""; DdlOpertor.SelectedValue = "0"; ddlstcircle.SelectedValue = "0";
            loadstate();
        }
        else
            if (res == "f")
            {
                string popupScript = "alert('record Already Exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lbllblNumber = (Label)GridView1.Rows[index].FindControl("lblNumber");
            Label lblcirclecode = (Label)GridView1.Rows[index].FindControl("Label1");
            Label lbloperatorcode = (Label)GridView1.Rows[index].FindControl("Label2");
            Label lblOperator = (Label)GridView1.Rows[index].FindControl("lblOperator");
            
            lblstateid.Text = lblid.Text;
            TxtNumberListEdit.Text= lbllblNumber.Text;
            ddlstcircledit.SelectedValue = lblcirclecode.Text.Trim();
            //SetDDLs(ddloperatoredit, lbloperatorcode.Text);
            ////if(ddloperatoredit.Items.FindByValue(string) == lbloperatorcode.Text)
            ////{
            ////          ddloperatoredit.Items.FindByValue(string).Selected = true;
            ////}
            //ddloperatoredit.SelectedValue = lbloperatorcode.Text.Trim();
           // ddloperatoredit.Items.FindByValue(lblOperator.Text).Selected = true;
            try
            {
                string dd = lbloperatorcode.Text.Trim();
                ddloperatoredit.SelectedValue = dd;
            }
            catch
            {

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    private void SetDDLs(DropDownList d, string val)
    {
        ListItem li;
        for (int i = 0; i < d.Items.Count; i++)
        {
            li = d.Items[i];
            if (li.Value == val)
            {
                d.SelectedIndex = i;
                break;
            }
        }
    }
}