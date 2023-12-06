using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class _2tfa_auth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the encrypted email from the query parameter
                string encryptedEmail = Request.QueryString["encryptedEmail"];

                if (string.IsNullOrEmpty(encryptedEmail))
                {
                    // If encryptedEmail is null or empty, redirect to the login page
                    Response.Redirect("login.aspx");
                }
                else
                {
                    string decryptedEmail = DecryptString(encryptedEmail);

                }

                
            }
        }




        protected void btn_login_Click(object sender, EventArgs e)
        {



            string sessionCode = Session["2FACode"] as string;

            if (sessionCode != null && tb_code.Text == sessionCode)
            {

               

                Session["isLogged"] = Session["tempisLogged"];
                Session["userId"] = Session["tempuserId"];
                Session["user_username"] = Session["tempuser_username"];
                Session["user_email"] = Session["tempuser_email"];
                Session["twoFactor"] = Session["temptwoFactor"];
                Session["role"] = Session["temprole"];

                Response.Redirect("main_page.aspx");
            }
            else
            {
                lbl_erro.Text = "Code is wrong";

            }
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

        protected void lbl_erro_enviar_Click(object sender, EventArgs e)
        {

        }
    }
}