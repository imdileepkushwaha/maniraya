using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;


public partial class admin_UserPermissiom : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
    clsplan objplan = new clsplan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                      
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loaddataplantype(string UserId)
    {
        DataTable dt = new DataTable();
        dt = objplan.getPlanwithoperatortypeAll(UserId);
        if (dt != null)
        {            
            GridView1.DataSource = dt;
            GridView1.DataBind();
            ViewState["UserId"] = txtuserid.Text;
        }
        else
        {
            Message.Show("this User Id is wrong !..");
            txtuserid.Text = string.Empty;
        }
        
    }
    void LoadPermission(string UserId)
    {
        DataTable dt = new DataTable();
        dt = objplan.getPlanGetUserPermission(UserId);
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("LblTypeid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            foreach (DataRow Dr in dt.Rows)
            {
                if (Dr["typeid"].ToString() == lbllevel.Text)
                {
                    ChStatus.Checked = true;
                    
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaddataplantype(txtuserid.Text);
        LoadPermission(txtuserid.Text);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
       
        if (GridView1.Rows.Count == 0)
        {
            string popupScript = "alert('enter search userid !....');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
          
            return;
        }
       
        if (ViewState["UserId"] == null)
        {
            string popupScript = "alert('enter search userid !....');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (ViewState["UserId"].ToString() != txtuserid.Text)
        {
            string popupScript = "alert('enter search userid !....');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        objplan.operatorPermission = getoperatorpermission();
         objplan.PlanName = ViewState["UserId"].ToString();
         string res = objplan.Insert_UserPermission(objplan);
          if (res == "t")
          {
              string popupScript = "alert('User permission Successfully');";
              ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
              GridView1.DataSource = null;
              GridView1.DataBind();
              txtuserid.Text = string.Empty;
          }
          else
          {
              string popupScript = "alert('Unknow error occurred');";
              ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
          }

    }
    private string getoperatorpermission()
    {
        int i = 0;
        string operatorpermission = "";
        foreach (GridViewRow r in GridView1.Rows)
        {
           
            Label lbllevel = (Label)r.FindControl("LblTypeid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            if (ChStatus.Checked == true)
            {
                if (i == 0)
                {
                    operatorpermission += lbllevel.Text;
                }
                else
                {
                    operatorpermission += "," + lbllevel.Text;
                }
            }
            else
            {

            }
            i++;
        }
        return operatorpermission;
    }
}