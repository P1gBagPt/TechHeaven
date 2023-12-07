using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class all_products : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 9;
        private DataTable _dtOriginal;
        public static string query = @"
    SELECT
        p.id_products,
        p.name,
        p.description,
        p.quantity,
        c.category_name as category,
        p.brand,
        p.status,
        p.product_code AS codigoArtigo,
        p.price,
        CASE
            WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100)
            ELSE NULL
        END AS discounted_price,
        p.image,
        p.contenttype,
        p.creation_date
    FROM
        products p
    LEFT JOIN
        categories c ON p.category = c.id_category
    LEFT JOIN
        promotions pr ON p.id_products = pr.productID
    WHERE
        p.status = 'true' AND p.quantity > 0;
";

        public static string queryAUX = @"
    SELECT
        p.id_products,
        p.name,
        p.description,
        p.quantity,
        c.category_name as category,
        p.brand,
        p.status,
        p.product_code AS codigoArtigo,
        p.price,
        CASE
            WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100)
            ELSE NULL
        END AS discounted_price,
        p.image,
        p.contenttype,
        p.creation_date
    FROM
        products p
    LEFT JOIN
        categories c ON p.category = c.id_category
    LEFT JOIN
        promotions pr ON p.id_products = pr.productID
    WHERE
        p.status = 'true' AND p.quantity > 0;
