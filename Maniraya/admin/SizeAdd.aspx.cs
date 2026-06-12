using BusinessLogicTier;
using DataTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SizeAdd : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();
    Data objData = new Data();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loaddata();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

    void loaddata()
    {
        DataTable dt = objState.getSizeMaster();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string sizeId = lblcountryid.Text.Replace("'", "''");
        string sizeName = txtcountrynameedit.Text.Trim().Replace("'", "''");
        string status = Ddlststatus.SelectedValue.Replace("'", "''");

        objData.StartConnection();
        try
        {
            string checkSql = "select Id from sizeMaster where sizeName='" + sizeName + "' and Id !='" + sizeId + "'";
            DataTable dt = objData.RunDataTable(checkSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                string popupScript = "alert('Size already exists');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                return;
            }

            string sql = "update sizeMaster set sizeName='" + sizeName + "', Status='" + status + "' where Id='" + sizeId + "'";
            objData.RunInsUpDelQuery(sql);

            string successScript = "alert('Size updated successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), successScript, true);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "Closepopup();", true);
            loaddata();
        }
        catch
        {
            string popupScript = "alert('Unable to update size');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        finally
        {
            objData.EndConnection();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objState.Sizename = txtcountryname.Text;
        objState.MentionBy = Session["useradmin"].ToString();
        string res = objState.Insert_Size(objState);
        if (res == "t")
        {
            string popupScript = "alert('Size added successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            txtcountryname.Text = "";
            loaddata();
        }
        else if (res == "f")
        {
            string popupScript = "alert('Size already exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('Unknown error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblCountryname = (Label)GridView1.Rows[index].FindControl("lblCountryname");
            Label lblsize = (Label)GridView1.Rows[index].FindControl("lblsize");

            lblcountryid.Text = lblid.Text;
            txtcountrynameedit.Text = lblCountryname.Text;

            if (!string.IsNullOrEmpty(lblsize.Text) && Ddlststatus.Items.FindByValue(lblsize.Text) != null)
            {
                Ddlststatus.SelectedValue = lblsize.Text;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}
