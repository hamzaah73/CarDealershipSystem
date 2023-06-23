using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CarDealershipSystem
{
    public partial class DisplayAds : System.Web.UI.Page
    {
        static string constr = ConfigurationManager.ConnectionStrings["projectDBfinalConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(constr);

       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                showtable();
                conn.Close();

                ////////////FOR HIDING BUTTON//////////////////////////
                string extract = "select * from buyerCredentials";

                using (SqlCommand cmd2 = new SqlCommand(extract, conn))
                {
                    conn.Open();
                    cmd2.Connection = conn;
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {

                      
                    }


                }
            }


        }

        string extract2 = "exec SPdisplaycar @carid= '',@make= '',@model='', @company='',@price='',@sellerid='' ";
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(extract2, conn))
                {
                    
                    conn.Open();
                    showtable();
                    cmd.CommandType = CommandType.StoredProcedure;//SP                    
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public object showtable()
        {
            SqlDataAdapter sda = new SqlDataAdapter(extract2, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            Response.Redirect("LoginforSeller.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //Delete button webform direction

        }

        protected void GridView2_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void Button5_Click1(object sender, EventArgs e)
        {
            Response.Redirect("LoginforAdmin.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

      

        protected void Button7_Click1(object sender, EventArgs e)
        {
            Response.Redirect("LoginforBuyer.aspx");
        }
    }
}

