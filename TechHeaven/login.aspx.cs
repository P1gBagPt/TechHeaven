using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace TechHeaven
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string respostanome, respostaRole;
        public static int respostaTwoFactor, respostaverify, respostaId;

        protected void btn_login_Click(object sender, EventArgs e)
        {
            MySqlConnection mycon = Master.GetSetConn;

            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "login_user";

            cmd.Connection = mycon;

            /*cmd.Parameters.AddWithValue("@f_email", login_email.Text);
            cmd.Parameters.AddWithValue("@f_password", Master.EncryptString(login_password.Text));*/

            cmd.Parameters.AddWithValue("@f_email", "marcarpremio@gmail.com");
            cmd.Parameters.AddWithValue("@f_password", Master.EncryptString("!Password1"));

            MySqlParameter valor_retorno = new MySqlParameter();
            valor_retorno.ParameterName = "@retorno";
            valor_retorno.Direction = ParameterDirection.Output;
            valor_retorno.MySqlDbType = MySqlDbType.Int64;
            cmd.Parameters.Add(valor_retorno);

            MySqlParameter retorno_username = new MySqlParameter();
            retorno_username.ParameterName = "@retorno_username";
            retorno_username.Direction = ParameterDirection.Output;
            retorno_username.MySqlDbType = MySqlDbType.VarChar;
            retorno_username.Size = 10;
            cmd.Parameters.Add(retorno_username);


            MySqlParameter retorno_twofactor = new MySqlParameter();
            retorno_twofactor.ParameterName = "@retorno_twofactor";
            retorno_twofactor.Direction = ParameterDirection.Output;
            retorno_twofactor.MySqlDbType = MySqlDbType.Int64;
            cmd.Parameters.Add(retorno_twofactor);

            MySqlParameter retorno_verify = new MySqlParameter();
            retorno_verify.ParameterName = "@retorno_verify";
            retorno_verify.Direction = ParameterDirection.Output;
            retorno_verify.MySqlDbType = MySqlDbType.Int64;
            cmd.Parameters.Add(retorno_verify);

            MySqlParameter retorno_id = new MySqlParameter();
            retorno_id.ParameterName = "@retorno_id";
            retorno_id.Direction = ParameterDirection.Output;
            retorno_id.MySqlDbType = MySqlDbType.Int64;
            cmd.Parameters.Add(retorno_id);


            MySqlParameter retorno_role = new MySqlParameter();
            retorno_role.ParameterName = "@retorno_role";
            retorno_role.Direction = ParameterDirection.Output;
            retorno_role.MySqlDbType = MySqlDbType.VarChar;
            cmd.Parameters.Add(retorno_role);

           
            cmd.ExecuteNonQuery();

            int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            
            if(respostaSP == 0)
            {
                respostanome = cmd.Parameters["@retorno_username"].Value.ToString();
                respostaTwoFactor = Convert.ToInt32(cmd.Parameters["@retorno_twofactor"].Value);
                respostaverify = Convert.ToInt32(cmd.Parameters["@retorno_verify"].Value); ;
                respostaId = Convert.ToInt32(cmd.Parameters["@retorno_id"].Value);
                respostaRole = cmd.Parameters["@retorno_role"].Value.ToString();
            }
           

            mycon.Close();

            string respostaemail = login_email.Text;

            if (respostaSP == 0)
            {
                if (respostaverify == 0)
                {
                    lbl_erro.Text = "Account not activated";
                    lbl_erro_enviar.Text = "To activate the account click here";
                }
                else
                {
                    lbl_erro.Text = "Welcome";

                    Session["isLogged"] = "yes";
                    Session["userId"] = respostaId;
                    Session["user_username"] = respostanome;
                    Session["user_email"] = respostaemail;
                    Session["twoFactor"] = respostaTwoFactor;
                    Session["role"] = respostaRole;

                    Response.Redirect("main_page.aspx");
                }
            }
            else
            {
                lbl_erro.Text = "Email or Password Wrong!";
            }
        }

        protected void lbl_erro_enviar_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                mail.To.Add(new MailAddress(login_email.Text));
                mail.Subject = "Account Confirmation";

                mail.IsBodyHtml = true;
                mail.Body = "Your Registered TecHeaven, click <a href='https://localhost:44331/activation.aspx?m_uti=" + Master.EncryptString(login_email.Text) + "'>here</a> to confirm your account.";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                servidor.EnableSsl = true;

                servidor.Send(mail);
                lbl_erro_enviar.Enabled = false;
                lbl_erro_enviar.Visible = false;

                lbl_erro.Text = "Email sent successfully. Verify your email inbox";
                lbl_erro.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
                lbl_erro.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}