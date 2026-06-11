using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class user_DirectWithdrawlForFrachisee : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    //clsUser objUser = new clsUser();
    clsfranchisee objUser = new clsfranchisee();
    clsAccount objaccount = new clsAccount();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
              //  txtuserid.Text = Session["userid"].ToString();
               // txtuserid.Enabled = false;
                //loadsusername();
              //  loadnotification();
                //RDBtnTRecharge.Checked = true;
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    public string UploadImage()
    {
        string Imagename = "";
        if (ImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ImageUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
    void loadnotification()
    {
        objUser.UserId = Session["userid"].ToString();
        DataTable dt = new DataTable();
        dt = objUser.getUserDetail(objUser);        
        if (dt.Rows[0]["activestatus"].ToString() == "0")
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtusername.Text = dt.Rows[0]["username"].ToString();
            objaccount.UserId = txtuserid.Text;
            DataTable dtbalnce = new DataTable();
            dtbalnce = objaccount.getAccountBalanceForGetHelp(objaccount);
            if (dtbalnce.Rows.Count > 0)
            {
                txtbalance.Text = dtbalnce.Rows[0][0].ToString();
            }
            else 
            {
                txtbalance.Text = "0.0";
            }
        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txtusername.Text != "")
            {
                if (txtamount.Text != "")
                {
                    if (Convert.ToDecimal(txtbalance.Text) >= Convert.ToDecimal(txtamount.Text))
                    {
                        if (Convert.ToDecimal(txtamount.Text) >= 500.00M)
                        {
                            objaccount.img = UploadImage();
                            objaccount.WithdrawlAmount = Convert.ToDecimal(txtamount.Text);
                            objaccount.MentionBy = Session["useradmin"].ToString();
                            objaccount.UserId = txtuserid.Text;

                            string rs = objaccount.Insert_DirectWithdrawlFranchiseeByAdmin(objaccount);
                            if (rs == "t")
                            {
                                Message.Show("Amount Withdrawn Successfully...!!!");
                                txtamount.Text = "";
                                loadsusername();
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                        }
                        else
                        {
                            Message.Show("Withdrwal Amount Must Be Greater Than 500...!!!");
                        }
                    }
                    else
                    {
                        Message.Show("Withdrawl Amount Must Be Less Than Or Equal Than Account Balance...!!!");
                    }
                }
                else
                {
                    Message.Show("Enter Amount...!!!");
                }
            }
            else
            {
                Message.Show("Enter User Name...!!!");
            }
        }
        else
        {
            Message.Show("Enter User Id...!!!");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }  
}