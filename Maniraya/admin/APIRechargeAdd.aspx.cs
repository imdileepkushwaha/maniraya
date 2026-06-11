using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_APIRechargeAdd : System.Web.UI.Page
{
    public string LoginId = "";
    public string str = "";

    clsRechargeAPI objrecharge = new clsRechargeAPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                if (Request.QueryString["Id"] != "0")
                {
                    str = Request.QueryString["Id"].ToString();
                    fillform();
                    ButSubmit.Text = "Update";
                }
                else
                {
                    ButSubmit.Text = "Submit";
                }
            }
            else
            {
                Response.Redirect("APIRecharge.aspx");
            }
        }
    }
    void fillform()
    {
        DataTable dt = new DataTable();
        int i = Convert.ToInt32(str);
        dt = objrecharge.GetRechargeApi_ById(i);
        if (dt.Rows.Count > 0)
        {
            TxtName.Text = dt.Rows[0]["Name"].ToString();
            TxtHomePageUrl.Text = dt.Rows[0]["Url"].ToString();
            TxtOpId.Text = dt.Rows[0]["LiveId"].ToString();
            TxtStatusName.Text = dt.Rows[0]["StatusName"].ToString();
            TxtStatusValue.Text = dt.Rows[0]["StatusValue"].ToString();
            TxtVenId.Text = dt.Rows[0]["VenderId"].ToString();
            ddlType.SelectedValue = dt.Rows[0]["Type"].ToString();
            txt_statusUrl.Text = dt.Rows[0]["StatusCheckUrl"].ToString();
            txt_balUrl.Text = dt.Rows[0]["BalanceUrl"].ToString();
            txt_Default.Text = dt.Rows[0]["DefaultUrl"].ToString();
            txt_Error.Text = dt.Rows[0]["Errors"].ToString();
            txtoperatortype.Text = dt.Rows[0]["operatortype"].ToString();
            DDLstatus.SelectedValue = dt.Rows[0]["status"].ToString();
        }
    }
    public void clear()
    {
        TxtHomePageUrl.Text = "";
        TxtName.Text = "";
        TxtOpId.Text = "";
        TxtVenId.Text = "";
        TxtStatusValue.Text = "";
        TxtStatusName.Text = "";
        txt_balUrl.Text = "";
        txt_statusUrl.Text = "";
        txt_Default.Text = "";
        txt_Error.Text = "";
        ddlType.SelectedIndex = 0;
    }
    protected void ButSubmit_Click(object sender, EventArgs e)
    {
        if (TxtHomePageUrl.Text != "" && TxtName.Text != "" && ddlType.SelectedIndex != 0)
        {
            try
            {
                str = Request.QueryString["Id"].ToString();
            }
            catch { }
            if (str != "0")
            {
                objrecharge.UpdateRechargeApi(TxtName.Text, TxtHomePageUrl.Text, LoginId, str, TxtStatusName.Text, TxtStatusValue.Text, TxtOpId.Text, TxtVenId.Text, ddlType.SelectedValue, txt_statusUrl.Text, txt_balUrl.Text, txt_Default.Text, txt_Error.Text, txtoperatortype.Text,DDLstatus.SelectedValue);
              
                string popupScript = "alert('Data Updated Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else if (str == "0")
            {
                objrecharge.InsertRechargeApi(TxtName.Text, TxtHomePageUrl.Text, LoginId, TxtStatusName.Text, TxtStatusValue.Text, TxtOpId.Text, TxtVenId.Text, ddlType.SelectedValue, txt_statusUrl.Text, txt_balUrl.Text, txt_Default.Text, txt_Error.Text, txtoperatortype.Text,DDLstatus.SelectedValue);
                clear();
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Data Saved Successfully');", true);
                string popupScript = "alert('Data Saved Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            string popupScript = "alert('Enter or select required(*) fields');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("APIRecharge.aspx");
    }
}