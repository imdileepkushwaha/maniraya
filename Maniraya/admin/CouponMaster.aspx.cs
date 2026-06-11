using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CouponMaster : System.Web.UI.Page
{
    clsState objState = new clsState();
    clsCoupon cp = new clsCoupon();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {

                loadcoupon();
      
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


    public void loadcoupon()
    {
        DataTable dt = cp.getCouponDetail(null);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   
   
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        decimal deccheck;

        if (string.IsNullOrEmpty(txtcnameedit.Text))
        {
            Message.Show("Enter Coupon Name");
        }
        else
        if (string.IsNullOrEmpty(txtvalidfrom.Text))
        {
            Message.Show("Enter Valid Fromdate");
        }
        else
            if (string.IsNullOrEmpty(txtvalidtill.Text))
            {
                Message.Show("Enter Valid Till Date");
            }
            else
                if (string.IsNullOrEmpty(txtcouponamount.Text))
                {
                    Message.Show("Enter Coupon Amount");
                }
                else
                    if (!Decimal.TryParse(txtcouponamount.Text, out deccheck))
                    {
                        Message.Show("Invalid Coupon Amount");
                    }
                    else
                        if (string.IsNullOrEmpty(txtminshopamount.Text))
                        {

                            Message.Show("Enter Minimum Purchase Amount");
                        }
                        else
                            if (!Decimal.TryParse(txtminshopamount.Text, out deccheck))
                            {
                                Message.Show("Invalid Minimum Purchase Amount");
                            }
                            else
                            {
                                int status = Convert.ToInt16(ddlstatus.SelectedValue);
                                string res = cp.Insert_Coupon(Convert.ToDecimal(txtcouponamount.Text), DateTime.Parse(txtvalidfrom.Text), DateTime.Parse(txtvalidtill.Text), Convert.ToDecimal(txtminshopamount.Text), Session["useradmin"].ToString(), Convert.ToInt16(hdId.Value), status, txtcnameedit.Text, "Update");
                                if (res == "t")
                                {
                                    Message.Show("Coupon Updated Successfully");
                                    loadcoupon();
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "Closepopup();", true);
                                    return;
                                }
                                else
                                    if (res == "f")
                                    {
                                        Message.Show("Coupon Update Failed");
                                    }
                                    else
                                    {
                                        Message.Show("Unknown Error Occurred");
                                    }
                            }

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), "showModal();", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        decimal deccheck;

        if(string.IsNullOrEmpty(txtcname.Text))
        {
            Message.Show("Enter Coupon Name");
        }
        else
        if (string.IsNullOrEmpty(txtfdate.Text))
        {
            Message.Show("Enter Valid Fromdate");
        }
        else
            if (string.IsNullOrEmpty(txttdate.Text))
            {
                Message.Show("Enter Valid Till Date");
            }
            else
                if (string.IsNullOrEmpty(txtamount.Text))
                {
                    Message.Show("Enter Coupon Amount");
                }
                else
                    if (!Decimal.TryParse(txtamount.Text, out deccheck))
                    {
                        Message.Show("Invalid Coupon Amount");
                    }
                    else
                        if (string.IsNullOrEmpty(txtminamount.Text))
                        {

                            Message.Show("Enter Minimum Purchase Amount");
                        }
                        else
                            if (!Decimal.TryParse(txtminamount.Text, out deccheck))
                            {
                                Message.Show("Invalid Minimum Purchase Amount");
                            }
                            else
                            {

                                string res = cp.Insert_Coupon(Convert.ToDecimal(txtamount.Text), DateTime.Parse(txtfdate.Text), DateTime.Parse(txtfdate.Text), Convert.ToDecimal(txtminamount.Text), Session["useradmin"].ToString(), 0, 1, txtcname.Text,"Insert");
                                if (res == "t")
                                {
                                    loadcoupon();
                                    Message.Show("Coupon Added Successfully");
                                   
                                }
                                else
                                    if (res == "f")
                                    {
                                        Message.Show("Coupon Added Failed");
                                    }
                                    else
                                    {
                                        Message.Show("Unknown Error Occurred");
                                    }
                            }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }


    protected void lnkedit(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string id = btn.CommandArgument.ToString();
        hdId.Value = id;
        DataTable dt = cp.getCouponDetail(id);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtvalidfrom.Text = DateTime.Parse(dt.Rows[0]["fromdate"].ToString()).ToString("dd/MMM/yyyy");
            txtvalidtill.Text = DateTime.Parse(dt.Rows[0]["todate"].ToString()).ToString("dd/MMM/yyyy");
            txtcouponamount.Text = dt.Rows[0]["amount"].ToString();
            txtcnameedit.Text = dt.Rows[0]["code"].ToString();
            txtminshopamount.Text = dt.Rows[0]["minshopamount"].ToString();
            ddlstatus.SelectedValue = (dt.Rows[0]["status"].ToString() == "" ? "0" : dt.Rows[0]["status"].ToString());
        }
        ClientScript.RegisterStartupScript(this.GetType(), "dd", "showModal();", true);

    }


    protected void lnkdel(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string id = btn.CommandArgument.ToString();
        clsProduct obj = new clsProduct();
        string res = obj.DeleteCoupon(Convert.ToInt16(id));

        if (res == "t")
        {
            loadcoupon();
            Message.Show("Coupon Deleted Successfully");
        }
        else
            if (res == "f")
            {
                Message.Show("Coupon Cannot Be Deleted.It Is Assigned To Some User");
            }
        else
        {
            Message.Show("Coupon Deleted Successfully");
        }

    }

    
  
    
}