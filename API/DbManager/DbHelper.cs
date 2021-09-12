using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager
{
    public class DbHelper
    {
        public static string StrCon = null;
        public static MySqlConnection con;
        public static MySqlCommand cmd;

        private void DbHelperConnection()
        {
            string jsonfile = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\appsettings.json");
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonfile);
            StrCon = jsonData.Logging.ConnectionString;
        }
        public static DataTable ExecuteSQLQuery(string Query)
        {
            DbHelper dd = new DbHelper(); dd.DbHelperConnection();
            DataTable DtResults = new DataTable();
            try
            {
                using (MySqlConnection con = new MySqlConnection(StrCon))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(Query, con))
                    {
                        cmd.CommandTimeout = 6000;
                        cmd.ExecuteNonQuery();
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class SqlDBHelper
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public static DataTable ExecuteSQLQuery(string Query,string StrCon)
        {
            DataTable DtResults = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(StrCon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(DtResults);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return DtResults;
        }
        public static string ExecuteNonSQLQuery(string Query, string StrCon)
        {
            string Response = "";
            try
            {
                using (SqlConnection con = new SqlConnection(StrCon))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        Response = "Execute";
                    }
                }
            }
            catch (Exception ex) { Response = ex.Message; }
            return Response;
        }
    }
}
