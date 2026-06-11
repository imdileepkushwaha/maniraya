using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARA_StringHunt;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

public partial class user_PinRequestAdd : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsplan objplan = new clsplan();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loadsusername();
                loadnotification();
                loadbankaccount();
                loadplan();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loadbankaccount()
    {
        ddbankaccountno.Items.Clear();
        DataTable dt = new DataTable();
        dt = objaccount.getCompanyAccountDetail();
        ddbankaccountno.DataSource = dt;
        ddbankaccountno.DataTextField = "accno2";
        ddbankaccountno.DataValueField = "id";
        ddbankaccountno.DataBind();
        ListItem li = new ListItem("Select Account Type", "0");
        ddbankaccountno.Items.Insert(0, li);

    }

    protected void ddplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadamount();
    }

    void loadamount()
    {
        DataTable dt = new DataTable();
        objplan.id = ddplan.SelectedValue;
        dt = objplan.getPlan(objplan);
        if (dt.Rows.Count > 0)
        {
            //txtamount.Text = (Convert.ToDecimal(dt.Rows[0]["planamount"].ToString()) + (Convert.ToDecimal(dt.Rows[0]["planamount"].ToString()) * 5 * 0.01M)).ToString();
            txtamount.Text = dt.Rows[0]["planamount"].ToString();
        }
        else
        {
            txtamount.Text = "";

        }
    }
    void loadplan()
    {
        ddplan.Items.Clear();
        DataTable dt = new DataTable();
        dt = objplan.getPlanAll();
        ddplan.DataSource = dt;
        ddplan.DataTextField = "planname";
        ddplan.DataValueField = "id";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddplan.Items.Insert(0, li);
    }
    void loadaccountdetail()
    {
        objaccount.Id = ddbankaccountno.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objaccount.getCompanyAccountDetailById(objaccount);
        if (dt.Rows.Count > 0)
        {
            txtdepositaccountno.Text = dt.Rows[0]["accountno"].ToString();
            txtaccountholdername1.Text = dt.Rows[0]["AccountHolderName"].ToString();
            txtdepositbank.Text = dt.Rows[0]["BankName"].ToString();
            txtifsccode1.Text = dt.Rows[0]["IFSCCode"].ToString();
            QR.ImageUrl = "../ProductImage/" + dt.Rows[0]["BranchName"].ToString();
        }
        else
        {
            txtdepositaccountno.Text = "";
            txtaccountholdername.Text = "";
            txtdepositbank.Text = "";

            txtbranchname.Text = "";
        }

    }
    protected void ddbankaccountno_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadaccountdetail();
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
            dtbalnce = objaccount.getAccountBalance(objaccount);
            txtbalance.Text = dtbalnce.Rows[0][0].ToString();
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
                  
                    //if (Convert.ToDecimal(txtbalance.Text) >= Convert.ToDecimal(txtamount.Text))
                    //{
                    if (Convert.ToDecimal(txtnoofepin.Text) >= 1)
                        {
                            objaccount.img = UploadImage();
                            objaccount.WithdrawlAmount = Convert.ToDecimal(txtamount.Text);
                            objaccount.MentionBy = Session["userid"].ToString();
                            objaccount.UserId = Session["userid"].ToString();
                            objaccount.Remark = TxtNarration.Text;
                            objaccount.PaymentMode = ddmode.SelectedValue;
                            objaccount.NoOfEpin = txtnoofepin.Text;
                            objaccount.plananame = ddplan.SelectedItem.Text;
                            objaccount.planid = ddplan.SelectedValue;
                            objaccount.Epinamount = txtamount.Text;
                            objaccount.OnlineTransactionId = TxtTransactionId.Text;
                            objaccount.BankAccountId = ddbankaccountno.SelectedValue;
                            string rs = objaccount.Insert_PinRequest(objaccount);
                            if (rs == "t")
                            {
                                Message.Show("Request Submitted Successfully...!!!");
                                txtamount.Text = "";
                                TxtTransactionId.Text = string.Empty;
                                ddmode.SelectedIndex = 0;
                                TxtNarration.Text = string.Empty;
                                loadsusername();

                                string username = Session["username"].ToString();
                    string userid = Session["userid"].ToString();
                    string Epin = txtnoofepin.Text;
                    string email = "info@sandhyasoftware.com";
                    string pinamount = ddplan.SelectedItem.Text;

                    userMail(username, userid, Epin, email, pinamount);

                    smssending(username, userid, Epin, pinamount);

                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                        }
                        else
                        {
                            Message.Show("Minimum 1 Pin Required...!!!");
                        }
                    //}
                    //else
                    //{
                    //    Message.Show("Deposit Amount Must Be Lesss Than Or Equal Than Account Balance...!!!");
                    //}
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

    public bool userMail(string username, string userid, string Epin, string email, string pinamount)
    {

        MailMessage mail = new MailMessage();
        //
        string Subject2 = "E-Pin Request by User";
        string Body2 = "Dear Admin, <br> This User Id:- " + userid + "<br> Username:- " + username + " <br> Request for " + Epin + " No of Epin <br> E-Pin Amount:- " + pinamount + "<br>";
        //


        ContentType mimeType = new System.Net.Mime.ContentType("text/html");
        string body = HttpUtility.HtmlDecode(Body2);
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
        mail.AlternateViews.Add(alternate);

        mail.To.Add(email);
        mail.From = new MailAddress("info@sandhyasoftware.com");
        mail.Subject = Subject2;
        mail.Body = Body2;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("info@sandhyasoftware.com", "mwapztpaipcolttw");


        try
        {
            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    
     public string smssending(string username, string userid, string Epin, string pinamount)
    {
        string txtnumber = "+919876543210";
        string txtmessage = "Dear Admin, This User Id:- " + userid + " Username:- " + username + " Request for " + Epin + " No of Epin & E-Pin Amount:- " + pinamount + "<br>";
        //txtmessage = txtmessage.Replace("#", "%23").Replace(":", "%3A").Replace(",", "%2C").Replace(" ", "%20");


        string strurl = "http://chatway.in/api/" + txtnumber + "&message=" + txtmessage + "&";

      //string strurl = "http://chatway.in/api/send-msg?username=SG686869&number=" + txtnumber + "&message=" + txtmessage + "&token=TDlua0RhVlZQOFhMTGlOSFU3bG5GQT09";
        //string strurl = "http://chatway.in/api/send-file?username=SG686869&number=" + txtnumber + "&message=" + txtmessage + "&token=TDlua0RhVlZQOFhMTGlOSFU3bG5GQT09&file_url=&file_name=";

        string result = apicall(strurl);

        return result;
    }

     public string apicall(string url)
     {
         HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
         try
         {
             HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
             StreamReader sr = new StreamReader(httpres.GetResponseStream());
             string results = sr.ReadToEnd();
             sr.Close();
             return results;
         }
         catch
         {
             return "0";
         }

     }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }  
}