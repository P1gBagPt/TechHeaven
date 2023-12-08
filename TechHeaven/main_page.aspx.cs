using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class main_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

                string query2 = "SELECT TOP 6 id_category, category_name FROM categories";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query2, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Repeater1.DataSource = reader;
                                Repeater1.DataBind();
                            }
                            else
                            {
                                // Handle the case when no rows are returned (e.g., display a message).
                            }
                        }
                    }
                    con.Close();
                }


                string query3 = "SELECT TOP 6 id_brand, brand_name FROM brands";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query3, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Repeater3.DataSource = reader;
                                Repeater3.DataBind();
                            }
                            else
                            {
                                // Handle the case when no rows are returned (e.g., display a message).
                            }
                        }
                    }
                    con.Close();
                }


                string query1 = "SELECT TOP 6 " +
     "p.id_products, " +
     "p.name, " +
     "p.description, " +
     "p.quantity, " +
     "c.category_name as category, " +
     "p.brand, " +
     "p.status, " +
     "p.product_code AS codigoArtigo, " +
     "p.price, " +
     "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, " +
     "p.image, " +
     "p.contenttype, " +
     "p.creation_date " +
     "FROM products p " +
     "LEFT JOIN categories c ON p.category = c.id_category " +
     "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
     "WHERE p.status = 'true' AND p.quantity > 0 " +
     "ORDER BY p.creation_date DESC;";  // Order by creation_date in descending order



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query1, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Repeater2.DataSource = reader;
                                Repeater2.DataBind();
                            }
                            else
                            {
                                // Handle the case when no rows are returned (e.g., display a message).
                            }
                        }
                    }
                    con.Close();
                }


                string query = "SELECT TOP 8 p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
     "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
     "FROM products p " +
     "LEFT JOIN categories c ON p.category = c.id_category " +
     "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
     "WHERE p.status = 'true' AND p.quantity > 0 " +
     "ORDER BY NEWID();";



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                Repeater4.DataSource = reader;
                                Repeater4.DataBind();
                            }
                            else
                            {
                                // Handle the case when no rows are returned (e.g., display a message).
                            }
                        }
                    }
                    con.Close();
                }




            }
            catch (Exception ex)
            {

            }
        }

        protected bool ShowDiscountedPrice(object discountedPrice)
        {
            return discountedPrice != DBNull.Value && Convert.ToDecimal(discountedPrice) > 0;
        }

        protected string GetBase64Image(object imageData, object contentType)
        {
            if (imageData != DBNull.Value)
            {
                byte[] bytes = (byte[])imageData;
                string base64Image = Convert.ToBase64String(bytes);
                string imageSource = "data:" + contentType + ";base64," + base64Image;
                return imageSource;
            }
            else
            {
                // Use the application root operator (~) to specify the correct path to the default image
                return "~/admin_assets/img/default_image_product.png";
            }
        }




    }
}