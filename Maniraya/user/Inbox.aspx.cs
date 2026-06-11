using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using BusinessLogicTier;

public partial class Associate_Details : System.Web.UI.Page
{
    clsSupport objsupport = new clsSupport();
    string query = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {
                query = setquery();
                this.BindItemsList();
                if (GridView1.Rows.Count > 0)
                {
                    pnllist.Visible = true;
                    pnlempty.Visible = false;
                }
                else
                {
                    pnllist.Visible = false;
                    pnlempty.Visible = true;
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }
    }

    private int CurrentPage
    {
        get
        {
            object objPage = ViewState["_CurrentPage"];
            int _CurrentPage = 0;
            if (objPage == null)
            {
                _CurrentPage = 0;
            }
            else
            {
                _CurrentPage = (int)objPage;
            }
            return _CurrentPage;
        }
        set { ViewState["_CurrentPage"] = value; }
    }
    private int fistIndex
    {
        get
        {

            int _FirstIndex = 0;
            if (ViewState["_FirstIndex"] == null)
            {
                _FirstIndex = 0;
            }
            else
            {
                _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
            }
            return _FirstIndex;
        }
        set { ViewState["_FirstIndex"] = value; }
    }
    private int lastIndex
    {
        get
        {

            int _LastIndex = 0;
            if (ViewState["_LastIndex"] == null)
            {
                _LastIndex = 0;
            }
            else
            {
                _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
            }
            return _LastIndex;
        }
        set { ViewState["_LastIndex"] = value; }
    }

    PagedDataSource _PageDataSource = new PagedDataSource();

    /// <summary>
    /// Build DataTable to bind Main Items List
    /// </summary>
    /// <returns>DataTable</returns>
    private DataTable GetDataTable()
    {
        DataTable dtItems = new DataTable();
        objsupport.ToId = Session["userid"].ToString();
        dtItems = objsupport.getInboxnew(objsupport);
        return dtItems;
    }
    /// <summary>
    /// Binding Main Items List
    /// </summary>
    private void BindItemsList()
    {
        DataTable dataTable = this.GetDataTable();
        if(dataTable.Rows.Count>0)
        {
            _PageDataSource.DataSource = dataTable.DefaultView;
            _PageDataSource.AllowPaging = true;
            _PageDataSource.PageSize = 20;
            _PageDataSource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _PageDataSource.PageCount;
            this.lblPageInfo.Text = "Page " + (CurrentPage + 1) + " of " + _PageDataSource.PageCount;
            this.lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
            this.lbtnNext.Enabled = !_PageDataSource.IsLastPage;
            this.lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
            this.lbtnLast.Enabled = !_PageDataSource.IsLastPage;
            this.GridView1.DataSource = _PageDataSource;
            this.GridView1.DataBind();
            this.doPaging();
        }
    }

    /// <summary>
    /// Binding Paging List
    /// </summary>
    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");

        fistIndex = CurrentPage - 5;


        if (CurrentPage > 5)
        {
            lastIndex = CurrentPage + 5;
        }
        else
        {
            lastIndex = 10;
        }
        if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            fistIndex = lastIndex - 10;
        }

        if (fistIndex < 0)
        {
            fistIndex = 0;
        }

        for (int i = fistIndex; i < lastIndex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        this.ListPaging.DataSource = dt;
        this.ListPaging.DataBind();
    }



    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        query = setquery();
        CurrentPage += 1;
        this.BindItemsList();

    }
    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        query = setquery();
        CurrentPage -= 1;
        this.BindItemsList();

    }
    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {

    }
    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {

    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        query = setquery();

        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        this.BindItemsList();

    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        query = setquery();

        CurrentPage = 0;
        this.BindItemsList();


    }
    string setquery()
    {
        string s = "select * from SupportDetail where toid='" + Session["username"].ToString() + "'";
        return s;

    }
    protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            query = setquery();

            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
            this.BindItemsList();
        }
    }
    protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            //lnkbtnPage.Style.Add("fone-size", "14px");
            //lnkbtnPage.Font.Bold = true;
            //  lnkbtnPage.Font.Underline = true;

        }
    }

    protected void btnview_click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        if (btn == null)
        {
            return;
        }

        GridViewRow gvRow = btn.NamingContainer as GridViewRow;
        if (gvRow == null)
        {
            return;
        }

        Label lblmessagetitle = (Label)gvRow.FindControl("lblmessagetitle");
        Label lblmessagedescription = (Label)gvRow.FindControl("lblmessagedescription");
        Label lblfromidgrid = (Label)gvRow.FindControl("lblfromid");
        Label lblmessagedate = (Label)gvRow.FindControl("lbldate");
        Label lblAttachment = (Label)gvRow.FindControl("lblHyperLink");

        lblmodtitle.Text = lblmessagetitle != null && !string.IsNullOrWhiteSpace(lblmessagetitle.Text) ? lblmessagetitle.Text : "-";
        lblmoddescription.Text = lblmessagedescription != null && !string.IsNullOrWhiteSpace(lblmessagedescription.Text) ? lblmessagedescription.Text : "-";
        lblmodfrom.Text = lblfromidgrid != null && !string.IsNullOrWhiteSpace(lblfromidgrid.Text) ? lblfromidgrid.Text : "-";
        lblmoddate.Text = lblmessagedate != null && !string.IsNullOrWhiteSpace(lblmessagedate.Text) ? lblmessagedate.Text : "-";

        if (lblAttachment != null && !string.IsNullOrWhiteSpace(lblAttachment.Text))
        {
            pnlModAttachment.Visible = true;
            hlModAttachment.NavigateUrl = ResolveUrl("~/ProductImage/" + lblAttachment.Text);
            hlModAttachment.Text = lblAttachment.Text;
        }
        else
        {
            pnlModAttachment.Visible = false;
            hlModAttachment.NavigateUrl = "";
            hlModAttachment.Text = "";
        }

        pnlModal_ModalPopupExtender.Show();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPgAttachment = (Label)e.Row.FindControl("lblHyperLink");
            HyperLink lnkPgAttachment = (HyperLink)e.Row.FindControl("HyperLink1");
            if (string.IsNullOrEmpty(lblPgAttachment.Text))
            {
                lnkPgAttachment.CssClass = "fa fa-ban inbox-attach-btn is-empty";
                lnkPgAttachment.NavigateUrl = "";
                lnkPgAttachment.Enabled = false;
                lnkPgAttachment.ToolTip = "No attachment";
            }
            else
            {
                lnkPgAttachment.CssClass = "fa fa-download inbox-attach-btn is-available";
                lnkPgAttachment.ToolTip = "Download attachment";
            }
        }
    }
}