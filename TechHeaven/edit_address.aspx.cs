using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TechHeaven.account;
using System.Configuration;

namespace TechHeaven
{
    public partial class edit_address : System.Web.UI.Page
    {
        public static int addressId;
        public static int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["addressId"] != null)
                {
                    userID = Convert.ToInt32(Session["userId"].ToString());
                    addressId = Convert.ToInt32(Request.QueryString["addressId"]);

                    string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("user_address", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@userID", userID);
                            command.Parameters.AddWithValue("@addressID", addressId);

                            // Output parameters for retrieving data
                            command.Parameters.Add("@return_name", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_phone", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_address", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_floor", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_city", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_location", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_zipcode", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;

                            connection.Open();
                            command.ExecuteNonQuery();

                            // Check if data was retrieved successfully
                            if (command.Parameters["@return_name"].Value != DBNull.Value)
                            {
                                // Populate TextBoxes with retrieved data
                                address_name.Text = command.Parameters["@return_name"].Value.ToString();
                                address_phone.Text = command.Parameters["@return_phone"].Value.ToString();
                                address_address.Text = command.Parameters["@return_address"].Value.ToString();
                                address_floor.Text = command.Parameters["@return_floor"].Value.ToString();
                                address_city.Text = command.Parameters["@return_city"].Value.ToString();
                                address_location.Text = command.Parameters["@return_location"].Value.ToString();
                                address_zipcode.Text = command.Parameters["@return_zipcode"].Value.ToString();
                            }
                            else
                            {
                                // Handle the case where no data is found
                                lbl_erro.Text = "Address data not found for the specified user and address ID.";
                                lbl_erro.Visible = true;
                            }
                        }
                    }
                }
            }
        }


        protected void submit_address_Click(object sender, EventArgs e)
        {

            // Get the values entered in the form
            string name = address_name.Text;
            string phone = address_phone.Text;
            string address = address_address.Text;
            string floor = address_floor.Text;
            string city = address_city.Text;
            string location = address_location.Text;
            string zipcode = address_zipcode.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("update_address", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userID", userID);
                        command.Parameters.AddWithValue("@addressID", addressId);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@floor", floor);
                        command.Parameters.AddWithValue("@city", city);
                        command.Parameters.AddWithValue("@location", location);
                        command.Parameters.AddWithValue("@zipcode", zipcode);

                        connection.Open();
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        connection.Close();

                        // Optionally, you can retrieve the updated data
                        // SqlDataReader reader = command.ExecuteReader();
                        // if (reader.Read())
                        // {
                        //     // Access updated data if needed
                        // }

                        // Optionally, close the reader if used
                        // reader.Close();
                    }
                }

                // Optionally, redirect to another page or show a success message
                Response.Redirect("account.aspx");
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message; 
                lbl_erro.Visible = true;
            }


        }
    }
}