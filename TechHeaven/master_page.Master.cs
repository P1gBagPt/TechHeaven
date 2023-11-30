using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Configuration;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace TechHeaven
{
    public partial class master_page : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["userId"] != null)
                {
                    int id_user = Convert.ToInt32(Session["userId"].ToString());

                    // Chamando um método para obter a contagem de itens no carrinho
                    int quantidadeItensCarrinho = ObterQuantidadeItensCarrinho(id_user);
                    int quantidadeItensWishlist = ObterQuantidadeItensWishlist(id_user);


                    if (quantidadeItensCarrinho != 0)
                    {
                        lt_cart.Text = quantidadeItensCarrinho.ToString();
                    }
                    else
                    {
                        Panel2.Visible = false;
                    }

                    if (quantidadeItensWishlist != 0)
                    {
                        lt_wishlist.Text = quantidadeItensWishlist.ToString();
                    }
                    else
                    {
                        Panel1.Visible = false;
                    }
                }
                else
                {
                    lt_cart.Visible = false;
                    lt_wishlist.Visible = false;
                    Panel1.Visible = false;
                    Panel2.Visible = false;

                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(ex.Message);
            }
        }
        private int ObterQuantidadeItensCarrinho(int userId)
        {
            // Aqui você deve implementar a lógica para acessar o banco de dados e contar os itens do carrinho para o usuário
            // Substitua o exemplo abaixo com a consulta real ou a lógica que você está usando

            // Exemplo fictício usando um SqlConnection e SqlCommand
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM cart WHERE userID = @userId AND status = 1", connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    int quantidade = (int)command.ExecuteScalar();
                    return quantidade;
                }
            }
        }


        private int ObterQuantidadeItensWishlist(int userId)
        {
            // Aqui você deve implementar a lógica para acessar o banco de dados e contar os itens do carrinho para o usuário
            // Substitua o exemplo abaixo com a consulta real ou a lógica que você está usando

            // Exemplo fictício usando um SqlConnection e SqlCommand
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM wishlist WHERE userID = @userId AND status = 1", connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    int quantidade = (int)command.ExecuteScalar();
                    return quantidade;
                }
            }
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

        protected void lb_logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("main_page.aspx");
        }
    }
}