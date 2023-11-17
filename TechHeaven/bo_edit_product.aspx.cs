using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;


namespace TechHeaven
{
    public class Produto
    {
        public int id_products { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
        public string codigoArtigo { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int category { get; set; }
        public int brand { get; set; }
    }

    public partial class bo_edit_product : System.Web.UI.Page
    {
        public static int productId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["productId"] != null)
                {
                    productId = Convert.ToInt32(Request.QueryString["productId"]);
                    Produto product = GetProductDetails(productId);


                    if (product != null)
                    {
                        tb_nome.Text = product.name;
                        tb_numero_artigo.Text = product.codigoArtigo;
                        tb_preco.Text = product.price.ToString();
                        tb_stock.Text = product.quantity.ToString();
                        tb_descricao.Text = product.description;
                        ddl_categoria.SelectedValue = product.category.ToString();
                        ddl_marca.SelectedValue = product.brand.ToString();


                        try
                        {
                            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                            SqlCommand myCommand = new SqlCommand();
                            myCommand.CommandType = CommandType.StoredProcedure;
                            myCommand.CommandText = "ler_imagem";
                            myCommand.Connection = myConn;
                            myCommand.Parameters.AddWithValue("@id_produto", productId);

                            myConn.Open();

                            SqlDataReader reader = myCommand.ExecuteReader();

                            if (reader.Read())
                            {
                                // Get the image data from the database
                                byte[] imageBytes = reader["image"] as byte[];
                                string contentType = reader["contenttype"].ToString();

                                // Check if the content type is "application/octet-stream" to indicate no image
                                if (contentType != "application/octet-stream")
                                {
                                    // If the image exists, display it
                                    string base64Image = Convert.ToBase64String(imageBytes);
                                    string imageSource = "data:" + contentType + ";base64," + base64Image;
                                    productImage.ImageUrl = imageSource;
                                }
                                else
                                {
                                    lbl_nao_imagem.Enabled = true;
                                    lbl_nao_imagem.Visible = true;
                                    productImage.ImageUrl = "admin_assets/img/default_image_product.png";
                                }
                            }
                            else
                            {
                                lbl_nao_imagem.Enabled = true;
                                lbl_nao_imagem.Visible = true;
                                productImage.ImageUrl = "admin_assets/img/default_image_product.png";
                            }

                            myConn.Close();
                        }
                        catch (Exception ex)
                        {
                            lbl_erro.Text = ex.Message;
                            lbl_erro.Visible = true;
                            lbl_erro.Enabled = true;
                        }
                    }
                    else
                    {
                        lbl_erro.Text = "Produt not found!";
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                        btn_editar.Enabled = false;
                    }
                }
                else
                {
                    lbl_erro.Text = "Product ID not found!";
                    btn_editar.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }

            }
        }

        private Produto GetProductDetails(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString;
            Produto product = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT
                    p.id_products,
                    p.quantity,
                    p.name,
                    p.product_code AS codigoArtigo,
                    p.price,
                    p.description,
                    p.category,
                    p.brand
                FROM products p
                WHERE p.id_products = @productId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        product = new Produto
                        {
                            id_products = Convert.ToInt32(reader["id_products"]),
                            quantity = Convert.ToInt32(reader["quantity"]),
                            name = reader["name"].ToString(),
                            codigoArtigo = reader["codigoArtigo"].ToString(),
                            price = Convert.ToDecimal(reader["price"]),
                            description = reader["description"].ToString(),
                            category = Convert.ToInt32(reader["category"]),
                            brand = Convert.ToInt32(reader["brand"]),
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                    lbl_erro.Visible = true;
                    lbl_erro.Enabled = true;
                }
            }

            return product;
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            if (fu_imagem.HasFile)
            {
                try
                {
                    Stream imgStream = fu_imagem.PostedFile.InputStream;
                    int imgTamanho = fu_imagem.PostedFile.ContentLength;

                    string contentType = fu_imagem.PostedFile.ContentType;

                    byte[] imgBinary = new byte[imgTamanho];

                    imgStream.Read(imgBinary, 0, imgTamanho);

                    SqlConnection myConnn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                    SqlCommand myCommandd = new SqlCommand();
                    myCommandd.CommandType = CommandType.StoredProcedure;
                    myCommandd.CommandText = "edit_product";

                    myCommandd.Connection = myConnn;

                    myCommandd.Parameters.AddWithValue("@id_produto", productId);
                    myCommandd.Parameters.AddWithValue("@nome", tb_nome.Text);
                    myCommandd.Parameters.AddWithValue("@descricao", tb_descricao.Text);
                    myCommandd.Parameters.AddWithValue("@numero_artigo", tb_numero_artigo.Text);
                    myCommandd.Parameters.AddWithValue("@preco", decimal.Parse(tb_preco.Text));
                    myCommandd.Parameters.AddWithValue("@stock", int.Parse(tb_stock.Text));
                    myCommandd.Parameters.AddWithValue("@categoria", ddl_categoria.SelectedValue);
                    myCommandd.Parameters.AddWithValue("@imagem", imgBinary);
                    myCommandd.Parameters.AddWithValue("@ct", contentType);
                    myCommandd.Parameters.AddWithValue("@marca", ddl_marca.SelectedValue);



                    myConnn.Open();
                    myCommandd.ExecuteNonQuery();

                    lbl_erro.Text = "Product edited successfuly!";
                    lbl_erro.ForeColor = System.Drawing.Color.Green;

                    myConnn.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = "Imagem demasiado grande!";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
                
            }
            else
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "edit_product_without_img";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@id_produto", productId);
                myCommand.Parameters.AddWithValue("@nome", tb_nome.Text);
                myCommand.Parameters.AddWithValue("@descricao", tb_descricao.Text);
                myCommand.Parameters.AddWithValue("@numero_artigo", tb_numero_artigo.Text);
                myCommand.Parameters.AddWithValue("@preco", decimal.Parse(tb_preco.Text));//
                myCommand.Parameters.AddWithValue("@stock", int.Parse(tb_stock.Text));//
                myCommand.Parameters.AddWithValue("@categoria", ddl_categoria.SelectedValue);
                myCommand.Parameters.AddWithValue("@marca", ddl_marca.SelectedValue);



                myConn.Open();
                myCommand.ExecuteNonQuery();

                lbl_erro.Text = "Product updated successfuly!";
                lbl_erro.ForeColor = System.Drawing.Color.Green;

                myConn.Close();
            }
        }
    }
}