using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using DataTier;

public partial class admin_UserReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsUser objuser = new clsUser();
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loadprevproduct();
    }

    public DataTable getPrevProduct()
    {
        string str_query = @"SELECT ud.username, sd.*, pm.productname, pm.ImageName AS imagename
            FROM SavingAccountDetail sd WITH (NOLOCK)
            LEFT JOIN savingproductmaster pm WITH (NOLOCK) ON sd.productid = pm.id
            LEFT JOIN userdetail ud WITH (NOLOCK) ON ud.userid = sd.userid
            WHERE 1=1 ";

        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            str_query += " AND CONVERT(date, sd.entrydate) >= CONVERT(date,'" + Message.GetIndianDate(txtfromdate.Text)
                + "') AND CONVERT(date, sd.entrydate) <= CONVERT(date,'" + Message.GetIndianDate(txttodate.Text) + "') ";
        }

        if (ddstatus.SelectedValue != "0")
        {
            str_query += " AND sd.status = '" + ddstatus.SelectedValue.Replace("'", "''") + "' ";
        }

        if (!string.IsNullOrWhiteSpace(txtuserid.Text))
        {
            str_query += " AND sd.UserId = '" + txtuserid.Text.Trim().Replace("'", "''") + "' ";
        }

        str_query += " ORDER BY sd.entrydate DESC";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }

    void loadprevproduct()
    {
        DataTable dt = getPrevProduct();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void grdGetHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        Label lblstatus = (Label)e.Row.FindControl("lblstatus");
        Label lblremark = (Label)e.Row.FindControl("lblremark");
        TextBox txtremark = (TextBox)e.Row.FindControl("txtremark");
        LinkButton btnApprove = (LinkButton)e.Row.FindControl("btnApprove");
        LinkButton btnReject = (LinkButton)e.Row.FindControl("btnReject");

        if (lblremark != null)
        {
            lblremark.Visible = false;
        }

        if (txtremark != null)
        {
            txtremark.Visible = false;
        }

        if (lblstatus == null)
        {
            return;
        }

        if (lblstatus.Text == "Pending")
        {
            lblstatus.CssClass = "label label-warning";
            if (btnApprove != null) btnApprove.Visible = true;
            if (btnReject != null) btnReject.Visible = true;
            if (txtremark != null) txtremark.Visible = true;
        }
        else if (lblstatus.Text == "Approved")
        {
            lblstatus.CssClass = "label label-success";
            if (btnApprove != null) btnApprove.Visible = false;
            if (btnReject != null) btnReject.Visible = false;
            if (lblremark != null) lblremark.Visible = true;
        }
        else if (lblstatus.Text == "Rejected")
        {
            lblstatus.Text = "Cancelled";
            lblstatus.CssClass = "label label-danger";
            if (btnApprove != null) btnApprove.Visible = false;
            if (btnReject != null) btnReject.Visible = false;
            if (lblremark != null) lblremark.Visible = true;
        }
    }

    protected void btnApprove_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((Control)sender).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");

        string res = Approve_ProductPurchase(lblgalleryid.Text, Session["useradmin"].ToString());
        if (res == "t")
        {
            SetInitialDeliveryStatus(lblgalleryid.Text);
            ShowAlert("Purchase Approved Successfully");
            loadprevproduct();
        }
        else if (res == "f")
        {
            ShowAlert("Purchase Already Processed");
            loadprevproduct();
        }
        else
        {
            ShowAlert("Something wrong");
            loadprevproduct();
        }
    }

    public string Approve_ProductPurchase(string str_id, string str_approveby)
    {
        string res = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            string s2 = "sp_approveSavingAccountDetail";
            SqlParameter[] parameter = {
                new SqlParameter("@id", str_id),
                new SqlParameter("@Approveby", str_approveby)
            };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch
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

    void SetInitialDeliveryStatus(string recordId)
    {
        try
        {
            SavingProductHelper.EnsureDeliveryColumns();
            ObjData.StartConnection();
            try
            {
                ObjData.RunInsUpDelQuery("UPDATE SavingAccountDetail SET DeliveryStatus='Confirmed' WHERE id=" + recordId);
            }
            finally
            {
                ObjData.EndConnection();
            }
        }
        catch
        {
        }
    }

    public string Reject_ProductPurchase(string str_id, string str_approveby)
    {
        string res = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            string s2 = "sp_rejectSavingAccountDetail";
            SqlParameter[] parameter = {
                new SqlParameter("@id", str_id),
                new SqlParameter("@Approveby", str_approveby)
            };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch
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

    protected void btnReject_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((Control)sender).Parent.Parent;
        Label lblgalleryid = (Label)gvRow.FindControl("lblId");

        string res = Reject_ProductPurchase(lblgalleryid.Text, Session["useradmin"].ToString());
        if (res == "t")
        {
            ShowAlert("Purchase Rejected Successfully");
            loadprevproduct();
        }
        else if (res == "f")
        {
            ShowAlert("Purchase Already Processed");
            loadprevproduct();
        }
        else
        {
            ShowAlert("Something wrong");
            loadprevproduct();
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "photolarge")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label LblImage = (Label)GridView1.Rows[index].FindControl("LblImage");
            ImageLarge.ImageUrl = "../ProductImage/" + LblImage.Text;
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Pop", "showAdminModal('DivPhotolarge');", true);
        }
    }

    void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}
