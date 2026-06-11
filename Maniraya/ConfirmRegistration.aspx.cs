using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class user_ConfirmRegistration : System.Web.UI.Page
{
    clsUser objuser = new clsUser();
    Documet_Upload_class ObjDUc = new Documet_Upload_class();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            laoddata();
            //ImageUrl();
        }
    }
    void laoddata()
    {

        LblLoginId.Text = Session["LoginId1"].ToString();
        LblPassword.Text = Session["Password1"].ToString();
        LblSponsorName.Text = "Necta Network";
        LblSponsorId.Text = Session["SponserId1"].ToString();
        lblName.Text = Session["UserName2"].ToString();
        }
   // public void ImageUrl()
   // {
    //    ObjDUc.CreatedBy = "1";
    //    ObjDUc.Doc_Name = "Logo";
     //   DataSet ds = ObjDUc.GetUpload_Document(ObjDUc);
     //   if (ds.Tables[0].Rows.Count > 0)
      //  {

      //  Image1.ImageUrl = ds.Tables[0].Rows[0]["Path"].ToString();

      //  }
    //}
    }
