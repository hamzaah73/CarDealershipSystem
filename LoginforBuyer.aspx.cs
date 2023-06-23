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
    public partial class LoginforBuyer : System.Web.UI.Page
    {
        static string constr = ConfigurationManager.ConnectionStrings["projectDBfinalConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(constr);
        public string buyerid;
        public string password;
        private void updatelogin()
        {
            conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand("update loggedin set logged=  1 where buyerid ='" + buyerid + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        private bool CheckLogin()
        {
            buyerid = TextBox1.Text;
            bool returnlog = false;
            conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select logged from loggedin where buyerid = '" + buyerid + "'", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            //dr.Read();
            while (dr.Read())
            {
                if (dr["logged"].ToString().Equals("1"))
                {
                    returnlog = true;
                }
                else if (dr["logged"].ToString().Equals("0"))
                {
                    returnlog = false;
                }
            }
            dr.Close();
            conn.Close();
            return returnlog;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {  //LOGIN
            
            //try
            {
                string buyerid = TextBox1.Text.ToLower();
                Application["loginbuyerid"] = buyerid;
                string pass = TextBox2.Text.ToLower();
                {
                    conn.Close();
                    conn.Open();
                    string extract = "select * from buyerCredentials where buyerid ='" + buyerid + "'and pass='" + pass + "'";
                    SqlCommand cmd = new SqlCommand(extract, conn);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                if (CheckLogin() == false)
                                {
                                    updatelogin();
                            conn.Close();
                            conn.Open();
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        Response.Redirect("DisplayAds.aspx");
                                       
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Incorrect Email or Password or Register your account');", true);
                                        
                                    }

                                }
                                else
                                {
                                    
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('USER IS LOGGED IN ALREADY. PLEASE TRY AGAIN LATER.');", true);
                                    
                                }
                            }

                            else
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Username and Password')</script>");
                                
                            }
                            conn.Close();

                        }
                        conn.Close();
                        

                    
            }

            //catch (Exception ex)
            //{
                
            //    conn.Close();
            //    Response.Write(ex.Message);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Something went wrong, Try again later');", true);

            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateBuyerAccount.aspx");
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bookyourcar.aspx");
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Bookyourcar.aspx");

        }
    }
}