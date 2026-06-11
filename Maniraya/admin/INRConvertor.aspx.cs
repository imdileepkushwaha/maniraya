using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DownlineReport : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                //txtuserid.Text = Session["useradmin"].ToString();
                //txtuserid.Enabled = false;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }

            Strfill();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i = objuser.InsertINRDollor(txt_inr.Text, txt_dollar.Text);
            if (i > 0)
            {
                Message.Show("Record Updated!!!");
            }
            else
            {
                Message.Show("Updation Failed!!!");
            }
        }
        catch { }
    }

    protected void Strfill()
    {
        try
        {
            DataTable dattab = new DataTable();
            dattab = objuser.Find_INRDollor().Tables[0];
            txt_inr.Text = dattab.Rows[0]["INR"].ToString();
            txt_dollar.Text = dattab.Rows[0]["Dollar"].ToString();
        }
        catch { }
    }
}