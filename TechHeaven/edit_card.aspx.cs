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
    public partial class edit_card : System.Web.UI.Page
    {
        public static int cardId;
        public static int userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cardId"] != null)
                {
                    userID = Convert.ToInt32(Session["userId"].ToString());
                    cardId = Convert.ToInt32(Request.QueryString["cardId"]);

                    string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("user_card", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@userID", userID);
                            command.Parameters.AddWithValue("@cardID", cardId);

                            // Output parameters for retrieving data

                            command.Parameters.Add("@return_name", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_number", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_cvv", SqlDbType.Int).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_valid", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                            command.Parameters.Add("@return_cardTypeID", SqlDbType.Int).Direction = ParameterDirection.Output;


                            connection.Open();
                            command.ExecuteNonQuery();

                            // Check if data was retrieved successfully
                            if (command.Parameters["@return_name"].Value != DBNull.Value)
                            {
                                card_name.Text = command.Parameters["@return_name"].Value.ToString();
                                card_number.Text = command.Parameters["@return_number"].Value.ToString();
                                cvv.Text = command.Parameters["@return_cvv"].Value.ToString();
                                expiration.Text = command.Parameters["@return_valid"].Value.ToString();
                                DropDownList1.SelectedValue = command.Parameters["@return_cardTypeID"].Value.ToString();
                                //DropDownList1


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

        protected void submit_card_Click(object sender, EventArgs e)
        {
            string name = card_name.Text;
            string number = card_number.Text;
            string cvv3 = cvv.Text;
            string valid = Convert.ToString(expiration.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("update_card", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userID", userID);
                        command.Parameters.AddWithValue("@cardID", cardId);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@number", number);
                        command.Parameters.AddWithValue("@cvv", cvv3);
                        command.Parameters.AddWithValue("@valid", valid);
                        command.Parameters.AddWithValue("@cardTypeID", DropDownList1.SelectedValue);

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