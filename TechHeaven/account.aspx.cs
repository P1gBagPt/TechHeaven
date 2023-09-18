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

namespace TechHeaven
{
    public partial class account : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogged"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {

                MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.AddWithValue("@f_id", Session["userId"]);


                MySqlParameter valor_retorno = new MySqlParameter();
                valor_retorno.ParameterName = "@retorno";
                valor_retorno.Direction = ParameterDirection.Output;
                valor_retorno.MySqlDbType = MySqlDbType.Int64;
                cmd.Parameters.Add(valor_retorno);


                MySqlParameter retorno_firstName = new MySqlParameter();
                retorno_firstName.ParameterName = "@retorno_firstName";
                retorno_firstName.Direction = ParameterDirection.Output;
                retorno_firstName.MySqlDbType = MySqlDbType.VarChar;
                retorno_firstName.Size = 50;
                cmd.Parameters.Add(retorno_firstName);


                MySqlParameter retorno_lastName = new MySqlParameter();
                retorno_lastName.ParameterName = "@retorno_lastName";
                retorno_lastName.Direction = ParameterDirection.Output;
                retorno_lastName.MySqlDbType = MySqlDbType.VarChar;
                retorno_lastName.Size = 50;
                cmd.Parameters.Add(retorno_lastName);


                MySqlParameter retorno_username = new MySqlParameter();
                retorno_username.ParameterName = "@retorno_username";
                retorno_username.Direction = ParameterDirection.Output;
                retorno_username.MySqlDbType = MySqlDbType.VarChar;
                retorno_username.Size = 50;
                cmd.Parameters.Add(retorno_username);


                MySqlParameter retorno_phoneNumber = new MySqlParameter();
                retorno_phoneNumber.ParameterName = "@retorno_phoneNumber";
                retorno_phoneNumber.Direction = ParameterDirection.Output;
                retorno_phoneNumber.MySqlDbType = MySqlDbType.VarChar;
                retorno_phoneNumber.Size = 100;
                cmd.Parameters.Add(retorno_phoneNumber);

                MySqlParameter retorno_nif = new MySqlParameter();
                retorno_nif.ParameterName = "@retorno_nif";
                retorno_nif.Direction = ParameterDirection.Output;
                retorno_nif.MySqlDbType = MySqlDbType.Int64;
                retorno_nif.Size = 9;
                cmd.Parameters.Add(retorno_nif);

                MySqlParameter retorno_2fa = new MySqlParameter();
                retorno_2fa.ParameterName = "@retorno_2fa";
                retorno_2fa.Direction = ParameterDirection.Output;
                retorno_2fa.MySqlDbType = MySqlDbType.Int64;
                retorno_2fa.Size = 1;
                cmd.Parameters.Add(retorno_2fa);

                MySqlParameter retorno_newsletter = new MySqlParameter();
                retorno_newsletter.ParameterName = "@retorno_newsletter";
                retorno_newsletter.Direction = ParameterDirection.Output;
                retorno_newsletter.MySqlDbType = MySqlDbType.Int64;
                retorno_newsletter.Size = 1;
                cmd.Parameters.Add(retorno_newsletter);

                cmd.CommandText = "user_info";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Connection = mycon;
                mycon.Open();
                cmd.ExecuteNonQuery();


                int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                string respostafirstName = cmd.Parameters["@retorno_firstName"].Value.ToString();
                string respostalastName = cmd.Parameters["@retorno_lastName"].Value.ToString();
                string respostausername = cmd.Parameters["@retorno_username"].Value.ToString();
                string respostaPhonenumber = cmd.Parameters["@retorno_phoneNumber"].Value.ToString();
                int resposta_nif = Convert.ToInt32(cmd.Parameters["@retorno_nif"].Value);
                int tfa2 = Convert.ToInt32(cmd.Parameters["@retorno_2fa"].Value);
                int respostaNewsletter = Convert.ToInt32(cmd.Parameters["@retorno_newsletter"].Value);

                mycon.Close();

                if (respostaNewsletter == 1)
                {
                    rbl_newsletter.SelectedValue = "Enable";
                }
                else if (respostaNewsletter == 0)
                {
                    rbl_newsletter.SelectedValue = "Disabled";
                }

                lbl_nameTop.Text = respostafirstName + " " + respostalastName + " Profile";

                tb_first_name.Text = respostafirstName;
                tb_last_name.Text = respostalastName;

                lbl_username.Text = respostausername;
                lbl_email.Text = Session["user_email"].ToString();

                tb_phoneNumber.Text = respostaPhonenumber;

                if (tfa2 == 0)
                {
                    btn_2fa.Text = "Setup";
                }
                else
                {
                    btn_2fa.Text = "Remove";
                    btn_2fa.ForeColor = Color.Red;
                }

            }
        }


        protected void btn_2fa_Click(object sender, EventArgs e)
        {
            /*if (tfa2 == false)
            {

            }
            else if (tfa2 == true)
            {

            }*/
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

        int auxNews;

        protected void btn_save_newsletter_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbl_newsletter.SelectedItem.Text == "Enable")
                {
                    auxNews = 1;
                }
                else if (rbl_newsletter.SelectedItem.Text == "Disabled")
                {
                    auxNews = 0;
                }

                // Update the newsletter preference
                UpdateNewsletterPreference(auxNews);

                if (auxNews == 1)
                {
                    lbl_news.Text = "You have enabled newsletter";

                }
                else if (auxNews == 0)
                {
                    lbl_news.Text = "You have disabled newsletter";
                }

                Response.Redirect("account.aspx");
            }
            catch (Exception ex)
            {
                lbl_news.Text = ex.ToString();
            }
        }




        private void UpdateNewsletterPreference(int newsletter)
        {
            try
            {
                // Perform the database update here based on the newsletter value
                MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.AddWithValue("@p_Userid", Convert.ToInt32(Session["UserId"]));
                cmd.Parameters.AddWithValue("@p_newsletter", Convert.ToInt32(newsletter));

                cmd.CommandText = "account_newsletter_update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = mycon;
                mycon.Open();
                cmd.ExecuteNonQuery();
                mycon.Close();
            }
            catch (Exception ex)
            {
                throw ex; // Handle the exception as needed.
            }
        }

    }
}