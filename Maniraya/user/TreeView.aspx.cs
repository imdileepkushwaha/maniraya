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
    clsState objState = new clsState();
    clsUser objuser = new clsUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    public void Fill_Sub_Heads(string MainID, TreeNode Main_Head_Node)
    {
        DataTable dt = new DataTable();
        objuser.UserId = MainID;
        dt = objuser.FillSubNode(objuser);
        Open_Heads(dt, Main_Head_Node.ChildNodes);
    }
    public void Open_Heads(DataTable dt, TreeNodeCollection node_First)
    {
        foreach (DataRow dr in dt.Rows)
        {
            TreeNode t1 = new TreeNode();
            t1.Text = dr["username"].ToString() + " / " + dr["userid"].ToString();
            t1.Value = dr["userid"].ToString();
            node_First.Add(t1);
            t1.PopulateOnDemand = ((int)(dr["Subnode"]) > 0);
        }
    }
    protected void Account_Chart_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        Fill_Sub_Heads(e.Node.Value, e.Node);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            Account_Chart.Nodes.Clear();

            DataTable dattab = new DataTable();
            objuser.UserId = txtuserid.Text;
            dattab = objuser.Find_UserDetail2(objuser).Tables[0];
            Open_Heads(dattab, Account_Chart.Nodes);
            pnllist.Visible = true;
        }
        else
        {
            Message.Show("Enter User ID...!!!");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
}