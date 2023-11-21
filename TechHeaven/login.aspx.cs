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
using System.Data.SqlClient;

namespace TechHeaven
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string respostanome;
        public static int respostaId, respostaRole;
        public bool respostaTwoFactor, respostaverify;
        protected void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "user_login";  // Corrected the stored procedure name

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@email", login_email.Text);
            myCommand.Parameters.AddWithValue("@password", Master.EncryptString(login_password.Text));

            SqlParameter retorno_username = new SqlParameter();
            retorno_username.ParameterName = "@return_username";
            retorno_username.Direction = ParameterDirection.Output;
            retorno_username.SqlDbType = SqlDbType.VarChar;
            retorno_username.Size = 50;
            myCommand.Parameters.Add(retorno_username);

            SqlParameter retorno_twofactor = new SqlParameter();
            retorno_twofactor.ParameterName = "@return_twofactor";
            retorno_twofactor.Direction = ParameterDirection.Output;
            retorno_twofactor.SqlDbType = SqlDbType.Bit;
            myCommand.Parameters.Add(retorno_twofactor);

            SqlParameter retorno_verify = new SqlParameter();
            retorno_verify.ParameterName = "@return_verify";
            retorno_verify.Direction = ParameterDirection.Output;
            retorno_verify.SqlDbType = SqlDbType.Bit;
            myCommand.Parameters.Add(retorno_verify);

            SqlParameter retorno_id = new SqlParameter();
            retorno_id.ParameterName = "@return_id";
            retorno_id.Direction = ParameterDirection.Output;
            retorno_id.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(retorno_id);

            SqlParameter retorno_role = new SqlParameter();
            retorno_role.ParameterName = "@return_role";
            retorno_role.Direction = ParameterDirection.Output;
            retorno_role.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(retorno_role);

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@return";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);

            myConn.Open();
            myCommand.ExecuteNonQuery();

            int respostaSP = Convert.ToInt32(myCommand.Parameters["@return"].Value);
            
            if(respostaSP == 0)
            {
                respostanome = myCommand.Parameters["@return_username"].Value.ToString();
                object twofactorValue = myCommand.Parameters["@return_twofactor"].Value;
                respostaTwoFactor = twofactorValue != DBNull.Value && Convert.ToBoolean(twofactorValue);
                object verifyValue = myCommand.Parameters["@return_verify"].Value;
                respostaverify = verifyValue != DBNull.Value && Convert.ToBoolean(verifyValue);
                respostaId = Convert.ToInt32(myCommand.Parameters["@return_id"].Value);
                respostaRole = Convert.ToInt32(myCommand.Parameters["@return_role"].Value);
            }
           

            myConn.Close();

            string respostaemail = login_email.Text;

            if (respostaSP == 0)
            {
                if (respostaverify == false)
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
                Session["activation"] = true;

            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
                lbl_erro.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}