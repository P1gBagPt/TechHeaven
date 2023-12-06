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
using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;

namespace TechHeaven
{
    public partial class login : System.Web.UI.Page
    {
        public class GoogleProfile
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; }
            public string Email { get; set; }
            public string Verified_Email { get; set; }
        }

        public class FaceBookUser
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string UserName { get; set; }
            public string PictureUrl { get; set; }
            public string Email { get; set; }
        }

        public static string controlo = "";
        public static string socialType = "";
        public static string pw_user = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GoogleConnect.ClientId = "972730968305-o0llll7q9ot2i4ohrgpd382l6bc89v5k.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

            FaceBookConnect.API_Key = "654505023471114";
            FaceBookConnect.API_Secret = "c96f56d41be07b887d8495d4bca4a8a7";

            if (!this.IsPostBack)
            {
                socialType = Session["social"] as string;

                if (socialType == "google")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                    {
                        string code = Request.QueryString["code"];
                        string json = GoogleConnect.Fetch("me", code);
                        GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                        Session["user_email"] = profile.Email;
                        pw_user = profile.Id;
                        Session["controlo"] = "1";
                    }
                    if (Request.QueryString["error"] == "access_denied")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                    }
                }
                else if (socialType == "facebook")
                {
                    string code = Request.QueryString["code"];
                    if (!string.IsNullOrEmpty(code))
                    {
                        string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                        FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                        Session["user_email"] = faceBookUser.Email;
                        pw_user = faceBookUser.Id;
                        Session["controlo"] = "1";
                    }

                    if (Request.QueryString["error"] == "access_denied")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                        return;
                    }
                }


                if (Session["controlo"] as string == "1")
                {
                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "user_login";  // Corrected the stored procedure name

                    myCommand.Connection = myConn;

                    myCommand.Parameters.AddWithValue("@email", Session["user_email"].ToString());
                    myCommand.Parameters.AddWithValue("@password", Master.EncryptString(pw_user));

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

                    if (respostaSP == 0)
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

                    string respostaemail = Session["user_email"].ToString();

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
                        lbl_social.Enabled = true;
                        lbl_social.Visible = true;
                        lbl_social.ForeColor = System.Drawing.Color.Red;
                        lbl_social.Text = "This account doenst exist, register!";
                        lbl_social.Attributes.Add("style", "font-size: 30px;");

                        Session["user_email"] = null;
                        controlo = "0";
                    }

                }
            }
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
                Console.WriteLine(respostaTwoFactor);
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

                    if(respostaTwoFactor == true)
                    {
                        lbl_erro.Text = "Welcome";

                        Session["tempisLogged"] = "yes";
                        Session["tempuserId"] = respostaId;
                        Session["tempuser_username"] = respostanome;
                        Session["tempuser_email"] = respostaemail;
                        Session["temptwoFactor"] = respostaTwoFactor;
                        Session["temprole"] = respostaRole;

                        try
                        {
                            Random rand = new Random();
                            string code = rand.Next(100000, 999999).ToString();

                            // Store the code in the session for verification later
                            Session["2FACode"] = code;

                            MailMessage mail = new MailMessage();
                            SmtpClient servidor = new SmtpClient();

                            mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                            mail.To.Add(new MailAddress(Session["tempuser_email"].ToString()));
                            mail.Subject = "2TFA Code";

                            mail.IsBodyHtml = true;
                            mail.Body = "Your 2FA code is: " + code;


                            servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                            servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                            string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                            string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                            servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                            servidor.EnableSsl = true;

                            servidor.Send(mail);
                            //lbl_mensagem.Text = "";
                            //lbl_mensagem.Enabled = false;
                            //lbl_mensagem.Visible = false;
                            lbl_erro.Text = "Code sent sucessfully. Verify your mail invoice";
                            lbl_erro.ForeColor = System.Drawing.Color.Green;
                            Session["activation"] = true;

                            Response.Redirect($"2tfa_auth.aspx?encryptedEmail={Server.UrlEncode(Master.EncryptString(respostaemail))}");



                        }
                        catch (Exception ex)
                        {
                            lbl_erro.Text = ex.Message;
                        }
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
                    }

                    

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


        protected void btn_googleLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session["social"] = "google";
                GoogleConnect.Authorize("profile", "email");
            }
            catch (Exception ex)
            {
                // Log or display the error for debugging.
                // For example, you can use a label or log it to a file.
                lbl_erro.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void btn_facebookLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session["social"] = "facebook";
                //GoogleConnect.Authorize("profile", "email");
                FaceBookConnect.Authorize("email", Request.Url.AbsoluteUri.Split('?')[0]);
            }
            catch (Exception ex)
            {
                // Log or display the error for debugging.
                // For example, you can use a label or log it to a file.
                lbl_erro.Text = "An error occurred: " + ex.Message;
            }
        }

    }
}