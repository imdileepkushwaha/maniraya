using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_APIRecharge : System.Web.UI.Page
{
    clsRechargeAPI objrecharge = new clsRechargeAPI();
    public string LoginId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
          
            fillgrid();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                Session["GClass"] = "expand";
                Session["GridView1"] = GridView1.ClientID;
                //Attribute to hide column in Phone.
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
    void fillgrid()
    {
        DataTable dt = new DataTable();
        dt = objrecharge.GetRechargeApi();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            HiddenField1.Value = ((Label)gvrow.FindControl("LblId")).Text;
            LinkButton LinkStatus = (LinkButton)gvrow.FindControl("LnkEnable");
            string str = HiddenField1.Value;
            DataTable dt1 = new DataTable();
            dt1 = objrecharge.GetRechargeApi_ById(Convert.ToInt32(str));
            string boolval = dt1.Rows[0]["Status"].ToString();
            if (boolval == "True")
            {
                LinkStatus.CssClass = "btn btn-danger btn-round tooltips fa fa-times";
                LinkStatus.ToolTip = "API Deactive";
            }
            else
            {
                LinkStatus.CssClass = "btn btn-success btn-round tooltips fa fa-check-square-o";
                LinkStatus.ToolTip = "API Active";
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditUser")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            HiddenField1.Value = ((Label)row.FindControl("LblId")).Text;
            string str = HiddenField1.Value;
            Response.Redirect("APIRechargeAdd.aspx?id="+str);
        }
        if (e.CommandName == "EnableUser")
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            HiddenField1.Value = ((Label)row.FindControl("LblId")).Text;
            LinkButton LinkStatus = (LinkButton)row.FindControl("LnkEnable");
            string str = HiddenField1.Value;
            DataTable dt1 = new DataTable();
            dt1 = objrecharge.GetRechargeApi_ById(Convert.ToInt32(str));
            string boolval = dt1.Rows[0]["Status"].ToString();
            if (boolval == "True")
            {
                objrecharge.UpdateStatus("0", LoginId, str);
            }
            else
            {
                objrecharge.UpdateStatus("1", LoginId, str);
            }
            fillgrid();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:100px");
            e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:100px");
            e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:100px");
            e.Row.Cells[5].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:100px");
            e.Row.Cells[6].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:100px");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("APIRechargeAdd.aspx?id=0");
    }
}