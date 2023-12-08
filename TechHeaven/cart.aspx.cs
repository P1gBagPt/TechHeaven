using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class cart : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 12;
        private DataTable _dtOriginal;

        public static string query = "";
        public static int id_user;

        public static decimal total = 0;

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


                if (!IsPostBack)
                {
                    total = 0;
                    try
                    {
                        SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "cart_total";

                        cmd.Connection = myConn;

                        cmd.Parameters.AddWithValue("@userId", id_user);

                        SqlParameter totalRetorno = new SqlParameter("@total", SqlDbType.Decimal);
                        totalRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(totalRetorno);

                        myConn.Open();
                        cmd.ExecuteNonQuery();

                        decimal totali = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                        if (totali == 0)
                        {
                            lbl_vazio.Text = "Cart is empty!";
                            lbl_vazio.Enabled = true;
                            lbl_vazio.Visible = true;
                            lbl_vazio.ForeColor = Color.Red;


                            ltTotal.Text = "";
                            checkout_panel.Visible = false;
                            checkout_panel.Enabled = false;

                            btn_esvaziar.Visible = false;
                            btn_esvaziar.Enabled = false;

                        }
                        else
                        {

                            query = "SELECT c.id_cart, c.quantity, p.id_products, p.*, " +
                                "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, " +
                                "p.quantity as quantidade " +
                    "FROM cart c " +
                    "INNER JOIN products p ON c.productID = p.id_products " +
                    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
                    "WHERE c.userID = " + id_user + " AND c.status = 1";


                            BindDataIntoRepeater(query);
                        }

                    }
                    catch (Exception ex)
                    {
                        lbl_vazio.Text = ex.Message;
                    }

                }

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

                    decimal totali = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                    if (totali == 0)
                    {
                        lbl_vazio.Text = "Cart is empty!";
                        lbl_vazio.Enabled = true;
                        lbl_vazio.Visible = true;
                        lbl_vazio.ForeColor = Color.Red;


                        ltTotal.Text = "";
                        checkout_panel.Visible = false;
                        checkout_panel.Enabled = false;

                        btn_esvaziar.Visible = false;
                        btn_esvaziar.Enabled = false;

                    }
                    else
                    {


                        query = "SELECT c.id_cart, c.quantity, p.*, " +
                                "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, " +
                                "p.quantity as quantidade " +
                    "FROM cart c " +
                    "INNER JOIN products p ON c.productID = p.id_products " +
                    "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
                    "WHERE c.userID = " + id_user + " AND c.status = 1";


                        BindDataIntoRepeater(query);
                    }

                }
                catch (Exception ex)
                {
                    lbl_vazio.Text = ex.Message;
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

        public static decimal subtotal = 0;

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dataItem = e.Item.DataItem as DataRowView;

                if (dataItem != null)
                {
                    Label lblPrice = (Label)e.Item.FindControl("lbl_productPrice");
                    Label lblDiscountedPrice = (Label)e.Item.FindControl("lblDiscountedPrice");
                    Label ltSubtotal = e.Item.FindControl("lbl_subtotal") as Label;

                    // Verifica se "price" é DBNull ou nulo antes de tentar converter
                    if (dataItem["price"] != DBNull.Value && dataItem["price"] != null)
                    {
                        decimal originalPrice = Convert.ToDecimal(dataItem["price"]);
                        decimal discountedPrice = dataItem["discounted_price"] != DBNull.Value ? Convert.ToDecimal(dataItem["discounted_price"]) : originalPrice;

                        // Verifica se há promoção e calcula o preço com desconto
                        if (discountedPrice < originalPrice)
                        {
                            lblDiscountedPrice.Text = discountedPrice.ToString("N2") + "€"; // Ajusta para exibir apenas duas casas decimais
                            lblDiscountedPrice.Visible = true;

                            // Aplica a classe CSS "old-price" ao lblPrice
                            lblPrice.CssClass = "old-price";
                            lblPrice.Text = FormatPrice(originalPrice, null);


                        }
                        else
                        {
                            lblDiscountedPrice.Visible = false;
                        }

                        // Agora, define o lblPrice, independentemente de haver desconto ou não
                        lblPrice.Text = FormatPrice(originalPrice, null);

                        // Calcula o subtotal para este item
                        int quantidade = Convert.ToInt32(dataItem["quantity"]);
                        decimal subtotalItem = quantidade * discountedPrice;

                        ltSubtotal.Text = subtotalItem.ToString("C", new CultureInfo("pt-PT"));

                        // Adiciona o subtotal ao total geral
                        total += subtotalItem;
                    }
                }
            }

            // Atualiza o valor total após o processamento de todos os itens
            ltTotal.Text = total.ToString("N2");
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



        protected void lb_diminuir_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Diminuir")
            {
                int idProduto = 0;
                int idCarrinho = 0;

                // Split the command argument to get product and cart IDs
                string argument = e.CommandArgument.ToString();
                string[] arguments = argument.Split(',');

                if (arguments.Length == 2)
                {
                    idProduto = Convert.ToInt32(arguments[0]);
                    idCarrinho = Convert.ToInt32(arguments[1]);
                }

                try
                {
                    // Use the same SqlConnection object for both queries
                    using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                    {
                        myConn.Open();  // Open the connection

                        // Check stock information and current quantity
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = myConn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "stock_info_product";

                            cmd.Parameters.AddWithValue("@productID", idProduto);
                            cmd.Parameters.AddWithValue("@cartID", idCarrinho);

                            SqlParameter retorno = new SqlParameter("@return_stock", SqlDbType.Int);
                            retorno.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(retorno);

                            SqlParameter retornoQuantidade = new SqlParameter("@return_quantity", SqlDbType.Int);
                            retornoQuantidade.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(retornoQuantidade);

                            cmd.ExecuteNonQuery();

                            int respostaSP = Convert.ToInt32(cmd.Parameters["@return_stock"].Value);
                            int respostaQuantidades = Convert.ToInt32(cmd.Parameters["@return_quantity"].Value);

                            // Check if the quantity is about to become zero
                            if (respostaQuantidades - 1 == 0)
                            {
                                // Remove the item from the cart
                                using (SqlCommand cmd3 = new SqlCommand())
                                {
                                    cmd3.Connection = myConn;
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.CommandText = "DELETE FROM cart WHERE id_cart = @IdCarrinho AND userID = @UserId";

                                    cmd3.Parameters.AddWithValue("@IdCarrinho", idCarrinho);
                                    cmd3.Parameters.AddWithValue("@UserId", id_user);

                                    int rowsAffected = cmd3.ExecuteNonQuery();

                                    // Update cart total and check if the cart is empty
                                    UpdateCartTotal(myConn);
                                }
                            }
                            else if (respostaQuantidades - 1 <= respostaSP)
                            {
                                // Decrease the quantity in the cart
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    cmd2.Connection = myConn;
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.CommandText = "change_quantity";

                                    cmd2.Parameters.AddWithValue("@cartID", idCarrinho);
                                    cmd2.Parameters.AddWithValue("@quantity", respostaQuantidades - 1);

                                    cmd2.ExecuteNonQuery();
                                }

                                // Update cart total
                                UpdateCartTotal(myConn);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle and display the exception
                    lbl_vazio.Text = "An error occurred: " + ex.Message;
                    lbl_vazio.ForeColor = Color.Red;
                }
            }
        }

        private void UpdateCartTotal(SqlConnection connection)
        {
            try
            {
                using (SqlCommand cmd4 = new SqlCommand())
                {
                    cmd4.Connection = connection;
                    cmd4.CommandType = CommandType.StoredProcedure;
                    cmd4.CommandText = "cart_total";

                    cmd4.Parameters.AddWithValue("@userId", id_user);

                    SqlParameter totalRetorno = new SqlParameter("@total", SqlDbType.Float);
                    totalRetorno.Direction = ParameterDirection.Output;
                    cmd4.Parameters.Add(totalRetorno);

                    cmd4.ExecuteNonQuery();

                    decimal totali = (cmd4.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd4.Parameters["@total"].Value) : 0;

                    // Check if the cart is empty
                    if (totali == 0)
                    {
                        lbl_vazio.Text = "Cart is empty!";
                        lbl_vazio.Enabled = true;
                        lbl_vazio.Visible = true;
                        lbl_vazio.ForeColor = Color.Red;

                        ltTotal.Text = "";
                        checkout_panel.Visible = false;
                        checkout_panel.Enabled = false;

                        btn_esvaziar.Visible = false;
                        btn_esvaziar.Enabled = false;
                        BindDataIntoRepeater(query);
                    }
                    else
                    {
                        // Reload the data into the repeater
                        BindDataIntoRepeater(query);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle and display the exception
                lbl_vazio.Text = "An error occurred: " + ex.Message;
                lbl_vazio.ForeColor = Color.Red;
            }
        }

        protected void lb_remover_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Remover")
            {
                total = 0;

                int idCarrinho = Convert.ToInt32(e.CommandArgument);

                // Construa a consulta SQL DELETE com base no ID do carrinho
                string deleteQuery = "DELETE FROM cart WHERE id_cart = @IdCarrinho AND userID = " + id_user;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCarrinho", idCarrinho);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

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

                    if (totali == 0)
                    {
                        lbl_vazio.Text = "Cart is empty!";
                        lbl_vazio.Enabled = true;
                        lbl_vazio.Visible = true;
                        lbl_vazio.ForeColor = Color.Red;

                        ltTotal.Text = "";
                        checkout_panel.Visible = false;
                        checkout_panel.Enabled = false;

                        btn_esvaziar.Visible = false;
                        btn_esvaziar.Enabled = false;

                    }
                    else
                    {
                        query = "SELECT c.id_cart, c.quantity, p.id_products, p.*, " +
                                 "CASE WHEN pr.discount_percent IS NOT NULL AND pr.status = 1 THEN p.price - (p.price * pr.discount_percent / 100) ELSE NULL END AS discounted_price, " +
                                 "p.quantity as quantidade " +
                     "FROM cart c " +
                     "INNER JOIN products p ON c.productID = p.id_products " +
                     "LEFT JOIN promotions pr ON p.id_products = pr.productID " +
                     "WHERE c.userID = " + id_user + " AND c.status = 1";


                        BindDataIntoRepeater(query);
                    }

                }
                catch (Exception ex)
                {
                    lbl_vazio.Text = ex.Message;
                }



                BindDataIntoRepeater(query);

            }
        }


        protected void lb_increase_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Aumentar")
            {
                int idProduto = 0;
                int idCarrinho = 0;

                string argument = e.CommandArgument.ToString();
                string[] arguments = argument.Split(',');

                if (arguments.Length == 2)
                {
                    idProduto = Convert.ToInt32(arguments[0]);
                    idCarrinho = Convert.ToInt32(arguments[1]);
                }
                try
                {
                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "stock_info_product";

                    cmd.Connection = myConn;

                    cmd.Parameters.AddWithValue("@productID", idProduto);
                    cmd.Parameters.AddWithValue("@cartID", idCarrinho);

                    SqlParameter retorno = new SqlParameter("@return_stock", SqlDbType.Int);
                    retorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(retorno);

                    SqlParameter retornoQuantidade = new SqlParameter("@return_quantity", SqlDbType.Int);
                    retornoQuantidade.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(retornoQuantidade);

                    myConn.Open();
                    cmd.ExecuteNonQuery();

                    int respostaSP = Convert.ToInt32(cmd.Parameters["@return_stock"].Value);
                    int respostaQuantidades = Convert.ToInt32(cmd.Parameters["@return_quantity"].Value);

                    myConn.Close();

                    if (respostaQuantidades + 1 <= respostaSP)
                    {
                        try
                        {
                            SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                            SqlCommand cmd2 = new SqlCommand();

                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.CommandText = "change_quantity";

                            cmd2.Connection = myConn2;

                            cmd2.Parameters.AddWithValue("@cartID", idCarrinho);
                            cmd2.Parameters.AddWithValue("@quantity", respostaQuantidades + 1);


                            myConn2.Open();
                            cmd2.ExecuteNonQuery();
                            myConn2.Close();

                            BindDataIntoRepeater(query);

                        }
                        catch (Exception ex)
                        {
                            lbl_vazio.Enabled = true;
                            lbl_vazio.Visible = true;
                            lbl_vazio.Text = ex.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbl_vazio.Enabled = true;
                    lbl_vazio.Visible = true;
                    lbl_vazio.Text = ex.Message;
                }
            }

        }

        protected void btn_continuar_comprar_Click(object sender, EventArgs e)
        {
            Response.Redirect("all_products.aspx");
        }

        protected void btn_checkout_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect("billing.aspx");
            }
        }

        protected void btn_esvaziar_Click(object sender, EventArgs e)
        {
            try
            {
                string esvaziarQuery = "DELETE FROM cart WHERE userID = " + id_user + " AND status = 1";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(esvaziarQuery, con))
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        ltTotal.Text = "0";

                        Response.Redirect("cart.aspx");

                    }
                }
            }
            catch (Exception ex)
            {
                lbl_vazio.Enabled = true;
                lbl_vazio.Visible = true;
                lbl_vazio.Text = ex.Message;
            }
        }

        public static float shipping;
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RadioButtonList1.SelectedIndex)
            {
                case 0: // Free Shipping
                    ltShipping.Text = "Free";
                    Session["shipping"] = "0";
                    break;
                case 1: // Standard
                    ltShipping.Text = "Standart 10 ";
                    ltTotal.Text = (total + 10).ToString("N2");
                    Session["shipping"] = "10";
                    break;
                case 2: // Express
                    ltShipping.Text = "Express 20 ";
                    ltTotal.Text = (total + 20).ToString("N2");
                    Session["shipping"] = "20";
                    break;
                default:
                    ltShipping.Text = "FREE";
                    Session["shipping"] = "0";
                    break;
            }
        }



    }
}