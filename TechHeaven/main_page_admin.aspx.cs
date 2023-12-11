using PdfSharp.Pdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace TechHeaven
{
    public partial class main_page_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExibirTotalVendas();
            ContarEncomendas();
            ContarClientes();
            ContarProdutos();

            ContarCategories();
            ContarBrands();
            ContarNews();
            ContarReviews();
        }



        private void ExibirTotalVendas()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o total de vendas na tabela de encomendas
                    string query = "SELECT SUM(total) AS TotalVendas FROM orders";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o total de vendas na página
                            h6_TotalVendas.InnerText = $"{result:N2} €";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há vendas
                            h6_TotalVendas.InnerText = "No orders.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_TotalVendas.InnerText = $"Erro ao obter: {ex.Message}";
            }
        }

        private void ContarEncomendas()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalEncomendas FROM orders";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_TotalEncomendas.InnerText = $"{result} vendas";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_TotalEncomendas.InnerText = "No profit.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_TotalEncomendas.InnerText = $"Erro ao obter: {ex.Message}";
            }
        }


        private void ContarClientes()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalClientes FROM users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_TotalClientes.InnerText = $"{result} clientes";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_TotalClientes.InnerText = "No Users.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_TotalClientes.InnerText = $"Erro ao obter: {ex.Message}";
            }
        }




        private void ContarProdutos()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalProducts FROM products WHERE status = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_totalProducts.InnerText = $"{result} products";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_totalProducts.InnerText = "No Products.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_totalProducts.InnerText = $"Erro ao obter : {ex.Message}";
            }
        }


        private void ContarCategories()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalCategories FROM categories";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_total_categories.InnerText = $"{result} categories";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_total_categories.InnerText = "No categories.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_totalProducts.InnerText = $"Erro ao obter : {ex.Message}";
            }
        }

        private void ContarBrands()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalBrands FROM brands";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_totalBrands.InnerText = $"{result} brands";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_totalBrands.InnerText = "No brands.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_totalBrands.InnerText = $"Erro ao obter : {ex.Message}";
            }
        }




        private void ContarNews()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalNews FROM newsletter";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_totalNewsletter.InnerText = $"{result} newsletter";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_totalNewsletter.InnerText = "No newsletter.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_totalNewsletter.InnerText = $"Erro ao obter : {ex.Message}";
            }
        }



        private void ContarReviews()
        {
            try
            {
                // Conectar ao banco de dados
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Consultar o número total de encomendas na tabela de encomendas
                    string query = "SELECT COUNT(*) AS TotalReviews FROM review_classification WHERE status = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Executar a consulta
                        object result = command.ExecuteScalar();

                        // Verificar se o resultado não é nulo
                        if (result != null)
                        {
                            // Exibir o número total de encomendas na página
                            h6_totalReviews.InnerText = $"{result} reviews";
                        }
                        else
                        {
                            // Se o resultado for nulo, exibir uma mensagem indicando que não há encomendas
                            h6_totalReviews.InnerText = "No reviews yet.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, por exemplo, registrar ou exibir uma mensagem de erro
                h6_totalReviews.InnerText = $"Erro ao obter : {ex.Message}";
            }
        }






        protected void lb_pdf_generate_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "GeneratePdf")
            {
                GeneratePdf();
            }
        }



        private void GeneratePdf()
        {
            string pdfFilePath = Server.MapPath("~/Reports/SummaryReport.pdf");

            using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create))
            {
                using (PdfWriter writer = new PdfWriter(fs))
                {
                    using (iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        // Adicionar informações ao PDF
                        AddInformationToPdf(document);

                        // Fechar o documento
                        document.Close();
                    }
                }
            }
        }

        private void AddInformationToPdf(Document document)
        {
            document.Add(new Paragraph("Summary Report - " + DateTime.Now.ToString("dd/MM/yyyy")));

            AddSectionToPdf(document, "Total Sales", h6_TotalVendas.InnerText);
            AddSectionToPdf(document, "Total Orders", h6_TotalEncomendas.InnerText);
            AddSectionToPdf(document, "Total Customers", h6_TotalClientes.InnerText);
            AddSectionToPdf(document, "Total Products", h6_totalProducts.InnerText);
            AddSectionToPdf(document, "Total Categories", h6_total_categories.InnerText);
            AddSectionToPdf(document, "Total Brands", h6_totalBrands.InnerText);
            AddSectionToPdf(document, "Total Newsletter", h6_totalNewsletter.InnerText);
        }



        private void AddSectionToPdf(Document document, string title, string value)
        {
            document.Add(new Paragraph(title).SetBold());
            document.Add(new Paragraph(value));
            document.Add(new Paragraph("\n"));
        }
    }
}