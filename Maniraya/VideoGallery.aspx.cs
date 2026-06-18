using System;
using System.Web.UI;
using System.Data;
using BusinessLogicTier;

public partial class VideoGallery : Page
{
    clsProduct objState = new clsProduct();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindVideos();
        }
    }

    private void BindVideos()
    {
        try
        {
            objState.CategoryId = null;
            objState.ProductImage = null;
            objState.ProductName = null;
            objState.Description = null;
            objState.Status = null; 
            
            DataTable dt = objState.GetVideos(objState);

            if (dt != null && dt.Rows.Count > 0)
            {
                rptVideoGallery.DataSource = dt;
                rptVideoGallery.DataBind();
            }
        }
        catch { }
    }

    protected string GetEmbedUrl(object urlObj)
    {
        if (urlObj == null) return "";
        string url = urlObj.ToString();
        
        if (string.IsNullOrEmpty(url)) return "";

        try
        {
            if (url.Contains("youtube.com/watch?v="))
            {
                return url.Replace("watch?v=", "embed/");
            }
            else if (url.Contains("youtu.be/"))
            {
                return url.Replace("youtu.be/", "www.youtube.com/embed/");
            }
        }
        catch { }

        return url; // Fallback to original url
    }
}
