using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class admin_CoreCommitteeClosingReport : System.Web.UI.Page
{
    clsClosing objCL = new clsClosing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
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
        DataTable Dt = objCL.getCoreCommitteeClosingReport(objCL);
        gvusers.DataSource = Dt;
        gvusers.DataBind();
    }
}