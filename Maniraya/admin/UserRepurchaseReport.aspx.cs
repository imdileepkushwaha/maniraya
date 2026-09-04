using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using DataTier;
using System.Data.SqlClient;

public partial class Franchisee_UserRepurchaseReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsUser objUser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (string.Equals(e.CommandName, "photolarge", StringComparison.OrdinalIgnoreCase))
        {
            int photoIndex = Convert.ToInt32(e.CommandArgument.ToString());
            Label LblImage = (Label)GridView1.Rows[photoIndex].FindControl("LblImage");
            string imageUrl = (LblImage != null && !string.IsNullOrWhiteSpace(LblImage.Text))
                ? LblImage.Text.Trim()
                : "../ProductImage/images.png";
            string script = "var img=document.getElementById('imgRepurchasePreview'); if(img){img.src='"
                + imageUrl.Replace("\\", "\\\\").Replace("'", "\\'")
                + "';} showAdminModal('DivPhotolarge');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "PopPhoto", script, true);
            return;
        }

        if (!string.Equals(e.CommandName, "mySuccess", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(e.CommandName, "myFail", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lbluserid = (Label)GridView1.Rows[index].FindControl("lbluserid123");
        Label lblPurchaseID = (Label)GridView1.Rows[index].FindControl("LblOrderNo");
        string str_status = string.Equals(e.CommandName, "mySuccess", StringComparison.OrdinalIgnoreCase)
            ? "Success"
            : "Failed";

        objUser.UserId = txtuserid.Text;
        objUser.TransferUserId = txtuserid.Text;
        objUser.EpinNo = lblPurchaseID.Text;
        objUser.MentionBy = "F000001";
        DataTable dt = UpdateStatusTopupManual(lbluserid.Text, str_status, lblPurchaseID.Text);
        if (dt != null)
        {
            if (string.Equals(str_status, "Failed", StringComparison.OrdinalIgnoreCase)
                && dt.Rows.Count > 0
                && Convert.ToString(dt.Rows[0][0]) == "1")
            {
                try
                {
                    SavingProductHelper.RestoreBulkCouponForPurchase(lbluserid.Text, lblPurchaseID.Text);
                }
                catch
                {
                }
            }
            Message.Show(dt.Rows[0][1].ToString());
        }
        else
        {
            Message.Show("Unknown error");
        }

        loaduser();
    }
    public DataTable UpdateStatusTopupManual(string userid, string status, string purchaseid)
    {
        DataTable dt = null;
        this.ObjData.StartConnection();
        try
        {
            string s2 = "sp_activeUserWithProductPurchase";
            SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@ActivateUserId", userid),
					new SqlParameter("@UserId", userid),
                    new SqlParameter("@Status", status),
					new SqlParameter("@OrderNo", purchaseid)
				};
            dt = this.ObjData.RunDataTableProcedure(s2, parameter);
        }
        catch (System.Exception ex_5F)
        {
            dt = null;
        }
        this.ObjData.EndConnection();
        return dt;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {
        string fromDate = TryGetSqlDate(txtfromdate.Text);
        string toDate = TryGetSqlDate(txttodate.Text);
        objP.PurchaseStatus = DDLSTStatus.SelectedValue;
        objP.UserId = txtuserid.Text;
        objP.FranchiseeID = "F000001"; //Session["fuserid"].ToString();
        DataTable dt = new DataTable();
        dt = getPurchaseProductQuantityFranchisee(objP, fromDate, toDate);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    static string TryGetSqlDate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        raw = raw.Trim();
        try
        {
            // Preferred UI format: dd/mm/yyyy
            if (raw.Contains("/"))
            {
                DateTime dt = Message.GetIndianDate(raw);
                if (dt.Year > 1900)
                {
                    return dt.ToString("yyyy-MM-dd");
                }
            }

            DateTime parsed;
            if (DateTime.TryParse(raw, out parsed) && parsed.Year > 1900)
            {
                return parsed.ToString("yyyy-MM-dd");
            }
        }
        catch
        {
        }

        return null;
    }

    public DataTable getPurchaseProductQuantityFranchisee(clsProduct objstate, string Fromdate, String Todate)
    {
        string str_query = @"SELECT PU.TotalDP,PU.Cstatus Pstatus,PU.orderNo,PU.PurchaseId,PU.TotalAmount,PU.Userid AS UserId,
Convert(CHAR,PU.PurchaseDate,103) AS PurchaseDate,U.Username ,U.Email Emailid,U.Mobile AS ContactNo,U.Address AS address,
(Select top 1 isnull(invoicestatus,0) from PurchaseProductMaster where purchaseId=PU.purchaseId) as InvoiceStatus,
CASE WHEN LTRIM(RTRIM(ISNULL(PU.img, ''))) = '' THEN '../ProductImage/images.png' ELSE '../ProductImage/' + LTRIM(RTRIM(PU.img)) END AS IMAGE,
PU.onlinetransactionID AS transactionid
FROM UserFranchiseePurchaseMaster PU
JOIN Userdetail U ON PU.UserId=U.userid
WHERE 1=1 ";

        if (!string.IsNullOrEmpty(Fromdate))
        {
            str_query += " and cast(PU.PurchaseDate as date)>=cast('" + Fromdate + "' as date)";
        }
        if (!string.IsNullOrEmpty(Todate))
        {
            str_query += " and cast(PU.PurchaseDate as date)<=cast('" + Todate + "' as date)";
        }
        if (objstate.UserId != string.Empty)
        {
            str_query += " and PU.Userid='" + objstate.UserId.Replace("'", "''") + "'";
        }
        if (objstate.FranchiseeID != string.Empty)
        {
            str_query += " and PU.franchiseeid='" + objstate.FranchiseeID.Replace("'", "''") + "'";
        }
        if (objstate.PurchaseStatus != string.Empty)
        {
            str_query += " and isnull(PU.Isdistributer,0)=0 and isnull(PU.Cstatus,0)='" + objstate.PurchaseStatus.Replace("'", "''") + "'";
        }
       // str_query += " and isnull(PU.RepurchaseStatus,0)='1'";
        //  str_query += "GROUP BY PU.TotalDP,PU.Pstatus,PU.PurchaseId,PU.Purchaseby,Convert(CHAR,PU.PurchaseDate,103)";
        str_query += " order by PU.id desc";
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
    public DataTable getPurchaseProductQuantityChild(string ID)
    {
        string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.TotalAmount,PT.UserID,PR.ProductID,PR.TotalDP,P.DP,PR.Franchiseeid,PR.CGST+PR.SGST+PR.IGST GST,ST.SizeName,C.ColorName,PR.purchaseamount FROM UserFranchiseePurchaseProductMaster PR INNER JOIN UserFranchiseePurchaseMaster PT ON PR.PurchaseId=PT.PurchaseId INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID JOIN subproductmaster S ON S.SubProductID=PR.SubProductID  JOIN colormaster C ON S.ColorId=C.ID JOIN Sizemaster ST ON S.SizeID=ST.ID where 1=1 ";


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
            dt1 = getPurchaseProductQuantityChild(customerId);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();


            Label Lblstatus = (Label)e.Row.FindControl("Lblstatus");
            Label lblpstatus = (Label)e.Row.FindControl("LblPstatus");
            LinkButton btnSuccess = (LinkButton)e.Row.FindControl("btnSuccess");
            LinkButton btnFail = (LinkButton)e.Row.FindControl("btnFail");

            if (lblpstatus.Text == "0")
            {
                Lblstatus.Text = "Pending";
                Lblstatus.CssClass = "label label-warning";
                btnSuccess.Visible = true;
                btnFail.Visible = true;
            }
            if (lblpstatus.Text == "1")
            {
                Lblstatus.Text = "Approved";
                Lblstatus.CssClass = "label label-success";
                btnSuccess.Visible = false;
                btnFail.Visible = false;
            }
            if (lblpstatus.Text == "2")
            {
                Lblstatus.Text = "Rejected";
                Lblstatus.CssClass = "label label-danger";
                btnSuccess.Visible = false;
                btnFail.Visible = false;
            }
        }
    }
    //protected void BtnSub_Click(object sender, EventArgs e)
    //{
    //    Button btndetails = sender as Button;
    //    GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
    //    string UserID = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("lbluserid123")).Text;
    //    string InvoiceD = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("LblInvoiceStatus")).Text;
    //    string lbluserid = ((Label)GridView1.Rows[gvrow.RowIndex].FindControl("lbluserid")).Text;

    //    if (InvoiceD == "0")
    //    {
    //        Response.Redirect("UserRepurchaseEntry.aspx?PID=" + lbluserid + "&UserId=" + UserID);
    //    }
    //    else
    //    {
    //        Message.Show("alredy generate invoice");
    //    }
    //}
}