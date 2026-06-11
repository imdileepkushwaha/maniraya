using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Collections;

public partial class admin_DTHOperatorStatusEdit : System.Web.UI.Page
{
    public string LoginId = "";
    DataTable dtsave = new DataTable();
    public static int opid;
    clsdthsuscription objoperator = new clsdthsuscription();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            Bindgrid();
        }
       
    }
    void Bindgrid()
    {
        DataTable dt2 = new DataTable();
        dt2 = objoperator.getserviceproviderAll(objoperator);
        GridView1.DataSource = dt2;
        GridView1.DataBind();
    }
  
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        try
        {
            int c = 0;
            ArrayList list = new ArrayList();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
                if(chk.Checked == true)
                {
                    c = 1;
                    list.Add(chk.CssClass.ToString());
                }
            }

            if (c == 1)
            {
                if (ddlstatus.SelectedIndex > 0)
                {

                    int x = objoperator.updateserviceproviderstatus(list,Convert.ToInt16(ddlstatus.SelectedValue));
                    if (x == 1)
                    {
                        Bindgrid();
                        string popupScript = "alert('Status Changed Successfully');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                    {
                        string popupScript = "alert('Some Error Occurred');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

                    }
                }
                else
                {
                    string popupScript = "alert('Please Select Status');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
            }
            else
            {
                string popupScript = "alert('Please Select Any Row');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        catch (Exception ex)
        {
            string popupScript = "alert('Something went wrong');";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        finally
        {
            dt = null;

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = e.Row.Cells[3].Text;
            if (status == "0")
            {
                e.Row.Cells[3].Text = "DeActive";
            }
            else
            {
                e.Row.Cells[3].Text = "Active";
            }
        }
    }
}