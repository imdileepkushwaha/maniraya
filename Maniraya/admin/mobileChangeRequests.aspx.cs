using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_mobileChangeRequests : System.Web.UI.Page
{
    clsUser objUser;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    void loaddata()
    {
        dt = new DataTable();
        objUser = new clsUser();

        try
        {
            objUser.queryType = "Select";

            dt = objUser.I_U_S_D_MobileChangeRequest(objUser);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch(Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        dt = new DataTable();
        objUser = new clsUser();

        try
        {
            int requestId = Convert.ToInt32(e.CommandArgument);

            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            Label lblUserType = gvr.FindControl("lblUserType") as Label;
            Label lblUser_Id = gvr.FindControl("lblUser_Id") as Label;
            Label lblchargeAmount = gvr.FindControl("lblchargeAmount") as Label;

            if (e.CommandName == "approve")
            {
                objUser.queryType = "Approve Request";
                objUser.requestId = requestId;
                objUser.RegType = lblUserType.Text;
                objUser.UserId = lblUser_Id.Text;
                
                if (lblUserType.Text == "Franchisee")
                    objUser.chargeAmount = 0;
                else
                    objUser.chargeAmount = Convert.ToDecimal(lblchargeAmount.Text);

                objUser.MentionBy = Session["useradmin"].ToString();

                dt = objUser.I_U_S_D_MobileChangeRequest(objUser);

                if (dt.Rows[0][0].ToString() == "Request Approved")
                {
                    Message.Show("Request Approved");
                }
                else if (dt.Rows[0][0].ToString() == "Recharge Wallet is Low")
                {
                    Message.Show("Recharge Wallet is Low, Cannot deduct charge, hence cannot change number");
                }
                else
                {
                    Message.Show("Unknown Error Occurred");
                }
            }
            else if (e.CommandName == "reject")
            {
                objUser.queryType = "Reject Request";
                objUser.requestId = requestId;

                dt = objUser.I_U_S_D_MobileChangeRequest(objUser);

                if (dt.Rows[0][0].ToString() == "Request Rejected")
                {
                    Message.Show("Request Rejected");
                }
                else
                {
                    Message.Show("Unknown Error Occurred");
                }
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
            loaddata();
        }
    }
}