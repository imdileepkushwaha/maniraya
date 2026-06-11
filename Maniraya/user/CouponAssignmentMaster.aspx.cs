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
            if (Session["userid"] != null)
            {
                if (Session["chktrans"] != null)
                {

                    loadAssginedCoupon();
                    Session.Remove("chktrans");
                }
                else
                {
                    Session["pg"] = "coupanassignment";
                    Response.Redirect("TransactionPass.aspx");
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }


   


    public void loadAssginedCoupon()
    {
        DataTable dt = cp.getCouponAssignedDetail(Session["userid"].ToString());
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
   


   
  

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

     
   

  
    
}