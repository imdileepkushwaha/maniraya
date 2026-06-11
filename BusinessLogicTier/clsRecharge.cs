using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataTier;
using System.Data.SqlClient;
using ARA_StringHunt;
using System.Xml.Linq;
using Newtonsoft.Json;
using CyberPlatOpenSSL;
using System.Configuration;
using System.Web;

namespace BusinessLogicTier
{
    public class clsRecharge
    {
        public string UserId
        {
            get;
            set;
        }

        public string RechargeMobile
        {
            get;
            set;
        }

        public string UserMobile
        {
            get;
            set;
        }

        public string OperatorCode
        {
            get;
            set;
        }

        public string rowno
        {
            get;
            set;
        }

        public decimal RechargeAmount
        {
            get;
            set;
        }

        public string IPAddress
        {
            get;
            set;
        }

        public string EntryBy
        {
            get;
            set;
        }
        public string RechargeFrom
        {
            get;
            set;
        }

        public string Rechargebile
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ReferenceId
        {
            get;
            set;
        }

        public string RechargeType
        {
            get;
            set;
        }

        public System.DateTime FromDate
        {
            get;
            set;
        }

        public System.DateTime ToDate
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Option1
        {
            get;
            set;
        }

        public string Option2
        {
            get;
            set;
        }

        public string Option3
        {
            get;
            set;
        }

        public string Option4
        {
            get;
            set;
        }

        public string DispalyValue1
        {
            get;
            set;
        }

        public string DispalyValue2
        {
            get;
            set;
        }

        public string DispalyValue3
        {
            get;
            set;
        }

        public string DispalyValue4
        {
            get;
            set;
        }
        Data ObjData = new Data();
        OpenSSL ssl = new OpenSSL();
        public string SessionNo = DateTime.Parse(DateTime.Now.ToString()).ToString("dddMMMddyyyyHHmmss");
        string keyPath = HttpContext.Current.Server.MapPath("~/keys/myprivatekey.pfx");
        public DataTable GetUserbal(string mobile)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_get_UserBal";
                SqlParameter[] parameter = { new SqlParameter("@mobile", mobile),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable fillNumberList()
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "select * from NumberList";
                dt = this.ObjData.RunDataTable(s2);
            }
            catch (System.Exception ex_26)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable OperatorOpetion(string OperatorCodeId)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "OpertorOption";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@OperatorCodeId", OperatorCodeId)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_41)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable RechargeNew(clsRecharge objrecharge)
        {
            DataTable dtresult = new DataTable();
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_RechargeNew";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@RechargeMobile", objrecharge.RechargeMobile),
					new SqlParameter("@RechargeAmount", objrecharge.RechargeAmount),
					new SqlParameter("@OperatorId", objrecharge.OperatorCode),
					new SqlParameter("@UserMobile", objrecharge.UserMobile),
					new SqlParameter("@IpAddress", objrecharge.IPAddress),
					new SqlParameter("@EntryBy", objrecharge.EntryBy),
					new SqlParameter("@RechargeType", objrecharge.RechargeType),
					new SqlParameter("@optional1", objrecharge.Option1),
					new SqlParameter("@optional2", objrecharge.Option2),
					new SqlParameter("@optional3", objrecharge.Option3),
					new SqlParameter("@optional4", objrecharge.Option4),
					new SqlParameter("@DispalyValue1", objrecharge.DispalyValue1),
					new SqlParameter("@DispalyValue2", objrecharge.DispalyValue2),
					new SqlParameter("@DispalyValue3", objrecharge.DispalyValue3),
					new SqlParameter("@DispalyValue4", objrecharge.DispalyValue4),
                    new SqlParameter("@RechargeFrom", objrecharge.RechargeFrom)
				};
                dtresult = this.ObjData.RunDataTableProcedure(s2, parameter);
                string referenceid = dtresult.Rows[0]["referenceid"].ToString();
                string ErrorMsg = dtresult.Rows[0]["errors"].ToString();
                string url = dtresult.Rows[0]["url"].ToString();
                string msg = "";
                string status = "";
                string Type = dtresult.Rows[0]["Type"].ToString();
                string StatusValue = dtresult.Rows[0]["StatusValue"].ToString();
                string msg1 = dtresult.Rows[0]["msg"].ToString();
                string StatusName = dtresult.Rows[0]["StatusName"].ToString();
                string VenderId = dtresult.Rows[0]["VenderId"].ToString();
                string VenderIdColumn = dtresult.Rows[0]["VenderId"].ToString();
                string VenderIdValue = "";
                string LiveId = dtresult.Rows[0]["LiveId"].ToString();
                string LiveIdValue = "";
                string Resp = url.CallURL();
                if (ErrorMsg != "")
                {
                    string[] errmsg = ErrorMsg.Split(new string[]
					{
						"||"
					}, System.StringSplitOptions.None);
                    for (int i = 0; i < errmsg.Length; i++)
                    {
                        if (errmsg[i].ToString().Trim() != "")
                        {
                            if (Resp.Contains(errmsg[i].ToString().Trim()) && errmsg[i] != "")
                            {
                                status = "Failed";
                                msg = errmsg[i];
                                if (msg.ToLower().Contains("insufficient"))
                                {
                                    msg = "System error found";
                                }
                                break;
                            }
                        }
                    }
                }
                if (status != "Failed")
                {
                    DataTable dtResp = this.GetResponseValues(Resp, Type);
                    DataColumnCollection columns = dtResp.Columns;
                    if (!dtResp.Columns.Contains("ResultErr"))
                    {
                        string[] StsCol = StatusName.Split(new char[]
						{
							'/'
						});
                        string[] StsVal = StatusValue.Split(new char[]
						{
							','
						});
                        if (dtResp.Columns.Contains(StsCol[0].Trim()))
                        {
                            string matchvalue = dtResp.Rows[0][StsCol[0].Trim()].ToString().ToUpper();
                            if (matchvalue == StsVal[0].Trim().ToUpper())
                            {
                                status = "Success";
                            }
                            if (matchvalue == StsVal[1].Trim().ToUpper())
                            {
                                status = "Failed";
                            }
                        }
                        if (columns.Contains(VenderIdColumn))
                        {
                            VenderId = dtResp.Rows[0][VenderIdColumn].ToString().ToUpper();
                            VenderIdValue = dtResp.Rows[0][VenderIdColumn].ToString().ToUpper();
                        }
                        else
                        {
                            VenderIdValue = "";
                        }
                        if (columns.Contains(LiveId))
                        {
                            LiveIdValue = dtResp.Rows[0][LiveId].ToString().ToUpper();
                        }
                        if (columns.Contains(StsCol[0].Trim()))
                        {
                            string matchvalue = dtResp.Rows[0][StsCol[0].Trim()].ToString().ToUpper();
                            if (matchvalue == StsVal[2].Trim().ToUpper())
                            {
                                status = "Pending";
                            }
                        }
                        if (columns.Contains(msg1))
                        {
                            msg = dtResp.Rows[0][msg1].ToString().ToUpper();
                            if (msg.ToLower().Contains("insufficient"))
                            {
                            }
                        }
                        if (dtResp.Columns.Contains(StsCol[StsCol.Length - 1]))
                        {
                            if (dtResp.Rows[0][StsCol[StsCol.Length - 1].Trim()].ToString() == StsVal[1].Trim().ToUpper())
                            {
                                status = "Failed";
                            }
                            else if (StsCol.Length == 2)
                            {
                                if (dtResp.Columns.Contains(StsCol[StsCol.Length - 1]))
                                {
                                    status = "Failed";
                                }
                            }
                        }
                    }
                    if (status == "")
                    {
                        status = "Pending";
                    }
                    if (columns.Contains("txstatus_desc"))
                    {
                        if (dtResp.Rows[0]["txstatus_desc"].ToString().ToLower() == "pending")
                        {
                            status = "Pending";
                        }
                    }
                }
                s2 = "sp_update_RechargeRequest";
                SqlParameter[] parameter2 = new SqlParameter[]
				{
					new SqlParameter("@ReferenceId", referenceid),
					new SqlParameter("@response", Resp),
					new SqlParameter("@VendorId", VenderIdValue),
					new SqlParameter("@Liveid", LiveIdValue),
					new SqlParameter("@status", status),
					new SqlParameter("@Message", msg)
				};
                DataTable dtUpdateresponse = this.ObjData.RunDataTableProcedure(s2, parameter2);

