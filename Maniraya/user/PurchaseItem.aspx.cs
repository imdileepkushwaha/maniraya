using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Data.SqlClient;
using DataTier;

public partial class admin_PurchaseItem : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    private int PageSize = 12;
    DataTable PurchaseDt;
    Decimal TAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadProduct(1);
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objuser.UserId = txtuserid.Text;
        dt = getUserNameWithBalance(objuser);
        if (dt.Rows.Count > 0)
        {

            Lblbalance.Text = dt.Rows[0]["balanceamount"].ToString();
            LblUtility.Text = dt.Rows[0]["utilitybalance"].ToString();
        }     
    }
    void loadProduct(int PageIndex)
    {
        DataTable dt = new DataTable();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        dt = ProductPageWise(PageIndex, PageSize);
        dlCustomers.DataSource = dt;
        dlCustomers.DataBind();
        int recordCount = Convert.ToInt32(dt.Rows[0]["Count"].ToString());
        this.PopulatePager(recordCount, PageIndex);       
        LblRecordCount.Text = "Showing " + Convert.ToString((PageSize * (PageIndex - 1)) + 1) + " to " + Convert.ToString(PageSize * PageIndex) + " of " + recordCount.ToString() + " entries";
       
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the First Page Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("First", "1"));
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }

        //Add the Last Button.
        if (currentPage != pageCount)
        {
            pages.Add(new ListItem("Last", pageCount.ToString()));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.loadProduct(pageIndex);
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "photolarge")
        {           
            string id = e.CommandArgument.ToString();
            objState.ProductId = id;           
            DataTable Dt = objState.getProduct(objState);
            if (Dt.Rows.Count > 0)
            {
                LblcategoryName123.Text = Dt.Rows[0]["categoryname"].ToString();
                LblProductCode.Text = Dt.Rows[0]["ProductId"].ToString();
                LblProductName.Text = Dt.Rows[0]["ProductName"].ToString();
                LblAmount.Text = Dt.Rows[0]["Amount"].ToString();
                LblBv.Text = Dt.Rows[0]["bv"].ToString();
                LblMRP.Text = Dt.Rows[0]["MRP"].ToString();
               // LblStock.Text = Dt.Rows[0]["Stock"].ToString();
                if (Dt.Rows[0]["Image"].ToString() != "../ProductImage/")
                {
                    Image2.ImageUrl = Dt.Rows[0]["Image"].ToString();
                }
                else
                {
                    Image2.ImageUrl = "../ProductImage/images.png";
                }
                /////////////////////////////////////////////////////////////////
                if (Dt.Rows[0]["Image2"].ToString() != "../ProductImage/" && Dt.Rows[0]["Image2"].ToString() != "")
                {
                    Image3.ImageUrl = Dt.Rows[0]["Image2"].ToString();
                }
                else
                {
                    Image3.ImageUrl = "../ProductImage/images.png";
                }
                /////////////////////////////////////////////////////////////////
                if (Dt.Rows[0]["Image3"].ToString() != "../ProductImage/" && Dt.Rows[0]["Image3"].ToString() != "")
                {
                    Image4.ImageUrl = Dt.Rows[0]["Image3"].ToString();
                }
                else
                {
                    Image4.ImageUrl = "../ProductImage/images.png";
                }
                LblDescription.Text = Dt.Rows[0]["Description"].ToString();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

        }
        if (e.CommandName == "BuyProduct")
        {
            ClearValue();
            string id = e.CommandArgument.ToString();
            objState.ProductId = id;
            DataTable Dt = objState.getProduct(objState);
            if (Dt.Rows.Count > 0)
            {
                HdBuisnessVolume.Value = Dt.Rows[0]["BV"].ToString();
                HdCatId.Value = Dt.Rows[0]["CategoryID"].ToString();
                TxtProductCode.Text = Dt.Rows[0]["ProductId"].ToString();
                TxtProductName.Text = Dt.Rows[0]["ProductName"].ToString();
                TxtAmount.Text = Dt.Rows[0]["Amount"].ToString();
                ViewState["st"] = Dt.Rows[0]["stock"].ToString();
                string Imaget = "";
                if (Dt.Rows[0]["Image"].ToString() != "../ProductImage/")
                {
                    Imaget = Dt.Rows[0]["Image"].ToString();
                }
                else
                {
                    Imaget = "../ProductImage/images.png";
                }
                TxtImage.Text = Imaget;
                TxtQuantity.Text = "1";
                TxtTotalAmount.Text = Dt.Rows[0]["Amount"].ToString();
            }
            BtnAdd.Text = "Add";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }
    }
    public void CreateDatatable()
    {
        PurchaseDt = new DataTable();
        PurchaseDt.Columns.Add("CatID");
        PurchaseDt.Columns.Add("ProductId");
        PurchaseDt.Columns.Add("ProductName");
        PurchaseDt.Columns.Add("Image");
        PurchaseDt.Columns.Add("Amount");
        PurchaseDt.Columns.Add("BV");
        PurchaseDt.Columns.Add("Quantity");
        PurchaseDt.Columns.Add("TotalAmount");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (BtnAdd.Text == "Add")
        {
            //if (ViewState["st"] != null)
            //{
            //    if (Convert.ToInt32(TxtQuantity.Text) > Convert.ToInt32(ViewState["st"].ToString()))
            //    {                  
            //        string popupScript = "alert('you can not purchase product more than stock');";
            //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            //        string popupScript3 = "Closepopup1();";
            //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
            //        return;
                   
            //    }
            //}
            if (PurchaseDt == null)
            {
                CreateDatatable();
            }
            DataRow DR;
            foreach (GridViewRow Gr in GridView1.Rows)
            {
                DR = PurchaseDt.NewRow();
                DR["CatID"] = ((Label)Gr.FindControl("LblCatId")).Text;
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                PurchaseDt.Rows.Add(DR);

                if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
                {
                    string popupScript = "alert('this Product already add');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    string popupScript3 = "Closepopup1();";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                    return;
                }
            }
            DR = PurchaseDt.NewRow();
            DR["CatID"] = HdCatId.Value;
            DR["ProductId"] = TxtProductCode.Text;
            DR["ProductName"] = TxtProductName.Text;
            DR["Image"] = TxtImage.Text;
            DR["Amount"] = TxtAmount.Text;
            DR["BV"] = HdBuisnessVolume.Value;
            DR["Quantity"] = TxtQuantity.Text;
            DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text);
            PurchaseDt.Rows.Add(DR);
            ViewState["PDT"] = PurchaseDt;
            GridView1.DataSource = PurchaseDt;
            GridView1.DataBind();
            ClearValue();
            PurchasePanel.Visible = true;
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else
        {
            if (PurchaseDt == null)
            {
                CreateDatatable();
            }            
            DataRow DR;
            foreach (GridViewRow Gr in GridView1.Rows)
            {
                if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
                {
                    DR = PurchaseDt.NewRow();
                    DR["CatID"] = ((Label)Gr.FindControl("LblCatId")).Text;
                    DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                    DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                    DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                    DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                    DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                    DR["Quantity"] = TxtQuantity.Text;
                    DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(((Label)Gr.FindControl("LblProductAmountG")).Text);
                    PurchaseDt.Rows.Add(DR);
                }
                else
                {
                    DR = PurchaseDt.NewRow();
                    DR["CatID"] = ((Label)Gr.FindControl("LblCatId")).Text;
                    DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                    DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                    DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                    DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                    DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                    DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                    DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                    PurchaseDt.Rows.Add(DR);
                }
            }
            ViewState["PDT"] = PurchaseDt;
            GridView1.DataSource = PurchaseDt;
            GridView1.DataBind();
            ClearValue();
            PurchasePanel.Visible = true;
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            HdCatId.Value = ((Label)GridView1.Rows[index].FindControl("LblCatId")).Text;
            TxtProductCode.Text = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
            TxtProductName.Text = ((Label)GridView1.Rows[index].FindControl("LblProductNameG")).Text;
            TxtAmount.Text = ((Label)GridView1.Rows[index].FindControl("LblProductAmountG")).Text;
            TxtQuantity.Text = ((Label)GridView1.Rows[index].FindControl("lblQuantity")).Text;
            TxtTotalAmount.Text = ((Label)GridView1.Rows[index].FindControl("lblTotalAmount")).Text;
            TxtImage.Text = ((Label)GridView1.Rows[index].FindControl("LblProductImageG")).Text;
            BtnAdd.Text = "Update";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);

        }
        if (e.CommandName == "del")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {

                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string id = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
                DataTable table = ViewState["PDT"] as DataTable;
                table.Rows.RemoveAt(index);
                ViewState["PDT"] = table;
                GridView1.DataSource = table;
                GridView1.DataBind();               
                if (GridView1.Rows.Count == 0)
                {
                    PurchasePanel.Visible = false;
                }
                ViewState["PDT"] = PurchaseDt;
            }
        }
    }
    private void ClearValue()
    {
        TxtProductCode.Text = "";
        TxtProductName.Text = "";
        TxtImage.Text = "";
        TxtAmount.Text = "";
        TxtQuantity.Text = "";
        TxtTotalAmount.Text = "";
    }
    Decimal total = 0;
    Decimal tt = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            total += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalAmount")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount = (Label)e.Row.FindControl("lblGrandTotal");
            lblAmount.Text = total.ToString();
            HDTotal.Value = total.ToString();
            TxtTotalpurchase.Text= total.ToString();
            
            if(TxtTotalpurchase.Text!=string.Empty)
            {
                if (Convert.ToDecimal(TxtTotalpurchase.Text) < 1000)
                {
                    TxtShipping.Text = "0";
                }
                if (Convert.ToDecimal(TxtTotalpurchase.Text) > 1000 && Convert.ToDecimal(TxtTotalpurchase.Text) < 3500)
                {
                    TxtShipping.Text = "0";
                }
                if (Convert.ToDecimal(TxtTotalpurchase.Text) > 3500 )
                {
                    TxtShipping.Text = "0";
                }
            }
            if(Decimal.TryParse(TxtTotalpurchase.Text,out tt))
            {
                TXTTTAmount.Text = Convert.ToString(total + Convert.ToDecimal(TxtShipping.Text));
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        PurchasePanel.Visible = false;
        if (ViewState["PDT"] != null)
        {
            ViewState["PDT"] = null;
        }
        PurchaseDt = null;
        if(GridView1.Rows.Count==0)
        {
            TxtTotalpurchase.Text = "0";
            TXTTTAmount.Text = "0";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (Convert.ToDecimal(HDTotal.Value) <= 0)
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        objState.TotalAmount = Convert.ToDecimal(HDTotal.Value);
        objState.UserId = txtuserid.Text;       
        string  i = AddPurchase(objState, ViewState["PDT"] as DataTable, Convert.ToDecimal(TxtShipping.Text));
        if (i == "1")
        {
            string popupScript = "alert('Purchase Successfull');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            GridView1.DataSource = null;
            GridView1.DataBind();
            PurchasePanel.Visible = false;
            if (ViewState["PDT"] != null)
            {
                ViewState["PDT"] = null;
            }
            PurchaseDt = null;

        }
        else if (i == "2")
        {
            string popupScript = "alert('you have insufficient balance');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "3")
        {
            string popupScript = "alert('insufficient stock');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('unknown error');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    Data ObjData = new Data();
    public string AddPurchase(clsProduct objP, DataTable Dt,Decimal shipping)
    {
        int i = 0;

        string res = "";
        string s2 = "";
        string ChkStock = "1";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {

            //foreach (DataRow Dr in Dt.Rows)
            //{
            //    string Q = "select isnull(Sum(CrQuantity),0) -isnull(Sum(DrQuantity),0) FROM StockMaster where ProductId='" + Dr["ProductId"].ToString() + "'";
            //    DataSet Ds = ObjData.RunSelectQueryTrans(Q, tr);
            //    if (Convert.ToInt32(Dr["Quantity"].ToString()) > Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString()))
            //    {
            //        ChkStock = "0";
            //    }
            //}
            if (ChkStock == "1")
            {
                s2 = "sp_add_Purchase";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",objP.UserId),
                    new SqlParameter("@TotalAmount",objP.TotalAmount),
                       new SqlParameter("@Shipping",shipping),
                       new SqlParameter("@WalletType",DDLSTWallet.SelectedValue),
                    new SqlParameter("@PurchaseProduct",Dt),

                };
                DataTable Dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = Dtresult.Rows[0][1].ToString();

                tr.Commit();
            }
            else
            {
                res = "3";
            }



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
    public DataTable ProductPageWise(int pageindex, int pageSize)
    {
        DataTable Dt = new DataTable();
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "GetProductPageWise";
            SqlParameter[] parameter = {
                new SqlParameter("@PageIndex",pageindex),
                new SqlParameter("@PageSize",pageSize),
                 new SqlParameter("@RecordCount",4)
                };
            Dt = ObjData.RunDataTableProcedureTRansGetPage(s2, tr, parameter);

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
        return Dt;
    }
   
    public DataTable getUserNameWithBalance(clsUser objUser)
    {
        //string str_query = "select isnull( sum(CrAmount),0) as sumCr from transactiondetail td where td.UserID='" + objuser.UserId + "'";

        string str_query = "SELECT ud.userid, ud.username,ud.mobile,ud.balanceamount,ud.utilitybalance FROM userdetail ud where ud.UserId = '" + objUser.UserId + "' ";
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