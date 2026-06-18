using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Data.SqlClient;
using DataTier;
using System.IO;


public partial class user_Incomestatement : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                SiteContactHelper.BindIncomeStatementContact(litIncomeContact);
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
                txtFromDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                loaduseraddressdetail();
                loaduserincomedetail();
            }

        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    public DataTable getuseraddressdetailviaprocedure(clsUser objUser)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "sp_getuseraddressdetail";
            SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objUser.UserId), 
                   
                 
                  
                };
            Dt = ObjData.RunDataTableProcedure(s2, parameter);



        }
        catch (Exception ex)
        {

        }
        finally
        {
            ObjData.EndConnection();

        }
        return Dt;
    }
    public DataTable getuserincomedetailviaprocedure(clsUser objUser,string fromdate,string todate)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "Incomesummary_Statement";
            SqlParameter[] parameter = {              
                    new SqlParameter("@userid",objUser.UserId), 
                      new SqlParameter("@Fromdate",fromdate), 
                        new SqlParameter("@Todate",todate), 
                   
                 
                  
                };
            Dt = ObjData.RunDataTableProcedure(s2, parameter);



        }
        catch (Exception ex)
        {

        }
        finally
        {
            ObjData.EndConnection();

        }
        return Dt;
    }
    void loaduseraddressdetail()
    {
        DataTable dt = new DataTable();
        objuser.UserId = Session["userid"].ToString();
        dt = getuseraddressdetailviaprocedure(objuser);
        if (dt.Rows.Count > 0)
        {
                LblUserid.Text = dt.Rows[0]["userid"].ToString();
                Lblusername.Text = dt.Rows[0]["username"].ToString();
                Lblusernametwo.Text = dt.Rows[0]["username"].ToString();
                Lblbankname.Text = dt.Rows[0]["bankname"].ToString();
                Lbladdress.Text = dt.Rows[0]["address"].ToString();
                Lblarea.Text = dt.Rows[0]["areaname"].ToString();
                LblState.Text = dt.Rows[0]["statename"].ToString();
                LblCity.Text = dt.Rows[0]["cityname"].ToString();
                LblPincode.Text = dt.Rows[0]["pincode"].ToString();
                LblMobile.Text = dt.Rows[0]["Mobile"].ToString();
                LblPanno.Text = dt.Rows[0]["PanNumber"].ToString();
                Lblifsccode.Text = dt.Rows[0]["IFSCCode"].ToString();
                Lblaccountno.Text = dt.Rows[0]["AccountNo"].ToString();
           
        }
        else
        {


            Message.Show("Invalid Franchisee Id...!!!");
        }
    }
    void loaduserincomedetail()
    {
        DataTable dt = new DataTable();
        objuser.UserId = Session["userid"].ToString();
        LblFromdate.Text = txtFromDate.Text+"-to-"+txtToDate.Text;
    
        dt = getuserincomedetailviaprocedure(objuser,txtFromDate.Text,txtToDate.Text);
        if (dt.Rows.Count > 0)
        {
            Lblbinaryincome.Text = dt.Rows[0]["BinaryIncome"].ToString();
            LblDirectincome.Text = dt.Rows[0]["directincome"].ToString();
            LblBoosterincome.Text = dt.Rows[0]["Boosterincome"].ToString();
            LblLevelINcome.Text = dt.Rows[0]["Levelincome"].ToString();
            LblSelfincome.Text = dt.Rows[0]["selfIncome"].ToString();
            LblREpurchase.Text = dt.Rows[0]["RepurchaseIncome"].ToString();
            LblTotaldaily.Text = dt.Rows[0]["totaldailyincome"].ToString();
            LblTotalweekly.Text = dt.Rows[0]["totalweeklyincome"].ToString();
            LblTotalMonthly.Text = dt.Rows[0]["totalmonthlyincome"].ToString();
            LblTotalincome.Text = dt.Rows[0]["totalincome"].ToString();
          

        }
        else
        {


          //  Message.Show("Invalid Franchisee Id...!!!");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loaduseraddressdetail();
        loaduserincomedetail();
    }
}