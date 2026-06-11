using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;


public partial class admin_DMRCommissionAdd : System.Web.UI.Page
{
    clsdthsuscription objDTh = new clsdthsuscription();
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
        GridView1.DataSource = null;
        GridView1.DataBind();       
        DataTable ds = objDTh.getCommission(objDTh);
        GridView1.DataSource = ds;
        GridView1.DataBind();
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
                //string col = ((char)(64 + j)).ToString();
                Label lbltype = (Label)e.Row.FindControl("lbl_commtype");
                Label lblchangetype = (Label)e.Row.FindControl("lbl_amttype");
                DropDownList cmtype = (DropDownList)e.Row.FindControl("ddlcommissiontype");
                DropDownList amttype = (DropDownList)e.Row.FindControl("ddlamounttype");               
                if (lbltype.Text != "")
                {
                    cmtype.SelectedValue = lbltype.Text;
                }
                if (lblchangetype.Text != "")
                {
                    amttype.SelectedValue = lblchangetype.Text;
                }          

        }
    }
   
   
 
   
    protected void btnAddDMRCommission_Click(object sender, EventArgs e)
    {
        string Cmd = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {            
            for (int j = 1; j < GridView1.Columns.Count - 1; j++)
            {
                //string col = ((char)(64 + j)).ToString();
                HiddenField lblpackageid = (HiddenField)GridView1.Rows[i].FindControl("txt_PackageID");
                HiddenField HDId = (HiddenField)GridView1.Rows[i].FindControl("HDId");
                DropDownList cmtype = (DropDownList)GridView1.Rows[i].FindControl("ddlcommissiontype");
                DropDownList amttype = (DropDownList)GridView1.Rows[i].FindControl("ddlamounttype");
                TextBox txtcommission = (TextBox)GridView1.Rows[i].FindControl("Txtrate");


                Cmd += "UPDATE DTH_CommissionModule1 SET ComType='" + cmtype.SelectedValue + "',amttype='" + amttype.SelectedValue + "',atrate='" + txtcommission.Text + "' WHERE ID='" + HDId.Value + "' AND PackageId='" + lblpackageid.Value + "';";

            }

        }
        int res = objDTh.insertCommission(Cmd);
        if (res == 1)
        {
            Message.Show("Commission Defined Successfully");
        }
        else
        {
            Message.Show("Unknown Error Occurred...!!!");
        }
    }
   
}