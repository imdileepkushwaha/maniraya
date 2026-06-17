using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataTier;

public partial class FAQAdd : System.Web.UI.Page
{
    Data ObjData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                FaqHelper.EnsureTableAndSeedDefaults();
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    private void BindGrid()
    {
        try
        {
            string sql = "SELECT * FROM tbl_FAQ ORDER BY Id DESC";
            ObjData.StartConnection();
            try {
                DataTable dt = ObjData.RunDataTable(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            } finally {
                ObjData.EndConnection();
            }
        }
        catch { }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtQuestion.Text) || string.IsNullOrWhiteSpace(txtAnswer.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Question and Answer are required');", true);
            return;
        }

        string question = txtQuestion.Text.Replace("'", "''");
        string answer = txtAnswer.Text.Replace("'", "''");
        int status = chkStatus.Checked ? 1 : 0;

        string sql = "";
        if (ViewState["EditId"] != null)
        {
            string id = ViewState["EditId"].ToString();
            sql = string.Format("UPDATE tbl_FAQ SET Question='{0}', Answer='{1}', Status={2} WHERE Id={3}", question, answer, status, id);
        }
        else
        {
            sql = string.Format("INSERT INTO tbl_FAQ (Question, Answer, Status) VALUES ('{0}', '{1}', {2})", question, answer, status);
        }

        try
        {
            ObjData.StartConnection();
            try {
                ObjData.RunInsUpDelQuery(sql);
            } finally {
                ObjData.EndConnection();
            }
            
            string msg = ViewState["EditId"] != null ? "FAQ Updated Successfully" : "FAQ Added Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + msg + "');", true);
            
            ClearForm();
            BindGrid();
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Error occurred. Please try again.');", true);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string id = lnk.CommandArgument;
        
        try
        {
            string sql = "SELECT * FROM tbl_FAQ WHERE Id=" + id;
            ObjData.StartConnection();
            DataTable dt = null;
            try {
                dt = ObjData.RunDataTable(sql);
            } finally {
                ObjData.EndConnection();
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                txtQuestion.Text = dt.Rows[0]["Question"].ToString();
                txtAnswer.Text = dt.Rows[0]["Answer"].ToString();
                chkStatus.Checked = Convert.ToBoolean(dt.Rows[0]["Status"]);
                ViewState["EditId"] = id;
                btnSubmit.Text = "Update";
            }
        }
        catch { }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string id = lnk.CommandArgument;
        
        try
        {
            string sql = "DELETE FROM tbl_FAQ WHERE Id=" + id;
            ObjData.StartConnection();
            try {
                ObjData.RunInsUpDelQuery(sql);
            } finally {
                ObjData.EndConnection();
            }
            
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('FAQ Deleted Successfully');", true);
            ClearForm();
            BindGrid();
        }
        catch { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        txtQuestion.Text = "";
        txtAnswer.Text = "";
        chkStatus.Checked = true;
        ViewState["EditId"] = null;
        btnSubmit.Text = "Submit";
    }
}
