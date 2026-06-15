using BusinessLogicTier;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class franchisee_UserEdit : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsfranchisee objUser = new clsfranchisee();
    clsBank objbank = new clsBank();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fuserid"] == null)
        {
            Response.Redirect("logout.aspx");
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                loadcountry();
                loadbank();
                loaddata();
            }
            catch (Exception)
            {
                // Keep form visible even if a lookup fails.
            }
        }
    }

    void loaddata()
    {
        objUser.UserId = Session["fuserid"].ToString();
        DataTable dt = objUser.getUserDetail(objUser);
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }

        DataRow row = dt.Rows[0];
        txtname.Text = GetRowValue(row, "username");
        txtmobile.Text = GetRowValue(row, "mobile");
        txtemail.Text = GetRowValue(row, "email");
        SelectListValue(ddgender, GetRowValue(row, "gender"));
        txtaddress.Text = GetRowValue(row, "address");
        SelectListValue(ddcountry, GetRowValue(row, "countryid"));
        loadstate();
        SelectListValue(ddstate, GetRowValue(row, "stateid"));
        loadcity();
        SelectListValue(ddcity, GetRowValue(row, "cityid"));
        txtareaname.Text = GetRowValue(row, "areaname");
        txtpincode.Text = GetRowValue(row, "pincode");

        string dob = GetRowValue(row, "dateofbirth");
        if (!string.IsNullOrWhiteSpace(dob))
        {
            try
            {
                txtdateofbirth.Text = Convert.ToDateTime(dob).ToString("dd/MM/yyyy");
            }
            catch
            {
                txtdateofbirth.Text = dob;
            }
        }

        txtnomineename.Text = GetRowValue(row, "nomineename");
        txtnomineerelation.Text = GetRowValue(row, "nomineerelation");
        txtaccountholdername.Text = GetRowValue(row, "accountholdername", "AccountHolderName");
        txtaccountno.Text = GetRowValue(row, "accountno", "AccountNo");
        txtpan.Text = GetRowValue(row, "pannumber", "PanNumber");
        txtifsccode.Text = GetRowValue(row, "ifsccode", "IFSCCode");
        txtbranchname.Text = GetRowValue(row, "branchname", "BranchName");
        SelectListValue(ddbank, GetRowValue(row, "bankname", "BankName", "BankID"));
    }

    static string GetRowValue(DataRow row, params string[] columnNames)
    {
        if (row == null || row.Table == null)
        {
            return string.Empty;
        }

        foreach (string columnName in columnNames)
        {
            if (string.IsNullOrWhiteSpace(columnName) || !row.Table.Columns.Contains(columnName))
            {
                continue;
            }

            object value = row[columnName];
            if (value != null && value != DBNull.Value)
            {
                return Convert.ToString(value);
            }
        }

        return string.Empty;
    }

    static void SelectListValue(ListControl list, string value)
    {
        if (list == null || string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        ListItem item = list.Items.FindByValue(value);
        if (item != null)
        {
            list.ClearSelection();
            item.Selected = true;
        }
    }

    void loadbank()
    {
        ddbank.Items.Clear();
        DataTable dt = objbank.getBank();
        ddbank.DataSource = dt;
        ddbank.DataTextField = "BankName";
        ddbank.DataValueField = "BankID";
        ddbank.DataBind();
        ddbank.Items.Insert(0, new ListItem("Select Bank", "0"));
    }

    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ddcountry.Items.Insert(0, new ListItem("Select Country", "0"));
    }

    void loadstate()
    {
        ddstate.Items.Clear();
        objState.CountryId = ddcountry.SelectedValue;
        DataTable dt = objState.getState(objState);
        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ddstate.Items.Insert(0, new ListItem("Select State", "0"));
    }

    void loadcity()
    {
        ddcity.Items.Clear();
        objState.StateId = ddstate.SelectedValue;
        DataTable dt = objState.getCity(objState);
        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ddcity.Items.Insert(0, new ListItem("Select City", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objUser.UserId = Session["fuserid"].ToString();
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.Gender = ddgender.SelectedValue;
        objUser.Address = txtaddress.Text;
        objUser.CityId = ddcity.SelectedValue;
        objUser.CountryId = ddcountry.SelectedValue;
        objUser.StateId = ddstate.SelectedValue;
        objUser.AreaName = txtareaname.Text;
        objUser.Pincode = txtpincode.Text;
        objUser.DateOfBirth = Message.GetIndianDate(txtdateofbirth.Text);
        objUser.MentionBy = Session["fuserid"].ToString();
        objUser.NomineeName = txtnomineename.Text;
        objUser.NomineeRelation = txtnomineerelation.Text;
        objUser.AccHolderName = txtaccountholdername.Text;
        objUser.AccNo = txtaccountno.Text;
        objUser.IFSCCode = txtifsccode.Text;
        objUser.PanCardNo = txtpan.Text;
        objUser.BankName = ddbank.SelectedValue;
        objUser.BranchName = txtbranchname.Text;

        string res = objUser.Update_UserProfile(objUser);
        if (res == "f")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('User Not Found.');", true);
        }
        else if (res == "0")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('Unknow error occurred');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('User Details Updated Successfully.');", true);
        }
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
        ddcity.Items.Clear();
        ddcity.Items.Insert(0, new ListItem(" Select City", "0"));
    }

    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}
