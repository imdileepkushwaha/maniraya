using BusinessLogicTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DownlineReport : System.Web.UI.Page
{
    clsUser objUser = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            txtuserid.Text = Session["userid"].ToString();
            txtuserid.Enabled = false;
            Getsalary();
            BindDownline(true);
        }
    }

    void BindDownline(bool resetPage)
    {
        if (resetPage)
        {
            GridView1.PageIndex = 0;
        }

        string userId = (txtuserid.Text ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(userId))
        {
            userId = Convert.ToString(Session["userid"]);
        }

        objUser.UserId = userId;

        int pageSize = GetPageSize();
        int pageIndex = GridView1.PageIndex;
        int totalCount;
        DataTable dt;

        if (pageSize <= 0)
        {
            dt = objUser.getUserDownlinePaged(objUser, 0, 0, out totalCount);
            GridView1.AllowPaging = false;
            GridView1.AllowCustomPaging = false;
            GridView1.PageSize = totalCount > 0 ? totalCount : 25;
        }
        else
        {
            GridView1.AllowPaging = true;
            GridView1.AllowCustomPaging = true;
            GridView1.PageSize = pageSize;
            dt = objUser.getUserDownlinePaged(objUser, pageIndex, pageSize, out totalCount);

            if (totalCount > 0)
            {
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                if (GridView1.PageIndex >= totalPages)
                {
                    GridView1.PageIndex = Math.Max(0, totalPages - 1);
                    dt = objUser.getUserDownlinePaged(objUser, GridView1.PageIndex, pageSize, out totalCount);
                }
            }

            GridView1.VirtualItemCount = totalCount;
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        lblResultSummary.Text = totalCount > 0
            ? totalCount + " downline member(s) found."
            : "No downline members found.";
    }

    int GetPageSize()
    {
        if (ddlRecordFilter == null || ddlRecordFilter.SelectedItem == null)
        {
            return 25;
        }

        string selected = ddlRecordFilter.SelectedItem.Text;
        if (string.Equals(selected, "All", StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        int pageSize;
        if (int.TryParse(selected, out pageSize) && pageSize > 0)
        {
            return pageSize;
        }

        return 25;
    }

    protected int GetSerialNumber(int dataItemIndex)
    {
        if (!GridView1.AllowPaging)
        {
            return dataItemIndex + 1;
        }

        return (GridView1.PageIndex * GridView1.PageSize) + dataItemIndex + 1;
    }

    public void Getsalary()
    {
        DataTable Dt = objUser.getUserDasboardproc(Session["userid"].ToString());
        if (Dt != null && Dt.Rows.Count > 0)
        {
            LblDownline.Text = Dt.Rows[0]["TotalTeam"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindDownline(true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDownline(true);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindDownline(false);
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        if (lblstatus != null)
        {
            lblstatus.CssClass = lblstatus.Text == "Unpaid"
                ? "team-status-badge team-status-unpaid"
                : "team-status-badge team-status-paid";
        }
    }
}
