using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using DataTier;
using System.Data.SqlClient;

public partial class FranchiseetypeEpinCommissionMaster : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsAccount objaccount = new clsAccount();
    clsDownload objD = new clsDownload();
    clsfranchisee objUser = new clsfranchisee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = getFranchiseetype();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            
            TextBox TxtTdswithpam = (TextBox)r.FindControl("TxtTdswithpam");

            Updatefranchiseecommission(lbllevel.Text,  TxtTdswithpam.Text);
        }
        string popupScript = "alert('Data Updated Successfully');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
           
    }
    public DataTable getFranchiseetype()
    {
        string str_query = "SELECT E.ID,F.TYpe,F.RequiredRMAP,P.PlanName,E.Commission FROM franchiseeTypeTb F JOIN FranchiseeEpinCommission E ON F.ID=E.franchiseeType JOIN planmaster P ON E.PlanID=P.Id order by P.ID";

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
    public string Updatefranchiseecommission(string Id, string profit)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "update FranchiseeEpinCommission set Commission='" + profit + "' where Id='" + Id + "'";
            ObjData.RunInsUpDelQueryTrans(s2, tr);
            res = "t";
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }
}