";


        public static string orderByClause = "";
        public static int categoryId, brandId;
        public static string procurar;


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
            BindDataIntoRepeater(query);


            if (Request.QueryString["categoryID"] != null)
            {
                categoryId = Convert.ToInt32(Request.QueryString["categoryID"]);

                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
    "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
    "WHERE p.status = 'true' AND p.quantity > 0 AND category = " + categoryId;



                BindDataIntoRepeater(query);

            }

            if (Request.QueryString["brandID"] != null)
            {
                brandId = Convert.ToInt32(Request.QueryString["brandID"]);

                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "WHERE status = 'true' AND quantity > 0 AND brand = " + brandId;

                BindDataIntoRepeater(query);

            }

            if (Request.QueryString["searchProduct"] != null)
            {
                procurar = Request.QueryString["searchProduct"];

                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
     "FROM products p " +
     "LEFT JOIN categories c ON p.category = c.id_category " +
     "WHERE status = 'true' AND quantity > 0 AND (p.name LIKE '%" + procurar + "%')";

                BindDataIntoRepeater(query);

            }




            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();
                string query = "SELECT TOP 8 id_brand, brand_name FROM brands";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query, con))
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




                string query2 = "SELECT TOP 8 id_category, category_name FROM categories";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query2, con))
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


            }
            catch (Exception ex)
            {

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


        private void BindDataIntoRepeater(string query)
        {
            var dt = GetDataFromDb(query);
            _pgsource.DataSource = dt.DefaultView;
            _pgsource.AllowPaging = true;
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _pgsource.PageCount;
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;

            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            HandlePaging();
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
            BindDataIntoRepeater(query);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater(query);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater(query);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater(query);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater(query);
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
            SetProductImage(e.Item);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dataItem = e.Item.DataItem as DataRowView;

                if (dataItem != null)
                {
                    Label lblPrice = (Label)e.Item.FindControl("lblPrice");
                    Label lblDiscountedPrice = (Label)e.Item.FindControl("lblDiscountedPrice");

                    // Verifica se "price" é DBNull ou nulo antes de tentar converter
                    if (dataItem["price"] != DBNull.Value && dataItem["price"] != null)
                    {
                        decimal originalPrice = Convert.ToDecimal(dataItem["price"]);

                        // Verifica se há promoção e calcula o preço com desconto
                        if (dataItem["discounted_price"] != DBNull.Value && dataItem["discounted_price"] != null)
                        {
                            decimal discountedPrice = Convert.ToDecimal(dataItem["discounted_price"]);
                            lblDiscountedPrice.Text = discountedPrice.ToString("N2") + "€"; // Ajusta para exibir apenas duas casas decimais
                            lblDiscountedPrice.Visible = true;

                            // Aplica a classe CSS "old-price" ao lblPrice
                            lblPrice.CssClass = "old-price";
                        }
                        else
                        {
                            lblDiscountedPrice.Visible = false;
                        }

                        // Agora, define o lblPrice, independentemente de haver desconto ou não
                        lblPrice.Text = FormatPrice(originalPrice, null);
                    }
                }
            }
        }


        protected string FormatPrice(object price, object discountedPrice)
        {
            decimal decimalValue;

            if (discountedPrice != null && decimal.TryParse(discountedPrice.ToString(), out decimalValue))
            {
                return string.Format("{0:C}", decimalValue);
            }
            else if (price != null && decimal.TryParse(price.ToString(), out decimalValue))
            {
                return string.Format("{0:C}", decimalValue);
            }
            else
            {
                return string.Empty; // ou outra mensagem de erro, se necessário
            }
        }



        protected void lb_add_cart_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "add_cart")
            {

                try
                {

                    if (Session["userId"] == null)
                    {
                        Response.Redirect("login.aspx");
                    }
                    else
                    {


                        int id_user = Convert.ToInt32(Session["userId"].ToString());
                        int productId = int.Parse(e.CommandArgument.ToString());
                        int quantidade = Convert.ToInt32(1);

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
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void lb_clean_filters_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Clear")
            {
                query = @"
    SELECT
        p.id_products,
        p.name,
        p.description,
        p.quantity,
        c.category_name as category,
        p.brand,
        p.status,
        p.product_code AS codigoArtigo,
        p.price,
        CASE
            WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100)
            ELSE NULL
        END AS discounted_price,
        p.image,
        p.contenttype,
        p.creation_date
    FROM
        products p
    LEFT JOIN
        categories c ON p.category = c.id_category
    LEFT JOIN
        promotions pr ON p.id_products = pr.productID
    WHERE
        p.status = 'true' AND p.quantity > 0;
";

                BindDataIntoRepeater(query);

                categoryId = 0;
                brandId = 0;

            }
        }

        protected void lb_category_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Category")
            {
                //brandId = null;
                string argumentAsString = e.CommandArgument.ToString();

                if (int.TryParse(argumentAsString, out categoryId))
                {

                    if (brandId != 0)
                    {


                        query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
    "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
    "WHERE p.status = 'true' AND p.quantity > 0 AND category = " + categoryId + " AND p.brand = " + brandId + " " + orderByClause;


                    }
                    else
                    {

                        query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
    "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
    "WHERE p.status = 'true' AND p.quantity > 0 AND category = " + categoryId + " " + orderByClause;


                    }


                }
                else
                {
                    query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
    "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
    "WHERE p.status = 'true' AND p.quantity > 0 AND category = " + argumentAsString + " " + orderByClause;



                }



                // Bind products based on the selected category
                BindDataIntoRepeater(query);
            }
        }

        protected void lb_brand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Brand")
            {
                string argumenttAsString = e.CommandArgument.ToString();

                if (int.TryParse(argumenttAsString, out brandId))
                {
                    if (categoryId != 0)
                    {
                        query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
    "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
    "WHERE p.status = 'true' AND p.quantity > 0 AND category = " + categoryId + " AND p.brand = " + brandId + " " + orderByClause;


                    }
                    else
                    {

                        query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
"CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
"FROM products p " +
"LEFT JOIN categories c ON p.category = c.id_category " +
"LEFT JOIN promotions pr ON p.id_products = pr.productID " +
"WHERE p.status = 'true' AND p.quantity > 0 AND p.brand = " + brandId + " " + orderByClause;




                    }

                }
                else
                {

                    query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
"CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
"FROM products p " +
"LEFT JOIN categories c ON p.category = c.id_category " +
"LEFT JOIN promotions pr ON p.id_products = pr.productID " +
"WHERE p.status = 'true' AND p.quantity > 0 AND category = " + argumenttAsString + " " + orderByClause;



                }

                // Bind data based on the selected brand
                BindDataIntoRepeater(query);
            }
        }


        protected void sortby_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = sortby.SelectedValue;
            orderByClause = "";

            switch (selectedValue)
            {
                case "name_asc":
                    orderByClause = "ORDER BY p.name ASC";
                    break;
                case "name_desc":
                    orderByClause = "ORDER BY p.name DESC";
                    break;
                case "price_asc":
                    orderByClause = "ORDER BY p.price ASC";
                    break;
                case "price_desc":
                    orderByClause = "ORDER BY p.price DESC";
                    break;
                default:
                    // Lidar com caso inválido ou padrão aqui
                    break;
            }



            if (categoryId != 0 && brandId != 0)
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
"CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
"FROM products p " +
"LEFT JOIN categories c ON p.category = c.id_category " +
"LEFT JOIN promotions pr ON p.id_products = pr.productID " +
"WHERE p.status = 'true' AND p.quantity > 0 AND category = " + categoryId + " AND p.brand = " + brandId + " " + orderByClause;

                BindDataIntoRepeater(query);
            }
            else if (categoryId != 0)
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
     "FROM products p " +
     "LEFT JOIN categories c ON p.category = c.id_category " +
     "WHERE status = 'true' AND quantity > 0 AND category = " + categoryId + " " + orderByClause;

                BindDataIntoRepeater(query);

            }else if(brandId != 0)
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, " +
"CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, p.image, p.contenttype, p.creation_date " +
"FROM products p " +
"LEFT JOIN categories c ON p.category = c.id_category " +
"LEFT JOIN promotions pr ON p.id_products = pr.productID " +
"WHERE p.status = 'true' AND p.quantity > 0 AND p.brand = " + brandId + " " + orderByClause;
            }


            BindDataIntoRepeater(query);
        }

        protected void lb_wishlist_Command(object sender, CommandEventArgs e)
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
                        int productId = int.Parse(e.CommandArgument.ToString());

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

        private void SetProductImage(RepeaterItem item)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Image productImage = item.FindControl("img_produto") as System.Web.UI.WebControls.Image;
                Panel newPanel = item.FindControl("Panel1") as Panel; // Assuming you have a Panel control for the "New" label
                DataRowView dataItem = item.DataItem as DataRowView;

                string contentType = dataItem["contenttype"].ToString();
                byte[] imageBytes = (byte[])dataItem["image"];
                DateTime creationDate = Convert.ToDateTime(dataItem["creation_date"]);

                if (contentType != "application/octet-stream")
                {
                    string base64Image = Convert.ToBase64String(imageBytes);
                    string imageSource = "data:" + contentType + ";base64," + base64Image;
                    productImage.ImageUrl = imageSource;

                    // Check if the product was inserted within the last 5 days
                    if (DateTime.Now.Subtract(creationDate).Days <= 5)
                    {
                        newPanel.Visible = true;
                    }
                    else
                    {
                        newPanel.Visible = false;
                    }
                }
                else
                {
                    productImage.ImageUrl = "admin_assets/img/default_image_product.png";
                    newPanel.Visible = false;
                }
            }
        }







    }
}