using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class admin_UserReport : System.Web.UI.Page
{
    clsRecharge objrecharge = new clsRecharge();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                if (Session["chktrans"] != null)
                {
                    txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    fillOperator();
                    Session.Remove("chktrans");
                }
                else
                {
                    Session["pg"] = "rechargereport";
                    Response.Redirect("TransactionPass.aspx");
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }

   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lbltransactionid = (Label)GridView1.Rows[index].FindControl("lbltransactionid");
        Label lbldispute = (Label)GridView1.Rows[index].FindControl("lbldispute");
        string str_status = "";
        if (e.CommandName == "mySuccess")
        {
            str_status = "Success";
        }
        else
            if (e.CommandName == "myDispute")
            {
                str_status = "Dispute";
            }
            else
                if (e.CommandName == "myFail")
                {
                    str_status = "Failed";
                }
        DataTable dt = new DataTable();       
        dt = objrecharge.UpdateRechargeStatusManual(lbltransactionid.Text, str_status, Session["userid"].ToString());
        loaduser();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    
    void loaduser()
    {
        string noOfRows = "";

        //objrecharge.Name = txtname.Text;
        objrecharge.UserMobile =  Session["userid"].ToString();
        objrecharge.RechargeMobile =txtrechargemobile.Text;
        objrecharge.ReferenceId = txttransactionid.Text;

        objrecharge.Status = ddlStatus.SelectedValue;
        objrecharge.RechargeType = ddlServiceType.SelectedValue;
        objrecharge.OperatorCode = ddlOperator.SelectedValue;

        if (txtfromdate.Text != "")
        {
            objrecharge.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objrecharge.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objrecharge.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objrecharge.ToDate = DateTime.MinValue;
        }




        if (ddlRecordFilter.SelectedItem.Text == "All")
            noOfRows = "";

        //else if (ddlRecordFilter.SelectedItem.Text == "5")
        //    noOfRows = "top 5";

        else if (ddlRecordFilter.SelectedItem.Text == "25")
            noOfRows = "top 25";

        else if (ddlRecordFilter.SelectedItem.Text == "50")
            noOfRows = "top 50";

        else if (ddlRecordFilter.SelectedItem.Text == "100")
            noOfRows = "top 100";

        else if (ddlRecordFilter.SelectedItem.Text == "500")
            noOfRows = "top 500";





        DataTable dt = new DataTable();
        dt = objrecharge.getRechargeReport(objrecharge, noOfRows);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            Label lbldispute = (Label)e.Row.FindControl("lbldispute");
            LinkButton btnDispute = (LinkButton)e.Row.FindControl("btnDispute");
            if (lblstatus.Text == "Request Sent")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-info";
                btnDispute.Visible = false;
            }
            else
                if (lblstatus.Text == "Pending")
                {
                    lblstatus.CssClass = "label label-info";
                    btnDispute.Visible = false;
                }
                else
                    if (lblstatus.Text == "Success")
                    {
                        lblstatus.CssClass = "label label-success";
                        if (lbldispute.Text == "")
                        {
                            btnDispute.Visible = true;
                        }
                        else
                        {
                            btnDispute.Visible = false;
                        }
                    }
                    else
                        if (lblstatus.Text == "Dispute")
                        {
                            lblstatus.CssClass = "label badge-info";
                            lblstatus.Style.Add("background-color", "#c8c639");
                            btnDispute.Visible = false;
                        }
                    else
                        if (lblstatus.Text == "Failed")
                        {
                            lblstatus.CssClass = "label label-danger";
                            btnDispute.Visible = false;
                        }
                        else
                            if (lblstatus.Text == "Refund")
                            {
                                lblstatus.CssClass = "label label-info";
                                btnDispute.Visible = false;
                            }
        }
    }

    protected void fillOperator()
    {
        DataTable dt = objrecharge.GetAllOpertorSelectedByUser("1");

        if (ddlServiceType.SelectedIndex > 0)
        {
            DataRow[] drOperator = dt.Select("Type = " + ddlServiceType.SelectedValue + "");

            ddlOperator.DataSource = drOperator.CopyToDataTable();
            ddlOperator.DataTextField = "Operator";
            ddlOperator.DataValueField = "Id";
            ddlOperator.DataBind();

            ddlOperator.Items.Insert(0, new ListItem("Select Operator", "0"));
        }
        else
        {
            ddlOperator.Items.Clear();
        }
    }

    protected void ddlServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillOperator(); loaduser();
    }
}