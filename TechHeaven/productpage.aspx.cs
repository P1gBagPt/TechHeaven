using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace TechHeaven
{
    public partial class productpage : System.Web.UI.Page
    {
        public class Produto
        {
            public int id_produto { get; set; }
            public int stock { get; set; }
            public string nome { get; set; }
            public string codigoArtigo { get; set; }
            public decimal preco { get; set; }
            public string descricao { get; set; }
            public string categoria { get; set; }
            public string marca { get; set; }
            public decimal discounted_price { get; set; }
        }
        public static int productId, totalReviews;

        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 5;
        public static string query = @"
    SELECT
        u.username AS UserName,
        rc.title AS Title,
        rc.review AS Review,
        rc.classification AS Rating,
        DATEDIFF(DAY, rc.creation_date, GETDATE()) AS DaysAgo
    FROM
        review_classification rc
    JOIN
        users u ON rc.userId = u.id
    WHERE
        rc.status = 1
        AND rc.productID = @productId
    ORDER BY
        rc.creation_date DESC";
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
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
                        lbl_nome.Text = product.nome;
                        lbl_descricao.Text = product.descricao;

                        // Se não for revenda, exibir apenas o preço normal sem rasurar
                        

                        if(product.discounted_price != 0)
                        {
                            lbl_preco.Text = string.Format("{0:N2} €", product.preco);
                            lbl_preco.CssClass = "old-price";

                            lblDiscountedPrice.Visible = true;
                            lblDiscountedPrice.Text = product.discounted_price.ToString("N2") + "€";
                        }
                        else
                        {
                            lbl_preco.Text = string.Format("{0:N2} €", product.preco);
                        }

                        //lbl_preco.Text = product.preco.ToString();
                        //lbl_codigo_artigo.Text = product.codigoArtigo;
                        //lbl_marca.Text = product.marca.ToString();

                        lb_categoria.Text = product.categoria.ToString();
                        lb_categoria.CommandArgument = product.categoria.ToString();

                        lb_marca.Text = product.marca.ToString();

                        lb_productCode.Text = product.codigoArtigo.ToString();

                        tb_quantidade.Text = "1"; // Define o valor padrão como 1
                        int availableStock = product.stock;
                        tb_quantidade.Attributes["max"] = availableStock.ToString();

                        try
                        {
                            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

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
                                    main_product_image.ImageUrl = imageSource;
                                }
                                else
                                {
                                    main_product_image.ImageUrl = "admin_assets/img/default_image_product.png";
                                }
                            }
                            else
                            {
                                main_product_image.ImageUrl = "admin_assets/img/default_image_product.png";
                            }

                            myConn.Close();
                        }
                        catch (Exception ex)
                        {
                            lbl_erro.Text = ex.Message;
                        }
                    }
                    else
                    {
                        lbl_erro.Text = "Produto não encontrado!";
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                        //btn_editar.Enabled = false;
                    }

                    try
                    {
                        // Get and display total reviews before binding data into the repeater
                        GetAndDisplayTotalReviews(productId);
                        BindDataIntoRepeater(query, productId);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                    }
                }
                else
                {
                    lbl_erro.Text = "ID do produto nao fornecido!";
                    //btn_editar.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


      




        static DataTable GetDataFromDb(string query, int productId)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString());

            var da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@productId", productId); // Add parameter for productId
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


        // Bind PagedDataSource into Repeater
        private void BindDataIntoRepeater(string query, int productId)
        {
            var dt = GetDataFromDb(query, productId);
            _pgsource.DataSource = dt.DefaultView;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Example: "Page 1 of 10"
            //lblpage.Text = "Página " + (CurrentPage + 1) + " de " + _pgsource.PageCount;
            // Enable First, Last, Previous, Next buttons
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;
            /*lbFirst.Enabled = !_pgsource.IsFirstPage;
            lbLast.Enabled = !_pgsource.IsLastPage;*/

            // Bind data into repeater
            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            // Call the function to do paging
            HandlePaging();









        }

        protected void GetAndDisplayTotalReviews(int productId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                // Query to get the count and average classification
                string query = "SELECT COUNT(*) AS TotalReviews, AVG(classification) AS AverageClassification FROM review_classification WHERE status = 1 AND productID = @productId";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // Use a parameter for the product ID to avoid SQL injection
                    command.Parameters.AddWithValue("@productId", productId);

                    con.Open();
                    // Use ExecuteReader to get both count and average
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get the count
                            int totalReviews = Convert.ToInt32(reader["TotalReviews"]);

                            // Get the average classification
                            decimal averageClassification = Convert.ToDecimal(reader["AverageClassification"]);

                            if (totalReviews > 0)
                            {
                                lbl_total_reviews.Text = $"Reviews ({totalReviews})";
                                lbl_reviews_nav_total.Text = $"Reviews ({totalReviews})";
                                lbl_classificacao_media.Text = averageClassification.ToString() + " stars &#9733;";
                            }
                            else
                            {
                                lbl_total_reviews.Text = "Reviews (0)";
                                lbl_reviews_nav_total.Text = "Reviews (0)";
                                lbl_classificacao_media.Text = "0";
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it or display an error message)
            }
        }



        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoRepeater(query, productId);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater(query, productId);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater(query, productId);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater(query, productId);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater(query, productId);
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = Color.FromName("#0398fc");
            lnkPage.ForeColor = Color.White;
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;


                if (drv != null)
                {
                    // Get the creation date of the review
                    DateTime creationDate = Convert.ToDateTime(drv["creation_date"]);

                    // Calculate the number of days ago
                    int daysAgo = (DateTime.Now - creationDate).Days;

                    // Update the DaysAgo field in the data source
                    drv.Row["DaysAgo"] = daysAgo;
                }
            }
        }






        private Produto GetProductDetails(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;
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
    CASE
        WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100)
        ELSE NULL
    END AS discounted_price,
    p.description,
    c.category_name AS categoria_nome,
    b.brand_name AS marca_nome
