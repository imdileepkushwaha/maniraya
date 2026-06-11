using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CouponAssignmentMaster : System.Web.UI.Page
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
                loadAssginedCoupon();
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
        ddlcoupon.Items.Clear();
        ddlcoupon.DataTextField = "Code";
        ddlcoupon.DataValueField = "id";
        ddlcoupon.DataSource = dt;
        ddlcoupon.DataBind();
        ddlcoupon.Items.Insert(0, new ListItem() { Text="Select Coupon", Value="" });
    }


    public void loadAssginedCoupon()
    {
        DataTable dt = cp.getCouponAssignedDetail(null);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    void loadsusername()
    {
        clsUser objUser = new clsUser();
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["username"].ToString();
        }
        else
        {
            txtname.Text = "";
            txtuserid.Text = "";
            string popupScript = "alert('Invalid User Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }


    protected void txtuserid_click(object sender, EventArgs e)
    {
        loadsusername();
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (ddlcoupon.SelectedIndex == 0)
        {
            Message.Show("Select Coupon");
        }
        else
            if (string.IsNullOrEmpty(txtuserid.Text))
            {
                Message.Show("Enter UserId");
            }


            else
            {
                DataTable dt = cp.I_D_CouponAssignment(0, Convert.ToInt16(ddlcoupon.SelectedValue),txtuserid.Text, "Insert");
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        ddlcoupon.SelectedIndex = 0;
                        txtuserid.Text = "";
                        txtname.Text = "";
                        loadAssginedCoupon();
                        Message.Show("Coupon Assigned Successfully");
                    }
                    else
                    {
                        Message.Show(dt.Rows[0][1].ToString());
                    }
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblstatus");
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnk");
            if (lbl.Text.ToLower() == "unused")
            {
                lnk.Visible = true;
            }
            else
            {
                lnk.Visible = false;
            }
        }
    }


    protected void lnkedit(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string id = btn.CommandArgument.ToString();
        DataTable dt = cp.I_D_CouponAssignment(Convert.ToInt16(id), 0,null, "Delete");
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "1")
            {
                loadAssginedCoupon();
                Message.Show("Coupon Deleted Successfully");
            }
            else
            {
                Message.Show(dt.Rows[0][1].ToString());
            }
        }
       

    }

  
    
}