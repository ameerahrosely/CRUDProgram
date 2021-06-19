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
    public class Club
    {
        public int clubID { get; set; }
        public String clubName { get; set; }
        public int amountOfMembers { get; set; }
        public String dateofReg { get; set; }
    }
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // C# Coding: CREATE
        [WebMethod]
        public static string SetClub(string name, int member, string date)
        {
            string msg = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO club (ClubName, ClubMembers, DateRegistered) VALUES (@ClubName, @ClubMembers, @DateRegistered)"))
                {
                    cmd.Parameters.AddWithValue("@ClubName", name);
                    cmd.Parameters.AddWithValue("@ClubMembers", member);
                    cmd.Parameters.AddWithValue("@DateRegistered", date);
                    cmd.Connection = con;
                    con.Open();
                    //cmd.ExecuteNonQuery();
                    
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
        public static List<Club> GetClub()
        {
            dbcon get = new dbcon();

            List<Club> dataList = new List<Club>();

            string getConfig = "Select id, ClubName, ClubMembers, DateRegistered From club";

            DataTable getDt = new DataTable();
            getDt = get.dtTable(getConfig);

            foreach (DataRow row in getDt.Rows)
            {
                Club data = new Club();
                data.clubID = Convert.ToInt32(row["id"]);
                data.clubName = row["ClubName"].ToString();
                data.amountOfMembers = Convert.ToInt32(row["ClubMembers"]);
                data.dateofReg = row["DateRegistered"].ToString();

                dataList.Add(data);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.Serialize(dataList);
            return dataList;
        }

        // C# Coding: UPDATE
        [WebMethod(EnableSession = true)]
        public static List<Club> UpdateClub()
        {
            dbcon get = new dbcon();

            List<Club> dataList = new List<Club>();

            string getConfig = "Select id, ClubName, ClubMembers, DateRegistered From club";

            DataTable getDt = new DataTable();
            getDt = get.dtTable(getConfig);

            foreach (DataRow row in getDt.Rows)
            {
                Club data = new Club();
                data.clubID = Convert.ToInt32(row["id"]);
                data.clubName = row["ClubName"].ToString();
                data.amountOfMembers = Convert.ToInt32(row["ClubMembers"]);
                data.dateofReg = row["DateRegistered"].ToString();
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.Serialize(dataList);
            return dataList;
        }

        // C# Coding: DELETE
        [WebMethod(EnableSession = true)]
        public static List<Club> DeleteClub()
        {
            dbcon get = new dbcon();

            List<Club> dataList = new List<Club>();

            string getConfig = "Select id, ClubName, ClubMembers, DateRegistered From club";

            DataTable getDt = new DataTable();
            getDt = get.dtTable(getConfig);

            foreach (DataRow row in getDt.Rows)
            {
                Club data = new Club();
                data.clubID = Convert.ToInt32(row["id"]);
                data.clubName = row["ClubName"].ToString();
                data.amountOfMembers = Convert.ToInt32(row["ClubMembers"]);
                data.dateofReg = row["DateRegistered"].ToString();
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.Serialize(dataList);
            return dataList;
        }
    }


}