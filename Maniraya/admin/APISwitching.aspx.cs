using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ARA_StringHunt;
using System.Data;
using BusinessLogicTier;

public partial class admin_APISwitching : System.Web.UI.Page
{
    public string LoginId = "";
    DataTable dtsave = new DataTable();
    clsRechargeAPI objrecharge = new clsRechargeAPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginId = (string)Session["useradmin"];
        if (LoginId == "" || LoginId == null)
        {
            Response.Redirect("logout.aspx");
        }
        if (!IsPostBack)
        {

        }
        addColumns();
    }
  
    void FillOperator(string OperatorName = "")
    {
        //HddOTP.Value = "0";
        DataTable dt = new DataTable();
        dt = objrecharge.Get_Operator_API(OperatorName);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        foreach (GridViewRow gv in GridView1.Rows)
        {
            DataList datalst = (DataList)gv.FindControl("DataList1");
            HiddenField hdn = (HiddenField)gv.FindControl("hdn_OpId");
            //HddOTP.Value = hdn.Value;
            DataTable dt1 = new DataTable();
            dt1 = objrecharge.Get_Recharge_API();
            if (dt1.Rows.Count > 0)
            {
                datalst.DataSource = dt1;
                datalst.DataBind();
            }
        }

    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        // int opid = 0;
        HiddenField hdn_OpId = (HiddenField)e.Item.Parent.Parent.Parent.FindControl("hdn_OpId");

        CheckBox chk1 = (CheckBox)e.Item.FindControl("chk");
        TextBox txt = (TextBox)e.Item.FindControl("txt_Priority");
        HiddenField hdn = (HiddenField)e.Item.FindControl("hdn_APIId");
        HiddenField hdn_BackUpAPI = (HiddenField)e.Item.Parent.Parent.Parent.FindControl("hdn_BackUpAPI");
        DropDownList ddl_BackUpAPI = (DropDownList)e.Item.Parent.Parent.Parent.FindControl("ddl_BackUpAPI");
        DataSet dt1 = new DataSet();
        dt1 = objrecharge.Get_ApiSwitching(hdn_OpId.Value.ToInt());
        try
        {
            ddl_BackUpAPI.DataSource = dt1.Tables[1];
            ddl_BackUpAPI.DataValueField = "id";
            ddl_BackUpAPI.DataTextField = "Name";
            ddl_BackUpAPI.DataBind();
            ddl_BackUpAPI.SelectedValue = hdn_BackUpAPI.Value.ToInt().ToString();
        }
        catch (Exception ex) { }
        if (dt1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Tables[0].Rows.Count; i++)
            {
                string api = dt1.Tables[0].Rows[i]["ApiId"].ToString();
                if (api == hdn.Value)
                {
                    chk1.Checked = true;
                    txt.Text = dt1.Tables[0].Rows[i]["Priority"].ToString();
                }
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string DownOperators = "-", UPOperators = "-", Roles = "-"; string cmd = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int var_opid = 0;
                HiddenField OPID11 = (HiddenField)GridView1.Rows[i].FindControl("hdn_OpId");
                var_opid = Convert.ToInt32(OPID11.Value);
                Label lbl_OPName = (Label)GridView1.Rows[i].FindControl("lbl_OPName");
                DataList dlst = (DataList)GridView1.Rows[i].FindControl("DataList1");
                HiddenField hdn_IsDown = (HiddenField)GridView1.Rows[i].FindControl("hdn_IsDown");
                CheckBox chk_IsDown = (CheckBox)GridView1.Rows[i].FindControl("chk_IsDown");
                DropDownList ddl_BackUpAPI = (DropDownList)GridView1.Rows[i].FindControl("ddl_BackUpAPI");
                HiddenField hdn_BackUpAPI = (HiddenField)GridView1.Rows[i].FindControl("hdn_BackUpAPI");
                if ((chk_IsDown.Checked != hdn_IsDown.Value.ToBoolean() && (chk_SMS.Checked || chk_Mail.Checked)) || hdn_BackUpAPI.Value != ddl_BackUpAPI.SelectedValue)
                {
                    string sts = "0";
                    if (chk_IsDown.Checked == true)
                    {
                        DownOperators += "," + lbl_OPName.Text; sts = "1";

                    }
                    else
                    {
                        UPOperators += "," + lbl_OPName.Text; sts = "0";
                    }
                    cmd += "update OperatorCode set ISdown=" + sts + ",BackupAPI='" + ddl_BackUpAPI.SelectedValue + "' where id=" + var_opid.ToString() + ";" + Environment.NewLine;
                    objrecharge.FetchData(cmd);
                }
                for (int j = 0; j < dlst.Items.Count; j++)
                {
                    int var_Apid = 0;
                    int var_pri = 1;
                    CheckBox chk1 = (CheckBox)dlst.Items[j].FindControl("chk");
                    TextBox txt = (TextBox)dlst.Items[j].FindControl("txt_Priority");
                    HiddenField hdn = (HiddenField)dlst.Items[j].FindControl("hdn_APIId");
                    if (chk1.Checked == true)
                    {
                        var_Apid = Convert.ToInt32(hdn.Value);
                        var_pri = Convert.ToInt32(txt.Text);
                        dtsave = (DataTable)ViewState["tbldata"];
                        dtsave.Rows.Add(dtsave.NewRow());
                        int r = dtsave.Rows.Count - 1;
                        dtsave.Rows[r]["OpId"] = var_opid;
                        dtsave.Rows[r]["ApiId"] = var_Apid;
                        dtsave.Rows[r]["Priority"] = var_pri;
                    }

                }
            }

            string count = objrecharge.Insert_ApiSwitching(dtsave);
           
            dtsave.Clear();
            dtsave.AcceptChanges();
            ViewState["tbldata"] = dtsave;
            FillOperator(ViewState["OpName"].ToString());
            string popupScript = "alert('Data Saved Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);

        }
        catch (Exception ex)
        {
            string popupScript = "alert('An Error occured, Data coud not be saved!! " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    public void addColumns()
    {
        dtsave.Columns.Add("OpId", typeof(Int32));
        dtsave.Columns.Add("ApiId", typeof(Int32));
        dtsave.Columns.Add("Priority", typeof(Int32));
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataList datalst = (DataList)e.Row.FindControl("DataList1");
    //        HiddenField hdn = (HiddenField)e.Row.FindControl("hdn_OpId");
    //        opid = Convert.ToInt32(hdn.Value);
    //        DataTable dt = new DataTable();
    //        dt = objswitch.Get_Recharge_API();
    //        if (dt.Rows.Count > 0)
    //        {
    //            datalst.DataSource = dt;
    //            datalst.DataBind();
    //        }
    //    }
    //}

    protected void Filter_Click(object sender, EventArgs e)
    {
        ViewState["OpName"] = "";
        LinkButton btn = (LinkButton)sender;
        if (btn.Text != "All") { ViewState["OpName"] = btn.Text; }
        FillOperator(ViewState["OpName"].ToString());
        ViewState["tbldata"] = dtsave;
        try
        {
            GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";
            Session["GClass"] = "expand";
            Session["GridView1"] = GridView1.ClientID;
            //Attribute to hide column in Phone.
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex) { }
    }
}