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
        private int _pageSize = 2;
        private DataTable _dtOriginal;
        public static string query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "WHERE status = 'true' AND quantity > 0;";


        public static string orderByClause = "";
        public static string categotiaFilter = "";
        public static string marcaFilter = "";
        public static int categoryId;
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
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "WHERE status = 'true' AND quantity > 0;";
                BindDataIntoRepeater(query);
            }
        }

        protected void lb_category_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Category")
            {
                marcaFilter = "";
                categotiaFilter = e.CommandArgument.ToString();


                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "WHERE status = 'true' AND quantity > 0 AND category = '" + categotiaFilter + "'" + orderByClause;

                // Bind products based on the selected category
                BindDataIntoRepeater(query);

            }
        }

        protected void lb_brand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Brand")
            {
                marcaFilter = e.CommandArgument.ToString();

                // Update the query based on whether a category filter is selected
                if (string.IsNullOrEmpty(categotiaFilter))
                {
                    query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
        "FROM products p " +
        "INNER JOIN categories c ON p.category = c.id_category " +  // Corrected JOIN condition
        "WHERE p.status = 'true' AND p.quantity > 0 AND p.brand = '" + marcaFilter + "' " + orderByClause;  // Corrected WHERE condition, assuming brand is a string

                }
                else
                {

                    query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
                    "FROM products p WHERE status = 'true' AND quantity > 0 AND category = '" + categotiaFilter + "' AND brand = " + marcaFilter + " " + orderByClause;

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


            if (!string.IsNullOrEmpty(categotiaFilter) && !string.IsNullOrEmpty(marcaFilter))
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
                   "FROM products p WHERE status = 'true' AND quantity > 0 AND category = '" + categotiaFilter + "' AND brand = " + marcaFilter + " " + orderByClause;
            }
            else if (!string.IsNullOrEmpty(categotiaFilter))
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
     "FROM products p " +
     "LEFT JOIN categories c ON p.category = c.id_category " +
     "WHERE status = 'true' AND quantity > 0 AND category = '" + categotiaFilter + "'" + orderByClause;

            }
            else if (!string.IsNullOrEmpty(marcaFilter))
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
     "FROM products p " +
     "LEFT JOIN categories c ON p.category = c.id_category " +
     "WHERE status = 'true' AND quantity > 0 AND brand = '" + marcaFilter + "'" + orderByClause;

            }
            else
            {
                query = "SELECT p.id_products, p.name, p.description, p.quantity, c.category_name as category, p.brand, p.status, p.product_code AS codigoArtigo, p.price, p.image, p.contenttype, p.creation_date " +
    "FROM products p " +
    "LEFT JOIN categories c ON p.category = c.id_category " +
    "WHERE status = 'true' AND quantity > 0 " + orderByClause;
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