using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
public partial class admin_AdminDownload : System.Web.UI.Page
{
    clsDownload objclsDownload = new clsDownload();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["useradmin"] == null)
        {
            Response.Redirect("logout.aspx");
        }
        if(!IsPostBack)
        {
            ClearFields();
            RDBTNAdmin.Checked=true;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objclsDownload.ToId = txtToID.Text.ToString();
            DataTable dt = new DataTable();
            dt = objclsDownload.getUserDetail(objclsDownload);
            if (dt.Rows.Count > 0)
            {
                if (fupContent.HasFile != null)
                {
                    String FileName = fupContent.PostedFile.FileName;
                    String FileExtName = Path.GetExtension(FileName);
                    FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + FileName;
                    if (CheckFileType(FileExtName.ToString()))
                    {
                        String FolderPath = Server.MapPath("~/content/");
                        fupContent.PostedFile.SaveAs(Path.Combine(FolderPath, FileName));
                        objclsDownload.DownloadContent = FileName.ToString();
                        objclsDownload.EntryBy = Session["useradmin"].ToString();
                        objclsDownload.InsertDownload(objclsDownload);
                        ClearFields();
                        Message.Show("File Uploaded Successfully");
                    }
                    else
                    {
                        Message.Show("File Type is not Valid, Please Upload a valid file...!");
                    }
                }
                else
                {
                    Message.Show("Please Choose a file...!");
                }
            }
            else
            {
                Message.Show("Wrong TO ID...!");
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message.ToString());
        }
    }

    public void ClearFields()
    {
        fupContent.Attributes.Clear();
        txtToID.Text = "";
    }

    public Boolean CheckFileType(String FileTypeName)
    {
        String[] ValidFilesTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".pdf" };
        foreach (var str in ValidFilesTypes)
        {
            if (str.ToString() == FileTypeName.ToString())
            {
                return true;
            }
        }
        return false;
    }
    void loadfranchiseename()
    {
        DataTable dt = new DataTable();
        clsfranchisee objfuser = new clsfranchisee();
        objfuser.UserId = txtToID.Text;
        dt = objfuser.getUserName(objfuser);
        if (dt.Rows.Count > 0)
        {
            Txtname.Text= dt.Rows[0]["username"].ToString();
          
        }
        else
        {
            txtToID.Text = "";
            Txtname.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    void loadusername()
    {
        DataTable dt = new DataTable();
        clsUser objfuser = new clsUser();
        objfuser.UserId = txtToID.Text;
        dt = objfuser.getUserName(objfuser);
        if (dt.Rows.Count > 0)
        {
            Txtname.Text = dt.Rows[0]["username"].ToString();

        }
        else
        {
            txtToID.Text = "";
            Txtname.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    protected void txtToID_TextChanged(object sender, EventArgs e)
    {
        if (RDBTNAdmin.Checked == true)
        {
            loadusername();
        }
        else
        {
            loadfranchiseename();
        }
    }
}