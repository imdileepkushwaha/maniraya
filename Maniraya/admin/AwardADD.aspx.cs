using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class AwardADD : System.Web.UI.Page
{
    clsaward objAward = new clsaward();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            loaddata();
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objAward.getawardDetail();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objAward.awardName = Txtawradname.Text;
        objAward.Fromdate = TxtFRomdate.Text;
        objAward.todate = TxtToDate.Text;
        objAward.targetleft = TxtTargetLaeft.Text;
        objAward.targetright = TxtTargetRight.Text;

        string res = objAward.Insert_Award(objAward);
        if (res == "t")
        {
            string popupScript = "alert('Award Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            Txtawradname.Text = "";
            TxtFRomdate.Text = "";
            TxtToDate.Text = "";
            TxtTargetLaeft.Text = "";
            TxtTargetRight.Text = "";
            loaddata();
        }
        else if (res == "f")
        {
            string popupScript = "alert('Account Already Exists');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            Label lblawardname = (Label)GridView1.Rows[index].FindControl("labawardname");
            Label lblfromdate = (Label)GridView1.Rows[index].FindControl("lblfromdate");
            Label lbltodate = (Label)GridView1.Rows[index].FindControl("lbltodate");
            Label lbltargetleft = (Label)GridView1.Rows[index].FindControl("lbltargetleft");
            Label lbltargetright = (Label)GridView1.Rows[index].FindControl("lbltargetright");
            lblawardid.Text = lblid.Text;
            awardname.Text = lblawardname.Text;
            txtfromdateedit.Text = lblfromdate.Text;
            txttodateedit.Text = lbltodate.Text;
            txtargerleftdit.Text = lbltargetleft.Text;
            txttargetrightedit.Text = lbltargetright.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objAward.AwardId = lblawardid.Text;
        objAward.awardName = awardname.Text;
        objAward.Fromdate = txtfromdateedit.Text;
        objAward.todate = txttodateedit.Text;
        objAward.targetleft = txtargerleftdit.Text;
        objAward.targetright = txttargetrightedit.Text;
        string res = objAward.Update_Award(objAward);
        if (res == "t")
        {
            string popupScript = "alert('award Details Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loaddata();
        }
        else
        {
            string popupScript = "alert('Error', 'Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
    }
}