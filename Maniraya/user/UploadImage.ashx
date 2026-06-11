<%@ WebHandler Language="C#" Class="UploadImage" %>

using System;
using System.Web;

public class UploadImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string filename = files.AllKeys[0].ToString();
                string fname = context.Server.MapPath("../ProductImage/" + filename);
                file.SaveAs(fname);
            }
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("File Uploaded Successfully!");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}