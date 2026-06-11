using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CalculateRepurchaseClosing : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
    clsClosing objCl = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtTodate.Attributes.Add("readonly", "readonly");
            TxtTodate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            
            if (Session["useradmin"] != null)
            {
                TxtFromdate.Text = objCl.GetClosingDate("REPURCHASE").ToString("dd/MMM/yyyy");
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
   

   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (TxtFromdate.Text != string.Empty && TxtTodate.Text != string.Empty)
        {
            if (Convert.ToDateTime(TxtFromdate.Text) <= Convert.ToDateTime(TxtTodate.Text))
            {
                CalculateweeklyClosing();
            }
            else
            {
                string popupScript = "alert('To date can not be larger than from date');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        else
        {
            string popupScript = "alert('Date can not be blank');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        
    }   
    public void CalculateweeklyClosing()
    {
        int h = objCl.CalculateClosingNew(TxtTodate.Text, "REPURCHASE");
        if (h == 1)
        {
            TxtFromdate.Text = objCl.GetClosingDate("REPURCHASE").ToString("dd/MMM/yyyy");
            string popupScript = "alert('Closing Calculate succfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (h == 2)
        {
            string popupScript = "alert('Closing Already generated');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
  

   
}