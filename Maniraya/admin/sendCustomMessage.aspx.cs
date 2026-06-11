using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicTier;
using System.Data;

public partial class admin_sendCustomMessage : System.Web.UI.Page
{
    DataTable dt;
    clsUser objUser;
    clsSecureService css;
    clsState objState = new clsState();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["useradmin"] != null)
        {
            loadstate();
            loadcity();
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loadcity()
    {
        ddcity.Items.Clear();
        DataTable dt = new DataTable();
        objState.StateId = ddstate.SelectedValue.ToString();
        dt = objState.getCityAll();

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        //objState.CountryId = ddcountry.SelectedValue.ToString();
        objState.CountryId = "1";
        dt = objState.getState(objState);

        ddstate.DataSource = dt;
        ddstate.DataTextField = "StateName";
        ddstate.DataValueField = "StateID";
        ddstate.DataBind();
        ListItem li = new ListItem("Select State", "0");
        ddstate.Items.Insert(0, li);
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = new DataTable();
        objUser = new clsUser();

        try
        {
            if (ddlUserType.SelectedIndex > 0)
            {
                string AddtionalQuery="";
                if (ddstate.SelectedIndex != 0)
                {
                    AddtionalQuery += " and cm.stateId='" + ddstate.SelectedValue + "'";
                }
                if (ddcity.SelectedIndex != 0)
                {
                    AddtionalQuery += " and ud.Cityid='" + ddcity.SelectedValue + "'";
                }
                if (txtname.Text != "")
                {
                    AddtionalQuery += " and ud.username like '%" + txtname.Text + "%'";
                }
                dt = objUser.getUsers(ddlUserType.SelectedValue,AddtionalQuery);

                if (dt.Rows.Count > 0)
                    grdList.DataSource = dt;

                else
                    grdList.EmptyDataText = "No Record(s) Found...";

                grdList.DataBind();

                pnlGrid.Visible = true;
            }
            else
            {
                grdList.EmptyDataText = "No Record(s) Found...";
                grdList.DataBind();

                pnlGrid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
        }
    }

    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        dt = new DataTable();
        objUser = new clsUser();
        css = new clsSecureService();

        try
        {
            foreach (GridViewRow gvr in grdList.Rows)
            {
                CheckBox chkItem = gvr.FindControl("chkItem") as CheckBox;
                Label lblMobile = gvr.FindControl("lblMobile") as Label;

                if (chkItem.Checked)
                {
                    #region Send Custom SMS

                    //clsAPI objapi = new clsAPI();

                    //DataTable dt1 = new DataTable();

                    //dt1 = objapi.GetSmsApi(Session["useradmin"].ToString());

                    //if (dt1.Rows.Count > 0)
                    //{
                    //    string smsBody = txtMessage.Text;

                    //    string smsAPI = dt1.Rows[0]["Url"].ToString();

                    //    smsAPI = smsAPI.Replace("to=mmm", "to=" + lblMobile.Text);

                    //    smsAPI = smsAPI.Replace("smstext=ttt", "smstext=" + smsBody);

                    //    System.Net.WebRequest request = System.Net.HttpWebRequest.Create(smsAPI);
                    //    System.Net.WebResponse response = request.GetResponse();
                    //    System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                    //    string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
                    //    Response.Write(urlText.ToString());

                    //    css.Insert_SendSMS(lblMobile.Text, urlText, smsBody);

                    //    objapi = null;
                    //    dt1 = null;
                    //}

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
            css = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dt = new DataTable();
        objUser = new clsUser();

        try
        {
            if (ddlUserType.SelectedIndex > 0)
            {
                string AddtionalQuery = "";
                if (ddstate.SelectedIndex != 0)
                {
                    AddtionalQuery += " and cm.stateId='" + ddstate.SelectedValue + "'";
                }
                if (ddcity.SelectedIndex != 0)
                {
                    AddtionalQuery += " and ud.Cityid='" + ddcity.SelectedValue + "'";
                }
                if (txtname.Text != "")
                {
                    AddtionalQuery += " and ud.username like '%" + txtname.Text + "%'";
                }
                dt = objUser.getUsers(ddlUserType.SelectedValue, AddtionalQuery);

                if (dt.Rows.Count > 0)
                    grdList.DataSource = dt;

                else
                    grdList.EmptyDataText = "No Record(s) Found...";

                grdList.DataBind();

                pnlGrid.Visible = true;
            }
            else
            {
                grdList.EmptyDataText = "No Record(s) Found...";
                grdList.DataBind();

                pnlGrid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Message.Show(ex.Message);
        }
        finally
        {
            dt = null;
            objUser = null;
        }
    }
}