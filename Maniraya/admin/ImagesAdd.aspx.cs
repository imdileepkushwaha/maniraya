using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImagesAdd : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadProduct();
                loadimage();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadProduct()
    {
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        objState.ProductId = string.Empty;

        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getProductAll(objState);
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "ProductName";
        ddcountry.DataValueField = "ProductId";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Product", "0");
        ddcountry.Items.Insert(0, li);
        string res = objState.GetUpdateBannerStatus("Select",0);
        ddlstatus.SelectedValue = res;
    }

    public void loadimage()
    {
        objState.ProductId = null;
        objState.Status = null;
        objState.CategoryId = null;
        objState.ProductImage = null;
        DataTable dt = objState.GetImages(objState);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
   
    public string UploadImage()
    {
        string Imagename = "";
        if (FileUpload1.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            Imagename = RandomNumber+ fileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);
            
        }
        return Imagename;
    }

    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string res = objState.GetUpdateBannerStatus("Update",Convert.ToInt16(ddlstatus.SelectedValue));
        if (res == "t")
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('Status Updated Successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "alert('Status Update Failed');", true);
        }
    }
    

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        if (ddcountry.SelectedIndex > 0)
        {

            if ((FileUpload1.HasFile && btnSubmit.Text == "Submit") || btnSubmit.Text == "Update")
            {
                objState.CategoryId = btnSubmit.Text == "Submit"? "0": ViewState["PID"].ToString();
                objState.ProductImage = UploadImage();
                if (string.IsNullOrEmpty(objState.ProductImage))
                {
                    objState.ProductImage = ViewState["Image"].ToString();
                }
                objState.ProductId = ddcountry.SelectedValue.ToString();
                objState.Status = "1";
                string res = objState.Insert_Images(objState, (btnSubmit.Text == "Submit"?"Insert": "Update"));
                if (res == "t")
                {
                    loadimage();
                    string popupScript = "";
                    if(btnSubmit.Text == "Submit")
                    {
                     popupScript = "alert('Image Added Successfully');";
                    }
                    else
                    {
                         popupScript = "alert('Image Updated Successfully');";
                    }
                    btnSubmit.Text = "Submit";
                    ddcountry.SelectedIndex = 0;
                    img.Visible = false;
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
                else
                    if (res == "f")
                    {
                        string popupScript = "alert('Product Already Exists');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
                    else
                    {
                        string popupScript = "alert('Unknow error occurred');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    }
            }
            else
            {
                Message.Show("Select Image");
            }
        }
        else
        {
            Message.Show("Select Product");
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        ddcountry.SelectedIndex = 0;
        ViewState["PID"] = null;
        ViewState["Image"] = null;
        img.Visible = false;
        btnSubmit.Text = "Submit";
    }

    protected void lnkedit(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string id = lnk.CommandArgument.ToString();
        objState.ProductId = null;
        objState.Status = null;
        objState.CategoryId = id;
        DataTable dt = objState.GetImages(objState);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddcountry.SelectedValue = dt.Rows[0]["ProductId"].ToString();
            img.ImageUrl = "/ProductImage/"+dt.Rows[0]["Images"].ToString();
            btnSubmit.Text = "Update";
            img.Visible = true;
            ViewState["PID"] = id;
            ViewState["Image"] = dt.Rows[0]["Images"].ToString();
        }
        else
        {
            Message.Show("Some Error Occurrred");
        }
    }

    protected void lnkdel(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string id = lnk.CommandArgument.ToString();

        string res = objState.Deleteimageslider(Convert.ToInt16(id));
        if (res == "t")
        {
            loadimage();
            Message.Show("slider image Deleted Successfully");
        }
        else
        {
            Message.Show("Some Error Occurrred");
        }
    }

   
}