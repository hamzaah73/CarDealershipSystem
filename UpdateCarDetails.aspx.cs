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
    public partial class UpdateCarDetails : System.Web.UI.Page
    {
        static string constr = ConfigurationManager.ConnectionStrings["projectDBfinalConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(constr);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showtable();
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisplayAds.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string carid = TextBox1.Text.ToUpper();
                int make = int.Parse(TextBox2.Text);
                string model = TextBox3.Text;
                string company = TextBox4.Text;
                int price = int.Parse(TextBox5.Text);

                conn.Open();
                string check = " select * from Cardetails where sellerid=@sellerid";
                string updatesp = "exec Updatecardetails '" + carid + "','" + make + "','" + model + "','" + company + "','" + price + "','" + Application["loginseller"] + "'";

                SqlCommand cmd = new SqlCommand(updatesp, conn);
                SqlCommand cmd2 = new SqlCommand(check, conn);


                cmd2.Parameters.AddWithValue("@sellerid", Application["loginseller"]);
                cmd2.Parameters.AddWithValue("@carid", carid);
                cmd2.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    showtable();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    Response.Write("you cannot edit other's data");
                }


            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Something went wrong, Try again later');", true);

            }

        }
        public object showtable()
        {

            SqlDataAdapter sda = new SqlDataAdapter("exec Updatecardetails  @carid= carid ,@make= 0 ,@model='',@company='',@price=0, @sellerid='' ", conn);

            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string carid = TextBox1.Text.ToUpper();

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
                        Label6.Visible = true;
                        Label6.Text = "The record '" + carid + "' has been deleted";

                    }


                    conn.Close();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Something went wrong, Try again later');", true);

            }

        }

        //protected void Button4_Click(object sender, EventArgs e)
        //{
        //    string carid = TextBox1.Text.ToUpper();
        //    int make = int.Parse(TextBox2.Text);
        //    string model = TextBox3.Text;
        //    string company = TextBox4.Text;
        //    int price = int.Parse(TextBox5.Text);
        //    conn.Open();
        //    // string updatesp = "exec Updatecardetails '" + carid + "','" + make + "','" + model + "','" + company + "','" + price + "'  ,'" + Application["loginseller"].ToString() + "'   "; 


        //    string updatesp = "exec Updatecardetails '" + carid + "','" + make + "','" + model + "','" + company + "','" + price + "','" + Application["loginseller"] + "'";
        //    using (SqlCommand cmd = new SqlCommand(updatesp, conn))

        //    {

        //        // SqlDataReader sqldr = cmd.ExecuteReader();
        //        // if (sqldr.Read())
        //        {
        //            showtable();
        //            cmd.ExecuteNonQuery();

        //        }
        //        //else
        //        //{
        //        //    ScriptManager.RegisterStartupScript(this,this.GetType(),"script","alert('You cannot delete any other car ad');",true);
        //        //}

        //        conn.Close();
        //    }
        //}
    }
}
