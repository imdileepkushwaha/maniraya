using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class JoiningPlanAdd : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
    clsplan objplan = new clsplan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loaddata();
                loaddataplantype();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loaddata()
    {
        DataTable dt = new DataTable();
        dt = objplan.getPlanAll();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    void loaddataplantype()
    {
        DataTable dt = new DataTable();
        dt = objplan.getOperatorType();
        GridView2.DataSource = dt;
        GridView2.DataBind();
        GridView3.DataSource = dt;
        GridView3.DataBind();
    }
    private string getoperatorpermission()
    {
        int i = 0;
        string operatorpermission = "";
        foreach (GridViewRow r in GridView2.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");           
            if (ChStatus.Checked == true)
            {
                if (i == 0)
                {
                    operatorpermission += lbllevel.Text;
                }
                else
                {
                    operatorpermission += ","+lbllevel.Text;
                }
            }
            else
            {
               
            }
            i++;
        }
        return operatorpermission;
    }
    private string getoperatorpermissionedit()
    {
        int i = 0;
        string operatorpermission = "";
        foreach (GridViewRow r in GridView3.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
            if (ChStatus.Checked == true)
            {
                if (i == 0)
                {
                    operatorpermission += lbllevel.Text;
                }
                else
                {
                    operatorpermission += "," + lbllevel.Text;
                }
            }
            else
            {

            }
            i++;
        }
        return operatorpermission;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objplan.operatorPermission = getoperatorpermission();
        objplan.PlanName = TxtPlanname.Text;
       objplan.PlanAmount = Convert.ToInt32(TxtPlanAmount.Text);
       objplan.BuisnessVolume = Convert.ToInt32(TxtBV.Text);
       objplan.MonthlyAmount = TxtMonthlyAmount.Text == "" ? "0" : TxtMonthlyAmount.Text;
       objplan.CountMonthly = TxtMonthCount.Text == "" ? "0" : TxtMonthCount.Text;
       objplan.MoneyTransfer = "0";     

        string res = objplan.Insert_Plan(objplan);
        if (res == "t")
        {
            string popupScript = "alert('Plan Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
           TxtPlanname.Text=string.Empty;
            TxtPlanAmount.Text = "";
           // TxtBV.Text = "";           
            loaddata();
            loaddataplantype();
            ChkMoneyTransfer.Checked = false;
        }
        else if (res == "f")
        {
            string popupScript = "alert('Plan Already Exists');";
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
            Label lblaccountholdername = (Label)GridView1.Rows[index].FindControl("lblaccountholdername");
            Label lblaccountno = (Label)GridView1.Rows[index].FindControl("lblaccountno");
            Label LblMonthlyAmount = (Label)GridView1.Rows[index].FindControl("LblMonthlyAmount");
            Label LblCountMonth = (Label)GridView1.Rows[index].FindControl("LblCountMonth");   
              Label LblMoneyTransfer = (Label)GridView1.Rows[index].FindControl("LblMoneyTransfer");   
              Label LblOperatorMermission = (Label)GridView1.Rows[index].FindControl("LblOperatorMermission");
              Label lblaccountno2 = (Label)GridView1.Rows[index].FindControl("lblaccountno2");   

            lblbankaccountid.Text = lblid.Text;
            TxtBvEdit.Text = lblaccountno2.Text;
            TxtPlanNameEdit.Text = lblaccountholdername.Text;
            TxtPlanAmountEdit.Text = lblaccountno.Text;
            TxtBuisnessVolume.Text = "0";
            TxtMonthlyAmountEdit.Text = LblMonthlyAmount.Text;
            TxtMonthlyCountEdit.Text = LblCountMonth.Text;
            fetchpermission(LblOperatorMermission.Text);
            if (LblMoneyTransfer.Text == "YES")
            {
                ChkMoneyTransferEdit.Checked = true;
            }
            else
            {
                ChkMoneyTransferEdit.Checked = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objplan.id = lblbankaccountid.Text;
        objplan.PlanName = TxtPlanNameEdit.Text;
        objplan.PlanAmount = Convert.ToInt32(TxtPlanAmountEdit.Text);
        objplan.BuisnessVolume = Convert.ToInt32(TxtBvEdit.Text);
        objplan.operatorPermission = getoperatorpermissionedit();
        objplan.MonthlyAmount = TxtMonthlyAmountEdit.Text;
        objplan.CountMonthly = TxtMonthlyCountEdit.Text;
        if (ChkMoneyTransferEdit.Checked == true)
        {
            objplan.MoneyTransfer = "1";
        }
        else
        {
            objplan.MoneyTransfer = "0";
        }
        string res = objplan.Update_Plan(objplan);
        if (res == "t")
        {
            string popupScript = "alert('Plan Details Edited Successfully');";
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
    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView2.HeaderRow.FindControl("checkAll");
        foreach (GridViewRow row in GridView2.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ChkStatus");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    private string fetchpermission(string Oppermission)
    {
        int i = 0;
        string operatorpermission = "";
        string[] op = Oppermission.Split(',');
        foreach (GridViewRow r in GridView3.Rows)
        {
            Label lbllevel = (Label)r.FindControl("lblid");
            CheckBox ChStatus = (CheckBox)r.FindControl("ChkStatus");
           foreach(string d in op)
           {
               if (lbllevel.Text == d)
               {
                   ChStatus.Checked = true;
               }
           }
        }
        return operatorpermission;
    }
}