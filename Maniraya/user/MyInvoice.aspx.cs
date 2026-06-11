﻿using BusinessLogicTier;
using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class user_MyInvoice : System.Web.UI.Page
{
    clsvendor objV = new clsvendor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PurchaseId"] != null)
        {
            filldate(HttpUtility.UrlDecode(Request.QueryString["UserId"].ToString()), HttpUtility.UrlDecode(Request.QueryString["PurchaseId"].ToString()));
            fillitem(HttpUtility.UrlDecode(Request.QueryString["PurchaseId"].ToString()));
        }

    }
    public void fillitem(string Id)
    {
        string customerId = Id;
        DataTable dt1 = new DataTable();
        dt1 = getUserPurchaseProductChild(customerId);
        gvOrders.DataSource = dt1;
        gvOrders.DataBind();
    }
    public void filldate(string UserId, string PurchaseId)
    {
        objV.VendorId = UserId;
        objV.PurchaseID = PurchaseId;
        DataTable dt1 = new DataTable();
        dt1 = getUserPurchasePurchaseforinvoice(objV);
        LblUserName.Text = dt1.Rows[0]["username"].ToString();
        LblAddress.Text = dt1.Rows[0]["address"].ToString();
        LblMobileNo.Text = dt1.Rows[0]["ContactNo"].ToString();
        LblInvoiceNo.Text = dt1.Rows[0]["userId"].ToString() + "00" + objV.PurchaseID;
        Lbldate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        LblPdate.Text = dt1.Rows[0]["PurchaseDate"].ToString();
        LblTotal.Text = dt1.Rows[0]["PurchaseAmount"].ToString();
        LblCGST.Text = dt1.Rows[0]["CGST"].ToString();
        LblSGSt.Text = dt1.Rows[0]["SGST"].ToString();
        LblIGST.Text = dt1.Rows[0]["IGST"].ToString();
        LblShipping.Text = dt1.Rows[0]["shippingcharges"].ToString();
        LblGTotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(LblShipping.Text) + Convert.ToDecimal(dt1.Rows[0]["TotalAmount"].ToString()), 0));


        LblEmail.Text = dt1.Rows[0]["email"].ToString();
        LblCgstPer.Text = dt1.Rows[0]["CGSTper"].ToString();
        LblSgstPer.Text = dt1.Rows[0]["SGSTper"].ToString();
        LblIgstPer.Text = dt1.Rows[0]["IGSTper"].ToString();
        if (Convert.ToDecimal(LblIGST.Text) > 0)
        {
            trCgst.Visible = false;
            trSgst.Visible = false;
            trIgst.Visible = true;

        }
        else
        {
            trCgst.Visible = true;
            trSgst.Visible = true;
            trIgst.Visible = false;
        }
        LblAmountinWorld.Text = ConvertWholeNumber(Math.Round(Convert.ToDecimal(LblGTotal.Text), 0).ToString());
        rt.HRef = "InvoicePrint.aspx?PurchaseId=" + Request.QueryString["PurchaseId"].ToString() + "&UserId=" + Request.QueryString["UserId"].ToString();
    }

    Data ObjData = new Data();
    public DataTable getUserPurchaseProductChild(string ID)
    {
        string str_query = "SELECT PR.PurchaseID,PR.PurchaseDate,P.ProductName,CASE WHEN P.productimage='' THEN '../ProductImage/images.png' ELSE  '../ProductImage/'+ P.productimage END AS [Image],PR.Quantity,Convert(DECIMAL(18,2),(PR.Amount*100)/(100+(P.GST))) Price,P.MRP,Convert(DECIMAL(18,2),(PR.Amount*100)/(100+(P.GST)))*PR.Quantity PurchaseAmount,Convert(DECIMAL(18,2),(PR.TotalAmount-Convert(DECIMAL(18,2),((PR.Amount*100)/(100+(P.GST))*PR.Quantity)))/2) CGST,Convert(DECIMAL(18,2),(PR.TotalAmount-Convert(DECIMAL(18,2),((PR.Amount*100)/(100+(P.GST))*PR.Quantity)))/2) SGST,Convert(decimal(18,0),(P.Gst/2)) CGSTPER,Convert(decimal(18,0),(P.Gst/2)) SGSTPER, PR.TotalAmount,PR.ProductID FROM PurchaseProductMaster PR INNER  JOIN ProductMaster P ON PR.ProductID=P.ProductID where 1=1 ";


        if (ID != string.Empty)
        {
            str_query += " and PR.PurchaseID='" + ID + "'";
        }
        str_query += " order by PR.PurchaseID";
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
    public DataTable getUserPurchasePurchaseforinvoice(clsvendor objstate)
    {
        string str_query = "SELECT P.PurchaseID,P.Purchaseby userid,V.username,(SELECT Mobile FROM UserDetail WHERE UserId=P.Purchaseby) AS ContactNo,(SELECT Address + ' ' + C.CityName + ' ' + S.StateName + ' ' + U.Pincode FROM UserDetail U INNER  JOIN CityMaster C ON U.CityId = C.CityId INNER JOIN StateMaster S on S.Stateid = C.StateId   WHERE UserId = P.Purchaseby) AS address, P.Amount PurchaseAmount,0 CGST,0 SGST,0 IGST,0 CGSTper,0 SGSTper,0 IGSTper,   (SELECT sum(TotalAmount) FROM PurchaseProductMaster WHERE PurchaseId=P.PurchaseId ) TotalAmount,Convert(Char, P.PurchaseDate, 103) as PurchaseDate,'Joining' as PurchaseType,V.email,P.shippingcharges   FROM PurchaseProductMaster P INNER JOIN UserDetail V ON P.Purchaseby = V.userID ";


        if (objstate.VendorId != string.Empty)
        {
            str_query += " and P.Purchaseby='" + objstate.VendorId + "'";
        }
        if (objstate.PurchaseID != string.Empty)
        {
            str_query += " and P.PurchaseId='" + objstate.PurchaseID + "'";
        }
        str_query += " order BY P.PurchaseId";
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
    private static String ConvertWholeNumber(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX   
            bool isDone = false;//test if already translated   
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))   
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric   
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping   
                String place = "";//digit grouping name:hundres,thousand,etc...   
                switch (numDigits)
                {
                    case 1://ones' range   

                        word = ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range   
                        word = tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range   
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range   
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range   
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range   
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...   
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)   
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                    }


                }
                //ignore digit grouping names   
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { }
        return word.Trim();
    }
    private static String tens(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (_Number > 0)
                {
                    name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                }
                break;
        }
        return name;
    }
    private static String ones(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = "";
        switch (_Number)
        {

            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }
    decimal price = 0;
    decimal CGST = 0;
    decimal SGST = 0;
    decimal TAmount = 0;
    protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label LblPurchaseAmount = (Label)e.Row.FindControl("LblPurchaseAmount");
            Label LblCGST = (Label)e.Row.FindControl("LblCGST");
            Label LblSGST = (Label)e.Row.FindControl("LblSGST");
            Label LblTotalAmount = (Label)e.Row.FindControl("LblTotalAmount");

            price += Convert.ToDecimal(LblPurchaseAmount.Text);
            CGST += Convert.ToDecimal(LblCGST.Text);
            SGST += Convert.ToDecimal(LblSGST.Text);
            TAmount += Convert.ToDecimal(LblTotalAmount.Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            LblTotal.Text = price.ToString();
            LblCGST.Text = CGST.ToString();
            LblSGSt.Text = SGST.ToString();
            // LblTotal.Text = TAmount.ToString();
        }

    }
}