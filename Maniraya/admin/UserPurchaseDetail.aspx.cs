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
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            return;
        }

        // Recreate pager LinkButtons so First/Prev/Next postbacks raise Click events.
        if (ViewState["HasSearched"] != null && (bool)ViewState["HasSearched"])
        {
            loaduser(false);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ViewState["HasSearched"] = true;
        loaduser(true);
    }

    void loaduser()
    {
        loaduser(false);
    }

    void loaduser(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

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
        DataTable dt = getuserFranchiseePurchase(objV);
        BindGrid(dt);
    }

    void BindGrid(DataTable dt)
    {
        if (dt == null)
        {
            dt = new DataTable();
        }

        int totalRecords = dt.Rows.Count;
        int pageSize = GetPageSize();
        bool showAll = pageSize <= 0 || string.Equals(ddlRecordFilter.SelectedItem.Text, "All", StringComparison.OrdinalIgnoreCase);

        if (showAll || totalRecords == 0)
        {
            GridView1.AllowPaging = false;
            GridView1.PageSize = Math.Max(totalRecords, 1);
            if (showAll)
            {
                GridView1.PageIndex = 0;
            }
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.PageSize = pageSize;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            if (GridView1.PageIndex >= totalPages)
            {
                GridView1.PageIndex = Math.Max(0, totalPages - 1);
            }
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (totalRecords == 0)
        {
            lblSummary.Text = "No franchisee sales found for selected filters.";
        }
        else
        {
            int fromRecord = 1;
            int toRecord = totalRecords;
            if (GridView1.AllowPaging)
            {
                fromRecord = (GridView1.PageIndex * GridView1.PageSize) + 1;
                toRecord = Math.Min(totalRecords, (GridView1.PageIndex + 1) * GridView1.PageSize);
            }
            lblSummary.Text = "Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + " record(s)";
        }

        BuildExternalPager(totalRecords);
    }

    int GetPageSize()
    {
        int pageSize;
        if (ddlRecordFilter != null && int.TryParse(ddlRecordFilter.SelectedItem.Text, out pageSize))
        {
            return pageSize;
        }
        return 10;
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["HasSearched"] == null || !(bool)ViewState["HasSearched"])
        {
            return;
        }
        loaduser(true);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (ViewState["HasSearched"] == null || !(bool)ViewState["HasSearched"])
        {
            return;
        }
        loaduser(false);
    }

    void BuildExternalPager(int totalRecords)
    {
        pnlPager.Controls.Clear();

        if (!GridView1.AllowPaging || totalRecords <= 0)
        {
            pnlPager.Visible = false;
            return;
        }

        int pageSize = GridView1.PageSize;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (totalPages <= 1)
        {
            pnlPager.Visible = false;
            return;
        }

        int currentPage = GridView1.PageIndex;
        pnlPager.Visible = true;

        int fromRecord = (currentPage * pageSize) + 1;
        int toRecord = Math.Min(totalRecords, (currentPage + 1) * pageSize);
        pnlPager.Controls.Add(new LiteralControl(
            "<span class=\"admin-pager-info\">Page " + (currentPage + 1) + " of " + totalPages
            + " · Showing " + fromRecord + "–" + toRecord + " of " + totalRecords + "</span>"));

        AddPagerLink("First", 0, currentPage > 0, false);
        AddPagerLink("Prev", currentPage - 1, currentPage > 0, false);

        const int windowSize = 5;
        int startPage = Math.Max(0, currentPage - (windowSize / 2));
        int endPage = Math.Min(totalPages - 1, startPage + windowSize - 1);
        startPage = Math.Max(0, endPage - windowSize + 1);

        if (startPage > 0)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-ellipsis\">...</span>"));
        }

        for (int i = startPage; i <= endPage; i++)
        {
            AddPagerLink((i + 1).ToString(), i, true, i == currentPage);
        }

        if (endPage < totalPages - 1)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-ellipsis\">...</span>"));
        }

        AddPagerLink("Next", currentPage + 1, currentPage < totalPages - 1, false);
        AddPagerLink("Last", totalPages - 1, currentPage < totalPages - 1, false);
    }

    void AddPagerLink(string text, int pageIndex, bool enabled, bool isActive)
    {
        if (isActive)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-active\">" + text + "</span>"));
            return;
        }
        if (!enabled)
        {
            pnlPager.Controls.Add(new LiteralControl("<span class=\"admin-pager-btn is-disabled\">" + text + "</span>"));
            return;
        }

        LinkButton link = new LinkButton();
        link.ID = "pagerBtn_" + pageIndex + "_" + text.Replace(" ", "");
        link.Text = text;
        link.CssClass = "admin-pager-btn";
        link.CommandArgument = pageIndex.ToString();
        link.Click += ExternalPager_Click;
        link.CausesValidation = false;
        pnlPager.Controls.Add(link);
    }

    protected void ExternalPager_Click(object sender, EventArgs e)
    {
        LinkButton link = sender as LinkButton;
        int pageIndex;
        if (link == null || !int.TryParse(link.CommandArgument, out pageIndex))
        {
            return;
        }
        GridView1.PageIndex = pageIndex;
        if (ViewState["HasSearched"] == null || !(bool)ViewState["HasSearched"])
        {
            return;
        }
        loaduser(false);
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
