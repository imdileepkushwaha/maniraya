using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class FranchiseeSearch : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    private int PageSize = 12;
    DataTable PurchaseDt;
    Decimal TAmt = 0;
    clsState objState1 = new clsState();
    clsState objSt = new clsState();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                loadsusername();
                loadstate();
                loadProduct(1);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showSearchModal();", true);
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState1.CountryId = "1";
        dt = objState1.getState(objState1);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState1.StateId = ddstate.SelectedValue.ToString();
        dt = objState1.getCity(objState1);

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li1 = new ListItem("Other", "111111");
        ddcity.Items.Insert(0, li1);
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }

   

    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showSearchModal();", true);
    }



    void loadsusername()
    {
        DataTable dt = new DataTable();
        objuser.UserId = Session["userid"].ToString();
        dt = objuser.getUserNameWithBalance(objuser);
        if (dt.Rows.Count > 0)
        {

            //Lblbalance.Text = dt.Rows[0]["balance"].ToString();
        }     
    }
    void loadProduct(int PageIndex)
    {
        DataTable dt = new DataTable();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        objState.State = null;
        objState.City = null;
        objState.PinCode = null;
        objState.tehsilid = null;
        objState.marketid = null;

        if (ddstate.SelectedIndex > 0)
        {
            objState.State = ddstate.SelectedValue;
        }
        if (ddcity.SelectedIndex > 0)
        {
            objState.City = ddcity.SelectedValue;
        }
        if (!string.IsNullOrEmpty(txtpincode.Text))
        {
            objState.PinCode = txtpincode.Text.Trim();
        }
        if (ddlsttehsil.SelectedIndex > 0)
        {
            objState.tehsilid = ddlsttehsil.SelectedValue;
        }
        if (ddlstmarket.SelectedIndex > 0)
        {
            objState.marketid = ddlstmarket.SelectedValue;
        }

        dt = objState.FranchiseePageWise(PageIndex, PageSize, objState);
        dlCustomers.DataSource = dt;
        dlCustomers.DataBind();
        if (dt != null && dt.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dt.Rows[0]["Count"].ToString());
            this.PopulatePager(recordCount, PageIndex);
            LblRecordCount.Text = "Showing " + Convert.ToString((PageSize * (PageIndex - 1)) + 1) + " to " + Convert.ToString(PageSize * PageIndex) + " of " + recordCount.ToString() + " entries";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop1", "alert('No Record Found');showSearchModal();", true);
        }
       
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
   
  
    protected void BtnSearchFranchisee_Click(object sender,EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "Closesearchpopup();", true);
        loadProduct(1);
    }


    
    protected void BtnCancelFranchisee_Click(object sender,EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "Closesearchpopup();", true);
    }


    protected void imgclick(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        objState.State = null;
        objState.City = null;
        objState.PinCode = null;
        lblfname.Text = "";
        lbladdress.Text = "";
        lblcity.Text = "";
        lblstate.Text = "";
        lblpincode.Text = "";
        lblmob.Text = "";

        string[] value = ((ImageButton)sender).CommandArgument.ToString().Split('_');
        string sid = value[0];
        string cid = value[1];
        string fid = value[2];

       
        objState.State = sid;
        objState.City = cid;
        objState.FranchiseeID = fid;
    
        dt = objState.FranchiseePageWise(1, 1, objState);

        if (dt != null && dt.Rows.Count > 0)
        {
            lblfname.Text = dt.Rows[0]["username"].ToString();
            lbladdress.Text = dt.Rows[0]["address"].ToString();
            lblcity.Text = dt.Rows[0]["cityname"].ToString();
            lblstate.Text = dt.Rows[0]["statename"].ToString();
            lblpincode.Text = dt.Rows[0]["pincode"].ToString();
            lblmob.Text = dt.Rows[0]["mobile"].ToString();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showFranchiseeModal();", true);
    }

    protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTehsil();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showSearchModal();", true);
    }
    protected void DDlstTehsil_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmarket();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showSearchModal();", true);
    }
    void loadTehsil()
    {
        ddlsttehsil.Items.Clear();
        DataTable dt = new DataTable();
        objSt.CityId = ddcity.SelectedValue.ToString();
        dt = objSt.getTehsil(objSt);

        ddlsttehsil.DataSource = dt;
        ddlsttehsil.DataTextField = "tehsilName";
        ddlsttehsil.DataValueField = "tehsilID";
        ddlsttehsil.DataBind();
        ListItem li = new ListItem("Select Tehsil", "0");
        ddlsttehsil.Items.Insert(0, li);
    }
    void loadmarket()
    {
        ddlstmarket.Items.Clear();
        DataTable dt = new DataTable();
        objSt.TehsilId = ddlsttehsil.SelectedValue.ToString();
        dt = objSt.getMarket(objSt);

        ddlstmarket.DataSource = dt;
        ddlstmarket.DataTextField = "Marketname";
        ddlstmarket.DataValueField = "MarketId";
        ddlstmarket.DataBind();
        ListItem li = new ListItem("Select market", "0");
        ddlstmarket.Items.Insert(0, li);
    }
}