using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarDealershipSystem
{
    public partial class Bookyourcar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string carid = TextBox1.Text.ToUpper();
                string buyerid = TextBox2.Text;
                string constr = ConfigurationManager.ConnectionStrings["projectDBfinalConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    string extract = "select * from Cardetails where carid =@carid";
                    using (SqlCommand cmd = new SqlCommand(extract, conn))
                    {
                        //This connection is checking id is registered or not
                        conn.Open();
                        cmd.Parameters.AddWithValue("@carid", carid);
                        cmd.Connection = conn;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('The car is not registered');", true);

                            conn.Close();
                        }
                        else
                        {
                            string Spbooking = "SPbookcar";
                            using (SqlCommand cmd2 = new SqlCommand(Spbooking, conn))
                            {
                                //this connection is saving buyer id and car id in database
                                conn.Close();
                                conn.Open();
                                cmd2.CommandType = System.Data.CommandType.StoredProcedure;//SP
                                cmd2.Parameters.AddWithValue("@buyerid", buyerid);
                                cmd2.Parameters.AddWithValue("@carid", carid);
                                cmd2.ExecuteNonQuery();
                                conn.Close();
                            }
                            Label3.Visible = true;
                            Label3.Text = "Car ID number " + carid + " has been booked";

                        }
                    }

                    conn.Open();

                    string dltimg = ("exec dltfromImg '" + carid + "'   ");

                    using (SqlCommand cmd2 = new SqlCommand(dltimg, conn))

                    {

                        {
                            SqlDataAdapter sda = new SqlDataAdapter("exec dltfromImg  @carid='' ", conn);
                            cmd2.ExecuteNonQuery();

                        }


                    }
                    string carid2 = carid;
                    string dltcar = ("exec Deletecardetails '" + carid2 + "'   ");
                    using (SqlCommand cmd = new SqlCommand(dltcar, conn))

                    {


                        {
                            SqlDataAdapter sda = new SqlDataAdapter("exec Deletecardetails  @carid='' ", conn);
                            DataSet ds = new DataSet();
                            sda.Fill(ds);
                            cmd.ExecuteNonQuery();

                        }


                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Something went wrong, Try again later');", true);

            }

        }
    }
}