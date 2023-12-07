using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TechHeaven.productpage;

namespace TechHeaven
{
    public partial class bo_add_promotion : System.Web.UI.Page
    {
        public static int productId;
        public static decimal CurrentPrice;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["productId"] != null)
                {
                    productId = Convert.ToInt32(Request.QueryString["productId"]);

                    Product product = GetProductDetails(productId);

                    if (product != null)
                    {
                        // Display product information on the page
                        DisplayProductInfo(product);
                    }
                    else
                    {
                        lbl_erro.Text = "Product not found";
                    }

                }
                else
                {
                    Response.Redirect("bo_products.aspx");
                }
            }
        }


        private Product GetProductDetails(int productId)
        {
            // Implement logic to retrieve product details from the database
            // Replace the connection string with your actual database connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM products WHERE id_products = @productId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Create a Product object and populate its properties
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(reader["id_products"]),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Category = Convert.ToInt32(reader["category"]),
                                Brand = Convert.ToInt32(reader["brand"]),
                                Image = (byte[])reader["image"],
                                ContentType = reader["contenttype"].ToString(),
                                // Add other properties as needed
                            };

                           

                            return product;
                        }
                    }
                }
            }

            return null;
        }

        private string GetCategoryName(int categoryId)
        {
            // Implement logic to retrieve category name from the database
            // You may need to replace the connection string and query with your actual values
            string connectionString = WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT category_name FROM categories WHERE id_category = @categoryId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }

            return string.Empty;
        }

        private string GetBrandName(int brandId)
        {
            // Implement logic to retrieve brand name from the database
            // You may need to replace the connection string and query with your actual values
            string connectionString = WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT brand_name FROM brands WHERE id_brand = @brandId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@brandId", brandId);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }

            return string.Empty;
        }

        private void DisplayProductInfo(Product product)
        {
            lb_current_price.Text = $"{product.Price:C}";

            // Get brand and category names
            string brandName = GetBrandName(product.Brand);
            string categoryName = GetCategoryName(product.Category);

            // Display extended product information on the page
            lbl_product_info.Text = $"Product Name: {product.Name}<br/>" +
                $"Description: {product.Description}<br/>" +
                $"Price: {product.Price:C}<br/>" +
                $"Quantity: {product.Quantity}<br/>" +
                $"Category: {categoryName}<br/>" + // Display category name
                $"Brand: {brandName}<br/>"; // Display brand name

            img_product.ImageUrl = $"data:{product.ContentType};base64,{Convert.ToBase64String(product.Image)}";

            CurrentPrice = product.Price;
        }




        public static decimal promotionPercentage;
        protected void lb_preview_Command(object sender, CommandEventArgs e)
        {
            if (tb_numero_artigo.Text == "")
            {
                promotionPercentage = 1;
            }
            else
            {
                promotionPercentage = decimal.Parse(tb_numero_artigo.Text);

            }
            // Get the promotion percentage from the textbox

            // Calculate the new price
            decimal newPrice = CurrentPrice - (CurrentPrice * (promotionPercentage / 100));

            // Display the new price in the lb_new_price label
            lb_new_price.Text = newPrice.ToString("F2");

            Panel1.Visible = true;

        }





        protected void btn_promocao_Click(object sender, EventArgs e)
        {


            DateTime startDate = DateTime.Parse(tb_start.Text);
            DateTime endDate = DateTime.Parse(tb_end.Text);
            decimal discountPercent = promotionPercentage;

            // Chamar a stored procedure para inserir a promoção no banco de dados
            InsertPromotion(productId, startDate, endDate, discountPercent);


        }



        private void InsertPromotion(int productID, DateTime startDate, DateTime endDate, decimal discountPercent)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("add_promotion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Adicionar parâmetros à stored procedure
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@start_date", startDate);
                    command.Parameters.AddWithValue("@end_date", endDate);
                    command.Parameters.AddWithValue("@discount_percent", discountPercent);

                    // Executar a stored procedure
                    command.ExecuteNonQuery();
                }
            }
        }










    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Category { get; set; }
        public int Brand { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }


        // Add other properties as needed
    }
}