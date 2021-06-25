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
using OfficeOpenXml;

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
        public double pageCount { get; set; }
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

        // C# Coding: READ FOR PAGINATION TABLE
        [WebMethod(EnableSession = true)]
        public static List<Token> GetTokenList(int selectedPage)
        {
            dbcon get = new dbcon();

            List<Token> dataList = new List<Token>();

            int nextRow = 10;
            int offset = (selectedPage * 10) - 10;

            string getConfig = "Select id, symbol, name, total_supply, contract_address, total_holders From token " +
                " Limit " + nextRow +
                " Offset " + offset;

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

            string getConfigList = "Select id, symbol, name, total_supply, contract_address, total_holders From token ";

            DataTable getDts = new DataTable();
            getDts = get.dtTable(getConfigList);

            double pageSize = (double)getDts.Rows.Count / 10;

            Token count = new Token();
            if (pageSize < 1)
                count.pageCount = 1.0;
            else
                count.pageCount = Math.Ceiling(pageSize);

            dataList.Add(count);

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.Serialize(dataList);
            return dataList;
        }

        // C# Coding: EXPORT TO EXCEL AS XLSX
        protected void btnExportMasterList_Click(object sender, EventArgs e)
        {
            dbcon get = new dbcon();

            char a = 'A';
            int rowStart = 3;
            int i = 0;
        
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Token Summary");

            string fileName = "Token Summary.xlsx";

            for (i = 4; i <= 20; i++)
            {
                ws.Column(i).Width = 25;
            }
            ws.Column(4).Width = 15;
            ws.Column(4).Width = 35;

            ws.Cells["A1"].Value = "Rank";
            ws.Cells["B1"].Value = "Symbol";
            ws.Cells["C1"].Value = "Name";
            ws.Cells["D1"].Value = "Contact Address";
            ws.Cells["E1"].Value = "Total Holders";
            ws.Cells["F1"].Value = "Total Supply";
            ws.Cells["G1"].Value = "Total Supply (%)";

            List<Token> dataList = new List<Token>();

            string getList = "Select id, symbol, name, total_supply, contract_address, total_holders From token";

            DataTable listDt = new DataTable();
            listDt = get.dtTable(getList);

            foreach (DataRow row in listDt.Rows)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = row["id"].ToString();
                ws.Cells[string.Format("B{0}", rowStart)].Value = row["symbol"].ToString();
                ws.Cells[string.Format("C{0}", rowStart)].Value = row["name"].ToString();
                ws.Cells[string.Format("D{0}", rowStart)].Value = row["total_supply"].ToString();
                ws.Cells[string.Format("E{0}", rowStart)].Value = row["contract_address"].ToString();
                ws.Cells[string.Format("F{0}", rowStart)].Value = row["total_holders"].ToString();
                ws.Cells[string.Format("G{0}", rowStart)].Value = 100;

                rowStart++;
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}