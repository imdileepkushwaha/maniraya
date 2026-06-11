using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using DataTier;

public partial class FranchiseeJoiningPurchaseDetail : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsvendor objV = new clsvendor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["fuserid"] != null)
            {

                TxtFranchiseeId.Text = Session["fuserid"].ToString();
                TxtFranchiseeId.Enabled = false;

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

        DataTable dt = new DataTable();
        dt = getFranchiseePurchaseNew(objV);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public DataTable getFranchiseePurchaseNew(clsvendor objstate)
    {
        string str_query = "SELECT P.PurchaseID,P.FranchiseeID,V.username,P.PurchaseAmount,P.CGST,P.SGST,P.IGST,P.TotalAmount,Convert(Char,P.PurchaseDate,103) as PurchaseDate,P.orderNo,P.Cash,P.restamount,P.paymentmode,P.chequeNo,case when P.Status=0 then 'Pending' when P.Status=1 then 'Approved' else 'Reject' end as Status1,P.Epinno  FROM FranchiseeJoiningPurchaseMaster P INNER JOIN FranchiseeDetail V ON P.FranchiseeID=V.userid where 1=1 ";

        if (objstate.Fromdate != DateTime.MinValue)
        {
            str_query += " and P.PurchaseDate>='" + objstate.Fromdate + "'";
        }
        if (objstate.Todate != DateTime.MinValue)
        {
            str_query += " and P.PurchaseDate<='" + objstate.Todate + "'";
        }
        if (objstate.VendorId != string.Empty)
        {
            str_query += " and P.FranchiseeID='" + objstate.VendorId + "'";
        }
        str_query += " and isnull(P.Isdistributer,0)=0 order BY P.PurchaseId desc";
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
    public DataTable getFranchiseePurchaseProductChildNew(string ID)
    {
        string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.MRP,PR.TotalAmount,PR.ProductID,PR.BV,PR.DP,PR.CGST,PR.SGST,PR.IGST,PR.PurchaseAmount,PR.totalBV,PR.totalDP FROM FranchiseeJoiningProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID  where 1=1 ";


        if (ID != string.Empty)
        {
            str_query += " and PR.PurchaseID='" + ID + "'";
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
            DataTable dt1 = new DataTable();
            dt1 = getFranchiseePurchaseProductChildNew(customerId);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();

            Label lblstatus = (Label)e.Row.FindControl("Lblstatus");
            HyperLink HyperLink1 = (HyperLink)e.Row.FindControl("HyperLink1");
            if (lblstatus.Text == "Approved")
            {
                HyperLink1.Visible = true;
            }
            else
            {
                HyperLink1.Visible = false;
            }
            //LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
            //LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");
            //if (lblstatus.Text.ToLower() == "pending")
            //{
            //    lblstatus.Text = "Pending";
            //    lblstatus.CssClass = "label label-warning";
            //    btnApprove.Visible = true;
            //    btnReject.Visible = true;
            //}
            //else
            //if (lblstatus.Text.ToLower() == "approved")
            //{
            //    lblstatus.Text = "Approved";
            //    lblstatus.CssClass = "label label-success";
            //    btnApprove.Visible = false;
            //    btnReject.Visible = false;
            //}
            //else
            //if (lblstatus.Text.ToLower() == "reject")
            //{
            //    lblstatus.Text = "Rejected";
            //    lblstatus.CssClass = "label label-danger";
            //    btnApprove.Visible = false;
            //    btnReject.Visible = false;
            //}
        }
    }
    protected void btnApprove_click(object sender, EventArgs e)
    {
        //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //Label lblgalleryid = (Label)gvRow.FindControl("lbluserid123");
        //objV.PurchaseID = lblgalleryid.Text;
        //objV.Purchasetype = "1";
        //DataTable dT = objV.Approve_PurchaseRequestNew(objV);
        //if (dT != null && dT.Rows.Count > 0)
        //{
        //    if (dT.Rows[0][0].ToString() == "1")
        //    {
        //        string popupScript = "alert('" + dT.Rows[0][1].ToString() + "');";
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

        //    }
        //    else
        //    {
        //        string popupScript = "alert('" + dT.Rows[0][1].ToString() + "');";
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    }
        //}
        //else
        //{

        //    string popupScript = "alert('error occured');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}

    }
    protected void btnReject_click(object sender, EventArgs e)
    {
        //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //Label lblgalleryid = (Label)gvRow.FindControl("lbluserid123");
        //objV.PurchaseID = lblgalleryid.Text;
        //objV.Purchasetype = "2";
        //DataTable dT = objV.Approve_PurchaseRequestNew(objV);
        //if (dT != null && dT.Rows.Count > 0)
        //{
        //    if (dT.Rows[0][0].ToString() == "1")
        //    {
        //        string popupScript = "alert('" + dT.Rows[0][1].ToString() + "');";
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

        //    }
        //    else
        //    {
        //        string popupScript = "alert('" + dT.Rows[0][1].ToString() + "');";
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //    }
        //}
        //else
        //{

        //    string popupScript = "alert('error occured');";
        //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        //}
    }
}