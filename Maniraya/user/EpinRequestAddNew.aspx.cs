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

public partial class EpinRequestAddNew : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsAccount objaccount = new clsAccount();
    clsfranchisee objUserf = new clsfranchisee();
    clsDownload objD = new clsDownload();
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
                RDBtnFranchisee.Checked = true;
                RDBtnTRecharge.Checked = true;
                Pnlfranchisee.Visible = true;
                balance();               
                loadplan();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    void loadplan()
    {
        ddplan.Items.Clear();
        DataTable dt = new DataTable();
        dt = getPlanAll();
        ddplan.DataSource = dt;
        ddplan.DataTextField = "planname";
        ddplan.DataValueField = "id";
        ddplan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        ddplan.Items.Insert(0, li);
    }
    public DataTable getPlanAll()
    {
        string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where MoneyTransfer=1 ";
        //string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where 1=1 ";
        //str_query += " and PlanName Like 'Joining package%' ";

        DataTable ds = null;
        ObjData.StartConnection();
        try
        {
            ds = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            ds = null;
        }
        ObjData.EndConnection();
        return ds;
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
    void loadaccountdetail()
    {
        objaccount.Id = ddbankaccountno.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objaccount.getCompanyAccountDetailById(objaccount);
        if (dt.Rows.Count > 0)
        {
            txtdepositaccountno.Text = dt.Rows[0]["accountno"].ToString();
            txtaccountholdername.Text = dt.Rows[0]["AccountHolderName"].ToString();
            txtdepositbank.Text = dt.Rows[0]["BankName"].ToString();
            txtifsccode.Text = dt.Rows[0]["IFSCCode"].ToString();
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
                        //if (Convert.ToDecimal(txtamount.Text) >= 1000.00M)
                        //{
                            objaccount.img = UploadImage();
                            objaccount.WithdrawlAmount = Convert.ToDecimal(txtamount.Text);
                            objaccount.MentionBy = Session["userid"].ToString();
                            objaccount.UserId = Session["userid"].ToString();
                            objaccount.Remark = TxtNarration.Text;
                            objaccount.PaymentMode = ddmode.SelectedValue;
                            objaccount.OnlineTransactionId = TxtTransactionId.Text;
                            objaccount.BankAccountId = ddbankaccountno.SelectedValue;
                            objaccount.TransactionId = ddplan.SelectedValue;
                            objaccount.NoOfEpin = txtnoofepin.Text;
                            if (RDBTNAdmin.Checked == true)
                            {
                                objaccount.DepositrequestTo = "admin";
                            }
                            else
                            {
                                objaccount.DepositrequestTo = TxtFranchiseeUserId.Text;
                            }
                            if (RDBtnTRecharge.Checked == true)
                            {
                                objaccount.DepositrequestRequestType = "R";
                            }
                            else
                            {
                                objaccount.DepositrequestRequestType = "U";
                            }
                            //if (objaccount.DepositrequestRequestType == "U")
                            //{
                            //    if (Convert.ToDecimal(txtamount.Text) < 5000.00M)
                            //    {
                            //        Message.Show("Deposit Amount in cash wallet Must Be Greater Than or equal 5000 Rupees...!!!");
                            //        return;
                            //    }
                            //}

                            //if (objaccount.DepositrequestRequestType == "R")
                            //{
                            //    if (Convert.ToDecimal(txtamount.Text) > Convert.ToDecimal(ViewState["min"].ToString()) || Convert.ToDecimal(txtamount.Text) < Convert.ToDecimal(ViewState["max"].ToString()))
                            //    {
                            //        Message.Show("Request Amount Must Be Minimum " + ViewState["min"].ToString() + " and  Maximum " + ViewState["max"].ToString() + "...!!!");
                            //        return;
                            //    }
                            //}

                            string rs = Insert_EpinRequest(objaccount);
                            if (rs == "t")
                            {
                                Message.Show("Request Submitted Successfully...!!!");
                                TxtTransactionId.Text = string.Empty;
                                ddmode.SelectedIndex = 0;
                                TxtNarration.Text = string.Empty;

                                string username = Session["username"].ToString();
                                string userid = Session["userid"].ToString();
                                string amount = txtamount.Text;
                                string email = "info@sandhyasoftware.com";
                                string pinamount = ddplan.SelectedItem.Text;

                                userMail(username, userid, amount, email, pinamount);

                                smssending(username, userid, amount, pinamount);

                                loadsusername();
                                txtnoofepin.Text = string.Empty;
                                txtamount.Text = string.Empty;
                                ddplan.SelectedIndex = 0;
                                Txtplanamount.Text = string.Empty;
                            }
                            else if (rs == "f")
                            {
                                Message.Show("Pending request exist...!!!");
                            }
					        else if (rs == "ss")
                            {
                                Message.Show("this transection id is already exist...!!!");
                            }
                            else if (rs == "n")
                            {
                                Message.Show("you are alredy active...!!!");
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                        //}
                        //else
                        //{
                        //    Message.Show("Deposit Amount Must Be Greater Than or equal 1000 Rupees...!!!");
                        //}
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

    public string Insert_EpinRequest(clsAccount objaccount)
    {
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "sp_addEpinRequestNew";
            SqlParameter[] parameter = {                                              
                new SqlParameter("@UserId",objaccount.UserId),
                new SqlParameter("@Amount",objaccount.WithdrawlAmount), 
                new SqlParameter("@MentionBy",objaccount.MentionBy),
                new SqlParameter("@img",objaccount.img),
                 new SqlParameter("@narration",objaccount.Remark),
                  new SqlParameter("@transactionID",objaccount.OnlineTransactionId),
                     new SqlParameter("@paymentmode",objaccount.PaymentMode),
                        new SqlParameter("@DepositBankID",objaccount.BankAccountId),
                           new SqlParameter("@RequestTo",objaccount.DepositrequestTo),
                              new SqlParameter("@RequestTYpe",objaccount.DepositrequestRequestType),
                               new SqlParameter("@PlanId",objaccount.TransactionId),
                                 new SqlParameter("@NoOfEPin",objaccount.NoOfEpin)

                     
                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }
    public bool userMail(string username, string userid, string amount, string email, string pinamount)
    {

        MailMessage mail = new MailMessage();
        //
        string Subject2 = "Topup Request by User";
        string Body2 = "Dear Admin, <br> This User Id:- " + userid + "<br> Username:- " + username + " <br> Request for Topup of Amount:- " + amount + "<br>";
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

    public string smssending(string username, string userid, string amount, string pinamount)
    {
        string txtnumber = "+919876543210";
        string txtmessage = "Dear Admin, This User Id:- " + userid + " Username:- " + username + ", Request for Topup of Amount Rs. " + amount + "<br>";
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
    void franchiseedetail()
    {
        objUserf.UserId = TxtFranchiseeUserId.Text;
        DataTable dt = new DataTable();
        dt = objUserf.getUserDetail(objUserf);
        if (dt.Rows.Count > 0)
        {
            TxtFranchiseename.Text = dt.Rows[0]["username"].ToString();

        }
        else
        {
            TxtFranchiseeUserId.Text = string.Empty;
            TxtFranchiseename.Text = string.Empty;
            Message.Show("wrong franchisee Id...!!!");
        }
    }
    protected void TxtFranchiseeUserId_TextChanged(object sender, EventArgs e)
    {
        franchiseedetail();
    }
    protected void RDBTNAdmin_CheckedChanged(object sender, EventArgs e)
    {
        Pnladmin.Visible = true;
        Pnlfranchisee.Visible = false;
    }
    protected void RDBtnFranchisee_CheckedChanged(object sender, EventArgs e)
    {
        Pnlfranchisee.Visible = true;
        Pnladmin.Visible = false;
    }
    protected void RDBtnTRecharge_CheckedChanged(object sender, EventArgs e)
    {
        balance();
       
    }
    protected void RdBtnUtility_CheckedChanged(object sender, EventArgs e)
    {
        balance();
        lblmssg.Text = "";
        ViewState["min"] = null;
        ViewState["max"] = null;
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
            Txtplanamount.Text = dt.Rows[0]["planamount"].ToString();
            ViewState["min"] = dt.Rows[0]["planamount"].ToString();
            ViewState["max"] = dt.Rows[0]["productid"].ToString();
            lblmssg.Text = "Enter Minimum "+ViewState["min"].ToString()+" Maximum "+ViewState["max"].ToString() +" Amount";
        }
        else
        {
            txtamount.Text = "";
            lblmssg.Text = "";

        }
    }


    void balance()
    {
        objaccount.UserId = Session["userId"].ToString();
        if (RDBtnTRecharge.Checked == true)
        {
            DataTable dtrechrge = objaccount.getUserWalletBalanceReportrechargewallet(objaccount);
            txtbalance.Text = dtrechrge.Rows[0]["bal"].ToString();

        }
        if (RdBtnUtility.Checked == true)
        {
            DataTable dtuility = objaccount.getUserWalletBalanceReportrechargeuntility(objaccount);
            txtbalance.Text = dtuility.Rows[0]["bal"].ToString();
        }

    }
    protected void txtnoofepin_TextChanged(object sender, EventArgs e)
    {
        int dd = 0;
        if (int.TryParse(Txtplanamount.Text, out dd))
        {
            if (int.TryParse(txtnoofepin.Text, out dd))
            {
                int gg = Convert.ToInt16(Txtplanamount.Text) * Convert.ToInt16(txtnoofepin.Text);
                txtamount.Text = gg.ToString();
            }
            else
            {
                txtamount.Text = "";
            }
        }
        else
        {
            txtamount.Text = "";
        }
    }
}