using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_UserReport : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {

            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lbluserid = (Label)GridView1.Rows[index].FindControl("lbluserid");
            Label lblmobile = (Label)GridView1.Rows[index].FindControl("lblmobile");
            Label lblstatus = (Label)GridView1.Rows[index].FindControl("lblstatus");
            Label lblcallresultstatus = (Label)GridView1.Rows[index].FindControl("lblcallresultstatus");
            Label lblpickedby = (Label)GridView1.Rows[index].FindControl("lblpickedby");
            Label lblremark = (Label)GridView1.Rows[index].FindControl("lblremark");

            
            lblcallbackrequestid.Text = lblid.Text;
            txtuseridedit.Text = lbluserid.Text;
            txtmobile.Text = lblmobile.Text;
            rbstatus.SelectedValue = lblstatus.Text;
            txtremark.Text = lblremark.Text;
            ddpickedby.SelectedValue = lblpickedby.Text;
            rbresultstatus.SelectedValue = lblcallresultstatus.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {
        if (txtfromdate.Text != "")
        {
            objuser.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objuser.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objuser.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objuser.ToDate = DateTime.MinValue;
        }
        objuser.UserId = txtuserid.Text;
        objuser.CallStatus = ddcallstatus.SelectedValue.ToString();
        objuser.CallResultStatus = ddresultstatus.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objuser.getCallbackReport(objuser);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (rbstatus.SelectedValue != null)
        {
            objuser.Id = lblcallbackrequestid.Text;
            objuser.CallStatus = rbstatus.SelectedValue.ToString();
            objuser.Remark = txtremark.Text;
            objuser.CallResultStatus = rbresultstatus.SelectedValue.ToString();
            objuser.PickedBy = ddpickedby.SelectedValue.ToString();
            string res = objuser.EditCallbackRequest(objuser);
            if (res == "t")
            {
                string popupScript = "alert('Record Edited Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript2 = "Closepopup();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);

            }
            else
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript2 = "Closepopup();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            }
            loaduser();
        }
        else
        {
            string popupScript = "alert('Select Call Status');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
            Label lblcallresultstatus = (Label)e.Row.FindControl("lblcallresultstatus");
            if (lblcallresultstatus.Text == "Opened")
            {
                lbEdit.Visible = true;
            }
            else
            {
                lbEdit.Visible = false;
            
            }
        }
    }
}