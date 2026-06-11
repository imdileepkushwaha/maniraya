using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class user_CoreCommitteeClosingReport : System.Web.UI.Page
{
    clsClosing objCL = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            loaduser();          
        }
    }

    void loaduser()
    {
        objCL.paystatus = "all";
        objCL.GenerateUserId = Session["userid"].ToString();
        DataTable Dt = objCL.getCoreCommitteeClosingReport(objCL);
        gvusers.DataSource = Dt;
        gvusers.DataBind();
    }
}