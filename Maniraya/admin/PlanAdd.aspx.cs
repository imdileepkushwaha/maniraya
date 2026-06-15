using BusinessLogicTier;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_PlanAdd : System.Web.UI.Page
{
    private readonly clsplan objPlan = new clsplan();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        EnsureAutoCreateDate(txtCreateDate);

        if (!IsPostBack)
        {
            LoadPlans();
        }
    }

    private static void EnsureAutoCreateDate(TextBox createDateBox)
    {
        if (createDateBox == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(createDateBox.Text))
        {
            createDateBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    private void LoadPlans()
    {
        DataTable dt = objPlan.GetPlanMasterList();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPlanName.Text = string.Empty;
        txtPlanAmount.Text = string.Empty;
        txtBusinessVolume.Text = string.Empty;
        txtCappingAmount.Text = string.Empty;
        txtCreateDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string planName = txtPlanName.Text.Trim();
        if (string.IsNullOrWhiteSpace(planName))
        {
            ShowAlert("Enter Plan Name");
            return;
        }

        if (objPlan.PlanNameExists(planName, string.Empty))
        {
            ShowAlert("Plan name already exists");
            return;
        }

        decimal planAmount;
        decimal businessVolume;
        decimal cappingAmount;
        DateTime createDate;

        if (!TryParsePlanInputs(txtPlanAmount.Text, txtBusinessVolume.Text, txtCappingAmount.Text, txtCreateDate.Text, out planAmount, out businessVolume, out cappingAmount, out createDate))
        {
            return;
        }

        objPlan.PlanName = planName;
        objPlan.PlanAmount = planAmount;
        objPlan.BuisnessVolume = businessVolume;
        objPlan.CappingAmount = cappingAmount;
        objPlan.CreateDate = DateTime.Today;

        string res = objPlan.Insert_PlanMaster(objPlan);
        if (res == "t")
        {
            ShowAlert("Plan added successfully");
            btnClear_Click(sender, e);
            LoadPlans();
        }
        else
        {
            ShowAlert("Unable to save plan. Please verify Planmaster table columns and try again.");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string planName = txtPlanNameEdit.Text.Trim();
        if (string.IsNullOrWhiteSpace(planName))
        {
            ShowAlert("Enter Plan Name");
            return;
        }

        if (objPlan.PlanNameExists(planName, lblPlanIdEdit.Text))
        {
            ShowAlert("Plan name already exists");
            return;
        }

        decimal planAmount;
        decimal businessVolume;
        decimal cappingAmount;
        DateTime createDate;

        if (!TryParsePlanInputs(txtPlanAmountEdit.Text, txtBusinessVolumeEdit.Text, txtCappingAmountEdit.Text, txtCreateDateEdit.Text, out planAmount, out businessVolume, out cappingAmount, out createDate))
        {
            return;
        }

        objPlan.id = lblPlanIdEdit.Text;
        objPlan.PlanName = planName;
        objPlan.PlanAmount = planAmount;
        objPlan.BuisnessVolume = businessVolume;
        objPlan.CappingAmount = cappingAmount;
        objPlan.CreateDate = createDate;

        string res = objPlan.Update_PlanMaster(objPlan);
        if (res == "t")
        {
            ShowAlert("Plan updated successfully");
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "closePlanModal", "closePlanEditModal();", true);
            LoadPlans();
        }
        else
        {
            ShowAlert("Unable to update plan");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "edt")
        {
            return;
        }

        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lblPlanId = (Label)GridView1.Rows[index].FindControl("lblPlanId");
        Label lblPlanName = (Label)GridView1.Rows[index].FindControl("lblPlanName");
        Label lblPlanAmount = (Label)GridView1.Rows[index].FindControl("lblPlanAmount");
        Label lblBusinessVolume = (Label)GridView1.Rows[index].FindControl("lblBusinessVolume");
        Label lblCreateDateValue = (Label)GridView1.Rows[index].FindControl("lblCreateDateValue");
        Label lblCappingAmount = (Label)GridView1.Rows[index].FindControl("lblCappingAmount");

        lblPlanIdEdit.Text = lblPlanId.Text;
        txtPlanNameEdit.Text = lblPlanName.Text;
        txtPlanAmountEdit.Text = ParseGridDecimal(lblPlanAmount.Text).ToString(CultureInfo.InvariantCulture);
        txtBusinessVolumeEdit.Text = ParseGridDecimal(lblBusinessVolume.Text).ToString(CultureInfo.InvariantCulture);
        txtCappingAmountEdit.Text = ParseGridDecimal(lblCappingAmount.Text).ToString(CultureInfo.InvariantCulture);
        txtCreateDateEdit.Text = !string.IsNullOrWhiteSpace(lblCreateDateValue.Text)
            ? lblCreateDateValue.Text
            : DateTime.Today.ToString("yyyy-MM-dd");

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "openPlanModal", "openPlanEditModal();", true);
    }

    private bool TryParsePlanInputs(string planAmountText, string businessVolumeText, string cappingAmountText, string createDateText, out decimal planAmount, out decimal businessVolume, out decimal cappingAmount, out DateTime createDate)
    {
        planAmount = 0;
        businessVolume = 0;
        cappingAmount = 0;
        createDate = DateTime.Now;

        if (!decimal.TryParse(planAmountText, NumberStyles.Number, CultureInfo.InvariantCulture, out planAmount)
            && !decimal.TryParse(planAmountText, NumberStyles.Number, CultureInfo.CurrentCulture, out planAmount))
        {
            ShowAlert("Enter valid Plan Amount");
            return false;
        }

        if (!decimal.TryParse(businessVolumeText, NumberStyles.Number, CultureInfo.InvariantCulture, out businessVolume)
            && !decimal.TryParse(businessVolumeText, NumberStyles.Number, CultureInfo.CurrentCulture, out businessVolume))
        {
            ShowAlert("Enter valid Business Volume");
            return false;
        }

        if (!decimal.TryParse(cappingAmountText, NumberStyles.Number, CultureInfo.InvariantCulture, out cappingAmount)
            && !decimal.TryParse(cappingAmountText, NumberStyles.Number, CultureInfo.CurrentCulture, out cappingAmount))
        {
            ShowAlert("Enter valid Capping Amount");
            return false;
        }

        if (!DateTime.TryParse(createDateText, CultureInfo.InvariantCulture, DateTimeStyles.None, out createDate)
            && !DateTime.TryParse(createDateText, CultureInfo.CurrentCulture, DateTimeStyles.None, out createDate))
        {
            createDate = DateTime.Today;
        }
        else
        {
            createDate = createDate.Date;
        }

        return true;
    }

    private static decimal ParseGridDecimal(string value)
    {
        decimal amount;
        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
        {
            return amount;
        }

        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out amount))
        {
            return amount;
        }

        return 0;
    }

    private void ShowAlert(string message)
    {
        string popupScript = "alert('" + message.Replace("'", "\\'") + "');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
    }
}
