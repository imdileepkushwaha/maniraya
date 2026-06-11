﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DataTier;
using System.Configuration;
using BusinessLogicTier;

public partial class ActivateUserWithEpinNew : System.Web.UI.Page
{
    Data ObjData = new Data();
    clsEPin objEPin = new clsEPin();
    clsUser objUser = new clsUser();
    clsfranchisee objUserf = new clsfranchisee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txtuserid.Text = Session["userid"].ToString();

                loadusername();
                loadAmountepin();
                loadepin();
            }
        }
        else
        {
            Response.Redirect("index.aspx");
        }
    }
    void loadAmountepin()
    {
        DataTable dt = new DataTable();
        objEPin.GenerateUserId = txtuserid.Text;
        dt = getPlanAll();
        DDLstPlan.DataSource = dt;
        DDLstPlan.DataTextField = "planname";
        DDLstPlan.DataValueField = "Planamount";
        DDLstPlan.DataBind();
        ListItem li = new ListItem("Select Plan", "0");
        DDLstPlan.Items.Insert(0, li);
    }
    public DataTable getPlanAll()
    {
        string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where MoneyTransfer=1 ";
        //string str_query = "select *,case when MoneyTransfer=0 then 'NO' else 'YES' end as MoneyTransfer1 from Planmaster where 1=1 ";
        //str_query += " and PlanName Like 'Joining package%' ";

        DataTable ds = null;
        ObjData.StartConnection();
        try
        {
            ds = ObjData.RunDataTable(str_query);
        }
        catch (Exception ex)
        {
            ds = null;
        }
        ObjData.EndConnection();
        return ds;
    }
    void loadepin()
    {
        ddepin.Items.Clear();
        objEPin.GenerateUserId = txtuserid.Text;
        objEPin.Amount = Convert.ToDecimal(DDLstPlan.SelectedValue);
        DataTable dt = new DataTable();
        dt = objEPin.getEPinForRegamount(objEPin);
        ddepin.DataSource = dt;
        ddepin.DataTextField = "EpinNo";
        ddepin.DataValueField = "EpinNo";
        ddepin.DataBind();
        ListItem li = new ListItem("Select E-Pin", "0");
        ddepin.Items.Insert(0, li);
    }
    public DataTable getFranchiseeForRegamount(clsEPin objEPin)
    {
        string str_query = "SELECT Generateuserid FROM EPinMasterFranchisee WHERE EPinNo='"+objEPin.EPinNo+"' ";
       



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
    void loadusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txtuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {

            txtusername.Text = dt.Rows[0]["username"].ToString();


        }
        else
        {
            txtusername.Text = "";
            txtuserid.Text = "";

            Message.Show("Invalid User Id...!!!");
        }
    }

    void loadtransferusername()
    {
        DataTable dt = new DataTable();
        objUser.UserId = txttransferuserid.Text;
        dt = objUser.getUserName(objUser);
        if (dt.Rows.Count > 0)
        {
            objUser.UserId = Session["userid"].ToString();
            //DataTable Dt = objUser.getUserDownlineChkNew(objUser, txttransferuserid.Text);
            //if (Dt.Rows.Count > 0)
            //{
                txttransferusername.Text = dt.Rows[0]["username"].ToString();
            //}
            //else
            //{
            //    txttransferusername.Text = "";
            //    txttransferuserid.Text = "";
            //    Message.Show("this user is not your downline...!!!");
            //}
        }
        else
        {
            txttransferusername.Text = "";
            txttransferuserid.Text = "";
            Message.Show("Invalid User Id...!!!");
        }
    }
    public string activateUserWithEpin(clsUser objUser)
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
            s2 = "sp_activeUserWithEpinNew";
            SqlParameter[] parameter = {              
                    new SqlParameter("@ActivateUserId",objUser.TransferUserId), 
                    new SqlParameter("@UserId",objUser.UserId), 
                    new SqlParameter("@epin",objUser.EpinNo), 
                      new SqlParameter("@franchiseeid",objUser.Bhimno), 
                  
                };
            res = ObjData.RunInsUpDelQueryTransProcScalar(s2, tr, parameter);

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtuserid.Text != "")
        {
            if (txttransferuserid.Text != "")
            {

                if (ddepin.SelectedIndex != 0)
                {
                    objUser.UserId = txtuserid.Text;
                    objUser.TransferUserId = txttransferuserid.Text;
                    objUser.EpinNo = ddepin.SelectedValue;
                    objUser.MentionBy = Session["userid"].ToString();
                    objUser.Bhimno = TxtFranchiseeUserId.Text;
                    string rs = activateUserWithEpin(objUser);
                    if (rs == "t")
                    {
                        Message.Show("record update Successfully...!!!");
                        txttransferuserid.Text = "";
                        txttransferusername.Text = "";
                        //txtuserid.Text = "";
                        // txtusername.Text = "";
                        loadepin();
                    }
                    else
                        if (rs == "f")
                        {
                            Message.Show("this User Id alredy activate...!!!");
                        }
                        else
                            if (rs == "n")
                            {
                                Message.Show("invalid E-Pins...!!!");
                            }
                            else
                            {
                                Message.Show("Unknown Error Occurred...!!!");
                            }
                }
                else
                {
                    Message.Show("select Pin...!!!");
                }

            }
            else
            {
                Message.Show("Enter transfer user id...!!!");
            }
        }
        else
        {
            Message.Show("Enter user id...!!!");
        }
    }

    void loadepinamount()
    {
        objEPin.EPinNo = ddepin.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objEPin.getEPinFullDetail(objEPin);
        if (dt.Rows.Count > 0)
        {
            txtamount.Text = dt.Rows[0]["amount"].ToString();
        }
        else
        {
            txtamount.Text = "";
        }
    }
    void loadepinfranchisee()
    {
        objEPin.EPinNo = ddepin.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = getFranchiseeForRegamount(objEPin);
        if (dt.Rows.Count > 0)
        {
            TxtFranchiseeUserId.Text = dt.Rows[0]["Generateuserid"].ToString();
            franchiseedetail();
        }
        else
        {
            TxtFranchiseeUserId.Text = "";
            Message.Show("wrong Epin Id...!!!");
        }
    }
    protected void ddepin_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepinamount();
       
        loadepinfranchisee();

    }
    protected void txtuserid_TextChanged(object sender, EventArgs e)
    {
        loadusername();
    }
    protected void txttransferuserid_TextChanged(object sender, EventArgs e)
    {
        loadtransferusername();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    protected void DDLstPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadepin();
    }
    protected void TxtFranchiseeUserId_TextChanged(object sender, EventArgs e)
    {
        franchiseedetail();
    }
    void franchiseedetail()
    {
        objUserf.UserId = TxtFranchiseeUserId.Text;
        DataTable dt = new DataTable();
        dt = objUserf.getUserDetail(objUserf);
        if (dt.Rows.Count > 0)
        {
            TxtFranchiseename.Text = dt.Rows[0]["username"].ToString();

        }
        else
        {
            TxtFranchiseeUserId.Text = string.Empty;
            TxtFranchiseename.Text = string.Empty;
            Message.Show("wrong franchisee Id...!!!");
        }
    }
}