using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Net;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace TechHeaven
{
    public partial class billing : System.Web.UI.Page
    {

        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 12;
        private DataTable _dtOriginal;

        public static int id_user;
        public static decimal total = 0;
        public static string query = "";
        public static bool proceed = false;


        public static int encomenda_id = 0;
        public static string email_user = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                id_user = Convert.ToInt32(Session["UserId"].ToString());
                total = 0;

                try
                {
                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "cart_total";

                    cmd.Connection = myConn;

                    cmd.Parameters.AddWithValue("@userId", id_user);

                    SqlParameter totalRetorno = new SqlParameter("@total", SqlDbType.Float);
                    totalRetorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(totalRetorno);

                    myConn.Open();
                    cmd.ExecuteNonQuery();
                    myConn.Close();
                    decimal totali = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                    if (Session["shipping"] != null)
                    {
                        string shippingValue = Session["shipping"].ToString();

                        if (shippingValue == "0")
                        {
                            lbShipping.Text = "Free";
                        }
                        else if (shippingValue == "10") // Ajuste esses valores com base nos seus custos de envio reais
                        {
                            totali += 10;
                            lbShipping.Text = "Standard";
                        }
                        else if (shippingValue == "20")
                        {
                            totali += 20;
                            lbShipping.Text = "Express";
                        }
                    }
                    else
                    {
                        // Tratar o caso em que a chave "shipping" não existe na sessão
                        // Por exemplo, você pode definir um valor padrão ou gerar um erro adequado.
                    }



                    ltTotal.Text = totali.ToString();

                    query = "SELECT c.id_cart, c.quantity, p.*, p.quantity as quantidade " +
                    "FROM cart c " +
                    "INNER JOIN products p ON c.productID = p.id_products " +
                    "WHERE c.userID = " + id_user + " AND c.status = 1";

                    BindDataIntoRepeater(query);
                    //SqlDataSource2.SelectParameters["user_id"].DefaultValue = Session["UserId"].ToString();
                    // Dentro do método Page_Load



                    try
                    {
                        SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                        SqlCommand cmd2 = new SqlCommand();

                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.CommandText = "total_addresses";

                        cmd2.Connection = myConn2;

                        cmd2.Parameters.AddWithValue("@userID", id_user);

                        SqlParameter totalRetornoAddresses = new SqlParameter("@total", SqlDbType.Int);
                        totalRetornoAddresses.Direction = ParameterDirection.Output;
                        cmd2.Parameters.Add(totalRetornoAddresses);

                        myConn2.Open();
                        cmd2.ExecuteNonQuery();
                        myConn2.Close();

                        int respostaSP = Convert.ToInt32(cmd2.Parameters["@total"].Value);

                        Console.WriteLine(respostaSP);
                        if (respostaSP == 4)
                        {
                            HyperLink1.Enabled = false;
                            HyperLink1.Visible = false;
                        }
                        else if (respostaSP == 0)
                        {
                            ddl_address.Enabled = false;
                            ddl_address.Visible = false;
                            proceed = false;

                        }
                        else
                        {
                            // Configurar o SqlDataSource2
                            SqlDataSource2.SelectParameters["user_id"].DefaultValue = Session["UserId"].ToString();
                            ddl_address.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception
                    }

                }
                catch (Exception ex)
                {

                }

            }

        }


        static DataTable GetDataFromDb(string query)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString());

            var da = new SqlDataAdapter(query, con);
            var dt = new DataTable();

            try
            {
                con.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_address.Text == "")
            {
                proceed = false;
            }
            else
            {
                proceed = true;
            }



            if (proceed == true)
            {

                //ORDER INFO
                int selectedValue = Convert.ToInt32(ddl_address.SelectedValue);

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "order_proceed";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@userId", id_user);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@data_encomenda", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@pagamento", 1);
                cmd.Parameters.AddWithValue("@addressID", selectedValue);



                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno);



                myConn.Open();
                cmd.ExecuteNonQuery();
                encomenda_id = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                Session["EncomendaID"] = encomenda_id;
                myConn.Close();

                try
                {
                    //BILLINGS
                    SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);


                    SqlCommand cmd2 = new SqlCommand();

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "billing_order";

                    cmd2.Connection = myConn2;

                    cmd2.Parameters.AddWithValue("@userId", id_user);
                    cmd2.Parameters.AddWithValue("@addressID", selectedValue);
                    cmd2.Parameters.AddWithValue("@encomenda_id", encomenda_id);
                    cmd2.Parameters.AddWithValue("@nome", tb_firstname.Text);
                    cmd2.Parameters.AddWithValue("@alcunha", tb_lastname.Text);
                    cmd2.Parameters.AddWithValue("@rua", tb_address.Text);
                    cmd2.Parameters.AddWithValue("@apartamento", tb_address_floor.Text);
                    cmd2.Parameters.AddWithValue("@codigoPostal", tb_zipcode.Text);
                    cmd2.Parameters.AddWithValue("@pais", tb_state.Text);
                    cmd2.Parameters.AddWithValue("@cidade", tb_city.Text);
                    cmd2.Parameters.AddWithValue("@telemovel", tb_phonenumber.Text);


                    SqlParameter retorno2 = new SqlParameter("@retorno", SqlDbType.Int);
                    retorno2.Direction = ParameterDirection.Output;
                    cmd2.Parameters.Add(retorno2);



                    myConn2.Open();
                    cmd2.ExecuteNonQuery();

                    myConn2.Close();





                }
                catch (Exception ex)
                {
                    lbShipping.Text = ex.Message;

                }
                try
                {



                    string html = "<h1 style=\"font-family: Arial, sans-serif;\">Techeaven</h1><br/>" +
                    "<h2>Order Number " + encomenda_id + "</h2><br/><br/>" +
                    "<h3>Personal Information</h3><br/>" +
                    string.Format("<p>First Name: <b>{0}</b> | Last Name: <b>{1}</b></p><br/>", tb_firstname.Text, tb_lastname.Text) +
                    string.Format("<p>Phone Number: <b>{0}</b></p><br/><br/>", tb_phonenumber.Text) +
                    "<h3>Shipping Details</h3><br/>" +
                    string.Format("<p>Address: <b>{0}</b> | Floor: <b>{1}</b></p><br/>", tb_address.Text, tb_address_floor.Text) +
                    string.Format("<p>Country: <b>{0}</b> | City: <b>{1}</b></p><br/>", tb_state.Text, tb_city.Text) +
                    string.Format("<p>Zipcode: <b>{0}</b></p><br/><br/>", tb_zipcode.Text) +
                    "<h3>Products purchased</h3><br/>";

                    List<ProdutoCarrinho> produtosNoCarrinho = ObterProdutosDoCarrinho(encomenda_id, id_user);

                    foreach (var produto in produtosNoCarrinho)
                    {
                        html += $"<img src=\"data:{produto.ContentTypeImagem};base64,{Convert.ToBase64String(produto.Imagem)}\" style=\"max-width: 100px; margin-right: 10px; border: 1px solid black;\" />";


                        html += string.Format("<p>Product Name: <b>{0}</b> | Brand: <b>{1}</b> | Product Code: <b>{2}</b> <br/> Stock: <b>{3}</b> | Total: <b>€{4:F2}</b></p>",
                            produto.NomeProduto, produto.Marca, produto.NumeroArtigo, produto.Quantidade, produto.PrecoTotal);

                        html += "<hr style=\"border: 1px solid #ddd;\"/>";

                    }

                    html += string.Format("<h3>Total order: €{0:F2}</h3><p> Payment Method: <b>{1}</b></p>", total, 1);

                    GerarPdfEnviarEmail(html, encomenda_id, email_user);




                }
                catch (Exception ex)
                {
                    lbShipping.Text = ex.Message;

                }



            }

        }

        public List<ProdutoCarrinho> ObterProdutosDoCarrinho(int encomenda_id, int id_user)
        {
            List<ProdutoCarrinho> produtos = new List<ProdutoCarrinho>();

            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
    SELECT
    p.name AS NomeProduto,
    p.price AS PrecoArtigo,
    c.quantity AS Quantidade,
    m.brand_name AS Marca,
    p.product_code AS NumeroArtigo,
    c.quantity * p.price AS PrecoTotal,
    p.image AS ImagemProduto,
    p.contenttype AS ContentTypeImagem
FROM cart c
INNER JOIN products p ON c.productID = p.id_products
INNER JOIN brands m ON p.brand = m.id_brand
WHERE c.userID = @id_user
    AND c.orderID = @encomenda_id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@encomenda_id", encomenda_id);
                    cmd.Parameters.AddWithValue("@id_user", id_user);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProdutoCarrinho produto = new ProdutoCarrinho
                            {
                                NomeProduto = reader["NomeProduto"].ToString(),
                                PrecoArtigo = Convert.ToDecimal(reader["PrecoArtigo"]),
                                Quantidade = Convert.ToInt32(reader["Quantidade"]),
                                Marca = reader["Marca"].ToString(),
                                NumeroArtigo = reader["NumeroArtigo"].ToString(),
                                PrecoTotal = Convert.ToDecimal(reader["PrecoTotal"]),
                                Imagem = reader["ImagemProduto"] as byte[],
                                ContentTypeImagem = reader["ContentTypeImagem"].ToString()
                            };
                            produtos.Add(produto);
                        }
                    }
                }

                con.Close();
            }

            return produtos;
        }

        public class ProdutoCarrinho
        {
            public string NomeProduto { get; set; }
            public decimal PrecoArtigo { get; set; }
            public int Quantidade { get; set; }
            public string Marca { get; set; }
            public string NumeroArtigo { get; set; }
            public decimal PrecoTotal { get; set; }
            public byte[] Imagem { get; set; }
            public string ContentTypeImagem { get; set; }
        }


        private void BindDataIntoRepeater(string query)
        {
            total = 0;
            var dt = GetDataFromDb(query);
            _pgsource.DataSource = dt.DefaultView;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = _pageSize;
            //_pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Example: "Page 1 of 10"
            //lblpage.Text = "Página " + (CurrentPage + 1) + " de " + _pgsource.PageCount;
            // Enable First, Last, Previous, Next buttons
            /*lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;*/
            /*lbFirst.Enabled = !_pgsource.IsFirstPage;
            lbLast.Enabled = !_pgsource.IsLastPage;*/

            // Bind data into repeater
            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            // Call the function to do paging
            //HandlePaging();
        }



        public void GerarPdfEnviarEmail(string html, int encomenda_id, string email_user)
        {
            try
            {
                // Gere o PDF
                var pdfDocument = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                string caminhoDaPastaPDFs = Server.MapPath("~/PDFS");
                string nomeDoArquivoPDF = encomenda_id + ".pdf"; // Use o número da encomenda como nome do arquivo
                string caminhoDoPDF = Path.Combine(caminhoDaPastaPDFs, nomeDoArquivoPDF);
                pdfDocument.Save(caminhoDoPDF);

                // Envie o PDF por email
                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                mail.To.Add(new MailAddress(email_user));
                mail.Subject = "Order Number " + encomenda_id;
                mail.IsBodyHtml = true;

                // Anexe o PDF ao email
                MemoryStream pdfStream = new MemoryStream(File.ReadAllBytes(caminhoDoPDF));
                Attachment anexo = new Attachment(pdfStream, nomeDoArquivoPDF, "application/pdf");
                mail.Attachments.Add(anexo);

                // Corpo do email
                mail.Body = "<h1>Techeaven</h1><br/>" +
                    "<h2>Order number " + encomenda_id + "</h2>";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                servidor.EnableSsl = true;

                servidor.Send(mail);

                // Redirect after sending the email
                Session["EncomendaID"] = encomenda_id;
                Server.Transfer("donecheck.aspx");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                lbShipping.Text = ex.Message;
            }
        }




    }
}