using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using DataTier;

public partial class admin_AddGalleryImage : System.Web.UI.Page
{
    clsBank objbank = new clsBank();
    DBHelper con = new DBHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                cleardata();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
   

    private void BindGrid()
    {
        try
        {
            SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Flag", "S") };
            DataTable dt = con.ExecuteDataSet("Pro_Gallery", arr);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch { }
    }

    protected void ImageUpload(string Agentid)
    {
        try
        {
            string ext = "";

            ext = Path.GetExtension(FileUpload1.FileName);

            if (ext == ".jpg" || ext == ".JPG" || ext == ".JPEG" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".gif" || ext == ".gif")
            {
                FileUpload1.SaveAs(Server.MapPath("~/user/Gallery//" + Agentid + ext));
            }
            else
            {
                string display = "Please choose .jpg or jpeg or .png images only !";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }

        }
        catch { }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id;
            
            SqlParameter[] arr1 = new SqlParameter[] { new SqlParameter("@Flag", "SID") };
            DataTable dt1 = con.ExecuteDataSet("Pro_Gallery", arr1);
            id=int.Parse(dt1.Rows[0][0].ToString());


            string filepath = "";
            if (FileUpload1.HasFile)
            {
                int fileSize = FileUpload1.PostedFile.ContentLength;
                if (fileSize > 512500)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Filesize of image is too large. Maximum file size permitted is 500 KB');</script>");
                    return;
                }

                string ext = Path.GetExtension(FileUpload1.FileName);
                if (FileUpload1.HasFile && (ext == ".jpg" || ext == ".JPG" || ext == ".JPEG" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".gif" || ext == ".gif"))
                {
                    filepath = ("~/user/Gallery/" + id + Path.GetExtension(FileUpload1.FileName));
                }

            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Please Select Photo');</script>");
                return;

            }

            SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Id", id), new SqlParameter("@Title", txt_title.Text), new SqlParameter("@Content", txt_content.Text), new SqlParameter("@Img_Url", filepath), new SqlParameter("@flag", "I") };
            if (con.ExecuteNonQueryStoredProcedure("Pro_Gallery", arr))
            {
                if (FileUpload1.HasFile)
                {
                    ImageUpload(id.ToString());
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Record Saved Successfully');</script>");
                cleardata();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Please try Again');</script>");
            }
        }
        catch { }

    }


    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            string filepath = "";
            if (FileUpload1.HasFile)
            {
                int fileSize = FileUpload1.PostedFile.ContentLength;
                if (fileSize > 512500)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Filesize of image is too large. Maximum file size permitted is 500 KB');</script>");
                    return;
                }

                string ext = Path.GetExtension(FileUpload1.FileName);
                if (FileUpload1.HasFile && (ext == ".jpg" || ext == ".JPG" || ext == ".JPEG" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".gif" || ext == ".gif"))
                {
                    filepath = ("~/user/Gallery/" + HiddenField2.Value.ToString() + Path.GetExtension(FileUpload1.FileName));
                }

            }
            else
            {
                filepath = HiddenField1.Value;
            }

            SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Id", HiddenField2.Value.ToString()), new SqlParameter("@Title", txt_title.Text), new SqlParameter("@Content", txt_content.Text), new SqlParameter("@Img_Url", filepath), new SqlParameter("@flag", "U") };
            if (con.ExecuteNonQueryStoredProcedure("Pro_Gallery", arr))
            {
                if (FileUpload1.HasFile)
                {
                    ImageUpload(HiddenField2.Value.ToString());
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Record Saved Successfully');</script>");
                cleardata();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Please try Again');</script>");
            }
        }
        catch { }

    }

    protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Id", Convert.ToInt32(GridView1.DataKeys[e.NewSelectedIndex].Value)), new SqlParameter("@Flag", "SI") };
            DataTable dt = con.ExecuteDataSet("Pro_Gallery", arr);

            if (dt.Rows.Count > 0)
            {
                HiddenField1.Value = dt.Rows[0]["Img_Url"].ToString();
                HiddenField2.Value = dt.Rows[0]["ID"].ToString();
                txt_title.Text = dt.Rows[0]["Title"].ToString();
                txt_content.Text = dt.Rows[0]["Content"].ToString();

                btnSubmit.Visible = false;
                btn_update.Visible = true;
                btnCancel.Visible = true;
            }
        }
        catch { }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] arr2 = new SqlParameter[] { new SqlParameter("@Id", HiddenField2.Value.ToString()), new SqlParameter("@Flag", "SI") };
            DataTable dt = con.ExecuteDataSet("Pro_Gallery", arr2);

            string path1 = dt.Rows[0]["Img_Url"].ToString();


            SqlParameter[] arr = new SqlParameter[] { new SqlParameter("@Id", HiddenField2.Value.ToString()), new SqlParameter("@flag", "D") };
            if (con.ExecuteNonQueryStoredProcedure("Pro_Gallery", arr))
            {

                if (File.Exists(Server.MapPath(path1)))
                {
                    File.Delete(Server.MapPath(path1));
                }

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Record Deleted Successfully');</script>");
                cleardata();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Please try Again');</script>");
            }

        }
        catch { }
    }

    protected void cleardata()
    {
        try
        {
            HiddenField1.Value = "";
            HiddenField2.Value = "";
            txt_title.Text = "";
            txt_content.Text = "";

            btnSubmit.Visible = true;
            btn_update.Visible = false;
            btnCancel.Visible = false;
            BindGrid();
        }
        catch { }
    }
   
}