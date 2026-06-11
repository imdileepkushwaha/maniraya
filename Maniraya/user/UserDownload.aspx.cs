using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.IO;

public partial class user_UserDownload : System.Web.UI.Page
{
    clsDownload objclsDownload = new clsDownload();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("index.aspx");
        }
        if (!IsPostBack)
        {
            FillDownloadDetails();
        }
    }

    public void FillDownloadDetails()
    {
        DataTable dt = new DataTable();
        objclsDownload.ToId = Session["userid"].ToString();
        dt = objclsDownload.getDownloadDetail(objclsDownload);
        grdDownload.DataSource = dt;
        grdDownload.DataBind();
    }
    //protected void lnkDownloadFile_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        String filePath = (sender as LinkButton).CommandArgument;
    //        filePath = "~\\content\\" + filePath.ToString(); 
    //        HttpResponse response = HttpContext.Current.Response;
    //        response.Clear();
    //        response.ClearContent();
    //        response.ClearHeaders();
    //        response.Buffer = true;
    //        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath.ToString() +"\"");
    //        response.TransmitFile(Server.MapPath(filePath));
    //        response.End(); 
    //    }
    //    catch (Exception ex)
    //    {
    //        Message.Show("Something went wrong!" + ex.Message.ToString());
    //    }
    //}
}