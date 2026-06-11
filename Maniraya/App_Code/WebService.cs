using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessLogicTier;
using System.Data;
using Newtonsoft.Json;
using ARA_StringHunt;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Xml;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public static DataTable DtnumberDetail2 = new DataTable();
    clsRecharge obal = new clsRecharge();


    [WebMethod]

    public string getOperatorPrepaid(string search)
    {
        DataTable DtnumberDetail = new DataTable();
        string opertor = "", circle = "", irffcircle = "";
        try
        {

            if (search.Length > 3 && search.Length < 5)
            {


                if (DtnumberDetail.Rows.Count < 1)
                {
                    DtnumberDetail = obal.fillNumberList();
                }

                string searchExpression = "Number = '" + search + "'";
                DataRow[] foundRows = DtnumberDetail.Select(searchExpression);
                if (foundRows.Length > 0)
                {
                    opertor = JsonConvert.SerializeObject(foundRows.CopyToDataTable());
                }

            }
            return opertor;

        }
        catch
        {
            return opertor;

        }
    }
  
   
    [WebMethod]
    public string GetopertorOption(string value)
    {
        string opertor = "", res = "";
        if (value != "")
        {
            try
            {
                string[] errmsg = value.Split(new string[] { "__" }, StringSplitOptions.None);
                opertor = errmsg[0].ToString().Trim();
                clsRecharge objrechage = new clsRecharge();
                DataTable dt = objrechage.OperatorOpetion(opertor);

                if (dt.Rows.Count > 0)
                {
                    res = JsonConvert.SerializeObject(dt);
                }
            }
            catch { }
        }
        return res;
    }

    [WebMethod]
    public string GetLatrechargeSRSdata(string ApiName = "SRS")
    {
        string Result = "";
        DataTable DT = new DataTable();
       
            try
            {
                clsRecharge objrechage = new clsRecharge();
                DataTable dt = objrechage.CheckSRSREcharge(ApiName);

                if (dt.Rows.Count > 0)
                {
                    Result += "<LastTransaction>";
                    Result += "<Table>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Result += "<Status>";
                        Result += "<Mobile>" + dt.Rows[i]["AmountTransferTo"].ToString().Trim() + "</Mobile>";
                        Result += "<Amount>" + dt.Rows[i]["RequestedAmount"].ToString().Trim() + "</Amount>";
                        Result += "<ApiName>" + dt.Rows[i]["ApiName"].ToString().Trim() + "</ApiName>";
                        Result += "<Operator>" + dt.Rows[i]["OpratorName"].ToString() + "</Operator>";
                        Result += "<Code>" + dt.Rows[i]["ApiOpCode"].ToString().Trim() + "</Code>";
                        Result += "<RequestId>" + dt.Rows[i]["TransactionID"].ToString() + "</RequestId>";
                        Result += "<Date>" + dt.Rows[i]["EntryDate"].ToString().Trim() + "</Date>";
                        Result += "</Status>";
                    }

                    Result += "</Table>";
                    Result += "</LastTransaction>";
                }
                else
                {
                    Result += "<LastTransaction>";
                    Result += "<Table>";
                    Result += "<Status>Record Not Found</Status>";
                    Result += "</Table>";
                    Result += "</LastTransaction>";
                }
            }
            catch { }
          
      
        return Result;
    }
    [WebMethod]
    public XmlDocument GetLatrechargeSRS(string ApiName)
    {
        XmlDocument xmldoc = new XmlDocument();

        try
        {
            string Result = GetLatrechargeSRSdata(ApiName);
            xmldoc.LoadXml(Result);
        }
        catch (Exception ex)
        {
            xmldoc.LoadXml(ex.Message);
        }
        return xmldoc;
    }
    [WebMethod]
    public string RechargeReport()
    {
        //dal odal = new dal();
        string opertor = "";
        //DataTable Dt = new DataTable();
        //Dt = odal.GetDataByProcedure("ChartTable");
        //if (Dt.Rows.Count > 0)
        //{
        //    opertor = JsonConvert.SerializeObject(Dt);
        //}
        return opertor;
    }
    [WebMethod]
    public string getOpertorbalance(string MobileNo, string OPID, string optional1, string optional2, string optional3, string optional4)
    {
        DataTable dtCheck = new DataTable();
        clsRecharge objrechage = new clsRecharge();
        DataTable DT = objrechage.fillApiid(OPID);
        string apiid = DT.Rows[0][0].ToString();
        //string Respo = "http://rechargeplans.co.in/webservice/ViewOperatorBill.asmx/viewOperatorBill?MobileNo=" + MobileNo + "&OPID=" + OPID + "&optional1=" + optional1 + "&optional2=" + optional2 + "&optional3=" + optional3 + "&optional4=" + optional4 + "";
        string Respo = "http://roundpayapi.com/WebSerives/All_Recharge.asmx/GetOperatorBalance_App?Token=bcee7bd1-d119-4a7c-993c-4eab97fc1276&MobileNo=" + MobileNo + "&OPID=" + apiid + "&optional1=" + optional1 + "&optional2=" + optional2 + "";
        //http://rechargeplans.co.in/webservice/ViewOperatorBill.asmx/viewOperatorBill&MobileNo=" + MobileNo + "&OPID=" + OPID + "&optional1=" + optional1 + "&optional2=" + optional2 + "";
        string CheckUrl = "";
        try
        {
            Respo = Respo.CallURL().Replace("Fail,", "Fail !!");
            dtCheck = ConvertJSONToDataTable(Respo);
            if (dtCheck.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dtCheck);
            }
            //return 


        }
        catch
        {

        }

        return "";
    }
    private DataTable ConvertJSONToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        //strip out bad characters
        string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

        //hold column names
        List<string> dtColumns = new List<string>();

        //get columns
        foreach (string jp in jsonParts)
        {
            //only loop thru once to get column names
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                }

            }
            break; // TODO: might not be correct. Was : Exit For
        }

        //build dt
        foreach (string c in dtColumns)
        {
            dt.Columns.Add(c);
        }
        //get table data
        foreach (string jp in jsonParts)
        {
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string v = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[n] = v;
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            dt.Rows.Add(nr);
        }
        return dt;
    }
}
