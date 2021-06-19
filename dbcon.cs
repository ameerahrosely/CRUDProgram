using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web;
using MySql.Data.MySqlClient;

namespace CRUDdb
{
    public class dbcon
    {
        public string str = "";

        public System.Data.DataTable dtTable(String strsql)
        {
            MySqlCommand cmd = new MySqlCommand(strsql);
            System.Data.DataTable dt = new System.Data.DataTable();

            String strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            MySqlConnection con = new MySqlConnection(strConnString);

            MySqlDataAdapter sda = new MySqlDataAdapter();

            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public string executeQuery(String strsql)
        {
            MySqlCommand cmd = new MySqlCommand(strsql);

            String strConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strConnString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
    }
}