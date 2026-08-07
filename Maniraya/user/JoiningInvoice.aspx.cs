using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MehndilinkInvoice : System.Web.UI.Page
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        SiteContactHelper.BindInvoiceCompanyInfo(litCompanyContact);
        SiteContactHelper.BindInvoiceCompanyInfo(litCompanyFooter);
        SiteContactHelper.BindInvoiceSign(imgInvoiceSign, "../");
        SiteContactHelper.BindSupportContactLine(litSupportContact);

        string companyGst = SiteContactHelper.GetPrimaryGst();
        litCompanyGst.Text = string.IsNullOrWhiteSpace(companyGst) ? "-" : companyGst.Trim();
        litCompanyGstFooter.Text = litCompanyGst.Text;
        if (!string.IsNullOrWhiteSpace(companyGst) && companyGst.Trim().Length >= 2)
        {
            lblCompanyStateCode.Text = companyGst.Trim().Substring(0, 2);
        }

        if (Request.QueryString["OrderNo"] == null)
        {
            return;
        }

        string OrderNo = Request.QueryString["OrderNo"].ToString();
        DataTable DT = getPurchaseProductQuantityFranchisee(OrderNo);
        if (DT == null || DT.Rows.Count == 0)
        {
            return;
        }

        string entryDate = Convert.ToString(DT.Rows[0]["entrydate"]);
        string[] ValList = entryDate.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        LblInvoicedate.Text = ValList.Length > 0 ? ValList[0] : entryDate;
        if (ValList.Length >= 3)
        {
            LblInvoiceTime.Text = ValList[1] + " " + ValList[2];
        }
        else if (ValList.Length == 2)
        {
            LblInvoiceTime.Text = ValList[1];
        }

        LblInvoiceNumber.Text = Convert.ToString(DT.Rows[0]["orderno"]);
        LblOrderId.Text = Convert.ToString(DT.Rows[0]["PurchaseId"]);
        LblShipping.Text = Convert.ToString(DT.Rows[0]["shippingcharges"]);

        DataTable UDT = getUserDetail(DT.Rows[0]["userid"].ToString());
        if (UDT != null && UDT.Rows.Count > 0)
        {
            lbluserid.Text = Convert.ToString(UDT.Rows[0]["userid"]);
            LblBillingname.Text = Convert.ToString(UDT.Rows[0]["username"]);
            LblBillingAddress.Text = Convert.ToString(UDT.Rows[0]["address"]);
            LblBillingPost.Text = Convert.ToString(UDT.Rows[0]["areaname"]);
            LblBillingCity.Text = Convert.ToString(UDT.Rows[0]["cityname"]);
            LblBillingState.Text = Convert.ToString(UDT.Rows[0]["statename"]);
            LblBillingPincode.Text = Convert.ToString(UDT.Rows[0]["pincode"]);
            LblBillingMobile.Text = Convert.ToString(UDT.Rows[0]["mobile"]);
            LblBillingEmail.Text = Convert.ToString(UDT.Rows[0]["email"]);

            string shipAddress = Convert.ToString(UDT.Rows[0]["Shippingaddress"]);
            if (string.IsNullOrWhiteSpace(shipAddress))
            {
                shipAddress = Convert.ToString(UDT.Rows[0]["address"]);
            }
            LblShippingAddress.Text = shipAddress;
            LblShippingPost.Text = !string.IsNullOrWhiteSpace(Convert.ToString(UDT.Rows[0]["ShippingAreaName"]))
                ? Convert.ToString(UDT.Rows[0]["ShippingAreaName"])
                : Convert.ToString(UDT.Rows[0]["areaname"]);
            LblShippingCity.Text = !string.IsNullOrWhiteSpace(Convert.ToString(UDT.Rows[0]["shippingcityname"]))
                ? Convert.ToString(UDT.Rows[0]["shippingcityname"])
                : Convert.ToString(UDT.Rows[0]["cityname"]);
            LblShippingState.Text = !string.IsNullOrWhiteSpace(Convert.ToString(UDT.Rows[0]["shippingstatename"]))
                ? Convert.ToString(UDT.Rows[0]["shippingstatename"])
                : Convert.ToString(UDT.Rows[0]["statename"]);
            LblShippingPincode.Text = !string.IsNullOrWhiteSpace(Convert.ToString(UDT.Rows[0]["ShippingPincode"]))
                ? Convert.ToString(UDT.Rows[0]["ShippingPincode"])
                : Convert.ToString(UDT.Rows[0]["pincode"]);

            LblDistributername.Text = LblBillingname.Text;
            LbDistributerlUserid.Text = lbluserid.Text;
            LblDistributerSponserid.Text = Convert.ToString(UDT.Rows[0]["sponserid"]);
            LblDistributerSponsername.Text = Convert.ToString(UDT.Rows[0]["sponsername"]);
            LblDistributerRegdate.Text = Convert.ToString(UDT.Rows[0]["regdate"]);
            LblDistributerEmail.Text = LblBillingEmail.Text;
            LblDistributerMobile.Text = LblBillingMobile.Text;
        }

        DataTable PDT = getPurchaseProductQuantityChild(OrderNo);
        if (PDT != null && PDT.Rows.Count > 0)
        {
            GridView1.DataSource = PDT;
            GridView1.DataBind();

            bool isIgst = Convert.ToDecimal(PDT.Rows[0]["IGST"]) != 0;
            GridView1.Columns[8].Visible = !isIgst;
            GridView1.Columns[9].Visible = !isIgst;
            GridView1.Columns[10].Visible = isIgst;
            rowCgst.Visible = !isIgst;
            rowSgst.Visible = !isIgst;
            rowIgst.Visible = isIgst;

            BindInvoiceTotals(PDT);
        }

        DataTable GSTDT = getgstsummary(OrderNo);
        GridViewgst.DataSource = GSTDT;
        GridViewgst.DataBind();
    }

    private void BindInvoiceTotals(DataTable pdt)
    {
        decimal totalAmount = 0;
        decimal totalGst = 0;
        decimal totalQty = 0;
        decimal totalDp = 0;
        decimal totalBv = 0;
        decimal totalTaxable = 0;
        decimal totalCgst = 0;
        decimal totalSgst = 0;
        decimal totalIgst = 0;

        foreach (DataRow row in pdt.Rows)
        {
            totalAmount += ToDecimal(row["TotalAmount"]);
            totalGst += ToDecimal(row["GST"]);
            totalQty += ToDecimal(row["Quantity"]);
            totalDp += ToDecimal(row["TotalDP"]);
            totalBv += ToDecimal(row["TotalBV"]);
            totalTaxable += ToDecimal(row["PurchaseAmount"]);
            totalCgst += ToDecimal(row["CGST"]);
            totalSgst += ToDecimal(row["SGST"]);
            totalIgst += ToDecimal(row["IGST"]);
        }

        decimal shipping = ToDecimal(LblShipping.Text);
        decimal payable = totalAmount + shipping;

        LblSubTotal.Text = totalTaxable.ToString("0.00");
        LblCgstAmount.Text = totalCgst.ToString("0.00");
        LblSgstAmount.Text = totalSgst.ToString("0.00");
        LblIgstAmount.Text = totalIgst.ToString("0.00");
        lblTotalAmount.Text = totalAmount.ToString("0.00");
        LblPaybleamount.Text = payable.ToString("0.00");
        LblTotalgst.Text = totalGst.ToString("0.00");
        LblTotalqnty.Text = totalQty.ToString("0");
        lblTotaldp.Text = totalDp.ToString("0.00");
        lblTotalBV.Text = totalBv.ToString("0.00");
        LblPaybleamountwords.Text = ConvertWholeNumber(Math.Round(payable, 0).ToString()).ToUpper();
    }

    private static decimal ToDecimal(object value)
    {
        decimal result;
        return decimal.TryParse(Convert.ToString(value), out result) ? result : 0m;
    }
    public DataTable getFranchiseeDetail(string userid)
    {

        string str_query = "SELECT ud.*,cm.cityname,sm.statename,(select UserName from userdetail where UserId=ud.sponserid) as Sponsername,cm.stateid,sm.countryid,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage,CASE WHEN isnull(ud.AadharImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImage END AS AadharImg, CASE WHEN isnull(ud.AadharImageBack,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImageBack END AS AadharImgBack,  CASE WHEN isnull(ud.PanImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PanImage END AS PanImg FROM FranchiseeDetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid where ud.UserId = '" + userid + "' ";
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
    public DataTable getUserDetail(string userid)
    {
        string str_query = "SELECT ud.*,cm.stateid,sm.countryid,sm.Statename,cm.cityname,scm.cityname as shippingcityname,ssm.statename as shippingstatename,CASE WHEN isnull(ud.cancelcheque,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.cancelcheque END AS PasbookImage,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage,(select UserName from userdetail where UserId=ud.sponserid) as Sponsername,(select UserName from userdetail where UserId=ud.parentuserid) as parentname,convert(char,ud.activatedate,103) as activationdate,(select planamount from UserTopupTb where userid=ud.userid and type='A') planamount, CASE WHEN isnull(ud.AadharImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImage END AS AadharImg, CASE WHEN isnull(ud.AadharImageBack,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImageBack END AS AadharImgBack,  CASE WHEN isnull(ud.PanImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PanImage END AS PanImg FROM userdetail ud left join citymaster cm on ud.CityId=cm.cityid left join statemaster sm on cm.stateid=sm.stateid left join citymaster scm on ud.ShippingCityId=scm.cityid left join statemaster ssm on scm.stateid=ssm.stateid where ud.UserId = '" + userid + "' ";
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
    public DataTable getPurchaseProductQuantityFranchisee(string ID)
    {
        // LEFT JOIN city/state: missing CityId must not blank the whole invoice (amounts stay 0.00).
        string str_query = "SELECT PU.TotalDP,PU.Cstatus Pstatus,PU.orderNo,PU.PurchaseId,PU.TotalAmount,PU.Userid  AS UserId,PU.franchiseeid,Convert(CHAR,PU.PurchaseDate,103) AS PurchaseDate,U.Username ,U.Email Emailid,U.Mobile AS ContactNo,isnull(U.Address,'')+' '+isnull(C.Cityname,'')+' '+isnull(S.Statename,'')+' '+isnull(U.Pincode,'')  AS address,(Select top 1 isnull(invoicestatus,0) from PurchaseProductMaster where purchaseId=PU.purchaseId) as InvoiceStatus,PU.entrydate,isnull(PU.shippingcharges,0) shippingcharges   FROM   UserFranchiseePurchaseMaster PU JOIN Userdetail U ON PU.UserId=U.userid LEFT JOIN citymaster C ON C.CityId=U.CityId LEFT JOIN statemaster S ON C.StateId=S.StateId   where 1=1 ";



        if (ID != string.Empty)
        {
            str_query += " and PU.PurchaseID='" + ID + "'";
        }
        //  str_query += "GROUP BY PU.TotalDP,PU.Pstatus,PU.PurchaseId,PU.Purchaseby,Convert(CHAR,PU.PurchaseDate,103)";
        str_query += " order by PU.id desc";
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
    public DataTable getPurchaseProductQuantityChild(string ID)
    {
        string str_query = "SELECT P.HSNCODE,P.productname,U.Id, U.PurchaseId, U.ProductID, U.Quantity, U.Amount, U.MRP, U.TotalAmount, U.Purchasedate, U.Entrydate, U.BV, U.TotalBV,isnull(U.CGST,0) CGST,isnull(U.SGST,0) SGST,isnull(U.IGST,0) IGST, isnull(U.CGST,0)+isnull(U.SGST,0)+isnull(U.IGST,0) as GST, U.GSTPER, U.PurchaseAmount, U.DP, U.TotalDP,CASE WHEN isnull(U.IGST,0)=0 THEN CAST(Round(U.GSTPER/2,2) as numeric(18,2)) END CGSTPER,CASE WHEN isnull(U.IGST,0)=0 THEN CAST(Round(U.GSTPER/2,2) as numeric(18,2)) END SGSTPER,U.GSTPER AS IGSTPER from UserFranchiseePurchaseProductMaster U join productmaster P on U.productid=P.productid where 1=1 ";


        if (ID != string.Empty)
        {
            str_query += " and U.PurchaseID='" + ID + "'";
        }
        str_query += " order by U.PurchaseID";
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
    Decimal totalPurchase = 0;
    Decimal totalGST = 0;
    Decimal totalQnty = 0;
    Decimal totaldp = 0;
    Decimal totalbv = 0;
    Decimal totalAmount = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalPurchase += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalDP")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("lblTGST")).Text);
            totalQnty += Convert.ToDecimal(((Label)e.Row.FindControl("lblquantity")).Text);
            totaldp += Convert.ToDecimal(((Label)e.Row.FindControl("lblttdp")).Text);
            totalAmount += Convert.ToDecimal(((Label)e.Row.FindControl("LblTotalAmt")).Text);
            totalbv += Convert.ToDecimal(((Label)e.Row.FindControl("lblttbv")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            lblTotalAmount.Text = totalAmount.ToString();
            LblPaybleamount.Text = totalPurchase.ToString();
            LblTotalgst.Text = totalGST.ToString();
            LblTotalqnty.Text = totalQnty.ToString();
            lblTotaldp.Text = totaldp.ToString();
            lblTotalBV.Text = totalbv.ToString();
            LblPaybleamount.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmount.Text) + Convert.ToDecimal(LblShipping.Text));
            LblPaybleamountwords.Text = ConvertWholeNumber(Math.Round(Convert.ToDecimal(LblPaybleamount.Text), 0).ToString()).ToUpper();
        }
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
    public DataTable getgstsummary(string purchaseid)
    {

        string res = "";
        string s2 = "";
        SqlConnection cn;
        SqlTransaction tr = null;
        DataTable Dt = new DataTable();
        cn = ObjData.StartConnectionInTransaction();
        tr = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            s2 = "GetGSTSUMMARY";
            SqlParameter[] parameter = {              
                    new SqlParameter("@Purchaseid",purchaseid),                  
                  
                };
            Dt = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);

            tr.Commit();

        }
        catch (Exception ex)
        {
            tr.Rollback();
        }
        finally
        {
            ObjData.EndConnection();
            tr.Dispose();
        }
        return Dt;
    }
}