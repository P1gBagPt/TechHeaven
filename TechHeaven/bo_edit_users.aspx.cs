using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public class User
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public int nif { get; set; }
        public bool newsletter { get; set; }
        public bool tfa { get; set; }
        public bool verify { get; set; }
        public int role { get; set; }
    }
    public partial class bo_edit_users : System.Web.UI.Page
    {
        public static int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["userId"] != null)
                {
                    try
                    {
                        userId = Convert.ToInt32(Request.QueryString["userId"]);
                        User utilizador = GetUserDetails(userId);

                        if (utilizador != null)
                        {
                            tb_firstName.Text = utilizador.firstName;
                            tb_lastName.Text = utilizador.lastName;
                            tb_username.Text = utilizador.username;

                            tb_email.Text = utilizador.email;
                            tb_phone.Text = utilizador.phone;
                            tb_nif.Text = utilizador.nif.ToString();

                            DropDownList1.SelectedValue = utilizador.role.ToString();


                            if (utilizador.verify == false)
                            {
                                RadioButtonList1.SelectedValue = "0"; // Set to "Disable"
                            }
                            else
                            {
                                RadioButtonList1.SelectedValue = "1"; // Set to "Enable"
                            }

                            if (utilizador.newsletter == false)
                            {
                                RadioButtonList2.SelectedValue = "0"; // Set to "Disable"
                            }
                            else
                            {
                                RadioButtonList2.SelectedValue = "1"; // Set to "Enable"
                            }


                        }
                        else
                        {
                            lbl_erro.Text = "User not found!";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                            btn_edit.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lbl_erro.Text = ex.Message;
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                        btn_edit.Enabled = false;
                    }

                }
                else
                {
                    lbl_erro.Text = "User ID not received!";
                    btn_edit.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        private User GetUserDetails(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;
            User utilizador = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT u.id, u.firstName, u.lastName, u.email, u.username, u.phoneNumber, u.NIF, u.verify, u.tfa, u.newsletter, u.roleId FROM users u WHERE u.id = @userId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        utilizador = new User
                        {
                            userId = Convert.ToInt32(reader["id"]),
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            email = reader["email"].ToString(),
                            username = reader["username"].ToString(),
                            phone = reader["phoneNumber"] == DBNull.Value ? string.Empty : reader["phoneNumber"].ToString(),
                            nif = reader["NIF"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NIF"]),
                            verify = reader["verify"] == DBNull.Value ? false : Convert.ToBoolean(reader["verify"]),
                            tfa = reader["tfa"] == DBNull.Value ? false : Convert.ToBoolean(reader["tfa"]),
                            newsletter = reader["newsletter"] == DBNull.Value ? false : Convert.ToBoolean(reader["newsletter"]),
                            role = Convert.ToInt32(reader["roleId"]),
                        };

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }
            }

            return utilizador;
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "edit_user";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@userId", userId);
            myCommand.Parameters.AddWithValue("@firstName", tb_firstName.Text);
            myCommand.Parameters.AddWithValue("@lastName", tb_lastName.Text);
            myCommand.Parameters.AddWithValue("@email", tb_email.Text);
            myCommand.Parameters.AddWithValue("@username", tb_username.Text);
            myCommand.Parameters.AddWithValue("@phone", tb_phone.Text);
            myCommand.Parameters.AddWithValue("@nif", Convert.ToInt32(tb_nif.Text));

            if (RadioButtonList1.SelectedValue == "0")
            {
                myCommand.Parameters.AddWithValue("@verify", false);
            }
            else
            {
                myCommand.Parameters.AddWithValue("@verify", true);
            }


            if (RadioButtonList2.SelectedValue == "0")
            {
                myCommand.Parameters.AddWithValue("@newsletter", false);
            }
            else
            {
                myCommand.Parameters.AddWithValue("@newsletter", true);
            }

            myCommand.Parameters.AddWithValue("@role", DropDownList1.SelectedValue);


            myConn.Open();
            myCommand.ExecuteNonQuery();

            lbl_erro.Text = "Client updated successfully!";
            lbl_erro.ForeColor = System.Drawing.Color.Green;

            myConn.Close();
        }
    }
}