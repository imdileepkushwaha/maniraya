﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicTier;
using System.Data.SqlClient;
using DataTier;
using System.IO;

public partial class user_PurchaseItemRepurchase : System.Web.UI.Page
{
	  clsState objCState = new clsState();
    clsUser objuser = new clsUser();
    clsProduct objState = new clsProduct();
    clsfranchisee objF = new clsfranchisee();
    private int PageSize = 10;
    DataTable PurchaseDt;
    Decimal TAmt = 0;
    clsAccount objaccount = new clsAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FID"] != null)
                {
                    string Val = Request.QueryString["FID"].ToString();
                    string[] ValList = Val.Split('_');
                    HDPlantype.Value = ValList[1].ToString();
                    HdFranchiseeid.Value = ValList[0].ToString();
                    HDPlanId.Value = ValList[2].ToString();
                    lnksearch.HRef = "FranchiseeSeachNew.aspx";
                }
                txtuserid.Text = Session["userid"].ToString();
                txtuserid.Enabled = false;
                loaduser();
                loadFranchisee();
                loadsusername();
                loadProduct(1);
                loadbankaccount();
                HdFiled.Value = DateTime.Now.Ticks.ToString();
				 RDBtnTRecharge.Checked = true;
				loaduseraddressdetail();
				
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
	 void loaduseraddressdetail()
    {
        DataTable dt = new DataTable();
        objF.UserId = Session["userid"].ToString();
        dt = getuseraddressdetailviaprocedure(objF);
        if (dt.Rows.Count > 0)
        {
            if (RDBtnTRecharge.Checked == true)
            {
                txtaddress.Text = dt.Rows[0]["address"].ToString();
                txtareaname.Text = dt.Rows[0]["AreaName"].ToString();
                txtpincode.Text = dt.Rows[0]["Pincode"].ToString();
                loadstate();
                if (dt.Rows[0]["stateid"].ToString() != "")
                {
                    ddstate.SelectedValue = dt.Rows[0]["stateid"].ToString();
                    loadcity();
                    ddcity.SelectedValue = dt.Rows[0]["cityid"].ToString();
                }
            }
            if (RdBtnUtility.Checked == true)
            {
                txtaddress.Text = dt.Rows[0]["Shippingaddress"].ToString();
                txtareaname.Text = dt.Rows[0]["ShippingAreaName"].ToString();
                txtpincode.Text = dt.Rows[0]["ShippingPincode"].ToString();
                loadstate();
                if (dt.Rows[0]["Shippingstateid"].ToString() != "")
                {
                    ddstate.SelectedValue = dt.Rows[0]["Shippingstateid"].ToString();
                    loadcity();
                    ddcity.SelectedValue = dt.Rows[0]["ShippingCityId"].ToString();
                }
            }
        }
        else
        {


            Message.Show("Invalid Franchisee Id...!!!");
        }
    }
    void loadstate()
    {
        ddstate.Items.Clear();
        DataTable dt = new DataTable();
        objCState.CountryId = "1";
        dt = objCState.getState(objCState);

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
        objCState.StateId = ddstate.SelectedValue.ToString();
        dt = objCState.getCity(objCState);

        ddcity.DataSource = dt;
        ddcity.DataTextField = "CityName";
        ddcity.DataValueField = "CityID";
        ddcity.DataBind();
        ListItem li = new ListItem("Select City", "0");
        ddcity.Items.Insert(0, li);
    }
    public DataTable getuseraddressdetailviaprocedure(clsfranchisee objUser)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        ObjData.StartConnection();
        try
        {
            s2 = "sp_getuseraddressdetail";
            SqlParameter[] parameter = {              
                    new SqlParameter("@UserId",objUser.UserId), 
                   
                 
                  
                };
            Dt = ObjData.RunDataTableProcedure(s2, parameter);



        }
        catch (Exception ex)
        {

        }
        finally
        {
            ObjData.EndConnection();

        }
        return Dt;
    }
    void loaduser()
    {
        DataTable dt = new DataTable();
        objuser.UserId = txtuserid.Text;
        dt = objuser.getUserDetail(objuser);
        if (dt.Rows.Count > 0)
        {

            ViewState["ustate"] = dt.Rows[0]["Statename"].ToString();
        }
        else
        {

            txtuserid.Text = "";

            Message.Show("Invalid User Id...!!!");
        }
    }
    void loadFranchisee()
    {
        DataTable dt = new DataTable();
        objF.UserId = HdFranchiseeid.Value;
        dt = objF.getuserdetailviaprocedure(objF);
        if (dt.Rows.Count > 0)
        {

            ViewState["fstate"] = dt.Rows[0]["statenew"].ToString();
        }
        else
        {


            Message.Show("Invalid Franchisee Id...!!!");
        }
    }
    void loadsusername()
    {
        DataTable dt = new DataTable();
        objuser.UserId = txtuserid.Text;
        dt = getUserNameWithBalance(objuser);
        if (dt.Rows.Count > 0)
        {
           // HDIsdistributer.Value = dt.Rows[0]["Isdistributer"].ToString();
            Lblbalance.Text = dt.Rows[0]["balanceamount"].ToString();
            LblUtility.Text = dt.Rows[0]["utilitybalance"].ToString();
        }
    }
    void loadProduct(int PageIndex)
    {
        int recordCount = 0;
        DataTable dt = new DataTable();
        objState.ProductName = string.Empty;
        objState.Status = string.Empty;
        objState.PurchaseStatus = string.Empty;
        dt = ProductPageWiseFranchisee(PageIndex, PageSize, HdFranchiseeid.Value, HDPlantype.Value, HDPlanId.Value, "0");
        dlCustomers.DataSource = dt;
        dlCustomers.DataBind();
        if (dt.Rows.Count > 0)
        {
            recordCount = Convert.ToInt32(dt.Rows[0]["Count"].ToString());
        }        
        this.PopulatePager(recordCount, PageIndex);
        LblRecordCount.Text = "Showing " + Convert.ToString((PageSize * (PageIndex - 1)) + 1) + " to " + Convert.ToString(PageSize * PageIndex) + " of " + recordCount.ToString() + " entries";

    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the First Page Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("First", "1"));
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }

        //Add the Last Button.
        if (currentPage != pageCount)
        {
            pages.Add(new ListItem("Last", pageCount.ToString()));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.loadProduct(pageIndex);
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "photolarge")
        {
            string id = e.CommandArgument.ToString();
            objState.ProductId = id;
            DataTable Dt = objState.getProduct(objState);
            if (Dt.Rows.Count > 0)
            {
                LblcategoryName123.Text = Dt.Rows[0]["categoryname"].ToString();
                LblProductCode.Text = Dt.Rows[0]["ProductId"].ToString();
                LblProductName.Text = Dt.Rows[0]["ProductName"].ToString();
                LblAmount.Text = Dt.Rows[0]["Amount"].ToString();
                LblBv.Text = Dt.Rows[0]["bv"].ToString();
                LblMRP.Text = Dt.Rows[0]["MRP"].ToString();
                LblDP.Text = Dt.Rows[0]["Amount"].ToString();
                // LblStock.Text = Dt.Rows[0]["Stock"].ToString();
                if (Dt.Rows[0]["Image"].ToString() != "../ProductImage/")
                {
                    Image2.ImageUrl = Dt.Rows[0]["Image"].ToString();
                }
                else
                {
                    Image2.ImageUrl = "../ProductImage/images.png";
                }
                /////////////////////////////////////////////////////////////////
                if (Dt.Rows[0]["Image2"].ToString() != "../ProductImage/" && Dt.Rows[0]["Image2"].ToString() != "")
                {
                    Image3.ImageUrl = Dt.Rows[0]["Image2"].ToString();
                }
                else
                {
                    Image3.ImageUrl = "../ProductImage/images.png";
                }
                /////////////////////////////////////////////////////////////////
                if (Dt.Rows[0]["Image3"].ToString() != "../ProductImage/" && Dt.Rows[0]["Image3"].ToString() != "")
                {
                    Image4.ImageUrl = Dt.Rows[0]["Image3"].ToString();
                }
                else
                {
                    Image4.ImageUrl = "../ProductImage/images.png";
                }
                LblDescription.Text = Dt.Rows[0]["Description"].ToString();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        if (e.CommandName == "BuyProduct")
        {
            ClearValue();
            string id = e.CommandArgument.ToString();
            objState.ProductId = id;
            DataTable Dt = objState.getProduct(objState);
            DataTable StockDt = objState.getCheckStockfranchisee(objState.ProductId, HdFranchiseeid.Value);
            if (Dt.Rows.Count > 0)
            {
                HdBuisnessVolume.Value = Dt.Rows[0]["BV"].ToString();
                HdCatId.Value = Dt.Rows[0]["CategoryID"].ToString();
                TxtProductCode.Text = Dt.Rows[0]["ProductId"].ToString();
                TxtProductName.Text = Dt.Rows[0]["ProductName"].ToString();
                TxtAmount.Text = Dt.Rows[0]["Amount"].ToString();
                TxtMRP.Text = Dt.Rows[0]["MRP"].ToString();
                TxtDP.Text = Dt.Rows[0]["DP"].ToString();
                Txtbv.Text = Dt.Rows[0]["BV"].ToString();
                LblGST.Text = Dt.Rows[0]["GST"].ToString();
                int Stock = Convert.ToInt32(StockDt.Rows[0]["Cr"].ToString()) - Convert.ToInt32(StockDt.Rows[0]["Dr"].ToString());
                ViewState["st"] = Stock.ToString();
                string Imaget = "";
                if (Dt.Rows[0]["Image"].ToString() != "../ProductImage/")
                {
                    Imaget = Dt.Rows[0]["Image"].ToString();
                }
                else
                {
                    Imaget = "../ProductImage/images.png";
                }
                TxtImage.Text = Imaget;
                TxtQuantity.Text = "1";
                TxtTotalAmount.Text = Dt.Rows[0]["Amount"].ToString();
                TxtTotalSV2.Text = Dt.Rows[0]["BV"].ToString();
                TxtTotalDP.Text = Dt.Rows[0]["Amount"].ToString();
                GDoffer.DataSource = null;
                GDoffer.DataBind();
                GetofferProductdata(Convert.ToInt32(TxtProductCode.Text), Convert.ToInt32(TxtQuantity.Text));
            }
            BtnAdd.Text = "Add";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);
        }
    }
    public void CreateDatatable()
    {

        PurchaseDt = new DataTable();
        PurchaseDt.Columns.Add("ProductId");
        PurchaseDt.Columns.Add("ProductName");
        PurchaseDt.Columns.Add("Image");
        PurchaseDt.Columns.Add("Amount");
        PurchaseDt.Columns.Add("MRP");
        PurchaseDt.Columns.Add("BV");
        PurchaseDt.Columns.Add("DP");
        PurchaseDt.Columns.Add("STOCK");
        PurchaseDt.Columns.Add("TOTALBV");
        PurchaseDt.Columns.Add("TOTALDP");
        PurchaseDt.Columns.Add("Quantity");
        PurchaseDt.Columns.Add("TotalAmount");
        PurchaseDt.Columns.Add("CGST");
        PurchaseDt.Columns.Add("SGST");
        PurchaseDt.Columns.Add("IGST");
        PurchaseDt.Columns.Add("PurchaseAmount");
        PurchaseDt.Columns.Add("GSTPER");
        PurchaseDt.Columns.Add("OFFERPRODUCTID");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        decimal j = 0;
        string offerproduct = "";
        if (BtnAdd.Text == "Add")
        {
           
            // if (GridView1.Rows.Count > 0)
            // {
            //     string popupScript = "alert('you can not purchase product more than one at a time !');";
            //     ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            //     string popupScript3 = "Closepopup1();";
            //     ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
            //    return;
            // }
            if (ViewState["st"] != null)
            {
                if (Convert.ToInt32(TxtQuantity.Text) > Convert.ToInt32(ViewState["st"].ToString()))
                {
                    string popupScript = "alert('you can not purchase product more than franchisee stock, Please contact to franchisee !');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    string popupScript3 = "Closepopup1();";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                    return;

                }
            }
            if (PurchaseDt == null)
            {
                CreateDatatable();
            }
            //int h = 0;
            //if (!Int32.TryParse(TxtPurchaseStock.Text, out h))
            //{
            //    string popupScript = "alert('Input only number in Sale Quantity !..');";
            //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            //    string popupScript3 = "Closepopup1();";
            //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
            //    return;
            //}
            DataRow DR;
            foreach (GridViewRow Gr in GridView1.Rows)
            {
                DR = PurchaseDt.NewRow();
                //
                DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                DR["DP"] = ((Label)Gr.FindControl("LblDPAmountG")).Text;
                DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
                DR["TOTALDP"] = ((Label)Gr.FindControl("lblTotalAmountDP")).Text;
                DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;
                DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
                DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
                DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
                DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;
                DR["OFFERPRODUCTID"] = ((Label)Gr.FindControl("LblOfferProduct")).Text;
                PurchaseDt.Rows.Add(DR);

                if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
                {
                    string popupScript = "alert('this Product already add');";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                    string popupScript3 = "Closepopup1();";
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                    return;
                }
            }
            DR = PurchaseDt.NewRow();

            DR["ProductId"] = TxtProductCode.Text;
            DR["ProductName"] = TxtProductName.Text;
            DR["Image"] = TxtImage.Text;
            DR["Amount"] = TxtAmount.Text;
            DR["MRP"] = TxtMRP.Text;
            DR["BV"] = HdBuisnessVolume.Value;
            DR["DP"] = TxtDP.Text;
            DR["STOCK"] = ViewState["st"].ToString();
            DR["TOTALBV"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(HdBuisnessVolume.Value);
            DR["TOTALDP"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtDP.Text);
            DR["Quantity"] = TxtQuantity.Text;
            DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text);
            DR["PurchaseAmount"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text)), 2);
            if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
            {
                DR["CGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text)))) / 2, 2);
                DR["SGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text)))) / 2, 2);
                DR["IGST"] = "0";
            }
            else
            {
                DR["CGST"] = "0";
                DR["SGST"] = "0";
                DR["IGST"] = Math.Round(Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text))), 2);
            }
            DR["GSTPER"] = LblGST.Text;
            DR["OFFERPRODUCTID"] = "";
            PurchaseDt.Rows.Add(DR);
            if (GDoffer.Rows.Count > 0)
            {
                for (int i = 0; i < GDoffer.Rows.Count; i++)
                {
                    RadioButton rdSelection = GDoffer.Rows[i].FindControl("RowSelector") as RadioButton;
                    Label LblProductID = GDoffer.Rows[i].FindControl("LblProductID") as Label;
                    Label LblOfferQuantity = GDoffer.Rows[i].FindControl("LblOfferQuantity") as Label;
                    Label LblOfferAmount = GDoffer.Rows[i].FindControl("LblOfferAmount") as Label;
                    if (rdSelection.Checked)
                    {
                        offerproduct = LblProductID.Text;
                        AddOfferProduct(LblProductID.Text, LblOfferQuantity.Text, LblOfferAmount.Text, TxtProductCode.Text);
                    }
                }
                // AddOfferProduct
            }
           
            foreach (DataRow dr in PurchaseDt.Rows)
            {
                j += Convert.ToDecimal(dr["TotalAmount"]);
            }           
           // if (decimal.TryParse(sumObject.ToString(), out j))
           // {
            GetofferAmountProductdata(j, PurchaseDt);
           // }
            ViewState["PDT"] = PurchaseDt;
            GridView1.DataSource = PurchaseDt;
            GridView1.DataBind();

           
            
               
            //if (offerproduct != "")
            //{
            //    foreach (GridViewRow Gr in GridView1.Rows)
            //    {
            //        if (((Label)Gr.FindControl("LblProductCodeG")).Text == offerproduct)
            //        {
            //            ((Label)Gr.FindControl("LblOfferProduct")).Text = TxtProductCode.Text;
            //        }
            //        if (((Label)Gr.FindControl("LblOfferProduct")).Text != "")
            //        {
            //            ((LinkButton)Gr.FindControl("lbEdit")).Visible = false;
            //            ((LinkButton)Gr.FindControl("lbDelete")).Visible = false;
            //        }
            //    }
            //}
            ClearValue();
            PurchasePanel.Visible = true;
            
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
        else
        {
            if (PurchaseDt == null)
            {
                CreateDatatable();
            }
            DataRow DR;
            foreach (GridViewRow Gr in GridView1.Rows)
            {
                if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
                {                    
                    DR = PurchaseDt.NewRow();
                    DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                    DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                    DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                    DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                    DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                    DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                    DR["DP"] = ((Label)Gr.FindControl("LblDPAmountG")).Text;
                    DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                    DR["TOTALBV"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(HdBuisnessVolume.Value);
                    DR["TOTALDP"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(((Label)Gr.FindControl("LblDPAmountG")).Text);
                    DR["Quantity"] = TxtQuantity.Text;
                    DR["TotalAmount"] = Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text);
                    DR["PurchaseAmount"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text)), 2);
                    if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
                    {
                        DR["CGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text)))) / 2, 2);
                        DR["SGST"] = Math.Round((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text)))) / 2, 2);
                        DR["IGST"] = "0";
                    }
                    else
                    {
                        DR["CGST"] = "0";
                        DR["SGST"] = "0";
                        DR["IGST"] = Math.Round(Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) - ((Convert.ToDecimal(TxtQuantity.Text) * Convert.ToDecimal(TxtAmount.Text) * 100) / (100 + Convert.ToDecimal(LblGST.Text))), 2);
                    }
                    DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;
                    DR["OFFERPRODUCTID"] = ((Label)Gr.FindControl("LblOfferProduct")).Text;
                    if (Convert.ToInt32(((Label)Gr.FindControl("LblStock")).Text) < Convert.ToInt32(TxtQuantity.Text))
                    {
                        string popupScript = "alert('you can not purchase product more than franchisee stock, Please contact to franchisee !');";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                        string popupScript3 = "Closepopup1();";
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                        return;
                    }
                    if (GDoffer.Rows.Count > 0)
                    {
                        for (int i = 0; i < GDoffer.Rows.Count; i++)
                        {
                            RadioButton rdSelection = GDoffer.Rows[i].FindControl("RowSelector") as RadioButton;
                            Label LblProductID = GDoffer.Rows[i].FindControl("LblProductID") as Label;
                            Label LblOfferQuantity = GDoffer.Rows[i].FindControl("LblOfferQuantity") as Label;
                            Label LblOfferAmount = GDoffer.Rows[i].FindControl("LblOfferAmount") as Label;
                            if (rdSelection.Checked)
                            {
                                offerproduct = LblProductID.Text;
                                AddOfferProduct(LblProductID.Text, LblOfferQuantity.Text, LblOfferAmount.Text, TxtProductCode.Text);
                            }
                        }
                        // AddOfferProduct
                    }
                    PurchaseDt.Rows.Add(DR);
                }
                else
                {
                    if (((Label)Gr.FindControl("LblOfferProduct")).Text != TxtProductCode.Text)
                    {
                        DR = PurchaseDt.NewRow();
                        DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                        DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
                        DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
                        DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
                        DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
                        DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
                        DR["DP"] = ((Label)Gr.FindControl("LblDPAmountG")).Text;
                        DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
                        DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
                        DR["TOTALDP"] = ((Label)Gr.FindControl("lblTotalAmountDP")).Text;
                        DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
                        DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
                        DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;
                        DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
                        DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
                        DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
                        DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;
                        DR["OFFERPRODUCTID"] = ((Label)Gr.FindControl("LblOfferProduct")).Text;
                        PurchaseDt.Rows.Add(DR);
                    }
                }
            }

            foreach (DataRow dr in PurchaseDt.Rows)
            {
                j += Convert.ToDecimal(dr["TotalAmount"]);
            }
            // if (decimal.TryParse(sumObject.ToString(), out j))
            // {
            GetofferAmountProductdata(j, PurchaseDt);

            ViewState["PDT"] = PurchaseDt;
            
            GridView1.DataSource = PurchaseDt;
            GridView1.DataBind();
            //if (offerproduct != "")
            //{
            //    foreach (GridViewRow Gr in GridView1.Rows)
            //    {
            //        if (((Label)Gr.FindControl("LblProductCodeG")).Text == offerproduct)
            //        {
            //            ((Label)Gr.FindControl("LblOfferProduct")).Text = TxtProductCode.Text;
            //        }
            //        if (((Label)Gr.FindControl("LblOfferProduct")).Text != "")
            //        {
            //            ((LinkButton)Gr.FindControl("lbEdit")).Visible = false;
            //            ((LinkButton)Gr.FindControl("lbDelete")).Visible = false;
            //        }
                   
            //    }
            //}
            ClearValue();
            PurchasePanel.Visible = true;
            string popupScript2 = "Closepopup1();";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            // HdCatId.Value = ((Label)GridView1.Rows[index].FindControl("LblCatId")).Text;
            TxtProductCode.Text = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
            TxtProductName.Text = ((Label)GridView1.Rows[index].FindControl("LblProductNameG")).Text;
            TxtAmount.Text = ((Label)GridView1.Rows[index].FindControl("LblProductAmountG")).Text;
            TxtDP.Text = ((Label)GridView1.Rows[index].FindControl("LblDPAmountG")).Text;
            TxtQuantity.Text = ((Label)GridView1.Rows[index].FindControl("lblQuantity")).Text;
            TxtTotalAmount.Text = ((Label)GridView1.Rows[index].FindControl("lblTotalAmount")).Text;
            TxtImage.Text = ((Label)GridView1.Rows[index].FindControl("LblProductImageG")).Text;
            TxtMRP.Text = ((Label)GridView1.Rows[index].FindControl("LBlMrp")).Text;
            LblGST.Text = ((Label)GridView1.Rows[index].FindControl("LblGSTPER")).Text;
            BtnAdd.Text = "Update";
            GetofferProductdata(Convert.ToInt32(TxtProductCode.Text), Convert.ToInt32(TxtQuantity.Text));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal1();", true);

        }
        if (e.CommandName == "del")
        {
            string offerid = "";
            string id = "";
           
           
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {

                int index = Convert.ToInt32(e.CommandArgument.ToString());
                 id = ((Label)GridView1.Rows[index].FindControl("LblProductCodeG")).Text;
                 foreach (GridViewRow Gr in GridView1.Rows)
                 {
                     if (((Label)Gr.FindControl("LblOfferProduct")).Text == id)
                     {
                         offerid = ((Label)Gr.FindControl("LblProductCodeG")).Text;
                     }

                 }
                DataTable table = ViewState["PDT"] as DataTable;
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = table.Rows[i];
                    if (dr["ProductId"].ToString() == offerid)
                        dr.Delete();
                }
                table.AcceptChanges();
                table.Rows.RemoveAt(index);
                decimal j = 0;
                foreach (DataRow dr in table.Rows)
                {
                    j += Convert.ToDecimal(dr["TotalAmount"]);
                }
                DataTable dt = GetofferAmountProductdatatable(j.ToString());
                if (dt.Rows.Count == 0)
                {
                    for (int i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = table.Rows[i];
                        if (dr["OfferProductID"].ToString() == "999999")
                            dr.Delete();
                    }
                    table.AcceptChanges();
                }
                ViewState["PDT"] = table;
                GridView1.DataSource = table;
                GridView1.DataBind();
                if (GridView1.Rows.Count == 0)
                {
                    PurchasePanel.Visible = false;
                }
                //ViewState["PDT"] = PurchaseDt;
            }
           
        }
    }
    private void ClearValue()
    {
        TxtProductCode.Text = "";
        TxtProductName.Text = "";
        TxtImage.Text = "";
        TxtAmount.Text = "";
        TxtQuantity.Text = "";
        TxtTotalAmount.Text = "";
    }
    Decimal total = 0;
    Decimal tt = 0;
    Decimal totalPurchase = 0;
    Decimal totalSV = 0;
    Decimal totalGST = 0;
    Decimal totalCGST = 0;
    Decimal totalSGST = 0;
    Decimal totalIGST = 0;
    Decimal totalDP = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            total += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalAmount")).Text);
            totalDP += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalAmountDP")).Text);
            totalSV += Convert.ToDecimal(((Label)e.Row.FindControl("LblTotalBv")).Text);
            totalPurchase += Convert.ToDecimal(((Label)e.Row.FindControl("LblPurchaseAmount")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblIGST")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblSGST")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblCGST")).Text);

            totalCGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblCGST")).Text);
            totalSGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblSGST")).Text);
            totalIGST += Convert.ToDecimal(((Label)e.Row.FindControl("LblIGST")).Text);
            Label lblAmount = (Label)e.Row.FindControl("LblOfferProduct");
            if (lblAmount.Text != "")
            {
                ((LinkButton)e.Row.FindControl("lbEdit")).Visible = false;
                ((LinkButton)e.Row.FindControl("lbDelete")).Visible = false;
            }
           
                    
            
           

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount = (Label)e.Row.FindControl("lblGrandTotal");
            Label lblAmountDP = (Label)e.Row.FindControl("lblGrandTotalDP");
            Label lblsvtotal = (Label)e.Row.FindControl("lblsvtotal");
            lblAmount.Text = total.ToString();
            lblAmountDP.Text = totalDP.ToString();
            HDTotal.Value = total.ToString();
            lblsvtotal.Text = totalSV.ToString();
            TxtTotalpurchase.Text = totalPurchase.ToString();
            TxtTotalSV.Text = totalSV.ToString();

           // TxtTotalSV2.Text = totalSV.ToString();
            TxtTotalCGST.Text = totalCGST.ToString();
            TxtTotalSGST.Text = totalSGST.ToString();
            TxtTotalIGST.Text = totalIGST.ToString();

            TxtShipping.Text = "0";
            //if (TxtTotalpurchase.Text != string.Empty)
            //{
            //    if (Convert.ToDecimal(TxtTotalpurchase.Text) < 1000)
            //    {
            //        TxtShipping.Text = "0";
            //    }
            //    if (Convert.ToDecimal(TxtTotalpurchase.Text) > 1000 && Convert.ToDecimal(TxtTotalpurchase.Text) < 3500)
            //    {
            //        TxtShipping.Text = "0";
            //    }
            //    if (Convert.ToDecimal(TxtTotalpurchase.Text) > 3500)
            //    {
            //        TxtShipping.Text = "0";
            //    }
            //}
            if (Decimal.TryParse(TxtTotalpurchase.Text, out tt))
            {
                TXTTTAmount.Text = Convert.ToString(total + Convert.ToDecimal(TxtShipping.Text));
            }
            if (Decimal.TryParse(TxtTotalpurchase.Text, out tt))
            {
                TXTTTDP.Text = Convert.ToString(totalDP + Convert.ToDecimal(TxtShipping.Text));
            }
           
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        PurchasePanel.Visible = false;
        if (ViewState["PDT"] != null)
        {
            ViewState["PDT"] = null;
        }
        PurchaseDt = null;
        if (GridView1.Rows.Count == 0)
        {
            TxtTotalpurchase.Text = "0";
            TXTTTAmount.Text = "0";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0)
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (Convert.ToDecimal(HDTotal.Value) <= 0)
        {
            string popupScript = "alert('Buy any Product');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        if (Convert.ToString(HDFilename.Value) == "")
        {
            string popupScript = "alert('Upload transaction receipt');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            return;
        }
        objState.PurchaseAmount = Convert.ToDecimal(TxtTotalpurchase.Text);
        objState.CGST = Convert.ToDecimal(TxtTotalCGST.Text);
        objState.SGST = Convert.ToDecimal(TxtTotalSGST.Text);
        objState.IGST = Convert.ToDecimal(TxtTotalIGST.Text);
        objState.TotalAmount = Convert.ToDecimal(HDTotal.Value);
        objState.FranchiseeID = HdFranchiseeid.Value;
        objState.UserId = txtuserid.Text;
        objState.ProductId = HDPlantype.Value;
        objState.PaymentMode = ddmode.SelectedValue;
        objState.TransactionCode = TxtTransactionId.Text;
        objState.tehsilid = ddbankaccountno.SelectedValue;
        objState.ProductImage = HDFilename.Value;//UploadImage();
		 Update_Usershipping(txtuserid.Text,txtaddress.Text,ddcity.SelectedValue,txtareaname.Text,txtpincode.Text);
         DataTable ptable = ViewState["PDT"] as DataTable;
         ptable.Columns.Remove("OFFERPRODUCTID");
         ViewState["PDT"] = ptable;
        string i = AddPurchase(objState, ViewState["PDT"] as DataTable, Convert.ToDecimal(TxtShipping.Text));
        if (i == "1")
        {
            string popupScript = "alert('Purchase Successfull');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            GridView1.DataSource = null;
            GridView1.DataBind();
            PurchasePanel.Visible = false;
            if (ViewState["PDT"] != null)
            {
                ViewState["PDT"] = null;
            }
            PurchaseDt = null;

        }
        else if (i == "2")
        {
            string popupScript = "alert('you have insufficient balance');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "3")
        {
            string popupScript = "alert('insufficient stock');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "4")
        {
            string popupScript = "alert('Your already topup with this plan !');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "5")
        {
            string popupScript = "alert('Already one request is pending please approve/Reject first !');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "6")
        {
            string popupScript = "alert('you can purchase only 1000/2000/3000/4000 for freedom Plan !');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "7")
        {
            string popupScript = "alert('you can purchase only 600/1200/1800/2400 for Unity Plan !');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "8")
        {
            string popupScript = "alert('you can purchase only 2000 for Global Plan !');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else if (i == "9")
        {
            string popupScript = "alert('First purchase minimum should be 26 Point!');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
        else
        {
            string popupScript = "alert('unknown error');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
        }
    }
    Data ObjData = new Data();
    public string AddPurchase(clsProduct objP, DataTable Dt, Decimal shipping)
    {
        int i = 0;

        string res = "";
        string s2 = "";
        string ChkStock = "1";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {

            //foreach (DataRow Dr in Dt.Rows)
            //{
            //    string Q = "select isnull(Sum(CrQuantity),0) -isnull(Sum(DrQuantity),0) FROM StockMaster where ProductId='" + Dr["ProductId"].ToString() + "'";
            //    DataSet Ds = ObjData.RunSelectQueryTrans(Q, tr);
            //    if (Convert.ToInt32(Dr["Quantity"].ToString()) > Convert.ToInt32(Ds.Tables[0].Rows[0][0].ToString()))
            //    {
            //        ChkStock = "0";
            //    }
            //}
            if (ChkStock == "1")
            {
                s2 = "sp_add_PurchaseRepurchase";
                SqlParameter[] parameter = {
                    new SqlParameter("@UserId",objP.UserId),
                       new SqlParameter("@PurchaseAmount",objP.PurchaseAmount),
                          new SqlParameter("@CGSTAmount",objP.CGST),
                             new SqlParameter("@SGSTAmount",objP.SGST),
                                 new SqlParameter("@IGSTAmount",objP.IGST),
                                   new SqlParameter("@CGSTPer","0"),
                             new SqlParameter("@SGSTPer","0"),
                                 new SqlParameter("@IGSTPer","0"),
                                    new SqlParameter("@Paybleamount",objP.TotalAmount),
                    new SqlParameter("@FranchiseeId",objP.FranchiseeID),
                       new SqlParameter("@Cashamount","0"),
                       new SqlParameter("@RestAmount","0"),
					   new SqlParameter("@Plantype",objState.ProductId),
                    new SqlParameter("@PurchaseProduct",Dt),
					     new SqlParameter("@BankID",objState.tehsilid),
                        new SqlParameter("@Onlinetransactionid",objState.TransactionCode),
                          new SqlParameter("@PaymentMode",objState.PaymentMode),
					 new SqlParameter("@Img",objState.ProductImage),
                      new SqlParameter("@Isdistributer","0"),
                     
                };
                DataTable Dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = Dtresult.Rows[0][1].ToString();

                tr.Commit();
            }
            else
            {
                res = "3";
            }



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
    public DataTable ProductPageWise(int pageindex, int pageSize)
    {
        DataTable Dt = new DataTable();
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "GetProductPageWise";
            SqlParameter[] parameter = {
                new SqlParameter("@PageIndex",pageindex),
                new SqlParameter("@PageSize",pageSize),
                 new SqlParameter("@RecordCount",4)
                };
            Dt = ObjData.RunDataTableProcedureTRansGetPage(s2, tr, parameter);

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
        return Dt;
    }

    public DataTable ProductPageWiseFranchisee(int pageindex, int pageSize, string FranchiseeId, string Plantype, string Planid,string isdistributer)
    {
        DataTable Dt = new DataTable();
        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataSet ds = new DataSet();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);

        try
        {
            s2 = "GetProductPageWiseFranchiseeRepurchase";
            SqlParameter[] parameter = {
                new SqlParameter("@PageIndex",pageindex),
                new SqlParameter("@PageSize",pageSize),
                 new SqlParameter("@Plantype",Plantype),
                new SqlParameter("@FranchiseeId",FranchiseeId),
                 new SqlParameter("@PlanId",Planid),   
                   new SqlParameter("@Isdistributer",isdistributer),  
                 new SqlParameter("@RecordCount",4)
                };
            Dt = ObjData.RunDataTableProcedureTRansGetPage(s2, tr, parameter);

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
        return Dt;
    }


    public DataTable getUserNameWithBalance(clsUser objUser)
    {
        //string str_query = "select isnull( sum(CrAmount),0) as sumCr from transactiondetail td where td.UserID='" + objuser.UserId + "'";

        string str_query = "SELECT ud.userid, ud.username,ud.mobile,ud.balanceamount,ud.utilitybalance,isnull(Isdistributer,0) Isdistributer FROM userdetail ud where ud.UserId = '" + objUser.UserId + "' ";
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
    protected void TxtQuantity_TextChanged(object sender, EventArgs e)
    {
        Int32 g = 0;
        if (int.TryParse(TxtQuantity.Text, out g))
        {
            TxtTotalAmount.Text = Convert.ToString(Convert.ToDecimal(TxtAmount.Text) * Convert.ToDecimal(TxtQuantity.Text));
            TxtTotalDP.Text = Convert.ToString(Convert.ToDecimal(TxtDP.Text) * Convert.ToDecimal(TxtQuantity.Text));
            TxtTotalSV2.Text = Convert.ToString(Convert.ToDecimal(Txtbv.Text) * Convert.ToDecimal(TxtQuantity.Text));
            GetofferProductdata(Convert.ToInt32(TxtProductCode.Text), Convert.ToInt32(TxtQuantity.Text));
            
        }
        else
        {

            string popupScript = "alert('Enter only numeric number');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);


        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "Closepopup1();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "showModal1();", true);
    }
    void loadbankaccount()
    {
        ddbankaccountno.Items.Clear();
        DataTable dt = new DataTable();
        dt = objaccount.getCompanyAccountDetail();
        ddbankaccountno.DataSource = dt;
        ddbankaccountno.DataTextField = "accno2";
        ddbankaccountno.DataValueField = "id";
        ddbankaccountno.DataBind();
        ListItem li = new ListItem("Select Bank Account", "0");
        ddbankaccountno.Items.Insert(0, li);

    }
    protected void ddbankaccountno_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadaccountdetail();
    }
    void loadaccountdetail()
    {
        objaccount.Id = ddbankaccountno.SelectedValue.ToString();
        DataTable dt = new DataTable();
        dt = objaccount.getCompanyAccountDetailById(objaccount);
        if (dt.Rows.Count > 0)
        {
            txtdepositaccountno.Text = dt.Rows[0]["accountno"].ToString();
            txtaccountholdername.Text = dt.Rows[0]["AccountHolderName"].ToString();
            txtdepositbank.Text = dt.Rows[0]["BankName"].ToString();
            txtifsccode.Text = dt.Rows[0]["IFSCCode"].ToString();
            QR.ImageUrl = "../ProductImage/" + dt.Rows[0]["BranchName"].ToString();
        }
        else
        {
            txtdepositaccountno.Text = "";
            txtaccountholdername.Text = "";
            txtdepositbank.Text = "";
            txtifsccode.Text = "";
            QR.ImageUrl = "";
        }

    }
    public string UploadImage()
    {
        string Imagename = "";
        if (ImageUpload.HasFile)
        {
            string RandomNumber = DateTime.Now.Ticks.ToString();
            string fileName = Path.GetFileName(ImageUpload.PostedFile.FileName);
            Imagename = RandomNumber + fileName;
            ImageUpload.PostedFile.SaveAs(Server.MapPath("~/ProductImage/") + Imagename);

        }
        return Imagename;
    }
	 protected void RDBtnTRecharge_CheckedChanged(object sender, EventArgs e)
    {
        loaduseraddressdetail();
    }
    protected void RdBtnUtility_CheckedChanged(object sender, EventArgs e)
    {
        loaduseraddressdetail();
    }
	  protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcity();
    }
	 public string Update_Usershipping(string userid,string address,string city,string area,string pincode)
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
            s2 = "update UserDetail  set Shippingaddress='" + address + "',ShippingCityId='" + city + "', ShippingAreaName='" + area + "',ShippingPincode='" + pincode + "'  where UserId='" + userid + "'   ";
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
     protected void btnuploademo_Click(object sender, EventArgs e)
     {
         System.Threading.Thread.Sleep(2000);
         ImageButton1.ImageUrl = "../ProductImage/" + HDFilename.Value;
     }
     public DataTable GetofferProductdatatable(int Productid, int quantity)
     {
         DataTable Dt = new DataTable();
         string res = "";
         string s2 = "";
         SqlConnection cn;
         SqlTransaction tr = null;
         DataSet ds = new DataSet();
         cn = ObjData.StartConnectionInTransaction();
         tr = cn.BeginTransaction(IsolationLevel.Serializable);

         try
         {
             s2 = "getofferproduct";
             SqlParameter[] parameter = {
                new SqlParameter("@ProductId",Productid),
                new SqlParameter("@Quantity",quantity),
               
                };
             Dt = ObjData.RunDataTableProcedureTRansGetPage(s2, tr, parameter);

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
         return Dt;
     }
     private void GetofferProductdata(int productid, int quantity)
     {
         GDoffer.DataSource = null;
         GDoffer.DataBind();
         DataTable dt = GetofferProductdatatable(productid, quantity);
         if (dt != null && dt.Rows.Count > 0)
         {
             GDoffer.DataSource = dt;
             GDoffer.DataBind();

             for (int i = 0; i < GDoffer.Rows.Count; i++)
             {
                 RadioButton rdSelection = GDoffer.Rows[i].FindControl("RowSelector") as RadioButton;

                 if (i==0)
                 {
                     rdSelection.Checked = true;
                 }
             }
         }
     }
     private void AddOfferProduct(string productId,string Quantity,string amount,string offerproduct)
     {
         clsProduct objP = new clsProduct();
         int Stock = 0;
         DataTable Dt = objP.getProductForPurchaseselect(productId);
         DataTable StockDt = objState.getCheckStockfranchisee(productId, HdFranchiseeid.Value);
         if (StockDt.Rows.Count > 0)
         {
             Stock = Convert.ToInt32(StockDt.Rows[0]["Cr"].ToString()) - Convert.ToInt32(StockDt.Rows[0]["Dr"].ToString());
         }
        
         ViewState["offerst"] = Stock.ToString();
         //if (PurchaseDt == null)
         //{
         //    CreateDatatable();
         //}
         //int h = 0;
         //if (!Int32.TryParse(TxtPurchaseStock.Text, out h))
         //{
         //    string popupScript = "alert('Input only number in Sale Quantity !..');";
         //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
         //    string popupScript3 = "Closepopup1();";
         //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
         //    return;
         //}
         DataRow DRW;
         //foreach (GridViewRow Gr in GridView1.Rows)
         //{
         //    DR = PurchaseDt.NewRow();
         //    //
             
         //    DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
         //    DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
         //    DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
         //    DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
         //    DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
         //    DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
         //    DR["DP"] = ((Label)Gr.FindControl("LblDPAmountG")).Text;
         //    DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
         //    DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
         //    DR["TOTALDP"] = ((Label)Gr.FindControl("lblTotalAmountDP")).Text;
         //    DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
         //    DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
         //    DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;
         //    DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
         //    DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
         //    DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
         //    DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;

         //    PurchaseDt.Rows.Add(DR);

         //    if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
         //    {
         //        string popupScript = "alert('this Product already add');";
         //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
         //        string popupScript3 = "Closepopup1();";
         //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
         //        return;
         //    }
         //}
         if (ViewState["offerst"] != null)
         {
             if (Convert.ToInt32(Quantity) > Convert.ToInt32(ViewState["offerst"].ToString()))
             {
                 string popupScript = "alert('you can not purchase offer product more than franchisee stock, Please contact to franchisee !');";
                 ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                 string popupScript3 = "Closepopup1();";
                 ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                 return;

             }
         }
         DRW = PurchaseDt.NewRow();

         DRW["ProductId"] = productId;
         DRW["ProductName"] = Dt.Rows[0]["productname"].ToString();
         DRW["Image"] = Dt.Rows[0]["Image"].ToString();
         DRW["Amount"] = amount;
         DRW["MRP"] = Dt.Rows[0]["MRP"].ToString();
         DRW["BV"] = Dt.Rows[0]["BV"].ToString();
         DRW["DP"] = amount;
         DRW["STOCK"] = ViewState["offerst"].ToString();
         DRW["TOTALBV"] = Convert.ToDecimal(Quantity) * Convert.ToDecimal(Dt.Rows[0]["BV"].ToString());
         DRW["TOTALDP"] = Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount);
         DRW["Quantity"] = Quantity;
         DRW["TotalAmount"] = Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount);
         DRW["PurchaseAmount"] = Math.Round((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString())), 2);
         if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
         {
             DRW["CGST"] = Math.Round((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) - ((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString())))) / 2, 2);
             DRW["SGST"] = Math.Round((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) - ((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString())))) / 2, 2);
             DRW["IGST"] = "0";
         }
         else
         {
             DRW["CGST"] = "0";
             DRW["SGST"] = "0";
             DRW["IGST"] = Math.Round(Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) - ((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString()))), 2);
         }
         DRW["GSTPER"] = Dt.Rows[0]["GST"].ToString();
         DRW["OFFERPRODUCTID"] = offerproduct;
         PurchaseDt.Rows.Add(DRW);
         //ViewState["PDT"] = PurchaseDt;
         //GridView1.DataSource = PurchaseDt;
         //GridView1.DataBind();
         //ClearValue();
         //PurchasePanel.Visible = true;
         //   string popupScript2 = "Closepopupdivfamily();";
         //   ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);

     }
     public DataTable GetofferAmountProductdatatable(string Amount)
     {
         DataTable Dt = new DataTable();
         string res = "";
         string s2 = "";
         SqlConnection cn;
         SqlTransaction tr = null;
         DataSet ds = new DataSet();
         cn = ObjData.StartConnectionInTransaction();
         tr = cn.BeginTransaction(IsolationLevel.Serializable);

         try
         {
             s2 = "getofferAmount";
             SqlParameter[] parameter = {
                new SqlParameter("@Amount",Amount),
              
               
                };
             Dt = ObjData.RunDataTableProcedureTRansGetPage(s2, tr, parameter);

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
         return Dt;
     }
     private void GetofferAmountProductdata(Decimal Amount,DataTable Dt)
     {
        
         DataTable dt = GetofferAmountProductdatatable(Amount.ToString());
         if (dt != null && dt.Rows.Count > 0)
         {
             for (int i = Dt.Rows.Count - 1; i >= 0; i--)
             {
                 DataRow dr = Dt.Rows[i];
                 if (dr["OfferProductID"].ToString() == "999999")
                     dr.Delete();
             }
             Dt.AcceptChanges();
             AddOfferProductAmount(dt.Rows[0]["OfferProductID"].ToString(), dt.Rows[0]["OfferQuantity"].ToString(), dt.Rows[0]["OfferAmount"].ToString(), "999999");
         }
         else
         {
             for (int i = Dt.Rows.Count - 1; i >= 0; i--)
             {
                 DataRow dr = Dt.Rows[i];
                 if (dr["OfferProductID"].ToString() == "999999")
                     dr.Delete();
             }
             Dt.AcceptChanges();
         }
     }
     private void AddOfferProductAmount(string productId, string Quantity, string amount, string offerAmount)
     {
         clsProduct objP = new clsProduct();
         int Stock = 0;
         DataTable Dt = objP.getProductForPurchaseselect(productId);
         DataTable StockDt = objState.getCheckStockfranchisee(productId, HdFranchiseeid.Value);
         if (StockDt.Rows.Count > 0)
         {
             Stock = Convert.ToInt32(StockDt.Rows[0]["Cr"].ToString()) - Convert.ToInt32(StockDt.Rows[0]["Dr"].ToString());
         }

         ViewState["offeramtst"] = Stock.ToString();
         //if (PurchaseDt == null)
         //{
         //    CreateDatatable();
         //}
         //int h = 0;
         //if (!Int32.TryParse(TxtPurchaseStock.Text, out h))
         //{
         //    string popupScript = "alert('Input only number in Sale Quantity !..');";
         //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
         //    string popupScript3 = "Closepopup1();";
         //    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
         //    return;
         //}
         DataRow DRW;
         //foreach (GridViewRow Gr in GridView1.Rows)
         //{
         //    DR = PurchaseDt.NewRow();
         //    //

         //    DR["ProductId"] = ((Label)Gr.FindControl("LblProductCodeG")).Text;
         //    DR["ProductName"] = ((Label)Gr.FindControl("LblProductNameG")).Text;
         //    DR["Image"] = ((Label)Gr.FindControl("LblProductImageG")).Text;
         //    DR["Amount"] = ((Label)Gr.FindControl("LblProductAmountG")).Text;
         //    DR["MRP"] = ((Label)Gr.FindControl("LBlMrp")).Text;
         //    DR["BV"] = ((Label)Gr.FindControl("LblBv")).Text;
         //    DR["DP"] = ((Label)Gr.FindControl("LblDPAmountG")).Text;
         //    DR["STOCK"] = ((Label)Gr.FindControl("LblStock")).Text;
         //    DR["TOTALBV"] = ((Label)Gr.FindControl("LblTotalBv")).Text;
         //    DR["TOTALDP"] = ((Label)Gr.FindControl("lblTotalAmountDP")).Text;
         //    DR["Quantity"] = ((Label)Gr.FindControl("lblQuantity")).Text;
         //    DR["TotalAmount"] = ((Label)Gr.FindControl("lblTotalAmount")).Text;
         //    DR["PurchaseAmount"] = ((Label)Gr.FindControl("LblPurchaseAmount")).Text;
         //    DR["CGST"] = ((Label)Gr.FindControl("LblCGST")).Text;
         //    DR["SGST"] = ((Label)Gr.FindControl("LblSGST")).Text;
         //    DR["IGST"] = ((Label)Gr.FindControl("LblIGST")).Text;
         //    DR["GSTPER"] = ((Label)Gr.FindControl("LblGSTPER")).Text;

         //    PurchaseDt.Rows.Add(DR);

         //    if (((Label)Gr.FindControl("LblProductCodeG")).Text == TxtProductCode.Text)
         //    {
         //        string popupScript = "alert('this Product already add');";
         //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
         //        string popupScript3 = "Closepopup1();";
         //        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
         //        return;
         //    }
         //}
         if (ViewState["offeramtst"] != null)
         {
             if (Convert.ToInt32(Quantity) > Convert.ToInt32(ViewState["offeramtst"].ToString()))
             {
                 string popupScript = "alert('you can not purchase offer product more than franchisee stock, Please contact to franchisee !');";
                 ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript, true);
                 string popupScript3 = "Closepopup1();";
                 ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript3, true);
                 return;

             }
         }
         DRW = PurchaseDt.NewRow();

         DRW["ProductId"] = productId;
         DRW["ProductName"] = Dt.Rows[0]["productname"].ToString();
         DRW["Image"] = Dt.Rows[0]["Image"].ToString();
         DRW["Amount"] = amount;
         DRW["MRP"] = Dt.Rows[0]["MRP"].ToString();
         DRW["BV"] = Dt.Rows[0]["BV"].ToString();
         DRW["DP"] = amount;
         DRW["STOCK"] = ViewState["offeramtst"].ToString();
         DRW["TOTALBV"] = Convert.ToDecimal(Quantity) * Convert.ToDecimal(Dt.Rows[0]["BV"].ToString());
         DRW["TOTALDP"] = Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount);
         DRW["Quantity"] = Quantity;
         DRW["TotalAmount"] = Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount);
         DRW["PurchaseAmount"] = Math.Round((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString())), 2);
         if (ViewState["fstate"].ToString() == ViewState["ustate"].ToString())
         {
             DRW["CGST"] = Math.Round((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) - ((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString())))) / 2, 2);
             DRW["SGST"] = Math.Round((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) - ((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString())))) / 2, 2);
             DRW["IGST"] = "0";
         }
         else
         {
             DRW["CGST"] = "0";
             DRW["SGST"] = "0";
             DRW["IGST"] = Math.Round(Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) - ((Convert.ToDecimal(Quantity) * Convert.ToDecimal(amount) * 100) / (100 + Convert.ToDecimal(Dt.Rows[0]["GST"].ToString()))), 2);
         }
         DRW["GSTPER"] = Dt.Rows[0]["GST"].ToString();
         DRW["OFFERPRODUCTID"] = offerAmount;
         PurchaseDt.Rows.Add(DRW);
         //ViewState["PDT"] = PurchaseDt;
         //GridView1.DataSource = PurchaseDt;
         //GridView1.DataBind();
         //ClearValue();
         //PurchasePanel.Visible = true;
         //   string popupScript2 = "Closepopupdivfamily();";
         //   ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), Guid.NewGuid().ToString(), popupScript2, true);

     }

}