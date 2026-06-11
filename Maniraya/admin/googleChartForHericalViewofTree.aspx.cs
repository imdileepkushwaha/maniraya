using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Web.Services;

public partial class admin_googleChartForHericalViewofTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
            }
        }
    }
    [WebMethod]
    public static List<object> GetChartData(string UserId)
    {

        List<object> chartData = new List<object>();
        clsUser objU = new clsUser();
        // objU.UserId = UserId;
        objU.UserId = UserId;
        DataTable Dt = objU.getUserReportfortree(objU);
        if (Dt.Rows.Count > 0)
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                chartData.Add(new object[]
                    {
                         Dt.Rows[i]["EmployeeId"].ToString(), Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["Designation"].ToString() , Dt.Rows[i]["ReportingManager"].ToString()
                    });

            }
        }



        return chartData;
    }      
}