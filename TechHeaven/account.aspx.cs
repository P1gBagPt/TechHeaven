using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Serilog;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace TechHeaven
{
    public partial class account : System.Web.UI.Page
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["isLogged"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    LoadUserInfo();
                }
            }
            LoadUserInfo();
        }

        public class addresses
        {
            public int id { get; set; }
            public string name { get; set; }

            public string address { get; set; }

            public string floor { get; set; }

            public string zipcode { get; set; }

            public string location { get; set; }

            public string city { get; set; }

            public string phone { get; set; }

        }

       
        protected void btn_save_Click(object sender, EventArgs e)
        {
            int auxNIF;
            try
            {

                MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.AddWithValue("@p_UserId", Convert.ToInt32(Session["UserId"]));
                cmd.Parameters.AddWithValue("@p_FirstName", tb_first_name.Text);
                cmd.Parameters.AddWithValue("@p_LastName", tb_last_name.Text);
                cmd.Parameters.AddWithValue("@p_PhoneNumber", tb_phoneNumber.Text);

                if (tb_NIF.Text == "")
                {
                    auxNIF = 0;
                }
                else
                {
                    auxNIF = int.Parse(tb_NIF.Text);
                }

                cmd.Parameters.AddWithValue("@p_NIF", Convert.ToInt32(auxNIF));


                cmd.CommandText = "account_information_update";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Connection = mycon;
                mycon.Open();
                cmd.ExecuteNonQuery();
                mycon.Close();

                lbl_sucesso.Enabled = true;
                lbl_sucesso.Visible = true;

                lbl_sucesso.Text = "Information updated successfully";
                lbl_sucesso.ForeColor = Color.Green;

            }
            catch (MySqlException ex)
            {
                lbl_sucesso.Text = ex.ToString();
            }
        }
   

        protected void lb_save_tfa_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "tfa")
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "update_tfa";

                if (lb_save_tfa.Text == "Setup")
                {
                    myCommand.Parameters.AddWithValue("@tfa", true);
                    lb_save_tfa.Text = "Remove";
                    lb_save_tfa.ForeColor = Color.Red;


                }
                else if (lb_save_tfa.Text == "Remove")
                {
                    myCommand.Parameters.AddWithValue("@tfa", false);
                    lb_save_tfa.Text = "Setup";
                    lb_save_tfa.ForeColor = Color.White;

                }

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["UserId"]));

                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        protected void lb_save_news_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "news")
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "update_newsletter";

                if (lb_save_tfa.Text == "Setup")
                {
                    myCommand.Parameters.AddWithValue("@newsletter", true);
                    lb_save_news.Text = "Disable";
                    lb_save_news.ForeColor = Color.Red;


                }
                else if (lb_save_tfa.Text == "Remove")
                {
                    myCommand.Parameters.AddWithValue("@newsletter", false);
                    lb_save_news.Text = "Enable";
                    lb_save_news.ForeColor = Color.White;

                }

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["UserId"]));

                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        private void LoadUserInfo()
        {
            if (Session["userId"] != null)
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "user_info";
                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userId", Session["userId"]);

                SqlParameter return_firstName = new SqlParameter();
                return_firstName.ParameterName = "@return_firstName";
                return_firstName.Direction = ParameterDirection.Output;
                return_firstName.SqlDbType = SqlDbType.VarChar;
                return_firstName.Size = 50;
                myCommand.Parameters.Add(return_firstName);


                SqlParameter return_lastName = new SqlParameter();
                return_lastName.ParameterName = "@return_lastName";
                return_lastName.Direction = ParameterDirection.Output;
                return_lastName.SqlDbType = SqlDbType.VarChar;
                return_lastName.Size = 50;
                myCommand.Parameters.Add(return_lastName);

                SqlParameter return_username = new SqlParameter();
                return_username.ParameterName = "@return_username";
                return_username.Direction = ParameterDirection.Output;
                return_username.SqlDbType = SqlDbType.VarChar;
                return_username.Size = 10;
                myCommand.Parameters.Add(return_username);



                SqlParameter return_phone = new SqlParameter();
                return_phone.ParameterName = "@return_phone";
                return_phone.Direction = ParameterDirection.Output;
                return_phone.SqlDbType = SqlDbType.VarChar;
                return_phone.Size = 100;
                myCommand.Parameters.Add(return_phone);


                SqlParameter return_nif = new SqlParameter();
                return_nif.ParameterName = "@return_nif";
                return_nif.Direction = ParameterDirection.Output;
                return_nif.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(return_nif);


                SqlParameter return_2fa = new SqlParameter();
                return_2fa.ParameterName = "@return_2fa";
                return_2fa.Direction = ParameterDirection.Output;
                return_2fa.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(return_2fa);


                SqlParameter return_newsletter = new SqlParameter();
                return_newsletter.ParameterName = "@return_newsletter";
                return_newsletter.Direction = ParameterDirection.Output;
                return_newsletter.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(return_newsletter);


                SqlParameter return_total_address = new SqlParameter();
                return_total_address.ParameterName = "@return_total_address";
                return_total_address.Direction = ParameterDirection.Output;
                return_total_address.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(return_total_address);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@return";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);


                myConn.Open();
                myCommand.ExecuteNonQuery();


                int respostaSP = Convert.ToInt32(myCommand.Parameters["@return"].Value);

                string respostafirstName = myCommand.Parameters["@return_firstName"].Value.ToString();
                string respostalastName = myCommand.Parameters["@return_lastName"].Value.ToString();
                string respostausername = myCommand.Parameters["@return_username"].Value.ToString();
                string respostaPhonenumber = myCommand.Parameters["@return_phone"].Value.ToString();

                object resposta_nifValue = myCommand.Parameters["@return_nif"].Value;

                int resposta_nif;
                if (resposta_nifValue != DBNull.Value)
                {
                    resposta_nif = Convert.ToInt32(resposta_nifValue);
                }
                else
                {
                    // Handle the case where the database value is NULL
                    resposta_nif = 0; // Set a default value or handle it as per your application logic
                }

                object tfa2Value = myCommand.Parameters["@return_2fa"].Value;
                bool tfa2;
                if (tfa2Value != DBNull.Value)
                {
                    tfa2 = Convert.ToBoolean(tfa2Value);
                }
                else
                {
                    // Handle the case where the database value is NULL
                    tfa2 = false; // Set a default value or handle it as per your application logic
                }

                object newsletterValue = myCommand.Parameters["@return_newsletter"].Value;
                bool respostaNewsletter;
                if (newsletterValue != DBNull.Value)
                {
                    respostaNewsletter = Convert.ToBoolean(newsletterValue);
                }
                else
                {
                    // Handle the case where the database value is NULL
                    respostaNewsletter = false; // Set a default value or handle it as per your application logic
                }


                int respostaAddress = Convert.ToInt32(myCommand.Parameters["@return_total_address"].Value);


                myConn.Close();

                if (respostaAddress == 4)
                {
                    panel_add_address.Visible = false;
                }

                lbl_nameTop.Text = respostafirstName + " " + respostalastName + " Profile";
                tb_first_name.Text = respostafirstName;
                tb_last_name.Text = respostalastName;
                lbl_username.Text = respostausername;
                lbl_email.Text = Session["user_email"].ToString();
                tb_phoneNumber.Text = respostaPhonenumber;
                tb_NIF.Text = resposta_nif.ToString();

                if (respostaNewsletter)
                {
                    lb_save_news.Text = "Disable";
                    lb_save_news.ForeColor = Color.Red;
                }
                else
                {
                    lb_save_news.Text = "Enable";
                    lb_save_news.ForeColor = Color.White;
                }

                if (!tfa2)
                {
                    lb_save_tfa.Text = "Setup";
                    lb_save_tfa.ForeColor = Color.White;
                }
                else
                {
                    lb_save_tfa.Text = "Remove";
                    lb_save_tfa.ForeColor = Color.Red;
                }

                LoadUserAddresses();
            }
        }

        private void LoadUserAddresses()
        {
            try
            {
                List<addresses> lst_moradas = new List<addresses>();
                string query = "SELECT * FROM addresses WHERE status = 1 AND addresses.user_id = " + Session["userId"].ToString();

                using (SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCommand2 = new SqlCommand(query, myConn2))
                    {
                        myConn2.Open(); // Open the connection

                        using (SqlDataReader dr = myCommand2.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var moradas_address = new addresses();

                                moradas_address.id = Convert.ToInt32(dr["id_addresses"]);
                                moradas_address.name = dr["name"].ToString();
                                moradas_address.address = dr["address"].ToString();
                                moradas_address.floor = dr["floor"] is DBNull ? null : dr["floor"].ToString(); // Handle DBNull for nullable types
                                moradas_address.zipcode = dr["zipcode"].ToString();
                                moradas_address.location = dr["location"].ToString();
                                moradas_address.city = dr["city"].ToString();
                                moradas_address.phone = dr["phone"].ToString();

                                lst_moradas.Add(moradas_address);
                            }
                        }
                    }
                }

                Repeater1.DataSource = lst_moradas;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void lb_delete_address_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_address")
            {
                int addressId = Convert.ToInt32(e.CommandArgument);
                Console.WriteLine("Address ID: " + addressId); // Add this line
                // Crie uma conexão com o banco de dados.
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    // Abra a conexão.
                    con.Open();

                    // Consulta SQL para atualizar o estado do produto.
                    string query = "UPDATE addresses SET status = CASE WHEN status = 1 THEN 0 ELSE 1 END WHERE id_addresses = @addressID";

                    // Crie e configure o comando SQL.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@addressID", addressId);


                        // Execute a consulta.
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verifique se a consulta foi executada com sucesso.
                        if (rowsAffected > 0)
                        {
                            // Atualize a interface do usuário para refletir a mudança no estado do produto, se necessário.
                            LoadUserAddresses();

                        }
                    }
                }
            }
        }

        protected void lb_edit_address_Command(object sender, CommandEventArgs e)
        {

        }
    }
}