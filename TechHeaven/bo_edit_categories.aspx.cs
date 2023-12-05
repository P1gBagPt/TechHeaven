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
    public partial class bo_edit_categories : System.Web.UI.Page
    {
        public class Category
        {
            public int categorieID { get; set; }
            public string category_name { get; set; }
            
        }

        public static int categoryID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["categoryId"] != null)
                {
                    try
                    {
                        categoryID = Convert.ToInt32(Request.QueryString["categoryId"]);
                        Category utilizador = GetCategoriesDetails(categoryID);

                        if (utilizador != null)
                        {
                            tb_category_name.Text = utilizador.category_name;
                            
                        }
                        else
                        {
                            lbl_erro.Text = "Category not found!";
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
                    lbl_erro.Text = "Category ID not received!";
                    btn_edit.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        private Category GetCategoriesDetails(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;
            Category categories = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT c.id_category, c.category_name FROM categories c WHERE c.id_category = @categoryId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@categoryId", userID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        categories = new Category
                        {
                            categorieID = Convert.ToInt32(reader["id_category"]),
                            category_name = reader["category_name"].ToString(),
                            
                        };

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }
            }

            return categories;
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "edit_category";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@categoryId", categoryID);
            myCommand.Parameters.AddWithValue("@category_name", tb_category_name.Text);

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);




            myConn.Open();
            myCommand.ExecuteNonQuery();

            int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);


            if(resposta == 0)
            {
                lbl_erro.Text = "Category with that name already exists!";
                lbl_erro.ForeColor = System.Drawing.Color.Red;
            }
            else if(resposta == 1)
            {
                lbl_erro.Text = "Category updated successfully!";
                lbl_erro.ForeColor = System.Drawing.Color.Green;
            }

            

            myConn.Close();



        }
    }
}