FROM products p
JOIN categories c ON p.category = c.id_category
JOIN brands b ON p.brand = b.id_brand
LEFT JOIN promotions pr ON p.id_products = pr.productID
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
                            id_produto = Convert.ToInt32(reader["id_products"]),
                            stock = Convert.ToInt32(reader["quantity"]),
                            nome = reader["name"].ToString(),
                            codigoArtigo = reader["codigoArtigo"].ToString(),
                            preco = Convert.ToDecimal(reader["price"]),
                            descricao = reader["description"].ToString(),
                            categoria = reader["categoria_nome"].ToString(),
                            marca = reader["marca_nome"].ToString(),
                            discounted_price = DBNull.Value.Equals(reader["discounted_price"]) ? 0 : Convert.ToDecimal(reader["discounted_price"]),
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }
            }

            return product;
        }


        protected void lb_categoria_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "CategoriaMontra")
            {
                int idCategoriaValue = 0;

                string idCategoria = e.CommandArgument.ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_category FROM categories WHERE category_name = @categoria";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@categoria", idCategoria);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            idCategoriaValue = reader.GetInt32(0); // Retrieve the value from the first column (assuming it's an integer)

                            // Now you have the idCategoriaValue, and you can use it as needed.
                        }
                        else
                        {
                            // Handle the case where no matching category is found.
                        }
                    }
                }

                Response.Redirect("all_products.aspx?categoryID=" + idCategoriaValue);
            }
        }

        protected void btn_adicionar_carrinho_Click(object sender, EventArgs e)
        {
            //TEMP
            try
            {

                if (Session["userId"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {



                    int id_user = Convert.ToInt32(Session["userId"].ToString());
                    int quantidade = Convert.ToInt32(tb_quantidade.Text);

                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "add_cart";

                    myCommand.Connection = myConn;

                    myCommand.Parameters.AddWithValue("@idUser", id_user);
                    myCommand.Parameters.AddWithValue("@idProduto", productId);
                    myCommand.Parameters.AddWithValue("@quantity", quantidade);

                    SqlParameter valor = new SqlParameter();
                    valor.ParameterName = "@return";
                    valor.Direction = ParameterDirection.Output;
                    valor.SqlDbType = SqlDbType.Int;

                    myCommand.Parameters.Add(valor);

                    myConn.Open();
                    myCommand.ExecuteNonQuery();

                    int resposta = Convert.ToInt32(myCommand.Parameters["@return"].Value);

                    myConn.Close();

                    if (resposta == 3)
                    {
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = "A quantidade do carrinho é o stock existente!";
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                    }

                    if (resposta == 2)
                    {
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = "Carrinho atualizado com sucesso!";
                        lbl_erro.ForeColor = System.Drawing.Color.Green;
                    }

                    if (resposta == 1)
                    {
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = "Produto adicionado ao carrinho!";
                        lbl_erro.ForeColor = System.Drawing.Color.Green;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "return false;", true);

                }
            }
            catch (Exception ex)
            {

            }


        }


        protected void lb_add_wishlist_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Wishlist")
            {
                //TEMP
                try
                {

                    if (Session["userId"] == null)
                    {
                        Response.Redirect("login.aspx");
                    }
                    else
                    {

                        int id_user = Convert.ToInt32(Session["userId"].ToString());
                        SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.CommandText = "add_wishlist";

                        myCommand.Connection = myConn;

                        myCommand.Parameters.AddWithValue("@idUser", id_user);
                        myCommand.Parameters.AddWithValue("@idProduto", productId);

                        SqlParameter valor = new SqlParameter();
                        valor.ParameterName = "@return";
                        valor.Direction = ParameterDirection.Output;
                        valor.SqlDbType = SqlDbType.Int;

                        myCommand.Parameters.Add(valor);

                        myConn.Open();
                        myCommand.ExecuteNonQuery();

                        int resposta = Convert.ToInt32(myCommand.Parameters["@return"].Value);

                        myConn.Close();

                        if (resposta == 1)
                        {
                            lbl_erro.Enabled = true;
                            lbl_erro.Visible = true;
                            lbl_erro.Text = "Product already on wishlist!";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                        }

                        if (resposta == 0)
                        {
                            lbl_erro.Enabled = true;
                            lbl_erro.Visible = true;
                            lbl_erro.Text = "Product added to wishlist!";
                            lbl_erro.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = ex.Message;
                }
            }

        }

        private bool UsuarioComprouProduto(int productId, int userId)
        {
            // Adicione a lógica para verificar se o usuário comprou o produto
            // Aqui, estou assumindo que a tabela "cart" contém as informações necessárias.

            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM cart WHERE userID = @UserId AND productID = @ProductId AND orderID IS NOT NULL";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                    return count > 0;
                }
            }
        }

        /*protected void lb_add_review_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddReview")
            {
                // Verifique se o usuário está logado
                if (Session["userId"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    // Verifique se o usuário comprou o produto
                    if (UsuarioComprouProduto(productId, Convert.ToInt32(Session["userId"])))
                    {
                        // Usuário comprou o produto, permita adicionar a revisão
                        Panel2.Visible = true;  // Exiba o formulário de revisão
                    }
                    else
                    {
                        // Usuário não comprou o produto, exiba uma mensagem ou faça algo apropriado
                        lbl_erro.Text = "You can only review a product if you purchased it before.";
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }*/

        protected void lb_add_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "addReview")
            {

                if (Session["userId"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    int id_user = Convert.ToInt32(Session["userId"].ToString());
                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "add_review";

                    myCommand.Connection = myConn;

                    myCommand.Parameters.AddWithValue("@userID", id_user);
                    myCommand.Parameters.AddWithValue("@productID", productId);
                    myCommand.Parameters.AddWithValue("@title", tb_title_review.Text);
                    myCommand.Parameters.AddWithValue("@review", tb_review.Text);
                    myCommand.Parameters.AddWithValue("@classification", int.Parse(tb_classificarion.Text));


                    SqlParameter valor = new SqlParameter();
                    valor.ParameterName = "@retorno";
                    valor.Direction = ParameterDirection.Output;
                    valor.SqlDbType = SqlDbType.Int;

                    myCommand.Parameters.Add(valor);

                    myConn.Open();
                    myCommand.ExecuteNonQuery();

                    int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);


                    myConn.Close();

                    if (resposta == 0)
                    {
                        lbl_erro_review.Text = "You have already reviewed this product";
                        lbl_erro_review.ForeColor = Color.Red;
                    }
                    else if (resposta == 1)
                    {
                        lbl_erro_review.Text = "Review added!";
                        lbl_erro_review.ForeColor = Color.Green;

                        Response.Redirect(Request.RawUrl);

                    }
                    else if(resposta == 2)
                    {
                        lbl_erro_review.Text = "You need to buy this product to review";
                        lbl_erro_review.ForeColor = Color.Red;
                    }

                }

            }

        }


        /*protected string GetFormattedPrice(object preco, bool isRevenda)
        {
            decimal precoDecimal = Convert.ToDecimal(preco);

            if (isRevenda)
            {
                // Aplicar desconto de 20% para revendedores
                decimal precoDesconto = precoDecimal * 0.8m;

                // Utilize a função string.Format para garantir a formatação correta
                return string.Format("<del>{0:N2} €</del> {1:N2} €", precoDecimal, precoDesconto);
            }

            // Se não for revenda, exibir apenas o preço normal sem rasurar
            return string.Format("{0:N2} €", precoDecimal);
        }*/
    }
}