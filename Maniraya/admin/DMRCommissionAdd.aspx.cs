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
    clsMoneyTransfer objmoneytransfer = new clsMoneyTransfer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            if (!IsPostBack)
            {
                btnAddDMRCommission.Visible = false;
                Session["CheckUpdate"] = null;
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
        btnAddDMRCommission.Visible = false;
        DataSet ds = FetchSlabCommission();
        Session["CountDT"] = ds.Tables[0].Rows.Count;

        if (Convert.ToInt32( Session["CountDT"]) > 0)
            btnAddDMRCommission.Text = "Update DMR Commission";
        else
            btnAddDMRCommission.Text = "Save DMR Commission";
        BindGrid(ds.Tables[0]);
       
    }
    protected DataSet FetchSlabCommission()
    {
        DataSet dt = new DataSet();
        try
        {
            dt = objmoneytransfer.GetCommissionModule1New();
        }
        catch
        {
        }
        return dt;
    }
    protected void BindGrid(DataTable dt)
    {
        try
        {
            if (Convert.ToInt32( Session["CountDT"]) == 0 || Convert.ToInt32(Session["CheckUpdate"]) == 1)
            {
                dt.Rows.Add(dt.NewRow());
            }

            if (dt.Rows.Count > 0)
            {
                //if (Convert.ToInt32(dt.Rows[0][0]) != -1)
                //{
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    SetValuesToGrid();
                    Session[GridView1.ID] = dt;
                  //  GridView1.Rows[dt.Rows.Count - 1].Cells[0].FindControl("lnkSaveNew").Visible = true;
                 //  GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
                   btnAddDMRCommission.Visible = true;
                //}
                //else
                //{
                //    GridView1.DataSource = null;
                //    GridView1.DataBind();
                //    //Error.Visible = true;
                //    //ErrorText.InnerText = dt.Rows[0][1].ToString();
                //}
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch { }
    }
    protected void SetValuesToGrid()
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                DropDownList ddlCommType = row.FindControl("ddlComType") as DropDownList;
                DropDownList ddlAmtType = row.FindControl("ddlAmtType") as DropDownList;
                Label lblCommType = row.FindControl("lblComType") as Label;
                Label lblAmtType = row.FindControl("lblAmtType") as Label;
                ddlCommType.SelectedValue = lblCommType.Text.ToUpper();
                ddlAmtType.SelectedValue = lblAmtType.Text.ToUpper();
                if (Convert.ToInt32( Session["CountDT"]) == 0)
                {
                    ((TextBox)row.FindControl("txtMaxAmt")).Enabled = true;
                    ((TextBox)row.FindControl("txtMinAmt")).Enabled = true;
                }
                if (Convert.ToInt32( Session["CheckUpdate"]) == 1)
                {
                    if (GridView1.Rows[GridView1.Rows.Count - 1].RowIndex == row.RowIndex)
                    {
                        ((TextBox)row.FindControl("txtMaxAmt")).Enabled = true;
                        ((TextBox)row.FindControl("txtMinAmt")).Enabled = true;
                        Session["CheckUpdate"] = null;
                    }
                }

            }
        }
        catch { }
    }
    protected DataTable CopyGridData(int savetype)
    {
        DataTable dt = (DataTable)Session[GridView1.ID];
        dt.Rows.Clear();
        dt.Columns.Clear();
        dt.Columns.Add("ID", typeof(int));
        //dt.Columns.Add("UserID", typeof(int));
        dt.Columns.Add("MinAmt", typeof(int));
        dt.Columns.Add("MaxAmt", typeof(int));
        dt.Columns.Add("ComType", typeof(string));
        dt.Columns.Add("AmtType", typeof(string));
        dt.Columns.Add("AtRate", typeof(decimal));
        dt.Columns.Add("NewRate", typeof(decimal));
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow dr = dt.NewRow();

                dr["ID"] = Convert.ToInt32(dt.Rows.Count+1);
                dr["MinAmt"] = Convert.ToInt32((row.Cells[1].FindControl("txtMinAmt") as TextBox).Text);
                dr["MaxAmt"] = Convert.ToInt32((row.Cells[2].FindControl("txtMaxAmt") as TextBox).Text);
                dr["ComType"] = (row.Cells[3].FindControl("ddlComType") as DropDownList).SelectedValue;
                dr["AmtType"] = (row.Cells[4].FindControl("ddlAmtType") as DropDownList).SelectedValue;
                TextBox txtatrate = (TextBox)row.Cells[5].FindControl("txtAtRate");
                if (txtatrate.Text != "")
                {
                    dr["AtRate"] = Convert.ToDecimal((row.Cells[5].FindControl("txtAtRate") as TextBox).Text);
                }
                else {
                    dr["AtRate"] = Convert.ToDecimal("0");
                }
                
                if (savetype == 1)
                    dr["AtRate"] =Convert.ToDecimal( (row.Cells[5].FindControl("txtNewRate") as TextBox).Text);
                dr["NewRate"] = Convert.ToDecimal((row.Cells[5].FindControl("txtNewRate") as TextBox).Text);
                dt.Rows.Add(dr);
            }
        }
        catch { }

        return dt;
    }
    protected bool ValidateMinMax(DataTable dt)
    {
        bool IsValid = true;
        try
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["MinAmt"]) > 0 && Convert.ToInt32(dt.Rows[i]["MaxAmt"]) > 0 && Convert.ToInt32(dt.Rows[i]["MinAmt"]) < Convert.ToInt32(dt.Rows[i]["MaxAmt"]))
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i != j && ((Convert.ToInt32(dt.Rows[i]["MinAmt"]) >= Convert.ToInt32(dt.Rows[j]["MinAmt"]) && Convert.ToInt32(dt.Rows[i]["MinAmt"]) <= Convert.ToInt32(dt.Rows[j]["MaxAmt"])) || (Convert.ToInt32(dt.Rows[i]["MaxAmt"]) >= Convert.ToInt32(dt.Rows[j]["MinAmt"]) && Convert.ToInt32(dt.Rows[i]["MaxAmt"]) <= Convert.ToInt32(dt.Rows[j]["MaxAmt"]))))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        catch
        {
            return false;
        }
        return IsValid;
    }
    protected void btnAddDMRCommission_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["CountDT"]) == 0)
        {
            DataTable dt = CopyGridData(1);

            if (ValidateMinMax(dt))
            {
                DataTable dtt = objmoneytransfer.insertDMRCommission1(dt);
                if (dtt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtt.Rows[0][0]) == 1)
                    {
                        string popupScript = "alert('"+dtt.Rows[0][1].ToString()+"');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    
                        btnAddDMRCommission.Visible = false;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        //Session["UID"] = null;
                        Session["CountDT"] = null;
                        Session["CheckUpdate"] = null;
                    }
                    else
                    {
                        string popupScript = "alert('" + dtt.Rows[0][1].ToString() + "');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                       
                    }
                }
                else
                {
                    string popupScript = "alert('Something went wrong.Try again...');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
            }
            else
            {
                string popupScript = "alert('Some Min and Max range is not valid. Give min max carefully!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

        }
        else if (Convert.ToInt32(Session["CountDT"]) > 0)
        {
            DataTable dt = CopyGridData(2);
            if (ValidateMinMax(dt))
            {
                DataTable dtt = objmoneytransfer.updateDMRCommissionModule1(dt);
                if (dtt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtt.Rows[0][0]) == 1)
                    {
                        //string popupScript = "alert('" + dtt.Rows[0][1].ToString() + "');";
                        string popupScript = "alert('" + dtt.Rows[0][1].ToString() + "');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                        btnAddDMRCommission.Visible = false;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        Session["CheckUpdate"] = null;
                    }
                    else
                    {
                        string popupScript = "alert('" + dtt.Rows[0][1].ToString() + "');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                        btnAddDMRCommission.Visible = false;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        Session["CheckUpdate"] = null;

                    }
                }
                else
                {
                    string popupScript = "alert('Something went wrong.Try again...');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                }
            }
            else
            {
             
                string popupScript = "alert('Some Min and Max range is not valid. Give min max carefully!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
        }
        loaddata();
    }
    protected void lnkSaveNew_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = CopyGridData(1);
            Session["CheckUpdate"] = "1";
            if (ValidateMinMax(dt))
            {
                BindGrid(dt);
              
                string popupScript = "toastr.success('Success', 'New Range added.');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    

            }
            else
            {
                string popupScript = "alert('Some Min and Max range is not valid. Give min max carefully!');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
               
            }
        }
        catch
        {

        }
    }
}