                DataTable Dtrr = objrecharge.CheckReponsefromSMS(referenceid);
                if (Dtrr.Rows.Count > 0)
                {
                    if (Dtrr.Rows[0][0].ToString() == "1")
                    {
                        if (Dtrr.Rows[0]["rechargestatus"].ToString().ToUpper() == "SUCCESS")
                        {
                            if (Dtrr.Rows[0]["Successstatus"].ToString() == "1")
                            {
                                string url11 = objU.smstemplate("", Dtrr.Rows[0]["Mobile"].ToString(), "", "", "ARSENPAY", "www.arsenpay.in", Dtrr.Rows[0]["RechargeMobile"].ToString(), Dtrr.Rows[0]["Rechargeamount"].ToString(), Dtrr.Rows[0]["balanceamount"].ToString(), "", Dtrr.Rows[0]["operatorname"].ToString(), Dtrr.Rows[0]["transactionid"].ToString(), Dtrr.Rows[0]["LiveID"].ToString(), "rechargesuccess");
                                string Result = url11.CallURL();
                                objU.Insert_SendSMS(Dtrr.Rows[0]["Mobile"].ToString(), Result, url11);
                            }
                        }
                        if (Dtrr.Rows[0]["rechargestatus"].ToString().ToUpper() == "FAILED")
                        {

                            if (Dtrr.Rows[0]["failedstatus"].ToString() == "1")
                            {
                                string url11 = objU.smstemplate("", Dtrr.Rows[0]["Mobile"].ToString(), "", msg, "ARSENPAY", "www.arsenpay.in", Dtrr.Rows[0]["RechargeMobile"].ToString(), Dtrr.Rows[0]["Rechargeamount"].ToString(), Dtrr.Rows[0]["balanceamount"].ToString(), "", Dtrr.Rows[0]["operatorname"].ToString(), Dtrr.Rows[0]["transactionid"].ToString(), Dtrr.Rows[0]["LiveID"].ToString(), "rechargefailed");
                                string Result = url11.CallURL();
                                objU.Insert_SendSMS(Dtrr.Rows[0]["Mobile"].ToString(), Result, url11);
                            }
                        }
                    }
                }
                objrecharge.Status = status;
            }
            catch (System.Exception ex)
            {
                string res = "Error: ";
                res += ex.Message;
            }
            finally
            {
                this.ObjData.EndConnection();
            }
            return dtresult;
        }
        clsUser objU = new clsUser();
        public DataTable getRechargeReport(clsRecharge objrecharge, string noOfRows = "top 100")
        {
//            string str_query = @"SELECT " + noOfRows + @" rq.UserMobile,ud.Username,rq.RechargeMobile,rq.RechargeAmount,\r\n 
//                                (SELECT isnull(sum(cramount),0)-isnull(sum(dramount),0) FROM  TransactionDetail td WITH (nolock) WHERE id<=(SELECT isnull( max(id),0) 
//                                FROM TransactionDetail td2 WHERE td2.TransactionId=rq.ReferenceId AND td2.userid=rq.UserMobile) AND td.userid=rq.UserMobile) AS balanceamount\r\n, 
//                                rq.ReferenceId,rq.Status,rq.Message,rq.Entrydate,rq.LiveID,oc.Operator, 
//                                (case when oc.[Type]=1 then 'Mobile Prepaid' when oc.[Type]=6 then 'Mobile Postpaid' when oc.[Type]=3 then 'Landline' 
//                                when oc.[Type]=2 then 'DTH' when oc.[Type]=5 then 'Electricity' when oc.[Type]=9 then 'Gas' end) as serviceType 
//                                FROM rechargerequest rq LEFT JOIN userdetail ud ON rq.UserMobile=ud.userid 
//                                inner join ApiOpCode aoc on aoc.OpCode=rq.opCode inner join OperatorCode oc on aoc.opId=oc.id 
//                                where 1=1 ";

            string str_query = @"SELECT " + noOfRows + @" rq.UserMobile,ud.Username,rq.RechargeMobile,rq.RechargeAmount,rq.disputestatus, 
                               (SELECT isnull(currentbalance,0) FROM  TransactionDetail td WITH (nolock) WHERE id=(SELECT isnull( max(id),0) 
                            FROM TransactionDetail td2 WHERE td2.TransactionId=rq.ReferenceId AND td2.userid=rq.UserMobile and td2.type=1) AND td.userid=rq.UserMobile ) AS balanceamount, 
                                rq.ReferenceId,rq.Status,rq.Message,rq.Entrydate,rq.LiveID,oc.Id,oc.Operator, (select name from RechargeApi where id=rq.apiid) as apiname,
                                (case when oc.[Type]=1 then 'Mobile Prepaid' when oc.[Type]=6 then 'Mobile Postpaid' when oc.[Type]=3 then 'Landline' 
                                when oc.[Type]=2 then 'DTH' when oc.[Type]=5 then 'Electricity' when oc.[Type]=9 then 'Gas' end) as serviceType 
                                FROM rechargerequest rq LEFT JOIN userdetail ud ON rq.UserMobile=ud.userid 
                                inner join OperatorCode oc on oc.Id=rq.opcode
                                where 1=1 ";

            if (objrecharge.FromDate != System.DateTime.MinValue && objrecharge.ToDate != System.DateTime.MinValue)
            {
                string text = str_query;
                str_query = string.Concat(new string[]
				{
					text,
					"  and convert(DATE, rq.entrydate  )  >= '",
					objrecharge.FromDate.ToString("yyyy/MM/dd"),
					"'   and convert(DATE, rq.entrydate  )   <= '",
					objrecharge.ToDate.ToString("yyyy/MM/dd"),
					"' "
				});
            }
            //if (objrecharge.Name != "")
            if (!string.IsNullOrEmpty(objrecharge.Name))
            {
                str_query = str_query + "  and ud.username like '%" + objrecharge.Name + "%' ";
            }
            //if (objrecharge.UserMobile != "")
            if (!string.IsNullOrEmpty(objrecharge.UserMobile))
            {
                str_query = str_query + "  and rq.usermobile = '" + objrecharge.UserMobile + "' ";
            }
            //if (objrecharge.RechargeMobile != "")
            if (!string.IsNullOrEmpty(objrecharge.RechargeMobile))
            {
                str_query = str_query + "  and rq.rechargemobile = '" + objrecharge.RechargeMobile + "' ";
            }
            //if (objrecharge.ReferenceId != "")
            if (!string.IsNullOrEmpty(objrecharge.ReferenceId))
            {
                str_query = str_query + "  and rq.ReferenceId = '" + objrecharge.ReferenceId + "' ";
            }
            //if (objrecharge.ReferenceId != "")
            //if (!string.IsNullOrEmpty(objrecharge.ReferenceId))
            //{
            //    str_query = str_query + "  and rq.ReferenceId = '" + objrecharge.ReferenceId + "' ";
            //}

            if (objrecharge.Status != "0" && !string.IsNullOrEmpty(objrecharge.Status))
            {
                str_query = str_query + "  and rq.Status = '" + objrecharge.Status + "' ";
            }

            if (objrecharge.RechargeType != "0" && !string.IsNullOrEmpty(objrecharge.RechargeType))
            {
                str_query = str_query + "  and oc.[Type] = '" + objrecharge.RechargeType + "' ";
            }

            if (objrecharge.OperatorCode != "0" && !string.IsNullOrEmpty(objrecharge.OperatorCode))
            {
                str_query = str_query + "  and oc.Id = '" + objrecharge.OperatorCode + "' ";
            }


            str_query += " order by rq.entrydate  desc";
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                dt = this.ObjData.RunDataTable(str_query);
            }
            catch (System.Exception ex_1C1)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable UpdateRechargeStatusManual(string refId, string status, string entryby)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateStatusManual";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@ReferenceId", refId),
					new SqlParameter("@status", status),
					new SqlParameter("@EntryBy", entryby)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_5F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable UpdateRechargeStatusManualnew(string refId, string status, string entryby,string Liveid)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateStatusManual";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@ReferenceId", refId),
					new SqlParameter("@status", status),
					new SqlParameter("@EntryBy", entryby),
                    new SqlParameter("@Liveid", Liveid)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_5F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public int InsertRechargeCommission(string cmd)
        {
            int res = 0;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "delete from COMMISSIOIN_DISTRIBUTION_DETAIL";
                this.ObjData.RunInsUpDelQuery(s2);
                this.ObjData.RunInsUpDelQuery(cmd);
                res = 1;
            }
            catch (System.Exception ex_35)
            {
                res = 0;
            }
            this.ObjData.EndConnection();
            return res;
        }
        public DataTable getRechargeCommission()
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "\r\nSELECT tb1.*\r\n,tb2.[1] AS type1,tb2.[2] AS type2,tb2.[3] AS type3,tb2.[4] AS type4,tb2.[5] AS type5,tb2.[6] AS type6\r\n,tb3.[1] AS changetype1,tb3.[2] AS changetype2,tb3.[3] AS changetype3,tb3.[4] AS changetype4,tb3.[5] AS changetype5,tb3.[6] AS changetype6\r\n  FROM\r\n\r\n(\r\nselect * from (\r\nselect LVL,oc.id as OperatorId ,OC.Operator,Commission from COMMISSIOIN_DISTRIBUTION_DETAIL CD  right outer  JOIN OperatorCode OC ON OC.ID=CD.OperatorId \r\n\r\n) as T \r\nPivot\r\n(\r\nMax(Commission)\r\nfor Lvl in ([1],[2],[3],[4],[5],[6])\r\n  )As Pivot1\r\n  ) tb1\r\n  LEFT JOIN \r\n(\r\nselect * from (\r\nselect LVL,oc.id as OperatorId ,OC.Operator,cd.type from COMMISSIOIN_DISTRIBUTION_DETAIL CD  right outer  JOIN OperatorCode OC ON OC.ID=CD.OperatorId \r\n\r\n) as T \r\nPivot\r\n(\r\nMax(type)\r\nfor Lvl in ([1],[2],[3],[4],[5],[6])\r\n  )As Pivot1)\r\n  \r\n  tb2 ON tb2.operatorid=tb1.operatorid\r\nLEFT JOIN \r\n(\r\n\r\nselect * from (\r\nselect LVL,oc.id as OperatorId ,OC.operator,changetype from COMMISSIOIN_DISTRIBUTION_DETAIL CD  right outer  JOIN OperatorCode OC ON OC.ID=CD.OperatorId \r\n\r\n) as T \r\nPivot\r\n(\r\nMax(changetype)\r\nfor Lvl in ([1],[2],[3],[4],[5],[6])\r\n  )As Pivot1\r\n  )\r\n  tb3 ON tb1.operatorid=tb3.operatorid  order by tb1.operatorid";
                dt = this.ObjData.RunDataTable(s2);
            }
            catch (System.Exception ex_26)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }

        public DataTable UpdateDisputeStatus(string disputeid, string liveid, string remark, string status, string entryby)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateDisputeStatus";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@DIsputeid", disputeid),
					new SqlParameter("@LiveId", liveid),
					new SqlParameter("@Remark", remark),
					new SqlParameter("@status", status),
					new SqlParameter("@EntryBy", entryby)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_7F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable getDIsputeReport(clsRecharge objRecharge)
        {
            string str_query = "SELECT dd.*,rq.UserMobile,rq.RechargeMobile,rq.RechargeAmount,rq.entrydate as Rechargedate,rq.status as Rechargestatus FROM DisputeDetail dd LEFT JOIN rechargerequest rq ON dd.ReferenceId=rq.ReferenceId where 1=1 ";
            if (objRecharge.FromDate != System.DateTime.MinValue && objRecharge.ToDate != System.DateTime.MinValue)
            {
                string text = str_query;
                str_query = string.Concat(new string[]
				{
					text,
					"  and convert(DATE, dd.entrydate  )  >= '",
					objRecharge.FromDate.ToString("yyyy/MM/dd"),
					"'   and convert(DATE, dd.entrydate  )   <= '",
					objRecharge.ToDate.ToString("yyyy/MM/dd"),
					"' "
				});
            }
            if (objRecharge.RechargeMobile != "")
            {
                str_query = str_query + "  and rq.RechargeMobile = '" + objRecharge.RechargeMobile + "' ";
            }
            if (objRecharge.UserMobile != "")
            {
                str_query = str_query + "  and rq.UserMobile = '" + objRecharge.UserMobile + "' ";
            }
            if (objRecharge.Status != "0")
            {
                str_query = str_query + "  and dd.status = '" + objRecharge.Status + "' ";
            }
            str_query += " order by dd.entrydate  desc";
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                dt = this.ObjData.RunDataTable(str_query);
            }
            catch (System.Exception ex_15D)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable getDIsputeReportnew(clsRecharge objRecharge)
        {
            string str_query = "SELECT " + objRecharge.rowno + " dd.*,rq.UserMobile,rq.RechargeMobile,rq.RechargeAmount,rq.entrydate as Rechargedate,rq.status as Rechargestatus FROM DisputeDetail dd LEFT JOIN rechargerequest rq ON dd.ReferenceId=rq.ReferenceId where 1=1 ";
            if (objRecharge.FromDate != System.DateTime.MinValue && objRecharge.ToDate != System.DateTime.MinValue)
            {
                string text = str_query;
                str_query = string.Concat(new string[]
				{
					text,
					"  and convert(DATE, dd.entrydate  )  >= '",
					objRecharge.FromDate.ToString("yyyy/MM/dd"),
					"'   and convert(DATE, dd.entrydate  )   <= '",
					objRecharge.ToDate.ToString("yyyy/MM/dd"),
					"' "
				});
            }
            if (objRecharge.RechargeMobile != "")
            {
                str_query = str_query + "  and rq.RechargeMobile = '" + objRecharge.RechargeMobile + "' ";
            }
            if (objRecharge.UserMobile != "")
            {
                str_query = str_query + "  and rq.UserMobile = '" + objRecharge.UserMobile + "' ";
            }
            if (objRecharge.Status != "0")
            {
                str_query = str_query + "  and dd.status = '" + objRecharge.Status + "' ";
            }
            str_query += " order by dd.entrydate  desc";
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                dt = this.ObjData.RunDataTable(str_query);
            }
            catch (System.Exception ex_15D)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable GetCheckValues(string Msg)
        {
            string str = Msg.Trim().Replace(",", " ").Replace(":", " ").Replace("<", " ").Replace(">", " ").Replace("/", " ").Replace("*", " ").Replace("  ", " ").Replace("  ", " ").Replace("|", " ").Replace("=", " ").Replace("\"", " ").Trim();
            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_CheckTextResponse";
                SqlParameter[] parameter = { new SqlParameter("@Msg", str),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable LastRechargeRecode(string Userid)
        {           
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "LastTransactionIDRecode";
                SqlParameter[] parameter = { new SqlParameter("@Userid", Userid),                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable GetAllOpertorSelectedByUser(string Userid)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
               string s2 = "select * from OperatorCode order by Operator";
                dt = ObjData.RunDataTable(s2);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable GetAllOpertorSelectedByUsernew(string Userid)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "select * from OperatorCode where showstatus='1' order by Operator";
                dt = ObjData.RunDataTable(s2);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable GetAllOpertorSelectedByTYpe1(string Userid)
        {
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "select Operator,ltrim(rtrim(opid)) as OPID from OperatorCode where type='1' order by Operator";
                dt = ObjData.RunDataTable(s2);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public string checkvalidation(string TxtAmount, string TxtMobileNo, string DdlOpertor)
        {
            string Error = "Error";
            if (TxtMobileNo == "")
            {
                Error = "Please Enter Mobile No.";
            }
            else if (DdlOpertor == "Select Operator")
            {
                Error = "Please Select Operator.";

            }
            else if (TxtAmount == "")
            {
                Error = "Please Enter Amount";

            }
            else { return "success"; }
            return Error;
        }
        public DataTable GetResponseValues(string Resp, string Type)
        {
            try
            {
                if (Type == "1")
                {


                    Resp = Resp.Replace("{", "[{");
                    Resp = Resp.Replace("}", "}]");
                    DataTable dtValue = (DataTable)JsonConvert.DeserializeObject(Resp, (typeof(DataTable)));
                    return dtValue;

                }
                else
                {

                    XElement x = null;
                    if (Resp.Contains("<?xml"))
                    {
                        x = XElement.Parse(Resp);
                    }
                    else
                    {
                        x = XElement.Parse("<Root>" + Resp + "</Root>");
                    }
                    DataTable dt = new DataTable();
                    XElement setup = (from p in x.Descendants() select p).First();
                    foreach (XElement xe in setup.Descendants()) // build your DataTable
                        dt.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string))); // add columns to your dt

                    var all = from p in x.Descendants(setup.Name.ToString()) select p;
                    foreach (XElement xe in all)
                    {
                        DataRow dr = dt.NewRow();
                        foreach (XElement xe2 in xe.Descendants())
                            dr[xe2.Name.ToString()] = xe2.Value; //add in the values
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                DataTable dtt = new DataTable();
                dtt.Columns.Add("ResultErr");
                dtt.Rows.Add(ex.Message);
                return dtt;
            }
        }
        public string checkvalidation(System.Web.UI.WebControls.TextBox TxtAmount, System.Web.UI.WebControls.TextBox TxtMobileNo, System.Web.UI.WebControls.DropDownList DdlOpertor)
        {
            string Error = "Error";
            if (TxtMobileNo.Text.Trim() == "")
            {
                Error = "Please Enter Mobile No.";
            }
            else if (DdlOpertor.SelectedItem.Text == "Select Operator")
            {
                Error = "Please Select Operator.";

            }
            else if (TxtAmount.Text.Trim() == "")
            {
                Error = "Please Enter Amount";

            }
            else { return "success"; }
            return Error;
        }
        public String InsertTransaction_Api_response(String TransactionId, int APIId, String URL, string Response)
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
                s2 = "sp_add_TRANSACTION_API_RESPONSES";
                SqlParameter[] parameter = {  
                new SqlParameter("@TransactionID", TransactionId),
                new SqlParameter("@APIId", APIId),
                new SqlParameter("@URL", URL),
                new SqlParameter("@Response", Response),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = dtresult.Rows[0][0].ToString();
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "Error: ";
                res += ex.Message;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }

    
        private String _strRequest(string SDCode, string APCode, string OPCode, string SessionNo, string txtMobileNo, string account, string authenticator3, string amount)
        {
            ssl.CERTNo = ConfigurationManager.AppSettings["CERTNo"];
            StringBuilder _reqStr = new StringBuilder();
            #region Create Request
            _reqStr.Append("CERT=" + ssl.CERTNo + Environment.NewLine);
            _reqStr.Append("SD=" + SDCode + Environment.NewLine);
            _reqStr.Append("AP=" + APCode + Environment.NewLine);
            _reqStr.Append("OP=" + OPCode + Environment.NewLine);
            _reqStr.Append("SESSION=" + SessionNo + Environment.NewLine);
            _reqStr.Append("NUMBER=" + txtMobileNo + Environment.NewLine);
            _reqStr.Append("ACCOUNT=" + account + Environment.NewLine);
            _reqStr.Append("Authenticator3=" + authenticator3 + Environment.NewLine);
            _reqStr.Append("AMOUNT=" + amount + Environment.NewLine);
            _reqStr.Append("TERM_ID=" + APCode + Environment.NewLine);//Mostly value of TERM_ID=AP Code, but value may be vary.
            _reqStr.Append("COMMENT=test");
            #endregion
            return _reqStr.ToString();
        }
        public static DataTable convertStringToDataTable(string data)
        {
            DataTable dataTable = new DataTable();

            int StartIndex = data.IndexOf("BEGIN");
            int EndIndex = data.IndexOf("END");
            int length = EndIndex - StartIndex;
            data = data.Substring((StartIndex + 7), length - 7);
            data = data.Replace("\n", String.Empty);
            DataRow dataRow = dataTable.NewRow();
            dataTable.Rows.Add(dataRow);
            foreach (string row in data.Split('\r'))
            {
                if (row != "")
                {
                    string[] keyValue = row.Split('=');
                    //if (!columnsAdded)
                    {
                        DataColumn dataColumn = new DataColumn(keyValue[0]);
                        dataTable.Columns.Add(dataColumn);
                    }
                    dataRow[keyValue[0]] = keyValue[1];
                    //}
                    //columnsAdded = true;
                    //dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
        public String InsertError(string ProcedureName, string ErrorRecieved, string BlockName, string ErrorCode = "", string Transaction_ID = "", string RStatus = "", string CStatus = "")
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
                s2 = "insertError";
                SqlParameter[] parameter = {  
               new SqlParameter("@ProcedureName", ProcedureName),
               new SqlParameter("@ErrorRecieved", ErrorRecieved),
               new SqlParameter("@BlockName", BlockName),
               new SqlParameter("@ErrorCode", ErrorCode),
               new SqlParameter("@Transaction_ID", Transaction_ID),
               new SqlParameter("@RStatus", RStatus),
               new SqlParameter("@CStatus", CStatus),
                };
                DataTable dtresult = new DataTable();
                dtresult = ObjData.RunDataTableProcedureTRans(s2, tr, parameter);
                res = dtresult.Rows[0][0].ToString();
                tr.Commit();
            }
            catch (Exception ex)
            {
                res = "Error: ";
                res += ex.Message;
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public DataTable UpdateTransactionStatus(string url,string  status,string  Resp,string  VenderId,string  transactionid,string  DeductionAmt,string  UserId,string  MobileNo,string msg,string LiveIdValue,int APIID)
        {
          
            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateTransactionStatus";
                SqlParameter[] parameter = {
                                               new SqlParameter("@Url", url),
                                               new SqlParameter("@Status", status),
                                               new SqlParameter("@Response", Resp.Trim()),
                                               new SqlParameter("@VenderId", VenderId),
                                               new SqlParameter("@TransactionID", dt.Rows[0][1].ToString()),
                                               new SqlParameter("@DeductionAmt", DeductionAmt),
                                               new SqlParameter("@UserId", UserId),
                                               new SqlParameter("@RechargeNo", MobileNo),
                                               new SqlParameter("@msg", msg),
                                               new SqlParameter("@LiveId", LiveIdValue),
                                                new SqlParameter("@APIID", APIID),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }
        public DataTable UpdateTransactionStatus1(string url, string status, string Resp, string VenderId, string transactionid, string DeductionAmt, string UserId, string MobileNo, string msg, string LiveIdValue, int APIID)
        {

            DataTable dt = null;
            ObjData.StartConnection();
            try
            {
                string s2 = "sp_UpdateTransactionStatus";
                SqlParameter[] parameter = {
                                               new SqlParameter("@Url", url),
                                               new SqlParameter("@Status", status),
                                               new SqlParameter("@Response", Resp.Trim()),
                                               new SqlParameter("@VenderId", VenderId),
                                               new SqlParameter("@TransactionID", dt.Rows[0][1].ToString()),
                                               new SqlParameter("@DeductionAmt", DeductionAmt),
                                               new SqlParameter("@UserId", UserId),
                                               new SqlParameter("@RechargeNo", MobileNo),
                                               new SqlParameter("@msg", msg),
                                               new SqlParameter("@LiveId", LiveIdValue),
                                                new SqlParameter("@APIID", APIID),
                                           };
                dt = ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            ObjData.EndConnection();
            return dt;
        }

        public string Recharge(string MobileNo, string Code, string Amount, string STD, string RechargeType, string UserId, out string TransactionID, out decimal Balance, out string Error1, string ip, out string RequestLiveID, string ApiRequestID = "")
        {
            int[] ErrorCode = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 
                                    27, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47,
                                    48, 50,51,52,53,54,55,56,57,81,82,83,84,85,86,87,88,89,224,333};

            //  Balance = 0;
            int APIID = 0;
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "Sp_Rechargeoperatortype";
                SqlParameter[] parameter = { 
                                                new SqlParameter("@RechargeNo", MobileNo.Trim()),
                                                new SqlParameter("@OpId", Code),
                                                new SqlParameter("@Amount", Amount.Trim()),
                                                new SqlParameter("@STD", STD),
                                                new SqlParameter("@RechargeType", RechargeType),
                                                new SqlParameter("@UserId", UserId),
                                                new SqlParameter("@ApiRequestID", ApiRequestID),
                                                new SqlParameter("@IP", ip),
                };
                ds = ObjData.RunDataSetProcedureTRans(s2, tr, parameter);
                DataTable dt = ds.Tables[0].Copy();
                string status = "1", Resp = "", LiveIdValue = "";
                if (dt.Rows[0][0].ToString() == "-1")
                {
                    Balance = 0;
                    TransactionID = "";
                    status = "0";
                    RequestLiveID = "";
                    Error1 = dt.Rows[0][1].ToString();
                    res = status;
                    return status;
                }
                else
                {
                    try
                    {
                        Balance = Convert.ToDecimal(dt.Rows[0]["Balance"].ToString());
                        string url = dt.Rows[0]["URL"].ToString();
                        string Type = dt.Rows[0]["Type"].ToString();
                        string StatusValue = dt.Rows[0]["StatusValue"].ToString();
                        string StatusName = dt.Rows[0]["StatusName"].ToString();
                        string VenderId = dt.Rows[0]["VenderId"].ToString();
                        string LiveId = dt.Rows[0]["LiveId"].ToString();
                        TransactionID = dt.Rows[0]["TransactionID"].ToString();
                        string DeductionAmt = dt.Rows[0]["DeductionAmt"].ToString();
                        string PartialPay = dt.Rows[0]["PartialPay"].ToString();
                        string ErrorMsg = dt.Rows[0]["ErrorMsg"].ToString();
                        APIID = Convert.ToInt32( dt.Rows[0]["Apiid"].ToString());
                        RequestLiveID = LiveId;
                        string flag = "0";
                        string msg = "";


                    lblRehit:
                        status = "1";
                        if (url.ToUpper().Contains("HTTP"))
                        {
                            if (url.ToUpper().Contains("HTTP") && APIID != 23)
                            {

                                Resp = url.CallURL();

                                if (ErrorMsg != "")
                                {
                                    string[] errmsg = ErrorMsg.Split(new string[] { "||" }, StringSplitOptions.None);
                                    //string[] errmsg = ErrorMsg.Split('[]');
                                    for (int i = 0; i < errmsg.Length; i++)
                                    {
                                        if (errmsg[i].ToString().Trim() != "")
                                        {
                                            if (Resp.Contains(errmsg[i].ToString().Trim()) == true && errmsg[i] != "")
                                            {
                                                status = "0";
                                                msg = errmsg[i];
                                                if (msg.ToLower().Contains("insufficient") == true)
                                                {

                                                    msg = "System error found";
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (Type == "3" && status != "0")
                                {

                                    try
                                    {
                                       
                                        DataTable dtrespones = new DataTable();
                                        dtrespones = GetCheckValues(Resp);
                                        if (dtrespones.Rows.Count > 0)
                                        {
                                            if (dtrespones.Rows[0][0].ToString() == "No Match found")
                                            {
                                                status = "1";
                                            }
                                            else
                                            {
                                                status = dtrespones.Rows[0]["Status"].ToString();
                                                VenderId = dtrespones.Rows[0]["VendorID"].ToString();
                                                LiveIdValue = dtrespones.Rows[0]["OperatorId"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            status = "1";
                                        }
                                    }

                                    catch (SystemException ex)
                                    {
                                       
                                        InsertError("Block string match resend", ex.ToString(), "Last", "", TransactionID, "", "");
                                        status = "1";
                                    }
                                }

                                else if (status != "0")
                                {
                                    DataTable dtResp = GetResponseValues(Resp, Type);
                                    DataColumnCollection columns = dtResp.Columns;
                                    if (dtResp.Columns.Contains("ResultErr") == false)
                                    {
                                        string[] StsCol = StatusName.Split('/');
                                        string[] StsVal = StatusValue.Split(',');
                                        if (dtResp.Columns.Contains(StsCol[0].Trim()))
                                        {
                                            string matchvalue = dtResp.Rows[0][StsCol[0].Trim()].ToString().ToUpper();
                                            if (matchvalue == StsVal[0].Trim().ToUpper())
                                            {

                                                status = "2";
                                            }
                                            if (matchvalue == StsVal[1].Trim().ToUpper())
                                            {

                                                status = "0";
                                            }
                                        }
                                        if (columns.Contains(VenderId))
                                        {
                                            VenderId = dtResp.Rows[0][VenderId].ToString().ToUpper();
                                        }
                                        else
                                        {
                                            VenderId = "";
                                        }
                                        if (columns.Contains(LiveId))
                                        {
                                            LiveIdValue = dtResp.Rows[0][LiveId].ToString().ToUpper();
                                        }
                                        if (columns.Contains(StsCol[0].Trim()))
                                        {
                                            string matchvalue = dtResp.Rows[0][StsCol[0].Trim()].ToString().ToUpper();
                                            if (matchvalue == StsVal[2].Trim().ToUpper())
                                            {

                                                status = "1";
                                            }
                                        }
                                        if (columns.Contains("MSG"))
                                        {
                                            msg = dtResp.Rows[0]["MSG"].ToString().ToUpper();
                                            if (msg.ToLower().Contains("insufficient") == true)
                                            {

                                                Error1 = "System error found";
                                            }
                                            else
                                            {
                                                Error1 = msg;
                                            }
                                        }
                                        if (dtResp.Columns.Contains(StsCol[StsCol.Length - 1]))
                                        {
                                            if (dtResp.Rows[0][StsCol[StsCol.Length - 1].Trim()].ToString() == StsVal[1].Trim().ToUpper())
                                            {
                                                status = "0";

                                            }
                                            else if (StsCol.Length == 2)
                                            {
                                                if (dtResp.Columns.Contains(StsCol[StsCol.Length - 1]))
                                                {
                                                    status = "0";

                                                }

                                            }
                                        }


                                    }
                                    if (status == "")
                                    {
                                        status = "";
                                        status = "1";
                                    }
                                }
                                if (status == "0")
                                {

                                    if (ds.Tables.Count >= 2)
                                    {
                                        if (ds.Tables[1].Rows.Count > 0)
                                        {

                                            try
                                            {
                                                status = "1";
                                             
                                                InsertTransaction_Api_response(dt.Rows[0][1].ToString(), APIID, url, Resp.Trim());
                                            }
                                            catch (SystemException ex)
                                            {
                                               
                                                InsertError("Block 3", ex.ToString(), "Last", "", TransactionID, "", "");
                                            }
                                            url = ds.Tables[1].Rows[0]["URL"].ToString();
                                            StatusValue = ds.Tables[1].Rows[0]["StatusValue"].ToString();
                                            StatusName = ds.Tables[1].Rows[0]["StatusName"].ToString();
                                            ErrorMsg = ds.Tables[1].Rows[0]["ErrorMsg"].ToString();
                                            APIID = ds.Tables[1].Rows[0]["Apiid"].ToInt();
                                            VenderId = ds.Tables[1].Rows[0]["VenderId"].ToString();
                                            LiveId = ds.Tables[1].Rows[0]["LiveId"].ToString();
                                            Type = ds.Tables[1].Rows[0]["Type"].ToString();
                                            ds.Tables[1].Rows.RemoveAt(0);
                                            goto lblRehit;
                                        }
                                    }
                                }


                            }
                            else if (APIID == 23)
                            {
                                string CompleteResponse = string.Empty;
                                string CompleteRequest = string.Empty;
                                try
                                {
                                    string Request = string.Empty;


                                    string[] spliturl = dt.Rows[0][0].ToString().Split('|');
                                    int count = 0;
                                    foreach (string url1 in spliturl)
                                    {
                                        count += 1;
                                        string URL = url1.Trim();
                                        TransactionID = dt.Rows[0]["TransactionID"].ToString();
                                        if (Code == "TL" || Code == "TD")
                                            Request = _strRequest("326386", "331400", "331401", SessionNo, MobileNo, "2", "", Amount.ToString());
                                        else
                                            Request = _strRequest("326386", "331400", "331401", SessionNo, MobileNo, "", "", Amount.ToString());
                                        ssl.message = ssl.Sign_With_PFX(Request, keyPath, "gautam$3444");
                                        CompleteRequest += "Request:\r\n" + ssl.message + '|';
                                        ssl.htmlText = ssl.CallCryptoAPI(ssl.message, url1);
                                        Resp = "Response:\r\n" + ssl.htmlText;

                                        CompleteResponse += Resp + '|';
                                        DataTable ResPDt = convertStringToDataTable(Resp);
                                        if (ResPDt.Rows.Count > 0)
                                        {

                                            if (ResPDt.Rows[0]["ERROR"].ToString() == "0" || ResPDt.Rows[0]["ERROR"].ToString() == "36")
                                            {
                                                status = "2";
                                                //Sts = "SUCCESS";
                                            }
                                            else
                                            {
                                                bool has = ErrorCode.Contains(ResPDt.Rows[0]["ERROR"].ToString().ToInt());
                                                if (has == true && count < 3)
                                                {
                                                    status = "0";
                                                    break;
                                                    //if (has == true)
                                                    //{
                                                    //    DataTable dtPartial = new DataTable();
                                                    //    String Query = "Select PartialPay from OperatorCode where ID='" + Code + "'";
                                                    //    SqlDataAdapter da = new SqlDataAdapter(Query, Connection.ConnectionString);
                                                    //    da.Fill(dtPartial);
                                                    //    if (dtPartial.Rows.Count > 0)
                                                    //    {
                                                    //        if (dtPartial.Rows[0]["PartialPay"].ToString() == "N")
                                                    //        {
                                                    //            //LiveIdValue = ResPDt.Rows[0]["ERRMSG"].ToString();
                                                    //            status = "0";
                                                    //            //Sts = "FAILED";
                                                    //            break;
                                                    //        }
                                                    //        else if (dtPartial.Rows[0]["PartialPay"].ToString() == "Y" && count == 1)
                                                    //        {
                                                    //            status = "2";
                                                    //        }
                                                    //        else if (dtPartial.Rows[0]["PartialPay"].ToString() == "")
                                                    //        {
                                                    //            //LiveIdValue = ResPDt.Rows[0]["ERRMSG"].ToString();
                                                    //            status = "0";
                                                    //            //Sts = "FAILED";
                                                    //            break;
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            //LiveIdValue = ResPDt.Rows[0]["ERRMSG"].ToString();
                                                    //            status = "0";
                                                    //            //Sts = "FAILED";
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //}
                                                }
                                            }
                                        }
                                        if (ResPDt.Columns.Contains("TRANSID") && count == 2)
                                            VenderId = ResPDt.Rows[0]["TRANSID"].ToString();
                                        if (ResPDt.Columns.Contains("AUTHCODE") && count == 2)
                                            LiveIdValue = ResPDt.Rows[0]["AUTHCODE"].ToString();
                                        //}
                                        //else
                                        //{
                                        //    Sts = "FAILED";
                                        //    OperaterId = "Operator Disabled";
                                        //    System.Web.HttpContext.Current.Session["TRRANS"] = TransactionID;
                                        //}


                                    }
                                    CompleteResponse = CompleteResponse.Trim();
                                    CompleteResponse = CompleteResponse.Trim('|');
                                    CompleteRequest = CompleteRequest.Trim();
                                    CompleteRequest = CompleteRequest.Trim('|');

                                    Resp = "[" + CompleteRequest + "---" + CompleteResponse + "]";
                                }
                                catch (Exception ex)
                                {
                                    status = "0";
                                    Resp = "[" + CompleteRequest + "---" + CompleteResponse + "]";
                                    Balance = 0;
                                    RequestLiveID = "";
                                    TransactionID = "";
                                    Error1 = ex.ToString();
                                    //eko objeko = new eko();
                                    InsertError("Block 2", Error1, "Last", "", TransactionID, "", "");
                                }
                            }

                            else
                            {
                                status = "1";
                            }
                            if (msg.Contains("insufficient") == true)
                            {
                                Error1 = "System error found";
                            }
                            else
                            {
                                Error1 = msg;
                            }
                            //SqlParameter[] param1 = new SqlParameter[11];
                            //param1[0] = new SqlParameter("@Url", url);
                            //param1[1] = new SqlParameter("@Status", status);
                            //param1[2] = new SqlParameter("@Response", Resp.Trim());
                            //param1[3] = new SqlParameter("@VenderId", VenderId);
                            //param1[4] = new SqlParameter("@TransactionID", dt.Rows[0][1].ToString());
                            //param1[5] = new SqlParameter("@DeductionAmt", DeductionAmt);
                            //param1[6] = new SqlParameter("@UserId", UserId);
                            //param1[7] = new SqlParameter("@RechargeNo", MobileNo);
                            //param1[8] = new SqlParameter("@msg", msg);
                            //param1[9] = new SqlParameter("@LiveId", LiveIdValue);
                            //param1[10] = new SqlParameter("@APIID", APIID);
                            DataTable dttt = UpdateTransactionStatus(url, status, Resp.Trim(), VenderId, dt.Rows[0][1].ToString(), DeductionAmt, UserId, MobileNo,msg,LiveIdValue,APIID);
                            //string result = dttt.Rows[0][0].ToString();
                            res = status;
                            return status;
                        }

                        Error1 = status;
                        res = status;
                        return status;
                    }
                    catch (Exception ex)
                    {

                        Balance = 0;
                        RequestLiveID = "";
                        TransactionID = "";
                        Error1 = "Updation(04)";
                        //eko objeko = new eko();
                        InsertError("Block 1", ex.Message, "Last", "", TransactionID, "", "");
                        res = "Failed<font color='white'>:" + ex.Message + "</font>";
                        return "Failed<font color='white'>:" + ex.Message + "</font>";
                    }

                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                Balance = 0;
                RequestLiveID = "";
                TransactionID = "";
                Error1 = "Updation(04)";
                res = ex.Message.ToString();
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }

        public string Recharge1(string MobileNo, string Code, string Amount, string STD, string RechargeType, string UserId, out string TransactionID, out decimal Balance, out string Error1, string ip, out string RequestLiveID, string option1 = "", string option2 = "", string option3 = "", string option4 = "", string ApiRequestID = "")
        {
            int[] ErrorCode = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 
                                    27, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47,
                                    48, 50,51,52,53,54,55,56,57,81,82,83,84,85,86,87,88,89,224,333};

            //  Balance = 0;
            ApiRequestID = "";
            int APIID = 0;
            string res = "";
            string s2 = "";
            SqlConnection cn;
            SqlTransaction tr = null;
            DataSet ds = new DataSet();
            cn = ObjData.StartConnectionInTransaction();
            tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                s2 = "Sp_Rechargeoperatortype";
                SqlParameter[] parameter = { 
                                                new SqlParameter("@RechargeNo", MobileNo.Trim()),
                                                new SqlParameter("@OpId", Code),
                                                new SqlParameter("@Amount", Amount.Trim()),
                                                new SqlParameter("@STD", STD),
                                                new SqlParameter("@RechargeType", RechargeType),
                                                new SqlParameter("@UserId", UserId),
                                                new SqlParameter("@ApiRequestID", ApiRequestID),
                                                new SqlParameter("@IP", ip),
                                                new SqlParameter("@option1", option1),
                                                new SqlParameter("@option2", option2),
                                               new SqlParameter("@option3", option3),
                                               new SqlParameter("@option4", option4),
                };
                ds = ObjData.RunDataSetProcedureTRans(s2, tr, parameter);

                string status = "1", Resp = "", LiveIdValue = "";
                if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                {
                    Balance = 0;
                    TransactionID = "";
                    status = "0";
                    RequestLiveID = "";
                    Error1 = ds.Tables[0].Rows[0][1].ToString();
                    return status;
                }
                else
                {
                    try
                    {
                        string CompleteResponse = string.Empty;
                        string CompleteRequest = string.Empty;
                        Balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["Balance"].ToString());
                        string url = ds.Tables[0].Rows[0]["URL"].ToString();
                        string Type = ds.Tables[0].Rows[0]["Type"].ToString();
                        string StatusValue = ds.Tables[0].Rows[0]["StatusValue"].ToString();
                        string StatusName = ds.Tables[0].Rows[0]["StatusName"].ToString();
                        string VenderId = ds.Tables[0].Rows[0]["VenderId"].ToString();
                        string LiveId =ds.Tables[0].Rows[0]["LiveId"].ToString();
                        TransactionID = ds.Tables[0].Rows[0]["TransactionID"].ToString();
                        string DeductionAmt = ds.Tables[0].Rows[0]["DeductionAmt"].ToString();
                        string ErrorMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        APIID = ds.Tables[0].Rows[0]["Apiid"].ToInt();
                        RequestLiveID = LiveId;
                        string flag = "0";
                        string msg = "";
                        if (url.ToUpper().Contains("HTTP"))
                        {

                            if (url.ToUpper().Contains("HTTP") && APIID != 23)
                            {


                                Resp = url.CallURL();
                                if (ErrorMsg != "")
                                {
                                    string[] errmsg = ErrorMsg.Split(new string[] { "||" }, StringSplitOptions.None);
                                    //string[] errmsg = ErrorMsg.Split('[]');
                                    for (int i = 0; i < errmsg.Length; i++)
                                    {
                                        if (errmsg[i].ToString().Trim() != "")
                                        {
                                            if (Resp.Contains(errmsg[i].ToString().Trim()) == true && errmsg[i] != "")
                                            {
                                                status = "0";
                                                msg = errmsg[i];
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (status != "0")
                                {

                                    DataTable dtResp = GetResponseValues(Resp, Type);
                                    DataColumnCollection columns = dtResp.Columns;
                                    if (dtResp.Columns.Contains("ResultErr") == false)
                                    {
                                        string[] StsCol = StatusName.Split('/');
                                        string[] StsVal = StatusValue.Split(',');
                                        if (dtResp.Columns.Contains(StsCol[0].Trim()))
                                        {
                                            string matchvalue = dtResp.Rows[0][StsCol[0].Trim()].ToString().ToUpper();
                                            if (matchvalue == StsVal[0].Trim().ToUpper())
                                            {

                                                status = "2";
                                            }
                                        }
                                        if (columns.Contains(VenderId))
                                        {
                                            VenderId = dtResp.Rows[0][VenderId].ToString().ToUpper();
                                        }
                                        else
                                        {
                                            VenderId = "";
                                        }
                                        if (columns.Contains(LiveId))
                                        {
                                            LiveIdValue = dtResp.Rows[0][LiveId].ToString().ToUpper();
                                        }
                                        if (columns.Contains(StsCol[0].Trim()))
                                        {
                                            string matchvalue = dtResp.Rows[0][StsCol[0].Trim()].ToString().ToUpper();
                                            if (matchvalue == StsVal[2].Trim().ToUpper())
                                            {

                                                status = "1";
                                            }
                                        }
                                        if (columns.Contains("MSG"))
                                        {
                                            msg = dtResp.Rows[0]["MSG"].ToString().ToUpper();
                                            if (msg.ToLower().Contains("insufficient") == true)
                                            {
                                                Error1 = "System error found";
                                            }
                                            else
                                            {
                                                Error1 = msg;
                                            }
                                        }
                                        if (dtResp.Columns.Contains(StsCol[StsCol.Length - 1]))
                                        {
                                            if (dtResp.Rows[0][StsCol[StsCol.Length - 1].Trim()].ToString() == StsVal[1].Trim().ToUpper())
                                            {
                                                status = "0";

                                            }
                                            else if (StsCol.Length == 2)
                                            {
                                                if (dtResp.Columns.Contains(StsCol[StsCol.Length - 1]))
                                                {
                                                    status = "0";

                                                }

                                            }
                                        }


                                    }
                                    if (status == "")
                                    {
                                        status = "";
                                        status = "1";
                                    }
                                }

                            }
                            else if (APIID == 23)
                            {
                                try
                                {
                                    string Request = string.Empty;


                                    string[] spliturl = ds.Tables[0].Rows[0][0].ToString().Split('|');
                                    int count = 0;
                                    foreach (string url1 in spliturl)
                                    {
                                        count += 1;
                                        string URL = url1.Trim();
                                        TransactionID = ds.Tables[0].Rows[0]["TransactionID"].ToString();
                                        if (Code == "RD" || Code == "TD")
                                            Request = _strRequest("326386", "331400", "331401", SessionNo, MobileNo, "2", "", Amount.ToString());
                                        else
                                            Request = _strRequest("326386", "331400", "331401", SessionNo, MobileNo, "", "", Amount.ToString());
                                        ssl.message = ssl.Sign_With_PFX(Request, keyPath, "gautam$3444");
                                        CompleteRequest += "Request:\r\n" + ssl.message + '|';
                                        ssl.htmlText = ssl.CallCryptoAPI(ssl.message, url1);
                                        Resp = "Response:\r\n" + ssl.htmlText;

                                        CompleteResponse += Resp + '|';
                                        DataTable ResPDt = convertStringToDataTable(Resp);
                                        if (ResPDt.Rows.Count > 0)
                                        {

                                            if (ResPDt.Rows[0]["ERROR"].ToString() == "0")
                                            {
                                                status = "2";
                                                //Sts = "SUCCESS";
                                            }
                                            else
                                            {
                                                bool has = ErrorCode.Contains(ResPDt.Rows[0]["ERROR"].ToString().ToInt());
                                                if (has == true && count < 3)
                                                {
                                                    status = "0";
                                                    break;
                                                    //if (has == true)
                                                    //{
                                                    //    DataTable dtPartial = new DataTable();
                                                    //    String Query = "Select PartialPay from OperatorCode where ID='" + Code + "'";
                                                    //    SqlDataAdapter da = new SqlDataAdapter(Query, Connection.ConnectionString);
                                                    //    da.Fill(dtPartial);
                                                    //    if (dtPartial.Rows.Count > 0)
                                                    //    {
                                                    //        if (dtPartial.Rows[0]["PartialPay"].ToString() == "N")
                                                    //        {
                                                    //            //LiveIdValue = ResPDt.Rows[0]["ERRMSG"].ToString();
                                                    //            status = "0";
                                                    //            //Sts = "FAILED";
                                                    //            break;
                                                    //        }
                                                    //        else if (dtPartial.Rows[0]["PartialPay"].ToString() == "Y" && count == 1)
                                                    //        {
                                                    //            status = "2";
                                                    //        }
                                                    //        else if (dtPartial.Rows[0]["PartialPay"].ToString() == "")
                                                    //        {
                                                    //            //LiveIdValue = ResPDt.Rows[0]["ERRMSG"].ToString();
                                                    //            status = "0";
                                                    //            //Sts = "FAILED";
                                                    //            break;
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            //LiveIdValue = ResPDt.Rows[0]["ERRMSG"].ToString();
                                                    //            status = "0";
                                                    //            //Sts = "FAILED";
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //}
                                                }
                                            }
                                        }
                                        if (ResPDt.Columns.Contains("TRANSID") && count == 2)
                                            VenderId = ResPDt.Rows[0]["TRANSID"].ToString();
                                        if (ResPDt.Columns.Contains("AUTHCODE") && count == 2)
                                            LiveIdValue = ResPDt.Rows[0]["AUTHCODE"].ToString();
                                        //}
                                        //else
                                        //{
                                        //    Sts = "FAILED";
                                        //    OperaterId = "Operator Disabled";
                                        //    System.Web.HttpContext.Current.Session["TRRANS"] = TransactionID;
                                        //}


                                    }
                                    CompleteResponse = CompleteResponse.Trim();
                                    CompleteResponse = CompleteResponse.Trim('|');
                                    CompleteRequest = CompleteRequest.Trim();
                                    CompleteRequest = CompleteRequest.Trim('|');
                                    Resp = "[" + CompleteRequest + "---" + CompleteResponse + "]";
                                }
                                catch (Exception ex)
                                {
                                    Resp = "[" + CompleteRequest + "---" + CompleteResponse + "]";
                                    status = "0";
                                    Balance = 0;
                                    RequestLiveID = "";
                                    TransactionID = "";
                                    Error1 = ex.ToString();

                                }
                            }

                            else
                            {
                                status = "1";
                            }
                            if (msg.Contains("insufficient") == true)
                            {
                                Error1 = "System error found";
                            }
                            else
                            {
                                Error1 = msg;
                            }
                            //SqlParameter[] param1 = new SqlParameter[11];
                            //param1[0] = new SqlParameter("@Url", url);
                            //param1[1] = new SqlParameter("@Status", status);
                            //param1[2] = new SqlParameter("@Response", Resp.Trim());
                            //param1[3] = new SqlParameter("@VenderId", VenderId);
                            //param1[4] = new SqlParameter("@TransactionID", ds.Tables[0].Rows[0][1].ToString());
                            //param1[5] = new SqlParameter("@DeductionAmt", DeductionAmt);
                            //param1[6] = new SqlParameter("@UserId", UserId);
                            //param1[7] = new SqlParameter("@RechargeNo", MobileNo);
                            //param1[8] = new SqlParameter("@msg", msg);
                            //param1[9] = new SqlParameter("@LiveId", LiveIdValue);
                            //param1[10] = new SqlParameter("@APIID", APIID);
                            DataTable dttt = UpdateTransactionStatus1(url, status, Resp.Trim(), VenderId, ds.Tables[0].Rows[0][1].ToString(), DeductionAmt, UserId, MobileNo, msg, LiveIdValue, APIID);
                            //string result = dttt.Rows[0][0].ToString();
                            return status;
                        } Error1 = status;
                        return status;
                    }
                    catch (Exception ex)
                    {
                        Balance = 0;
                        RequestLiveID = "";
                        TransactionID = "";
                        Error1 = ex.ToString();
                        return "Failed<font color='white'>1:" + ex.Message + "</font>";
                    }

                }

                tr.Commit();
            }
            catch (Exception ex)
            {
                Balance = 0;
                RequestLiveID = "";
                TransactionID = "";
                Error1 = "Updation(04)";
                res = ex.Message.ToString();
                tr.Rollback();
            }
            finally
            {
                ObjData.EndConnection();
                tr.Dispose();
            }
            return res;
        }
        public DataTable InsertCallBackURL(string url)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_InsertCallbackUrl";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@Url", url)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_41)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable UpdateCallBackResponse(string refId, string LiveId, string status, string Message)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_update_Callback";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@ReferenceId", refId),
					new SqlParameter("@Liveid", LiveId),
					new SqlParameter("@status", status),
					new SqlParameter("@Message", Message)
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_6F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable CheckReponsefromSMS(string refId)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "sp_ChkRechargeFromSMS";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@RefrenceID", refId),					
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_6F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable fillApiid(string opid)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "SELECT A.OPcode FROM ApiOpCode A JOIN OperatorCode O ON A.Opid=O.Id WHERE apiid=1 AND A.opid='" + opid + "'";
                dt = this.ObjData.RunDataTable(s2);
            }
            catch (System.Exception ex_26)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
        public DataTable CheckSRSREcharge(string apiname)
        {
            DataTable dt = null;
            this.ObjData.StartConnection();
            try
            {
                string s2 = "SRSRecharge";
                SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@ApiName", apiname),					
				};
                dt = this.ObjData.RunDataTableProcedure(s2, parameter);
            }
            catch (System.Exception ex_6F)
            {
                dt = null;
            }
            this.ObjData.EndConnection();
            return dt;
        }
    }
}
