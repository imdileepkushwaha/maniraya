using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_BinaryReport : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            txtuserid.Text = Session["userid"].ToString();
           // txtuserid.Enabled = false;
            f1.Src = "../Treeviewdetail.aspx?SuperId=" + txtuserid.Text + "";
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
           // objuser.UserId = Session["userid"].ToString();
           // DataTable Dt = objuser.getUserDownlineChkNew(objuser, txtuserid.Text);
           // if (Dt.Rows.Count > 0)
           // {
                //ltiframe.Text = @"<iframe src=""test.aspx?SuperId=" + txtuserid.Text + @""" style=""width:100%;height:700px;""  />";
                f1.Src = "../Treeviewdetail.aspx?SuperId=" + txtuserid.Text + "";
          //  }
        }
        else
        {
            Message.Show("Enter User Id...!!!!");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}