using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class bo_sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Fetch and display the list of orders when the page loads.
                BindOrders();
            }
        }


        private void BindOrders()
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
        }

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