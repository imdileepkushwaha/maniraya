using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using DataTier;

public partial class TopupDetail : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            FillBankDetails();
        }
    }
    public void FillBankDetails()
    {
        DataTable dt = new DataTable();
        dt = getBankAccountList();
        grdBank.DataSource = dt;
        grdBank.DataBind();
    }
    public DataTable getBankAccountList()
    {
        string str_query = "SELECT U.Userid,U.entrydate,U.PlanAMount FROM UserTOPUPtb U JOIN planmaster P ON U.planid=P.Id WHERE U.Userid='" + Session["userid"].ToString() + "' order by U.id desc";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
}