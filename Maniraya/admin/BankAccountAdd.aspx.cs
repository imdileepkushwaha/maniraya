using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.IO;

public partial class admin_BankAccountAdd : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
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
        dt = objbank.getBankAccountList();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public string UploadImage()
    {
        string Imagename = "";
        if (ProductImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ProductImageUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ProductImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
    public string UploadImageedit()
    {
        string Imagename = "";
        if (FileUpload1.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objbank.BankName = txtdepositbank.Text;
        objbank.AccHolderName = txtaccountholdername.Text;
        objbank.AccNo = txtdepositaccountno.Text;
        objbank.IFSCCode = txtifsccode.Text;
        objbank.BranchName = UploadImage(); ;
        objbank.MentionBy = Session["useradmin"].ToString();
        string res = objbank.Insert_BankAccount(objbank);
        if (res == "t")
        {
            string popupScript = "alert('Account Added Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "resetAddBankQrUpload();", true);
            txtdepositbank.Text = "";
            txtaccountholdername.Text = "";
            txtdepositaccountno.Text = "";
            txtifsccode.Text = "";
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
            Label lblifsccode = (Label)GridView1.Rows[index].FindControl("lblimage");
            Image qrImage = (Image)GridView1.Rows[index].FindControl("lblbranchname");

            lblbankaccountid.Text = lblid.Text;
            txtaccholdernameedit.Text = lblaccountholdername.Text;
            txtaccountnoedit.Text = lblaccountno.Text;
            txtdepositbankedit.Text = lblbankname.Text;
            txtifsccodeedit.Text = lblifsccode.Text;

            string qrFile = qrImage != null ? qrImage.ImageUrl.Replace("../ProductImage/", "") : "";
            hfEditQrImage.Value = qrFile;
            ImageButton1.ImageUrl = "../ProductImage/" + qrFile;

            string previewUrl = "../ProductImage/" + qrFile.Replace("'", "\\'");
            string popupScript = "showAdminModal('myModal'); syncEditBankQrPreview('" + previewUrl + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Pop", popupScript, true);
        }
        if (e.CommandName == "del")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblid = (Label)GridView1.Rows[index].FindControl("lblid");
            string t = objbank.Delete_Bank(Convert.ToInt16(lblid.Text));
            if (t == "t")
            {
                loaddata();

                string popupScript = "alert('Bank Account Deleted Successfully');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('Bank Account Deleted Failed');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objbank.BankAccountId = lblbankaccountid.Text;
        objbank.BankName = txtdepositbankedit.Text;
        objbank.AccHolderName = txtaccholdernameedit.Text;
        objbank.AccNo = txtaccountnoedit.Text;
        objbank.IFSCCode = txtifsccodeedit.Text;
        string newQrImage = UploadImageedit();
        objbank.BranchName = string.IsNullOrEmpty(newQrImage) ? hfEditQrImage.Value : newQrImage;
        string res = objbank.Update_BankAccount(objbank);
        if (res == "t")
        {
            string popupScript = "alert('Bank Account Details Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "closeAdminModal('myModal');";
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