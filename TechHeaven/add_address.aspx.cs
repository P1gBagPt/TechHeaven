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

            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "insert_address";

                myCommand.Connection = myConn;


                myCommand.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["userId"]));
                myCommand.Parameters.AddWithValue("@name", address_name.Text);
                myCommand.Parameters.AddWithValue("@address", address_address.Text);
                myCommand.Parameters.AddWithValue("@floor", address_floor.Text);
                myCommand.Parameters.AddWithValue("@zipcode", address_zipcode.Text);
                myCommand.Parameters.AddWithValue("@location", address_location.Text);
                myCommand.Parameters.AddWithValue("@city", address_city.Text);
                myCommand.Parameters.AddWithValue("@phone", address_phone.Text);

               
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();

                Response.Redirect("account.aspx");

            }
            catch (Exception ex)
            {
                lbl_erro.Visible = true;
                lbl_erro.Text = ex.ToString();
            }





        }
    }
}