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



            if(proceed == true)
            {
                try
                {

                }catch (Exception ex)
                {

                }
            }


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
            mail.Subject = "Encomenda numero " + encomenda_id;
            mail.IsBodyHtml = true;

            // Anexe o PDF ao email
            MemoryStream pdfStream = new MemoryStream(File.ReadAllBytes(caminhoDoPDF));
            Attachment anexo = new Attachment(pdfStream, nomeDoArquivoPDF, "application/pdf");
            mail.Attachments.Add(anexo);

            // Corpo do email
            mail.Body = "<h1>Autoparts</h1><br/>" +
                "<h2>Encomenda Numero " + encomenda_id + "</h2>";

            servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
            servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

            string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

            servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
            servidor.EnableSsl = true;

            servidor.Send(mail);
            Session["EncomendaID"] = encomenda_id;
            Response.Redirect("donecheck.aspx");
        }



    }
}