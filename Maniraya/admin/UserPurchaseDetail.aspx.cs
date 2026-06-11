using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using DataTier;

public partial class UserPurchaseDetail : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsvendor objV = new clsvendor();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {

               // TxtFranchiseeId.Text = Session["fuserid"].ToString();
               // TxtFranchiseeId.Enabled = false;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {
        objV.VendorId = string.Empty;
        if (txtfromdate.Text != "")
        {
            objV.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objV.Fromdate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objV.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objV.Todate = DateTime.MinValue;
        }
       
        objV.VendorId = TxtFranchiseeId.Text;
        objV.WithdrawlRequestStatus = ddstatus.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = getuserFranchiseePurchase(objV);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public DataTable getuserFranchiseePurchase(clsvendor objstate)
    {
        string str_query = " SELECT P.PurchaseID,P.FranchiseeID,V.username as FranchiseeName,P.userid,U.username,P.PurchaseAmount,U.Shippingaddress,CM.CityName,SM.StateName,U.ShippingAreaName,U.ShippingPincode, P.CGST,P.SGST,P.IGST,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate,P.orderNo FROM UserFranchiseePurchaseMaster P INNER JOIN FranchiseeDetail V ON P.FranchiseeID=V.userid  INNER JOIN userDetail U ON P.userid=U.userid left JOIN CityMaster CM ON CM.CityId=U.ShippingCityId left JOIN  StateMaster SM ON CM.StateId = SM.StateId where 1=1 ";

        if (objstate.Fromdate != DateTime.MinValue && objstate.Todate != DateTime.MinValue)
        {
            str_query += "  and cast(P.Entrydate as date)  >= cast('" + objstate.Fromdate + "' as date)   and cast(P.Entrydate as date)   <= cast('" + objstate.Todate + "' as date) ";
        }






        if (objstate.WithdrawlRequestStatus != "0")
        {
            str_query += "  and P.Cstatus = '" + objstate.WithdrawlRequestStatus + "' ";
        }

        if (objstate.VendorId != string.Empty)
        {
            str_query += " and P.FranchiseeID='" + objstate.VendorId + "'";
        }
        str_query += " and isnull(P.isdistributer,0)=0 order BY P.PurchaseId desc";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
            DataTable dt1 = new DataTable();
            dt1 = objV.getUserFranchiseePurchaseProductChild(customerId);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();
        }
    }
}