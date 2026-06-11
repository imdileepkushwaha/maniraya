using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_MessageSetting : System.Web.UI.Page
{
    clsSetting objsetting = new clsSetting();
    public string LoginId = "";
    public int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            fillData();
        }
    }
    void fillData()
    {
        DataTable dt = new DataTable();
        dt = objsetting.Get_Message_All_Format();
        if (dt.Rows.Count > 0)
        {
            txt_FundTransferTo.Text = dt.Rows[0]["FundTransfer"].ToString();
            txt_FundTransferFrom.Text = dt.Rows[0]["FundReceive"].ToString();
            txt_FundDebit.Text = dt.Rows[0]["FundDebit"].ToString();
            txt_FundCredit.Text = dt.Rows[0]["FundCredit"].ToString();
            txt_Registration.Text = dt.Rows[0]["Registration"].ToString();
            txt_RechAccept.Text = dt.Rows[0]["RechargeAccept"].ToString();
            txt_RechSuccess.Text = dt.Rows[0]["RechargeSuccess"].ToString();
            txt_RechFailed.Text = dt.Rows[0]["RechargeFailed"].ToString();
            txt_OperatorUP.Text = dt.Rows[0]["OperatorUPMessage"].ToString();
            txt_OperatorDown.Text = dt.Rows[0]["OperatorDownMessage"].ToString();

            txtRefund.Text = dt.Rows[0]["RechargeRefund"].ToString();
            string Acc = dt.Rows[0]["RechAccSts"].ToString();
            if (Acc == "True")
                chk_Acc.Checked = true;
            else
                chk_Acc.Checked = false;
            string Refund = dt.Rows[0]["RechRefund"].ToString();
            if (Refund == "True")
                Chk_Refund.Checked = true;
            else
                Chk_Refund.Checked = false;
            string Succ = dt.Rows[0]["RechSuccSts"].ToString();
            if (Succ == "True")
                chk_succ.Checked = true;
            else
                chk_succ.Checked = false;
            string fail = dt.Rows[0]["RechFailSts"].ToString();
            if (fail == "True")
                chk_fail.Checked = true;
            else
                chk_fail.Checked = false;
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string RechAcc = "0";
            string RechSucc = "0";
            string RechFail = "0";
            string ChkRefund = "0";
            if (chk_Acc.Checked == true)
                RechAcc = "1";
            if (chk_succ.Checked == true)
                RechSucc = "1";
            if (chk_fail.Checked == true)
                RechFail = "1";
            if (chk_fail.Checked == true)
                RechFail = "1";
            if (Chk_Refund.Checked == true)
                ChkRefund = "1";
            objsetting.Insert_Message_Formats( txt_Registration.Text, txt_FundTransferTo.Text, txt_FundTransferFrom.Text, txt_FundDebit.Text, txt_FundCredit.Text, txt_RechAccept.Text, txt_RechSuccess.Text, txt_RechFailed.Text, LoginId, RechAcc, RechSucc, RechFail, txt_OperatorUP.Text, txt_OperatorDown.Text, ChkRefund, txtRefund.Text);
            fillData();
            string popupScript = "alert('Data Saved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        catch (Exception ex)
        {
            return;
        }
    }
}