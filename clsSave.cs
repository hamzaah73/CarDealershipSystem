using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarDealershipSystem
{
    public class clsSave
    {
        public void savedata(string buyerid, string pass)
        {

            string constr = ConfigurationManager.ConnectionStrings["projectDBfinalConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                string sql = "INSERT INTO buyerCredentials VALUES(@buyerid, @pass)";
                using (SqlCommand cmd2 = new SqlCommand(sql, conn))
                {
                    //this connection is saving id and password in database
                    conn.Close();
                    conn.Open();

                    cmd2.Parameters.AddWithValue("@buyerid",buyerid );
                    cmd2.Parameters.AddWithValue("@pass", pass);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    
                }
            }
        }
    }
}