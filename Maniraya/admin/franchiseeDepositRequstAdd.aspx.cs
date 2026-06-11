using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.IO;

public partial class franchiseeDepositRequstAdd : System.Web.UI.Page
{
    clsEPin objEPin = new clsEPin();
    clsfranchisee objUser = new clsfranchisee();
    clsAccount objaccount = new clsAccount();
    clsfranchisee objUserf = new clsfranchisee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
               // txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
               // loadsusername();
               // loadnotification();
                loadbankaccount();
                RDBTNAdmin.Checked = true;
                RDBtnTRecharge.Checked = true;
                Pnladmin.Visible = false;
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
        ListItem li = new ListItem("Select Bank Account", "0");
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
            txtbranchname.Text = dt.Rows[0]["BranchName"].ToString();
        }
        else
        {
            txtdepositaccountno.Text = "";
            txtaccountholdername.Text = "";
            txtdepositbank.Text = "";
            txtifsccode.Text = "";
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
                        if (Convert.ToDecimal(txtamount.Text) >= 10.00M)
                        {
                            objaccount.img = UploadImage();
                            objaccount.WithdrawlAmount = Convert.ToDecimal(txtamount.Text);
                            objaccount.MentionBy = Session["useradmin"].ToString();
                            objaccount.UserId = txtuserid.Text;
                            objaccount.Remark = TxtNarration.Text;
                            objaccount.PaymentMode = ddmode.SelectedValue;
                            objaccount.OnlineTransactionId = TxtTransactionId.Text;
                            objaccount.BankAccountId = ddbankaccountno.SelectedValue;
                            if (RDBTNAdmin.Checked == true)
                            {
                                objaccount.DepositrequestTo = "admin";
                            }
                            else
                            {
                                objaccount.DepositrequestTo = TxtFranchiseeUserId.Text;
                            }                            
                            objaccount.DepositrequestRequestType = "W";
                            string rs = objaccount.Direct_franciseeDepositRequest(objaccount);
                            if (rs == "t")
                            {
                                Message.Show("Request Submitted Successfully...!!!");
                                txtamount.Text = "";
                                TxtTransactionId.Text = string.Empty;
                                ddmode.SelectedIndex = 0;
                                TxtNarration.Text = string.Empty;
                                loadsusername();
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                        }
                        else
                        {
                            Message.Show("Deposit Amount Must Be Greater Than or equal 10 Rupees...!!!");
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
}