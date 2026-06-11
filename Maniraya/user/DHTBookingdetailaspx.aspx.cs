using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_DHTBookingdetailaspx : System.Web.UI.Page
{
    clsdthsuscription objS = new clsdthsuscription();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            txtuserid.Text = Session["userid"].ToString();
            txtuserid.Enabled = false;
            loadserviceprovider();
         
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    void loadserviceprovider()
    {
        DDlserviceprovider.Items.Clear();
        DataTable dt = new DataTable();
        dt = objS.getserviceproviderAll(objS);
        DDlserviceprovider.DataSource = dt;
        DDlserviceprovider.DataTextField = "Operator";
        DDlserviceprovider.DataValueField = "id";
        DDlserviceprovider.DataBind();
        ListItem li = new ListItem("Select Service Provider", "0");
        DDlserviceprovider.Items.Insert(0, li);
    }
    void loaduser()
    {
        objS.entryuser = string.Empty;
        objS.status = string.Empty;
        objS.mobileno = string.Empty;
        objS.ServiceProviderId = "";
        if (txtfromdate.Text != "")
        {
            objS.FRomdate = txtfromdate.Text;
        }
        else
        {
            objS.FRomdate = "";
        }
        if (txttodate.Text != "")
        {
            objS.Todate = txttodate.Text;
        }
        else
        {
            objS.Todate = "";
        }
        string noOfRows = "";
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

        objS.noofrow = noOfRows;
        if(txtuserid.Text!=string.Empty)
        {
            objS.entryuser = txtuserid.Text;
        }
        if (ddlstatus.SelectedValue != "0")
        {
            objS.status = ddlstatus.SelectedValue;
        }
        if (TxtCustomerMobile.Text != string.Empty)
        {
            objS.mobileno = TxtCustomerMobile.Text;
        }
        if (DDlserviceprovider.SelectedValue != "0")
        {

            objS.ServiceProviderId = DDlserviceprovider.SelectedValue;
        }
       
        DataTable dt = new DataTable();
        dt = objS.getDTHBookingdetail(objS);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void ddlRecordFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaduser();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lbStatus");
            Label lbldispute = (Label)e.Row.FindControl("lbldispute");
            LinkButton btnDispute = (LinkButton)e.Row.FindControl("btnDispute");
            if (lbldispute.Text == "Request Sent")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-info";
                btnDispute.Visible = false;
            }
            else
                if (lblstatus.Text.ToUpper() == "PENDING")
                {
                    lblstatus.CssClass = "label label-info";
                    btnDispute.Visible = false;
                }
                else
                    if (lblstatus.Text.ToUpper() == "SUCCESS")
                    {
                        lblstatus.CssClass = "label label-success";
                        //if (lbldispute.Text == "0")
                        //{
                        //    btnDispute.Visible = true;
                        //}
                        //else
                        //{
                        //    btnDispute.Visible = false;
                        //}
                        btnDispute.Visible = false;
                    }
                    else
                        if (lblstatus.Text == "Dispute")
                        {
                            lblstatus.CssClass = "label badge-info";
                            lblstatus.Style.Add("background-color", "#c8c639");
                            btnDispute.Visible = false;
                        }
                        else
                            if (lblstatus.Text.ToUpper() == "FAILED")
                            {
                                lblstatus.CssClass = "label label-danger";
                                btnDispute.Visible = false;
                            }
                            else
                                if (lblstatus.Text == "refund")
                                {
                                    lblstatus.CssClass = "label label-info";
                                    btnDispute.Visible = false;
                                }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        Label lbltransactionid = (Label)GridView1.Rows[index].FindControl("lbltransactionid");
        Label lbldispute = (Label)GridView1.Rows[index].FindControl("lbldispute");
        Label LblCustomername1 = (Label)GridView1.Rows[index].FindControl("LblCustomername");
        Label LblAddress1 = (Label)GridView1.Rows[index].FindControl("LblAddress");
        Label LblPincode1 = (Label)GridView1.Rows[index].FindControl("LblPincode");
        Label LblPackageName = (Label)GridView1.Rows[index].FindControl("LblPackageName");
        Label LblSBoxName1 = (Label)GridView1.Rows[index].FindControl("LblSBoxName");
        Label LblOperator1 = (Label)GridView1.Rows[index].FindControl("LblOperator");
        Label LblDistrict = (Label)GridView1.Rows[index].FindControl("LblDistrict");
        Label LblLandmark = (Label)GridView1.Rows[index].FindControl("LblLandmark");

        Label LblState = (Label)GridView1.Rows[index].FindControl("LblState");
        Label LblCity = (Label)GridView1.Rows[index].FindControl("LblCity");

        string str_status = "";
        if (e.CommandName == "edt")
        {
            LblOperator.Text = LblOperator1.Text;
            LblSboxname.Text = LblSBoxName1.Text;
            LblPackage.Text = LblPackageName.Text;

            LblCustomername.Text = LblCustomername1.Text;
            LblAddress.Text = LblAddress1.Text;
            LblPinconw.Text = LblPincode1.Text;
            lbldistrict.Text = LblDistrict.Text;
            lbllandmark.Text = LblLandmark.Text;
            lblCity.Text = LblCity.Text;
            lblState.Text = LblState.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else
        {
            if (e.CommandName == "mySuccess")
            {
                str_status = "Success";
            }
            if (e.CommandName == "myDispute")
            {
                str_status = "Dispute";
            }
            if (e.CommandName == "myFail")
            {
                str_status = "Failed";
            }

            //DataTable dt = new DataTable();
            //dt = objrecharge.UpdateRechargeStatusManual(lbltransactionid.Text, str_status, Session["userid"].ToString());
            //loaduser();
        }
    }
}