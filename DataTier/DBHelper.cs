using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace DataTier
{
    public class DBHelper
    {
     private static string connectionString = string.Empty;

        static DBHelper()
        {
            try
            {

                connectionString = "Server=97.74.91.222;Initial Catalog=www_manirayapremium_db_new;  USER ID=www_manirayapremium_db;PASSWORD=manirayapremium@123#; trusted_connection=false";
;                //connectionString = "Server=148.72.210.221;Initial Catalog=RBDCROP_db;  USER ID=RBDCROP_db;PASSWORD=RBDCROP@123#; trusted_connection=false;";

            }
            catch (Exception)
            {
                //todo error handling  mechanism
                throw;
            }
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }

        public DataTable ExecuteDataSet(string commandText, params SqlParameter[] parameters)
        {
            DataTable ds = new DataTable();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return ds;
        }


        public bool ExecuteNonQueryStoredProcedure(string ProcedureName, SqlParameter[] Parameters)
        {
            SqlConnection connectionDB = new SqlConnection(connectionString);

            try
            {

                SqlCommand cmd = new SqlCommand(ProcedureName, connectionDB);
                cmd.Connection = connectionDB;
                cmd.CommandType = CommandType.StoredProcedure;
                if (connectionDB.State == ConnectionState.Closed)
                    connectionDB.Open();
                int i = 0;
                int flag = 0;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();

                cmd.Parameters.AddRange(Parameters);
                int Result = cmd.ExecuteNonQuery();
                if (Result > 0)
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                }


                if (flag == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (connectionDB.State == ConnectionState.Open)
                    connectionDB.Close();
            }
        }


        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Code");
                dt.Columns.Add("Remark");

                DataRow dr = dt.NewRow();
                dr["Code"] = "0";
                dr["Remark"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }
    }
}