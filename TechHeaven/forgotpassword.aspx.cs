using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Drawing;

namespace TechHeaven
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        public static string userEmail;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_forgot_password_Click(object sender, EventArgs e)
        {
            // Get the user's email from the textbox
            userEmail = tb_email.Text;

            // Perform a database query to check if the email exists
            if (IsEmailExists(userEmail))
            {
                // Email exists, send password reset instructions


                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                mail.To.Add(new MailAddress(userEmail));
                mail.Subject = "Reset password";

                mail.IsBodyHtml = true;
                mail.Body = "Reset password, click <a href='https://localhost:44331/inputnewpass.aspx?m_email=" + EncryptString(userEmail) + "'>here</a> to reset your password.";


                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                servidor.EnableSsl = true;

                servidor.Send(mail);


                // Provide feedback to the user
                lbl_erro.Text = "Password reset instructions have been sent to your email.";
                lbl_erro.ForeColor = Color.Green;
                lbl_erro.Visible = true;
            }
            else
            {
                // Email does not exist, show an error message
                lbl_erro.Text = "Email not found. Please enter a valid email address.";
                lbl_erro.ForeColor = Color.Red;
                lbl_erro.Visible = true;
            }
        }

        private bool IsEmailExists(string email)
        {
            bool emailExists = false;

            // Use appropriate connection string for your database
            string connectionString = WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use a parameterized query to prevent SQL injection
                string query = "SELECT COUNT(*) FROM users WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    // ExecuteScalar returns the number of rows that match the query
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // If count is greater than 0, the email exists in the database
                    emailExists = count > 0;
                }
            }

            return emailExists;
        }

        public string EncryptString(string Message)
        {
            string Passphrase = "@Tec!?T3ChHe@v3NP@ssR3s3t";
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

        
    }
}