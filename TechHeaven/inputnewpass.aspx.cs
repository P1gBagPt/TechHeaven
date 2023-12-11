using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace TechHeaven
{
    public partial class inputnewpass : System.Web.UI.Page
    {
        public static string encryptedPassword;
        public static int pass_forte;
        public static string emailURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["m_email"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                emailURL = DecryptString(Request.QueryString["m_email"]);

            }
        }

        protected void SubmitNewPass_Click(object sender, EventArgs e)
        {
            if(pass_forte == 1)
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
                if (register_password.Text == register_password_agn.Text)
                {
                    encryptedPassword = EncryptString(register_password.Text);

                    // Chame a stored procedure para atualizar a senha no banco de dados
                    UpdateUserPassword(emailURL, encryptedPassword);

                    lbl_erro.Text = "Password updated successfully!";
                    lbl_erro.ForeColor = System.Drawing.Color.Green;

                    Response.Redirect("login.aspx");
                }
                else
                {
                    lbl_erro.Text = "The Passwords don't match!";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }

        }


        private void UpdateUserPassword(string email, string encryptedPassword)
        {
            // Use apropriado string de conexão para o seu banco de dados
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use a stored procedure para atualizar a senha
                using (SqlCommand command = new SqlCommand("update_user_password", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Adicione os parâmetros necessários
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@EncryptedPassword", encryptedPassword);

                    // Execute a stored procedure
                    command.ExecuteNonQuery();
                }
            }
        }
        public string DecryptString(string Message)
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