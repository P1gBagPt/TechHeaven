using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace TechHeaven
{
    public partial class add_address : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogged"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }


        protected void submit_address_Click(object sender, EventArgs e)
        {
            MySqlConnection myConn = null; // Declare the MySqlConnection variable outside the try block

            try
            {
                myConn = Master.GetSetConn; // Initialize the MySqlConnection inside the try block

                MySqlCommand myCommand = new MySqlCommand();

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "insert_address";
                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@p_name", address_name.Text);
                myCommand.Parameters.AddWithValue("@p_address", address_address.Text);
                myCommand.Parameters.AddWithValue("@p_floor", address_floor.Text);
                myCommand.Parameters.AddWithValue("@p_zipcode", address_zipcode.Text);
                myCommand.Parameters.AddWithValue("@p_location", address_location.Text);
                myCommand.Parameters.AddWithValue("@p_city", address_city.Text);
                myCommand.Parameters.AddWithValue("@p_phone", address_phone.Text);
                myCommand.Parameters.AddWithValue("@p_userId", Convert.ToInt32(Session["UserId"]));

               

                // Check if the connection is closed before opening it
                if (myConn.State != ConnectionState.Open)
                {
                    myConn.Open();
                }

                myCommand.ExecuteNonQuery();

                Response.Redirect("account.aspx");

            }
            catch (Exception ex)
            {
                lbl_erro.Visible = true;
                lbl_erro.Text = ex.ToString();
            }
            finally
            {
                // Always close the connection when done
                if (myConn != null && myConn.State != ConnectionState.Closed)
                {
                    myConn.Close();
                }
            }





        }
    }
}