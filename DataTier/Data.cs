using System;
using System.StubHelpers;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.IO;

namespace DataTier
{
    public class Data
    {
        SqlConnection connection;

        public void StartConnection()
        {
            string strin = System.Configuration.ConfigurationManager.ConnectionStrings["Connection String"].ToString();

            connection = new SqlConnection(strin);
            connection.Open();
        }
        public SqlConnection StartConnectionInTransaction()
        {
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["Connection String"].ToString();
            connection = new SqlConnection(str);
            connection.Open();
            return connection;
        }

        public void EndConnection()
        {
            connection.Close();
        }

        public DataSet RunSelectQuery(string sqlCon)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection);
            command.CommandTimeout = 100000;
            SqlDataAdapter data_A = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            data_A.Fill(ds);

            return ds;
        }

        public DataTable RunSelectQuerydatatable(string sqlCon)
        {
            SqlCommand selectCommand = new SqlCommand(sqlCon, this.connection);
            selectCommand.CommandTimeout = 100000;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public void RunInsUpDelQuery(string sqlCon)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection);
            command.ExecuteNonQuery();
        }

        public int RunInsUpDelQueryNew(string sqlCon)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection);
            int a = command.ExecuteNonQuery();
            return a;
        }
        public DataTable RunDataTableParam(string sql, SqlParameter[] sqlparam)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);

            for (int i = 0; i < sqlparam.Length; i++)
            {
                cmd.Parameters.Add(sqlparam[i]);
            }
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }

        public DataSet RunSelectQueryTrans(string sqlCon, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection, transaction);
            SqlDataAdapter dataA = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            dataA.Fill(ds);

            return ds;
        }

        public void RunInsUpDelQueryTrans(string sqlCon, SqlTransaction transaction)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection, transaction);

            command.ExecuteNonQuery();

        }
        public void RunInsUpDelQueryTransProc(string sqlCon, SqlTransaction transaction, SqlParameter[] sqlparam)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection, transaction);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                command.Parameters.Add(sqlparam[i]);
            }
            command.ExecuteNonQuery();

        }
        public string RunInsUpDelQueryTransProcScalar(string sqlCon, SqlTransaction transaction, SqlParameter[] sqlparam)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection, transaction);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                command.Parameters.Add(sqlparam[i]);
            }
            string s = command.ExecuteScalar().ToString();
            return s;

        }
        public DataTable RunDataTableProcedureTRans(string sql, SqlTransaction transaction, SqlParameter[] sqlparam)
        {
            SqlCommand cmd = new SqlCommand(sql, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                cmd.Parameters.Add(sqlparam[i]);
            }
            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        public DataTable RunDataTableProcedureTRansGetPage(string sql, SqlTransaction transaction, SqlParameter[] sqlparam)
        {
            SqlCommand cmd = new SqlCommand(sql, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                cmd.Parameters.Add(sqlparam[i]);
                if (sqlparam[i].ToString() == "@RecordCount")
                {
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                }

            }

            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
          
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        public DataSet RunDataSetProcedureTRans(string sql, SqlTransaction transaction, SqlParameter[] sqlparam)
        {
            SqlCommand cmd = new SqlCommand(sql, connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                cmd.Parameters.Add(sqlparam[i]);
            }
            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
                throw;
            }
            return ds;
        }
        public DataTable RunDataTable(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        public DataTable RunDataTableProcedure(string sql, SqlParameter[] sqlparam)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                cmd.Parameters.Add(sqlparam[i]);
            }
            //cmd.ExecuteNonQuery();
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        public DataSet RunDataSetProcedure(string sql, SqlParameter[] sqlparam)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                cmd.Parameters.Add(sqlparam[i]);
            }
            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet();
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt = null;
                throw;
            }
            return dt;
        }
        public DataTable RunSelectQueryTTrans(string sql, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand(sql, connection, trans);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
        public string send_sms(string senders, string message)
        {


            string baseurl = "http://173.45.76.227/send.aspx?";
            WebRequest w = WebRequest.Create(baseurl);
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            string status = "";
            using (Stream requestStream = w.GetRequestStream())
            {

                byte[] buffer = new UTF8Encoding().GetBytes("username=AmbroCabs&pass=Lucknow01&route=trans1&senderid=AMBROC&numbers=" + senders + "&message=" + message);
                requestStream.Write(buffer, 0, buffer.Length);
            }
            using (HttpWebResponse r = (HttpWebResponse)w.GetResponse())
            {
                using (StreamReader reader = new StreamReader(r.GetResponseStream()))
                {
                    status = reader.ReadToEnd().ToString();

                }
            }
            return status;
        }
        public int RunInsUpDelQueryTransProcNew(string sqlCon, SqlTransaction transaction, SqlParameter[] sqlparam)
        {
            SqlCommand command = new SqlCommand(sqlCon, connection, transaction);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < sqlparam.Length; i++)
            {
                command.Parameters.Add(sqlparam[i]);
            }
            int c = command.ExecuteNonQuery();
            return c;
        }
        
    }
}