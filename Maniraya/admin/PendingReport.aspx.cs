using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
public partial class PendingReport : System.Web.UI.Page
{
    clsRecharge objrecharge = new clsRecharge();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fillOperator();
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
        if (e.CommandName == "mySuccess")
        {
            LblRefrtenceId.Text = lbltransactionid.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

        }
        else
        {
            DataTable dt = new DataTable();
            dt = objrecharge.UpdateRechargeStatusManual(lbltransactionid.Text, str_status, Session["useradmin"].ToString());
            Message.Show("record updated !-----");
        }
        loaduser();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }
    void loaduser()
    {
        string noOfRows = "";
        objrecharge.Name = txtname.Text;
        objrecharge.UserMobile = txtusermobile.Text;
        objrecharge.RechargeMobile = txtrechargemobile.Text;
        objrecharge.ReferenceId = txttransactionid.Text;

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

        objrecharge.Status = "Pending";
        objrecharge.RechargeType = ddlServiceType.SelectedValue;
        objrecharge.OperatorCode = ddlOperator.SelectedValue;

        DataTable dt = new DataTable();
        dt = objrecharge.getRechargeReport(objrecharge);
        ViewState["dt"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            LinkButton btnSuccess = (LinkButton)e.Row.FindControl("btnSuccess");
            LinkButton btnDispute = (LinkButton)e.Row.FindControl("btnDispute");
            LinkButton btnFail = (LinkButton)e.Row.FindControl("btnFail");
            if (lblstatus.Text == "Request Sent")
            {
                lblstatus.Text = "Pending";
                lblstatus.CssClass = "label label-info";
                btnSuccess.Visible = true;
                btnDispute.Visible = false;
                btnFail.Visible = true;
            }
            else
                if (lblstatus.Text == "Pending")
                {
                    lblstatus.CssClass = "label label-info";
                    btnSuccess.Visible = true;
                    btnDispute.Visible = false;
                    btnFail.Visible = true;
                }
                else
                    if (lblstatus.Text == "Success")
                    {
                        lblstatus.CssClass = "label label-success";
                        btnSuccess.Visible = false;
                        btnDispute.Visible = true;
                        btnFail.Visible = true;
                    }
                    else
                        if (lblstatus.Text == "Dispute")
                        {
                            lblstatus.CssClass = "label badge-info";
                            btnSuccess.Visible = false;
                            btnDispute.Visible = false;
                            btnFail.Visible = false;
                        }
                        else
                            if (lblstatus.Text == "Failed")
                            {
                                lblstatus.CssClass = "label label-danger";
                                btnSuccess.Visible = true;
                                btnDispute.Visible = false;
                                btnFail.Visible = false;
                            }
                            else
                                if (lblstatus.Text == "refund")
                                {
                                    lblstatus.CssClass = "label label-info";
                                    btnSuccess.Visible = false;
                                    btnDispute.Visible = false;
                                    btnFail.Visible = false;
                                }
        }
    }
    protected void ddlServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillOperator();
    }
    protected void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "TransactionReport_" + DateTime.Now + ".xls";
        System.IO.StringWriter strwritter = new System.IO.StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.HeaderStyle.BackColor = System.Drawing.Color.AliceBlue;
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.Columns[14].Visible = false;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportGridToExcel();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = objrecharge.UpdateRechargeStatusManualnew(LblRefrtenceId.Text, "Success", Session["useradmin"].ToString(), txtliveid.Text);
        string popupScript2 = "Closepopup();";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        Message.Show("record updated !-----");
        loaduser();
    }
}