using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_PurchaseReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
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

        if (txtfromdate.Text != "")
        {
            objP.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objP.Fromdate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objP.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objP.Todate = DateTime.MinValue;
        }
        objP.purchasestatus = ddstatus.SelectedValue.ToString();
        objP.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = getPurchaseProduct(objP);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    Data ObjData = new Data();
    public DataTable getPurchaseProduct(clsProduct objstate)
    {
        string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.TotalAmount,PR.PurchaseBy AS UserID,U.UserName FROM PurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID INNER JOIN Userdetail U ON PR.PurchaseBy=U.UserId where 1=1 ";



        if (objstate.Fromdate != DateTime.MinValue && objstate.Todate != DateTime.MinValue)
        {
            str_query += "  and cast(PR.PurchaseDate as date)  >= cast('" + objstate.Fromdate + "' as date)   and cast(PR.PurchaseDate as date)   <= cast('" + objstate.Todate + "' as date) ";
        }


        if (objstate.purchasestatus != "0")
        {
            str_query += "  and PR.Pstatus = '" + objstate.purchasestatus + "' ";
        }




        if (objstate.UserId != string.Empty)
        {
            str_query += " and PR.PurchaseBy='" + objstate.UserId + "'";
        }
        str_query += " order by PR.PurchaseID";
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
    protected void BtnSub_Click(object sender, EventArgs e)
    {
        Button btndetails = sender as Button;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        string lblPurchaseID = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("lblPurchaseID")).Text;     
        string lbluserid = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("lbluserid")).Text;

       
         Response.Redirect("MyInvoice.aspx?PurchaseId=" + lblPurchaseID + "&UserId=" + lbluserid);
       
    }
}