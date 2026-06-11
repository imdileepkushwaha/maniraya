using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;

public partial class admin_OperatorAPICodeEdit : System.Web.UI.Page
{
    public string LoginId = "";
    DataTable dtsave = new DataTable();
    public static int opid;
    clsOperator objoperator = new clsOperator();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {
            Bindgrid();
            ViewState["tbldata"] = dtsave;
            lbl_msg.Visible = false;

            GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";
            Session["GClass"] = "expand";
            Session["GridView1"] = GridView1.ClientID;
           // Attribute to hide column in Phone.
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
           // Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        addColumns();
    }
    void Bindgrid()
    {
        DataTable dt2 = new DataTable();
        dt2 = objoperator.Get_Operator();
        GridView1.DataSource = dt2;
        GridView1.DataBind();
        DataTable dt = new DataTable();
        dt = objoperator.GetRechargeApi();
        foreach (GridViewRow gv in GridView1.Rows)
        {
            DataList datalst = GridView1.HeaderRow.FindControl("DataList1") as DataList;
            DataList datalst1 = (DataList)gv.FindControl("DataList2");
            HiddenField hdn = (HiddenField)gv.FindControl("hdn_ApiId");
            HiddenField hdnop = (HiddenField)gv.FindControl("hdn_OpId");
            opid = Convert.ToInt32(hdnop.Value);
            if (dt.Rows.Count > 0)
            {
                datalst.DataSource = dt;
                datalst.DataBind();
                datalst1.DataSource = dt;
                datalst1.DataBind();
            }
        }
    }
    protected void DataList2_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        int ss = 0;
        TextBox txt = (TextBox)e.Item.FindControl("txt_code");
        HiddenField hdn = (HiddenField)e.Item.FindControl("hdn_ApiId");
        if (hdn.Value != "")
        {
            ss = Convert.ToInt32(hdn.Value);
            DataTable dt1 = new DataTable();
            dt1 = objoperator.Get_Operator_API_Code_ByOpid(opid, ss);
            if (dt1.Rows.Count > 0)
            {
                txt.Text = dt1.Rows[0]["OpCode"].ToString();
            }
            else
            {
                txt.Text = "";
            }
        }
        else
        {
            txt.Text = "";
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //HiddenField hdnop = (HiddenField)e.Row.FindControl("hdn_OpId");
                //opid = Convert.ToInt32(hdnop.Value);
                int var_opid = 0;
                HiddenField OPID11 = (HiddenField)GridView1.Rows[i].FindControl("hdn_OpId");
                var_opid = Convert.ToInt32(OPID11.Value);
                DataList dlst = (DataList)GridView1.Rows[i].FindControl("DataList2");
                for (int j = 0; j < dlst.Items.Count; j++)
                {
                    int var_Apid = 0;
                    string var_com = "";
                    TextBox txt = (TextBox)dlst.Items[j].FindControl("txt_code");
                    HiddenField hdn = (HiddenField)dlst.Items[j].FindControl("hdn_ApiId");
                    if (txt.Text != "" || txt.Text != null)
                    {
                        var_com = txt.Text;
                    }
                    if (hdn.Value != "" || hdn.Value != null)
                    {
                        var_Apid = Convert.ToInt32(hdn.Value);
                    }
                    dtsave = (DataTable)ViewState["tbldata"];
                    dtsave.Rows.Add(dtsave.NewRow());
                    int r = dtsave.Rows.Count - 1;
                    dtsave.Rows[r]["OpId"] = var_opid;
                    dtsave.Rows[r]["ApiId"] = var_Apid;
                    dtsave.Rows[r]["OpCode"] = var_com;
                }
            }
            string count = objoperator.Insert_OperatorApiCode(dtsave);
            dtsave.Clear();
            dtsave.AcceptChanges();
            ViewState["tbldata"] = dtsave;
            Bindgrid();
            string popupScript = "alert('Data Saved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        catch (Exception ex)
        {
            string popupScript = "alert('Something went wrong');";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        finally
        {
            dt = null;

        }
    }
    public void addColumns()
    {
        dtsave.Columns.Add("OpId", typeof(int));
        dtsave.Columns.Add("ApiId", typeof(int));
        dtsave.Columns.Add("OpCode");
    }
}