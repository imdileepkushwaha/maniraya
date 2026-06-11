using BusinessLogicTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DownlineReport : System.Web.UI.Page
{
    clsRecharge objrecharge = new clsRecharge();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadcommission();   
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadcommission()
    {      
        DataTable dt = new DataTable();
        dt = objrecharge.getRechargeCommission();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void bntn_Save_Click(object sender, EventArgs e)
    {
        string Cmd = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HiddenField txt_OperatorId = (HiddenField)GridView1.Rows[i].FindControl("txt_OperatorId");
            for (int j = 1; j < GridView1.Columns.Count - 1; j++)
            {
                //string col = ((char)(64 + j)).ToString();
                Label lblrank = (Label)GridView1.Rows[i].FindControl("lblrankno" + j.ToString());
                TextBox txt_Lvl = (TextBox)GridView1.Rows[i].FindControl("txt_" + j.ToString());
                RadioButtonList rbtype = (RadioButtonList)GridView1.Rows[i].FindControl("rbtype" + j.ToString());
                RadioButtonList rbchangetype = (RadioButtonList)GridView1.Rows[i].FindControl("rbchangetype" + j.ToString());
                string strtype = ""; string strchangetype = "";
                if (rbtype.SelectedValue != null)
                {
                    strtype = rbtype.SelectedValue.ToString();
                }
                if (rbchangetype.SelectedValue != null)
                {
                    strchangetype = rbchangetype.SelectedValue.ToString();
                }
                string str_lvl = "0";
                if (txt_Lvl.Text != "")
                {
                    str_lvl = txt_Lvl.Text;
                }
                Cmd += "INSERT INTO COMMISSIOIN_DISTRIBUTION_DETAIL(OperatorId,Commission,LVL,Type,ChangeType) VALUES(" + txt_OperatorId.Value + ",'" + str_lvl + "','" + lblrank.Text + "','" + strtype + "','" + strchangetype + "');";
             
            }

        }
        int res = objrecharge.InsertRechargeCommission(Cmd);
        if (res == 1)
        {
            Message.Show("Commission Defined Successfully");
        }
        else
        {
            Message.Show("Unknown Error Occurred...!!!");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int j = 1; j <= 6; j++)
            {
                //string col = ((char)(64 + j)).ToString();
                Label lbltype = (Label)e.Row.FindControl("lbltype" + j.ToString());
                Label lblchangetype = (Label)e.Row.FindControl("lblchangetype" + j.ToString());
                RadioButtonList rbtype = (RadioButtonList)e.Row.FindControl("rbtype" + j.ToString());
                RadioButtonList rbchangetype = (RadioButtonList)e.Row.FindControl("rbchangetype" + j.ToString());
                if (lbltype.Text != "")
                {
                    rbtype.SelectedValue = lbltype.Text;
                }
                if (lblchangetype.Text != "")
                {
                    rbchangetype.SelectedValue = lblchangetype.Text;
                }
            }

        }
    }
}