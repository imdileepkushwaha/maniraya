using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VideosAdd : System.Web.UI.Page
{
    clsProduct objState = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadVideo();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


    public void loadVideo()
    {
        objState.ProductName = null;
        objState.Status = null;
        objState.Description = null;
        objState.CategoryId = null;
        objState.ProductImage = null;
        DataTable dt = objState.GetVideos(objState);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
   
    

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        if (txttitle.Text != "")
        {
            if (txtdescripition.Text != "")
            {

                if(txtvideourl.Text != "")
                {

                objState.CategoryId = btnSubmit.Text == "Submit" ? "0" : ViewState["PID"].ToString();
                objState.ProductImage = txtvideourl.Text;
                objState.Description = txtdescripition.Text;
                objState.ProductName = txttitle.Text;
                objState.Status = "1";
                string res = objState.Insert_Videos(objState, (btnSubmit.Text == "Submit" ? "Insert" : "Update"));
                if (res == "t")
                {
                    loadVideo();
                    string popupScript = "";
                    if (btnSubmit.Text == "Submit")
                    {
                        popupScript = "alert('Video Added Successfully');";
                    }
                    else
                    {
                        popupScript = "alert('Video Updated Successfully');";
                    }
                    btnSubmit.Text = "Submit";
                    txttitle.Text = "";
                    txtdescripition.Text = "";
                    txtvideourl.Text = "";
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
                    Message.Show("Enter Video Url");
                }
            }
            else
            {
                Message.Show("Enter Description");
            }

           
        }
        else
        {
            Message.Show("Enter title");
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        txttitle.Text = "";
        txtdescripition.Text = "";
        txtvideourl.Text = "";
        ViewState["PID"] = null;
        ViewState["Image"] = null;
        btnSubmit.Text = "Submit";
    }

    protected void lnkedit(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string id = lnk.CommandArgument.ToString();
        objState.ProductName = null;
        objState.Status = null;
        objState.Description = null;
        objState.ProductImage = null;
        objState.CategoryId = id;
        DataTable dt = objState.GetVideos(objState);
        if (dt != null && dt.Rows.Count > 0)
        {
            txttitle.Text = dt.Rows[0]["title"].ToString();
            txtvideourl.Text = dt.Rows[0]["VideoUrl"].ToString();
            txtdescripition.Text = dt.Rows[0]["Description"].ToString();
            btnSubmit.Text = "Update";
            ViewState["PID"] = id;
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

        string res = objState.DeleteVideo(Convert.ToInt16(id));
        if (res == "t")
        {
            loadVideo();
            Message.Show("Video Deleted Successfully");
        }
        else
        {
            Message.Show("Some Error Occurrred");
        }
    }

   
}