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
    public partial class bo_sales : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 10;
        public string query = @"SELECT id_order, order_date, total, status, payment_methodID, addressID FROM orders";
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
                // Fetch and display the list of orders when the page loads.
                //BindOrders();
            }

            try
            {
                BindDataIntoRepeater(query);
            }
            catch (Exception ex)
            {
            }


        }
        static DataTable GetDataFromDb(string query)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString());

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

        // Bind PagedDataSource into Repeater
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

        /*private void BindOrders()
        {
            // Assuming you have a database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Fetch the orders from your database, adjust the SQL query as per your database schema.
                string query = "SELECT id_order, order_date, total, status, payment_methodID, addressID FROM orders";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable ordersTable = new DataTable();
                    adapter.Fill(ordersTable);

                    // Bind the orders to the Repeater1 control.
                    Repeater1.DataSource = ordersTable;
                    Repeater1.DataBind();
                }
            }
        }*/

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Get the order ID from the data item in the Repeater1 control.
                DataRowView orderRow = e.Item.DataItem as DataRowView;
                int orderID = (int)orderRow["id_order"];

                // Find the Repeater2 control inside the current Repeater1 item.
                Repeater Repeater2 = e.Item.FindControl("Repeater2") as Repeater;

                // Call the DisplayProducts method to populate products for the current order.
                DisplayProducts(orderID, Repeater2);
            }
        }

        private void DisplayProducts(int id_encomenda, Repeater Repeater2)
        {
            // Assuming you have a database connection string.
            string connectionString = ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Modify the SQL query to select all products in the shopping cart for the specified order.
                string query = "SELECT products.name, (products.price * cart.quantity) AS subtotal, brands.brand_name AS marca_nome " +
                    "FROM cart " +
                    "INNER JOIN products ON cart.productID = products.id_products " +
                    "INNER JOIN brands ON products.brand = brands.id_brand " +
                    "WHERE cart.orderID = @id_order";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_order", id_encomenda);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable productsTable = new DataTable();
                    adapter.Fill(productsTable);

                    // Bind the product details to the Repeater2 control inside the accordion.
                    Repeater2.DataSource = productsTable;
                    Repeater2.DataBind();
                }
            }
        }
    }
}