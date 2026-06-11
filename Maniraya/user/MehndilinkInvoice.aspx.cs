using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MehndilinkInvoice : System.Web.UI.Page
{
    Data ObjData = new Data();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userid"] != null)
        //{
        //    if (!IsPostBack)
        //    {
               string OrderNo= Request.QueryString["OrderNo"].ToString();
               DataTable DT = getPurchaseProductQuantityFranchisee(OrderNo);
               if (DT != null && DT.Rows.Count > 0)
               {
                   string[] ValList = DT.Rows[0]["entrydate"].ToString().Split(' ');

                   LblInvoicedate.Text = ValList[0].ToString();
                   LblInvoiceTime.Text = ValList[1].ToString() + " " + ValList[2].ToString();
                   LblInvoiceNumber.Text = DT.Rows[0]["orderno"].ToString();
                   DataTable UDT = getUserDetail(DT.Rows[0]["userid"].ToString());
                   if (UDT != null && UDT.Rows.Count > 0)
                   {
                       LblBillingname.Text = UDT.Rows[0]["username"].ToString();
                       LblBillingAddress.Text = UDT.Rows[0]["Address"].ToString();
                       LblBillingPost.Text = UDT.Rows[0]["areaname"].ToString();
                       LblBillingCity.Text = UDT.Rows[0]["cityname"].ToString();
                       LblBillingState.Text = UDT.Rows[0]["statename"].ToString();
                       LblBillingPincode.Text = UDT.Rows[0]["pincode"].ToString();
                       LblBillingMobile.Text = UDT.Rows[0]["mobile"].ToString();

                      // LblShipppingName.Text = UDT.Rows[0]["username"].ToString();
                      //// LblShippingAddress.Text = UDT.Rows[0]["shippingaddress"].ToString();
                      // LblShippingPost.Text = UDT.Rows[0]["shippingarea"].ToString();
                      // LblShippingCity.Text = UDT.Rows[0]["shippingcity"].ToString();
                      // LblShippingState.Text = UDT.Rows[0]["shippingstate"].ToString();
                      // LblShippingPincode.Text = UDT.Rows[0]["shippingpincode"].ToString();
                     //  LblShippingMobile.Text = UDT.Rows[0]["mobile"].ToString();

                       LblDistributername.Text = UDT.Rows[0]["username"].ToString();
                       LbDistributerlUserid.Text = UDT.Rows[0]["userid"].ToString();
                       LblDistributerSponserid.Text = UDT.Rows[0]["sponserid"].ToString();
                       LblDistributerSponsername.Text = UDT.Rows[0]["sponsername"].ToString();
                       LblDistributerRegdate.Text = UDT.Rows[0]["regdate"].ToString();
                       LblDistributerEmail.Text = UDT.Rows[0]["email"].ToString();
                       LblDistributerMobile.Text = UDT.Rows[0]["mobile"].ToString();
                   }
                   DataTable FDT = getFranchiseeDetail(DT.Rows[0]["franchiseeid"].ToString());
                    if (FDT != null && FDT.Rows.Count > 0)
                    {
                        
                     //   LblStorename.Text = FDT.Rows[0]["outletname"].ToString();
                       // LblStoreAdd.Text = FDT.Rows[0]["Address"].ToString() + " " + FDT.Rows[0]["areaname"].ToString() + " " + FDT.Rows[0]["cityname"].ToString() + " " + FDT.Rows[0]["statename"].ToString() + " " + FDT.Rows[0]["pincode"].ToString();
                       // LblStoreContact.Text = FDT.Rows[0]["mobile"].ToString();
                        //LblStoreemail.Text = FDT.Rows[0]["email"].ToString();
                    }
                    DataTable PDT = getPurchaseProductQuantityChild(OrderNo);
                    if (PDT != null && PDT.Rows.Count > 0)
                    {
                        GridView1.DataSource = PDT;
                        GridView1.DataBind();
                    }
                   
               }

        //    }

        //}
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
        string str_query = "SELECT ud.*,cm.stateid,sm.countryid,sm.Statename,cm.cityname,CASE WHEN isnull(ud.cancelcheque,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.cancelcheque END AS PasbookImage,CASE WHEN isnull(ud.PhotoImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PhotoImage END AS PhotoImage,(select UserName from userdetail where UserId=ud.sponserid) as Sponsername,(select UserName from userdetail where UserId=ud.parentuserid) as parentname,convert(char,ud.activatedate,103) as activationdate,(select planamount from UserTopupTb where userid=ud.userid and type='A') planamount, CASE WHEN isnull(ud.AadharImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImage END AS AadharImg, CASE WHEN isnull(ud.AadharImageBack,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.AadharImageBack END AS AadharImgBack,  CASE WHEN isnull(ud.PanImage,'')='' THEN 'img/default.png' ELSE '../ProductImage/'+ud.PanImage END AS PanImg FROM userdetail ud left join citymaster cm on ud.cityid=cm.cityid left join statemaster sm on cm.stateid=sm.stateid where ud.UserId = '" + userid + "' ";
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
        string str_query = "SELECT PU.TotalDP,PU.Cstatus Pstatus,PU.orderNo,PU.PurchaseId,PU.TotalAmount,PU.Userid  AS UserId,PU.franchiseeid,Convert(CHAR,PU.PurchaseDate,103) AS PurchaseDate,U.Username ,U.Email Emailid,U.Mobile AS ContactNo,U.Address+' '+C.Cityname+' '+S.Statename+' '+U.Pincode  AS address,(Select top 1 isnull(invoicestatus,0) from PurchaseProductMaster where purchaseId=PU.purchaseId) as InvoiceStatus,PU.entrydate  FROM   UserFranchiseePurchaseMaster PU JOIN Userdetail U ON PU.UserId=U.userid INNER JOIN citymaster C ON C.CityId=U.CityId INNER JOIN statemaster S ON C.StateId=S.StateId   where 1=1 ";



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
        string str_query = "SELECT P.HSNCODE,P.productname,U.Id, U.PurchaseId, U.ProductID, U.Quantity, U.Amount, U.MRP, U.TotalAmount, U.Purchasedate, U.Entrydate, U.BV, U.TotalBV, isnull(U.CGST,0)+isnull(U.SGST,0)+isnull(U.IGST,0) as GST, U.GSTPER, U.PurchaseAmount, U.DP, U.TotalDP from UserFranchiseePurchaseProductMaster U join productmaster P on U.productid=P.productid where 1=1 ";


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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalPurchase += Convert.ToDecimal(((Label)e.Row.FindControl("lblTotalDP")).Text);
            totalGST += Convert.ToDecimal(((Label)e.Row.FindControl("lblGST")).Text);
            totalQnty += Convert.ToDecimal(((Label)e.Row.FindControl("lblquantity")).Text);
            totaldp += Convert.ToDecimal(((Label)e.Row.FindControl("lblDP")).Text);
            totalbv += Convert.ToDecimal(((Label)e.Row.FindControl("lblBV")).Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            LblPaybleamount.Text = totalPurchase.ToString();
            LblTotalgst.Text = totalGST.ToString();
            LblTotalqnty.Text = totalQnty.ToString();
            lblTotaldp.Text = totaldp.ToString();
            lblTotalBV.Text = totalbv.ToString();
           
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
}