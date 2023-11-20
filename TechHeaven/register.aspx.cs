using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace TechHeaven
{
    public partial class register : System.Web.UI.Page
    {

        public static string encryptedPassword;
        public static int pass_forte;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string EncryptString(string Message)
        {
            string Passphrase = "@Tec!?T3ChHe@v3N";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();



            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below



            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));



            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();



            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;



            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);



            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }



            // Step 6. Return the encrypted string as a base64 encoded string



            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }

        public string DecryptString(string Message)
        {
            string Passphrase = "@Tec!?T3ChHe@v3N";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();



            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below



            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));



            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();



            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;



            // Step 4. Convert the input string to a byte[]



            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");




            byte[] DataToDecrypt = Convert.FromBase64String(Message);



            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }



            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

        protected void SubmitRegister_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

            if (pass_forte == 1)
            {
                if (register_password.Text == register_password_agn.Text)
                {
                    encryptedPassword = EncryptString(register_password.Text);
                    pass_forte = 2;
                }
                else
                {
                    lbl_erro.Text = "The Passwords don't match!";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (pass_forte == 2)
            {
                try
                {
                    if (myConn != null)
                    {
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.CommandText = "user_register";

                        myCommand.Connection = myConn;

                        myCommand.Parameters.AddWithValue("@email", register_email.Text);
                        myCommand.Parameters.AddWithValue("@firstName", register_firstName.Text);
                        myCommand.Parameters.AddWithValue("@lastName", register_lastName.Text);
                        myCommand.Parameters.AddWithValue("@username", register_username.Text);
                        myCommand.Parameters.AddWithValue("@password", encryptedPassword);

                        SqlParameter valor = new SqlParameter();
                        valor.ParameterName = "@return";
                        valor.Direction = ParameterDirection.Output;
                        valor.SqlDbType = SqlDbType.Int;
                        myCommand.Parameters.Add(valor);

                        myConn.Open();
                        myCommand.ExecuteNonQuery();
                        myConn.Close();

                       
                        int respostaSP = Convert.ToInt32(myCommand.Parameters["@return"].Value);

                        if (respostaSP == 0)
                        {
                            lbl_erro.Text = "This email is already being used, try another one";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (respostaSP == 1)
                        {
                            lbl_erro.Text = "This username has been taken use another one";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lbl_erro.Text = "User registred with sucess";
                            lbl_erro.ForeColor = System.Drawing.Color.Green;
                            try
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient servidor = new SmtpClient();

                                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                                mail.To.Add(new MailAddress(register_email.Text));
                                mail.Subject = "Account Confirmation";

                                mail.IsBodyHtml = true;
                                mail.Body = "Your Registed TecHeaven, click <a href='https://localhost:44331/activation.aspx?m_uti=" + EncryptString(register_email.Text) + "'>here</a> to confirm your account.";


                                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                                servidor.EnableSsl = true;

                                servidor.Send(mail);
                                lbl_mensagem.Text = "";
                                lbl_mensagem.Enabled = false;
                                lbl_mensagem.Visible = false;
                                lbl_erro.Text = "Email sent sucessfully. Verify your mail invoice";
                                lbl_erro.ForeColor = System.Drawing.Color.Green;
                                Session["activation"] = true;
                            }
                            catch (Exception ex)
                            {
                                lbl_erro.Text = ex.Message;
                            }
                            register_email.Text = "";
                            register_firstName.Text = "";
                            register_lastName.Text = "";
                            register_username.Text = "";
                            register_password.Text = "";
                            register_password_agn.Text = "";
                        }
                    }
                    else
                    {
                        lbl_erro.Text = "User not registred";
                    }
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.ToString();
                }
            }

        }


        protected void register_password_TextChanged(object sender, EventArgs e)
        {
            Regex maiusculas = new Regex("[A-Z]");
            Regex minusculas = new Regex("[a-z]");
            Regex numeros = new Regex("[0-9]");
            Regex especiais = new Regex("[^A-Z-a-z-0-9]");
            Regex plica = new Regex("'");
            string tipo_pw = "strong";
            if (register_password.Text.Length < 6)
            {
                tipo_pw = "Weak Password";
            }

            if (maiusculas.Matches(register_password.Text).Count < 1)
            {
                tipo_pw = "Weak Password";
            }

            if (minusculas.Matches(register_password.Text).Count < 1)
            {
                tipo_pw = "Weak Password";
            }

            if (numeros.Matches(register_password.Text).Count < 1)
            {
                tipo_pw = "Weak Password";
            }

            if (especiais.Matches(register_password.Text).Count < 1)
            {
                tipo_pw = "Weak Password";
            }

            if (plica.Matches(register_password.Text).Count > 0)
            {
                tipo_pw = "Weak Password";
            }

            if (tipo_pw == "Weak Password")
            {
                lbl_mensagem.Text = "Weak Password (Your password must have 6 characters containing at least 1 capital letter, 1 special character and 1 number)";
                pass_forte = 0;
            }
            else
            {
                //lbl_mensagem.Text = "Forte";
                pass_forte = 1;
            }
        }



    }
}
