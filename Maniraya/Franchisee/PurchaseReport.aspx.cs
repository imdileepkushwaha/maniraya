using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_PurchaseReport : System.Web.UI.Page
{
    clsAccount objaccount = new clsAccount();
    clsProduct objP = new clsProduct();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["fuserid"] != null)
            {
                //txtuserid.Text = Session["userid"].ToString();
                //txtuserid.Enabled = false;
                
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

   

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {

        if (txtfromdate.Text != "")
        {
            objP.Fromdate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objP.Fromdate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objP.Todate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objP.Todate = DateTime.MinValue;
        }
        if (txtransactionId.Text != "")
        {
            objP.TransactionCode = txtransactionId.Text.Trim();
        }
        else
        {
            objP.TransactionCode = string.Empty;
        }

        objP.FranchiseeID = new clsvendor().GetFranchiseeId(Session["fuserid"].ToString()).ToString();
        
        objP.UserId = txtuserid.Text;
        DataTable dt = new DataTable();
        dt = objP.getPurchaseProduct(objP);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }

    protected void lnk_frame_click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GetFranchiseeDetail(btn.CommandArgument.ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SearchPop", "showFranchiseeModal();", true);
    }

    public void GetFranchiseeDetail(string id)
    {
        clsProduct objState = new clsProduct();
        DataTable dt = new DataTable();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        objState.State = null;
        objState.City = null;
        objState.PinCode = null;
        lblfname.Text = "";
        lbladdress.Text = "";
        lblcity.Text = "";
        lblstate.Text = "";
        lblpincode.Text = "";
        lblmob.Text = "";

        string[] value = id.Split('_');
        string sid = value[0];
        string cid = value[1];
        string fid = value[2];

        objState.State = sid;
        objState.City = cid;
        objState.FranchiseeID = fid;

        dt = objState.FranchiseePageWise(1, 1, objState);

        if (dt != null && dt.Rows.Count > 0)
        {
            lblfname.Text = dt.Rows[0]["username"].ToString();
            lbladdress.Text = dt.Rows[0]["address"].ToString();
            lblcity.Text = dt.Rows[0]["cityname"].ToString();
            lblstate.Text = dt.Rows[0]["statename"].ToString();
            lblpincode.Text = dt.Rows[0]["pincode"].ToString();
            lblmob.Text = dt.Rows[0]["mobile"].ToString();
        }

    }



}