using BusinessLogicTier;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using DataTier;
using System.Web.UI.WebControls;
public partial class FranchiseeReport : System.Web.UI.Page
{
    clsState objState = new clsState();
    Data ObjData = new Data();
    clsfranchisee objUser = new clsfranchisee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["useradmin"] != null)
            {
                loadfranchiseetype();
                loadcountry();
                loadcountryedit();

                loaduser();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    void loadfranchiseetype()
    {
        DDLstFranchiseeType.Items.Clear();
        DataTable dt = new DataTable();
        dt = objUser.getFranchiseetype();
        DDLstFranchiseeType.DataSource = dt;
        DDLstFranchiseeType.DataTextField = "type";
        DDLstFranchiseeType.DataValueField = "ID";
        DDLstFranchiseeType.DataBind();
        ListItem li = new ListItem("Select Type", "0");
        DDLstFranchiseeType.Items.Insert(0, li);
    }
    void loadcountry()
    {
        ddcountry.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CountryName";
        ddcountry.DataValueField = "CountryID";
        ddcountry.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountry.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountry.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }
    void loadTehsil()
    {
        ddlsttehsil.Items.Clear();
        DataTable dt = new DataTable();
        objState.CityId = ddcity.SelectedValue.ToString();
        dt = objState.getTehsil(objState);

        ddlsttehsil.DataSource = dt;
        ddlsttehsil.DataTextField = "tehsilName";
        ddlsttehsil.DataValueField = "tehsilID";
        ddlsttehsil.DataBind();
        ListItem li = new ListItem("Select Tehsil", "0");
        ddlsttehsil.Items.Insert(0, li);
    }
    protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        loadTehsil();
    }
    void loadcountryedit()
    {
        ddcountryedit.Items.Clear();
        DataTable dt = new DataTable();
        dt = objState.getCountry();
        ddcountryedit.DataSource = dt;
        ddcountryedit.DataTextField = "CountryName";
        ddcountryedit.DataValueField = "CountryID";
        ddcountryedit.DataBind();
        ListItem li = new ListItem("Select Country", "0");
        ddcountryedit.Items.Insert(0, li);
    }
    void loadstateedit()
    {
        ddstateedit.Items.Clear();
        DataTable dt = new DataTable();
        objState.CountryId = ddcountryedit.SelectedValue.ToString();
        dt = objState.getState(objState);

        ddstateedit.DataSource = dt;
        ddstateedit.DataTextField = "StateName";
        ddstateedit.DataValueField = "StateID";
        ddstateedit.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstateedit.Items.Insert(0, li);
    }
    void loadcityedit()
    {
        ddcityedit.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstateedit.SelectedValue.ToString();
        dt = objState.getCity(objState);

        ddcityedit.DataSource = dt;
        ddcityedit.DataTextField = "CityName";
        ddcityedit.DataValueField = "CityID";
        ddcityedit.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcityedit.Items.Insert(0, li);
    }
   
 


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lbluser = (Label)GridView1.Rows[index].FindControl("lbluserid");
            objUser.UserId = lbluser.Text;
            DataTable dt = new DataTable();
            dt = objUser.getUserDetail(objUser);
            if (dt.Rows.Count > 0)
            {
                ViewState["UID"] = objUser.UserId;
                txtnameedit.Text = dt.Rows[0]["username"].ToString();
                txtmobileedit.Text = dt.Rows[0]["mobile"].ToString();
                txtemailedit.Text = dt.Rows[0]["email"].ToString();
                ddgenderedit.SelectedValue = dt.Rows[0]["gender"].ToString();
                txtaddressedit.Text = dt.Rows[0]["address"].ToString();
                ddcountryedit.SelectedValue = dt.Rows[0]["countryid"].ToString();
                loadstateedit();
                ddstateedit.SelectedValue = dt.Rows[0]["stateid"].ToString();
                loadcityedit();
                ddcityedit.SelectedValue = dt.Rows[0]["cityid"].ToString();
                //loadareaedit();
              //  ddltehsiledit.SelectedValue = dt.Rows[0]["tehsilID"].ToString();
                //loadmarket();
              //  ddlmarketedit.SelectedValue = dt.Rows[0]["marketid"].ToString();
                ddareaedit.Text = dt.Rows[0]["areaname"].ToString();
                txtpincodeedit.Text = dt.Rows[0]["pincode"].ToString();
                txtdateofbirthedit.Text = Convert.ToDateTime(dt.Rows[0]["dateofbirth"].ToString()).ToString("dd/MM/yyyy");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        if (e.CommandName == "changeStatus")
        {
            try
            {
                objUser.UserId = e.CommandArgument.ToString();

                if (objUser.changeFranchiseeStatus(objUser) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Status Changed Successfully...!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
            }
            finally
            {
                loaduser();
            }
        }

        if (e.CommandName == "epin")
        {
            try
            {
                objUser.UserId = e.CommandArgument.ToString();

                if (objUser.changeFranchiseeEpinStatus(objUser) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Status Changed Successfully...!')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "javascript", "alert('Please try again...!')", true);
            }
            finally
            {
                loaduser();
            }
        }
        if (e.CommandName == "sedt")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lbluser = (Label)GridView1.Rows[index].FindControl("lbluserid");
            objUser.UserId = lbluser.Text;
            DataTable dt = new DataTable();
            dt = objUser.getUserDetail(objUser);
            if (dt.Rows.Count > 0)
            {
                TxtFranchiseename.Text = dt.Rows[0]["username"].ToString();
                TxtFranchiseeid.Text = dt.Rows[0]["userid"].ToString();
                DDLstFranchiseeType.SelectedValue = dt.Rows[0]["franchiseetype"].ToString();
                txtSponsorId.Text = dt.Rows[0]["sponserid"].ToString();
                loadsusername();

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showModalsponser();", true);

        }

    }
    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstate();
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        loaduser();
    }

    protected void ddltehsiledit_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }




    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=UserReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages


            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }



    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    void loaduser()
    {
        objUser.UserName = txtname.Text;
        objUser.Mobile = txtmobile.Text;
        objUser.Email = txtemail.Text;
        objUser.CityId = ddcity.SelectedValue.ToString();
        objUser.AreaName = "";
        objUser.TehsilId = ddlsttehsil.SelectedValue.ToString();
        if (txtfromdate.Text != "")
        {
            objUser.FromDate = Message.GetIndianDate(txtfromdate.Text);
        }
        else
        {
            objUser.FromDate = DateTime.MinValue;
        }
        if (txttodate.Text != "")
        {
            objUser.ToDate = Message.GetIndianDate(txttodate.Text);
        }
        else
        {
            objUser.ToDate = DateTime.MinValue;
        }

        string noOfRows = "";
        if (ddlRecordFilter.SelectedItem.Text == "All")
            noOfRows = "";

       else if (ddlRecordFilter.SelectedItem.Text == "10")
           noOfRows = "top 10";

        else if (ddlRecordFilter.SelectedItem.Text == "25")
            noOfRows = "top 25";

        else if (ddlRecordFilter.SelectedItem.Text == "50")
            noOfRows = "top 50";

        else if (ddlRecordFilter.SelectedItem.Text == "100")
            noOfRows = "top 100";

        else if (ddlRecordFilter.SelectedItem.Text == "500")
            noOfRows = "top 500";
        objUser.UserId = "";
        DataTable dt = new DataTable();
        dt = getUserReportnew(objUser,noOfRows);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }



    public DataTable getUserReportnew(clsfranchisee objUser, string noofrows)
    {
        string str_query = @"SELECT " + noofrows + @" ud.userid, ud.username,ud.Mobile,ud.Email,ud.Gender,ud.Address,cm.CityName,ud.MentionDate,ld.password, 
                                isnull(ud.balanceamount,0) as balanceamount,ld.status as activeStatus,ft.type, 
                                (case when SignUpImgStatus is not null then ud.SignUpFormImage else null end)SignUpFormImage, 
                                (case when SignUpImgStatus=0 then 'Pending' when SignUpImgStatus=1 then 'Approved' when SignUpImgStatus=2 then 'Rejected' end)SignUpImgStatuss,  
                                (case when PanImgStatus is not null then ud.PanImage else null end)PanImage, 
                                (case when PanImgStatus=0 then 'Pending' when PanImgStatus=1 then 'Approved' when PanImgStatus=2 then 'Rejected' end)PanImgStatuss, 
                                (case when ChequeImgStatus is not null then ud.CancelCheque else null end)CancelCheque, 
                                (case when ChequeImgStatus=0 then 'Pending' when ChequeImgStatus=1 then 'Approved' when ChequeImgStatus=2 then 'Rejected' end)ChequeImgStatuss,
                                (case when AadharImgStatus is not null then ud.AadharImage else null end)AadharImage, 
                                (case when AadharImgStatus is not null then ud.AadharImageBack else null end)AadharImageBack, 
                                (case when AadharImgStatus=0 then 'Pending' when AadharImgStatus=1 then 'Approved' when AadharImgStatus=2 then 'Rejected' end)AadharImgStatuss, 
                                ud.epinGenerationStatus FROM FranchiseeDetail ud LEFT JOIN citymaster cm ON ud.Cityid=cm.CityId  LEFT JOIN franchiseeTypeTb ft ON ud.franchiseetype=ft.id
                                left join Logindetail ld on ud.userid=ld.username where 1=1 ";

        if (objUser.FromDate != DateTime.MinValue && objUser.ToDate != DateTime.MinValue)
        {
            str_query += "  and ud.MentionDate  >= '" + objUser.FromDate + "'   and ud.MentionDate   <= '" + objUser.ToDate + "' ";
        }
        //if (objUser.UserName != "")
        if (!string.IsNullOrEmpty(objUser.UserName))
        {
            str_query += "  and ud.username = '" + objUser.UserName + "' ";
        }
        //if (objUser.UserId != "")
        if (!string.IsNullOrEmpty(objUser.UserId))
        {
            str_query += "  and ud.UserId = '" + objUser.UserId + "' ";
        }
        //if (objUser.Mobile != "")
        if (!string.IsNullOrEmpty(objUser.Mobile))
        {
            str_query += "  and ud.mobile = '" + objUser.Mobile + "' ";
        }
        //if (objUser.Email != "")
        if (!string.IsNullOrEmpty(objUser.Email))
        {
            str_query += "  and ud.email = '" + objUser.Email + "' ";
        }
        if (objUser.CityId != "0" && !string.IsNullOrEmpty(objUser.CityId))
        {
            str_query += "  and ud.cityid = '" + objUser.CityId + "' ";
        }
        if (objUser.TehsilId != "0" && !string.IsNullOrEmpty(objUser.TehsilId))
        {
            str_query += "  and ud.tehsilid = '" + objUser.TehsilId + "' ";
        }

        str_query += " order by ud.MentionDate  desc";

        DataTable dt = null;
        ObjData.StartConnection();
        try
        {
            dt = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            dt = null;
        }
        ObjData.EndConnection();
        return dt;
    }
    protected void ddcountryedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadstateedit();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void ddstateedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcityedit();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void ddcityedit_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objUser.UserId = ViewState["UID"].ToString();
        objUser.UserName = txtnameedit.Text;
        objUser.Mobile = txtmobileedit.Text;
        objUser.Email = txtemailedit.Text;
        objUser.Gender = ddgenderedit.SelectedValue.ToString();
        objUser.Address = txtaddressedit.Text;
        objUser.CityId = ddcityedit.SelectedValue.ToString();
        objUser.CountryId = ddcountryedit.SelectedValue.ToString();
        objUser.StateId = ddstateedit.SelectedValue.ToString();
        objUser.AreaName = ddareaedit.Text;
        objUser.Pincode = txtpincodeedit.Text;
        objUser.DateOfBirth = Message.GetIndianDate(txtdateofbirthedit.Text);
        objUser.TehsilId = ddltehsiledit.SelectedValue;
        objUser.MarketId = ddlmarketedit.SelectedValue;
        string res = objUser.Update_UserProfile(objUser);
        if (res == "t")
        {
            loaduser();
            string popupScript = "alert('User Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopup();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else
        {
            string popupScript = "alert('unknown error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void Btnsponserupdate_Click(object sender, EventArgs e)
    {
        objUser.UserId = TxtFranchiseeid.Text;
        objUser.SponserId = txtSponsorId.Text;
        string res = Update_SponserUserProfile(objUser);
        if (res == "t")
        {
            loaduser();
            string popupScript = "alert('User Edited Successfully');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopupsponser();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else
        {
            string popupScript = "alert('unknown error occurred');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    public string Update_SponserUserProfile(clsfranchisee objUser)
    {
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            s2 = "update FranchiseeDetail  set sponserid='" + objUser.SponserId + "'  where UserId='" + objUser.UserId + "'   ";
            ObjData.RunInsUpDelQueryTrans(s2, tr);

            s2 = "INSERT INTO FranchiseeSponserDetailHistory(Userid,Sponserid,entrydate) values ('" + objUser.UserId + "','" + objUser.SponserId + "',getdate())  ";
            ObjData.RunInsUpDelQueryTrans(s2, tr);
            res = "t";
            tr.Commit();
        }
        catch (Exception ex)
        {
            res = "0";
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return res;
    }
    protected void txtSponsorId_TextChanged(object sender, EventArgs e)
    {
        loadsusername();
    }
    void loadsusername()
    {
        if (DDLstFranchiseeType.SelectedIndex != 0)
        {
            DataTable dt = new DataTable();
            // clsUser objUser = new clsUser();
            objUser.UserId = txtSponsorId.Text;
            dt = objUser.getuserdetailviaprocedure(objUser);
            //  dt = objUser.getUserName(objUser);
            if (dt.Rows.Count > 0)
            {

                if (Convert.ToInt16(dt.Rows[0]["FranchiseeType"].ToString()) == 1)
                {

                    if (txtSponsorId.Text == "F000001")
                    {
                        txtSponsorName.Text = dt.Rows[0]["username"].ToString();
                        TxtType.Text = dt.Rows[0]["type"].ToString();
                    }
                    else
                    {
                        txtSponsorId.Text = "";
                        txtSponsorName.Text = "";
                        string popupScript = "alert('You are creating Gallary Franchiee, Please entry only F000001');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                        string popupScript2 = "Closepopupsponser();";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                    }
                }
                else
                {
                    if (Convert.ToInt16(dt.Rows[0]["FranchiseeType"].ToString()) <= Convert.ToInt16(DDLstFranchiseeType.SelectedValue))
                    {
                        txtSponsorName.Text = dt.Rows[0]["username"].ToString();
                        TxtType.Text = dt.Rows[0]["type"].ToString();

                    }
                    else
                    {
                        txtSponsorId.Text = "";
                        txtSponsorName.Text = "";
                        string popupScript = "alert('Sponsor Id should be higher');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                        string popupScript2 = "Closepopupsponser();";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
                    }

                }


            }
            else
            {
                txtSponsorId.Text = "";
                txtSponsorName.Text = "";
                string popupScript = "alert('Invalid Sponsor Id');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                string popupScript2 = "Closepopupsponser();";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
            }
        }
        else
        {
            txtSponsorId.Text = "";
            txtSponsorName.Text = "";
            string popupScript = "alert('Invalid Sponsor Id');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            string popupScript2 = "Closepopupsponser();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
    }
}