using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRUDdb;
using MySql.Data.MySqlClient;

namespace CRUDProgram
{
    public class Token
    {
        public int tokenID { get; set; }
        public String name { get; set; }
        public String symbol { get; set; }
        public String address { get; set; }
        public int supply { get; set; }
        public int holders { get; set; }
        public double percentSupply { get; set; }
    }
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // C# Coding: CREATE
        [WebMethod]
        public static string SetToken(string name, string symbol, string contact, int supply, int holder)
        {
            string msg = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO token (symbol, name, total_supply, contract_address, total_holders) VALUES (@symbol, @name, @supply, @address, @holder)"))
                {
                    cmd.Parameters.AddWithValue("@symbol", symbol);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@supply", supply);
                    cmd.Parameters.AddWithValue("@address", contact);
                    cmd.Parameters.AddWithValue("@holder", holder);
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i == 1)
                    {
                        msg = "true";
                    }
                    else
                    {
                        msg = "false";
                    }
                    con.Close();
                }
            }
            return msg;
        }

        // C# Coding: READ
        [WebMethod(EnableSession = true)]
        public static List<Token> GetToken()
        {
            dbcon get = new dbcon();

            List<Token> dataList = new List<Token>();

            string getConfig = "Select id, symbol, name, total_supply, contract_address, total_holders From token";

            DataTable getDt = new DataTable();
            getDt = get.dtTable(getConfig);

            foreach (DataRow row in getDt.Rows)
            {
                Token data = new Token();
                data.tokenID = Convert.ToInt32(row["id"]);
                data.name = row["name"].ToString();
                data.symbol = row["symbol"].ToString();
                data.supply = Convert.ToInt32(row["total_supply"]);
                data.holders = Convert.ToInt32(row["total_holders"]);
                data.address = row["contract_address"].ToString();
                data.percentSupply = 100.0;

                dataList.Add(data);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.Serialize(dataList);
            return dataList;
        }

        // C# Coding: UPDATE
        [WebMethod(EnableSession = true)]
        public static String UpdateToken(int id, string name, string symbol, int supply)
        {
            string msg = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("UPDATE token SET name = @TokenName, symbol = @symbol, total_supply = @supply WHERE id = @ID"))
                {
                    cmd.Parameters.AddWithValue("@TokenName", name);
                    cmd.Parameters.AddWithValue("@symbol", symbol);
                    cmd.Parameters.AddWithValue("@supply", supply);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i == 1)
                    {
                        msg = "true";
                    }
                    else
                    {
                        msg = "false";
                    }
                    con.Close();
                }
            }
            return msg;
        }

        // C# Coding: DELETE
        [WebMethod(EnableSession = true)]
        public static String DeleteToken(int id)
        {
            string msg = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM Token WHERE id =" + id))
                {
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i == 1)
                    {
                        msg = "true";
                    }
                    else
                    {
                        msg = "false";
                    }
                    con.Close();
                }
            }
            return msg;
        }
    }
}