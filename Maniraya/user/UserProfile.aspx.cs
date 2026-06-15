using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserProfile : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsUser objUser = new clsUser();
    clsBank objbank = new clsBank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {

                loadcountry();
                loadbank();
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
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = objUser.getUserDetail(objUser);
        if (dt == null || dt.Rows.Count == 0)
        {
            txtname.Text = Convert.ToString(Session["username"]);
            return;
        }

        DataRow row = dt.Rows[0];
        txtsponserid.Text = GetRowValue(row, "sponserid");
        loadsusername();
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
        try
        {
            string dob = GetRowValue(row, "dateofbirth");
            if (!string.IsNullOrWhiteSpace(dob))
            {
                txtdateofbirth.Text = Convert.ToDateTime(dob).ToString("dd/MM/yyyy");
            }
        }
        catch { }
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
            string actualColumn = FindColumnName(row.Table, columnName);
            if (actualColumn == null)
            {
                continue;
            }

            object value = row[actualColumn];
            if (value != null && value != DBNull.Value)
            {
                return Convert.ToString(value).Trim();
            }
        }

        return string.Empty;
    }

    static string FindColumnName(DataTable table, string columnName)
    {
        if (table == null || string.IsNullOrWhiteSpace(columnName))
        {
            return null;
        }

        if (table.Columns.Contains(columnName))
        {
            return columnName;
        }

        foreach (DataColumn column in table.Columns)
        {
            if (string.Equals(column.ColumnName, columnName, StringComparison.OrdinalIgnoreCase))
            {
                return column.ColumnName;
            }
        }

        return null;
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
        DataTable dt = new DataTable();
        dt = objbank.getBank();
        ddbank.DataSource = dt;
        ddbank.DataTextField = "BankName";
        ddbank.DataValueField = "BankID";
        ddbank.DataBind();
        ListItem li = new ListItem("Select Bank", "0");
        ddbank.Items.Insert(0, li);
    }
    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objUser.UserId = Session["userid"].ToString();
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.Gender = ddgender.SelectedValue.ToString();
        objUser.Address = txtaddress.Text;
        objUser.CityId = ddcity.SelectedValue.ToString();
        objUser.CountryId = ddcountry.SelectedValue.ToString();
        objUser.StateId = ddstate.SelectedValue.ToString();
        objUser.AreaName = txtareaname.Text;
        objUser.Pincode = txtpincode.Text;
        objUser.DateOfBirth = Message.GetIndianDate(txtdateofbirth.Text);
        objUser.MentionBy = Session["userid"].ToString();
        objUser.NomineeName = txtnomineename.Text;
        objUser.NomineeRelation = txtnomineerelation.Text;
        objUser.AccHolderName = txtaccountholdername.Text;
        objUser.AccNo = txtaccountno.Text;
        objUser.IFSCCode = txtifsccode.Text;
        objUser.PanCardNo = txtpan.Text;
        objUser.BankName = ddbank.SelectedValue.ToString();
        objUser.BranchName = txtbranchname.Text;
        string res = objUser.Update_UserProfile(objUser);
        if (res == "f")
        {
            string popupScript = "alert('User Not Found.');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
            if (res == "0")
            {
                string popupScript = "alert('Unknow error occurred');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('User Details Updated Successfully.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }

    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtsponserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtsponsername.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            if (txtsponserid.Text == "0")
            {

                txtsponsername.Text = "Company";
                txtsponserid.Text = "0";
            }
            else
            {
                txtsponsername.Text = "";
                txtsponserid.Text = "";
                string popupScript = "alert('Invalid User Id');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}