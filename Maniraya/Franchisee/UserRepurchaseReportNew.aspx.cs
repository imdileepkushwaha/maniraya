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

public partial class UserRepurchaseReportNew : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    clsUser objUser = new clsUser();
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["fuserid"] != null)
            {
                // txtuserid.Text = Session["fuserid"].ToString();
                // txtuserid.Enabled = false;
                txtfromdate.Attributes.Add("readonly", "true");
                txttodate.Attributes.Add("readonly", "true");
                txtfromdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
		 if (e.CommandName == "mySuccess" || e.CommandName == "myFail")
        {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lbluserid = (Label)GridView1.Rows[index].FindControl("lbluserid123");
        Label lblPurchaseID = (Label)GridView1.Rows[index].FindControl("LblOrderNo");
        string str_status = "";
        if (e.CommandName == "mySuccess")
        {
            str_status = "Success";
        }
        else if (e.CommandName == "myFail")
        {
            str_status = "Failed";
        }

        objUser.UserId = txtuserid.Text;
        objUser.TransferUserId = txtuserid.Text;
        objUser.EpinNo = lblPurchaseID.Text;
        objUser.MentionBy = Session["fuserid"].ToString();
        DataTable dt = UpdateStatusTopupManual(lbluserid.Text, str_status, lblPurchaseID.Text);
        if (dt != null)
        {
            Message.Show(dt.Rows[0][1].ToString());
        }
        else
        {
            Message.Show("Unknown error");
        }

        
        loaduser();

		 }
        if (e.CommandName == "photolarge")
        {
            int index2 = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index2].FindControl("lblid");
            Label LblImage = (Label)GridView1.Rows[index2].FindControl("LblImage");
            ImageLarge.ImageUrl = LblImage.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showModal1();", true);
        }
        if (e.CommandName == "moredetail")
        {
            int index2 = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index2].FindControl("lbluserid");
            DataTable dt1 = new DataTable();
            dt1 = getPurchaseProductQuantityChild(lblid.Text);
            gvOrders.DataSource = dt1;
            gvOrders.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showModal2();", true);
        }
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


        string fromDate = "", toDate = "";
        fromDate = txtfromdate.Text;
        toDate = txttodate.Text;
        objP.PurchaseStatus = DDLSTStatus.SelectedValue;
        objP.UserId = txtuserid.Text;
        objP.FranchiseeID = Session["fuserid"].ToString();
        DataTable dt = new DataTable();
        dt = getPurchaseProductQuantityFranchisee(objP, fromDate, toDate);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public DataTable getPurchaseProductQuantityFranchisee(clsProduct objstate, string Fromdate, String Todate)
    {
        string str_query = "SELECT PU.TotalDP,PU.Cstatus Pstatus,PU.orderNo,PU.PurchaseId,PU.TotalAmount,PU.Userid  AS UserId,Convert(CHAR,PU.PurchaseDate,103) AS PurchaseDate,U.Username ,U.Email Emailid,U.Mobile AS ContactNo,U.Address AS address,U.Shippingaddress,(Select top 1 isnull(invoicestatus,0) from PurchaseProductMaster where purchaseId=PU.purchaseId) as InvoiceStatus,case when img='' then '../ProductImage/images.png' else '../ProductImage/'+ img end as ImageIMAGE,PU.onlinetransactionID AS transactionid   FROM   UserFranchiseePurchaseMaster PU JOIN Userdetail U ON PU.UserId=U.userid  where 1=1 ";

        if (Fromdate != string.Empty)
        {
            str_query += " and cast(PU.PurchaseDate as date)>=cast('" + Fromdate + "' as date)";
        }
        if (Todate != string.Empty)
        {
            str_query += " and cast(PU.PurchaseDate as date)<=cast('" + Todate + "' as date)";
        }
        if (objstate.UserId != string.Empty)
        {
            str_query += " and PU.Userid='" + objstate.UserId + "'";
        }
        if (objstate.FranchiseeID != string.Empty)
        {
            str_query += " and PU.franchiseeid='" + objstate.FranchiseeID + "'";
        }
        if (objstate.PurchaseStatus != string.Empty)
        {
            str_query += " and isnull(PU.Cstatus,0)='" + objstate.PurchaseStatus + "'";
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
        string str_query = "SELECT PR.BV,PR.PurchaseType type,PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,PR.Amount,PR.TotalAmount,PR.PurchaseBy AS UserID,PR.ProductID,PR.TotalDP,P.DP FROM PurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID   where 1=1 ";


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
           // GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
           // DataTable dt1 = new DataTable();
           // dt1 = getPurchaseProductQuantityChild(customerId);
           // gvOrders.DataSource = dt1;
           // gvOrders.DataBind();


            Label Lblstatus = (Label)e.Row.FindControl("Lblstatus");
            Label lblpstatus = (Label)e.Row.FindControl("LblPstatus");
           // LinkButton btnSuccess = (LinkButton)e.Row.FindControl("btnSuccess");
           // LinkButton btnFail = (LinkButton)e.Row.FindControl("btnFail");

            if (lblpstatus.Text == "0")
            {
                Lblstatus.Text = "Pending";
             //   btnSuccess.Visible = true;
             //   btnFail.Visible = true;
            }
            if (lblpstatus.Text == "1")
            {
                Lblstatus.Text = "Success";
             //   btnSuccess.Visible = false;
             //   btnFail.Visible = false;
            }
            if (lblpstatus.Text == "2")
            {
                Lblstatus.Text = "Failed";
             //   btnSuccess.Visible = false;
             //   btnFail.Visible = false;
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            Label lbluserid = (Label)r.FindControl("lbluserid123");
            Label lblPurchaseID = (Label)r.FindControl("LblOrderNo");
            string str_status = "";
            str_status = "Success";
            if (ChStatus.Checked == true)
            {
                objUser.UserId = txtuserid.Text;
                objUser.TransferUserId = txtuserid.Text;
                objUser.EpinNo = lblPurchaseID.Text;
                objUser.MentionBy = Session["fuserid"].ToString();
                DataTable dt = UpdateStatusTopupManual(lbluserid.Text, str_status, lblPurchaseID.Text);
                if (dt != null)
                {
                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        Message.Show("OrderNo " + objUser.EpinNo+ " "+dt.Rows[0][1].ToString());
                        break;
                    }
                   
                }
                else
                {

                    Message.Show("OrderNo " + objUser.EpinNo + " Unknown error");
                    break;
                }
            }
            

        }
        Message.Show("Record has been update successfully");
        loaduser();
    }
    protected void BtnReject_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            Label lbluserid = (Label)r.FindControl("lbluserid123");
            Label lblPurchaseID = (Label)r.FindControl("LblOrderNo");
            string str_status = "";
            str_status = "Failed";
            if (ChStatus.Checked == true)
            {
                objUser.UserId = txtuserid.Text;
                objUser.TransferUserId = txtuserid.Text;
                objUser.EpinNo = lblPurchaseID.Text;
                objUser.MentionBy = Session["fuserid"].ToString();
                DataTable dt = UpdateStatusTopupManual(lbluserid.Text, str_status, lblPurchaseID.Text);
                if (dt != null)
                {
                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        Message.Show("OrderNo " + objUser.EpinNo + " " + dt.Rows[0][1].ToString());
                        break;
                    }

                }
                else
                {

                    Message.Show("OrderNo " + objUser.EpinNo + " Unknown error");
                    break;
                }
            }


        }
        Message.Show("Record has been update successfully");
        loaduser();
    }
    private void Multipleaction(string userid, string status, string purchaseid)
    {

    }
}