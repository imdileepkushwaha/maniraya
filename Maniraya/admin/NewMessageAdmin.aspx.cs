using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using BusinessLogicTier;

public partial class Associate_Details : System.Web.UI.Page
{
    clsSupport objsupport = new clsSupport();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                lblUserName.Text = "";
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objsupport.FromId = "admin";
        objsupport.ToId = txttoid.Text;
        objsupport.Attachment = UploadImage(fupAttachment);
        objsupport.Messagetitle = txtmessagetitle.Text;
        objsupport.MessageDescription = txtdescription.Text;
        objsupport.MentionBy = Session["useradmin"].ToString();
        int rs = objsupport.SendMessageByAdmin(objsupport);
        if (rs == 1)
        {
            Message.Show("Message Sent Successfully...!!!");
            txtdescription.Text = "";
            txtmessagetitle.Text = "";
            txttoid.Text = "";
        }
        else
            if (rs == 2)
            {
                Message.Show("Message Not Sent. Invalid To Id...!!!");
            }
            else
            {
                Message.Show("unknown Error occurred...!!!");
            }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void txttoid_TextChanged(object sender, EventArgs e)
    {
        objsupport.ToId = txttoid.Text.ToString().Trim();
        DataTable dt = new DataTable();
        dt = objsupport.getUserName(objsupport);
        if (dt.Rows.Count > 0)
        {
            lblUserName.Text = dt.Rows[0]["UserName"].ToString().ToUpper();
            lblUserName.Attributes.Add("class", "label label-success");
        }
        else
        {
            lblUserName.Text = "Wrong User Name!";
            lblUserName.Attributes.Add("class", "label label-danger");
        }
    }


    public string UploadImage(FileUpload fileUpload)
    {
        string Imagename = "";
        if (fileUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
            String FileExtName = Path.GetExtension(fileName);
            if (CheckFileType(FileExtName.ToString()))
            {
                Imagename = RandomNumber + fileName;
                fileUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);
            }
        }
        return Imagename;
    }

    public Boolean CheckFileType(String FileTypeName)
    {
        String[] ValidFilesTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".pdf" };
        foreach (var str in ValidFilesTypes)
        {
            if (str.ToUpper().ToString() == FileTypeName.ToUpper().ToString())
            {
                return true;
            }
        }
        return false;
    }

}