using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class VendorMaster : System.Web.UI.Page
{
    clsvendor objbank = new clsvendor();
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
        dt = objbank.getVendorList();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objbank.CompanyName = TxtCompanyname.Text;
        objbank.Ownername = TxtOwnername.Text;
        objbank.ContactNo = TxtContactNo.Text;
        objbank.Address = TxtAddress.Text;
        objbank.GSTNo = TxtGStNo.Text;
        objbank.MentionBy = Session["useradmin"].ToString();
        string res = objbank.Insert_Vendor(objbank);
        if (res == "t")
        {
            string popupScript = "alert('Vendor Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            TxtCompanyname.Text = "";
            TxtOwnername.Text = "";
            TxtContactNo.Text = "";
            TxtAddress.Text = "";
            TxtGStNo.Text = "";
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
            Label lblaccountholdername = (Label)GridView1.Rows[index].FindControl("lblaccountholdername");
            Label lblaccountno = (Label)GridView1.Rows[index].FindControl("lblaccountno");
            Label lblbankname = (Label)GridView1.Rows[index].FindControl("lblbankname");
            Label lblifsccode = (Label)GridView1.Rows[index].FindControl("lblifsccode");
            Label lblbranchname = (Label)GridView1.Rows[index].FindControl("lblbranchname");
            LblVendorId.Text = lblid.Text;
            TxtCompanyNameEdit.Text = lblaccountholdername.Text;
            TxtOwnernameEdit.Text = lblaccountno.Text;
            TxtConatctNoEdit.Text = lblbankname.Text;
            TxtAddressEdit.Text = lblifsccode.Text;
            TxtGstNoEdit.Text = lblbranchname.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objbank.VendorId = LblVendorId.Text;
        objbank.CompanyName = TxtCompanyNameEdit.Text;
        objbank.Ownername = TxtOwnernameEdit.Text;
        objbank.ContactNo = TxtConatctNoEdit.Text;
        objbank.Address = TxtAddressEdit.Text;
        objbank.GSTNo = TxtGstNoEdit.Text;
        string res = objbank.Update_BankAccount(objbank);
        if (res == "t")
        {
            string popupScript = "alert('Vendor Details Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            loaddata();
        }
        else
        {
            string popupScript = "alert('Error', 'Unknow error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
}