using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TechHeaven.bo_edit_categories;

namespace TechHeaven
{
    public partial class bo_edit_brands : System.Web.UI.Page
    {
        public class Brand
        {
            public int brandID { get; set; }
            public string brand_name { get; set; }

        }

        public static int brandiesID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //brandId
            if (!IsPostBack)
            {

                if (Request.QueryString["brandId"] != null)
                {
                    try
                    {
                        brandiesID = Convert.ToInt32(Request.QueryString["brandId"]);
                        Brand brand = GetBrandsDetails(brandiesID);

                        if (brand != null)
                        {
                            tb_brand_name.Text = brand.brand_name;
                        }
                        else
                        {
                            lbl_erro.Text = "Brand not found!";
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
                    lbl_erro.Text = "Brand ID not received!";
                    btn_edit.Enabled = false;
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private Brand GetBrandsDetails(int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString;
            Brand brands = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT b.id_brand, b.brand_name FROM brands b WHERE b.id_brand = @brandId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@brandId", userID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        brands = new Brand
                        {
                            brandID = Convert.ToInt32(reader["id_brand"]),
                            brand_name = reader["brand_name"].ToString(),

                        };

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }
            }

            return brands;
        }


        protected void btn_edit_Click(object sender, EventArgs e)
        {
            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("edit_brand", myConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@brandId", brandiesID); // Assuming brandiesID is declared and has a value
                    myCommand.Parameters.AddWithValue("@brand_name", tb_brand_name.Text);

                    SqlParameter valor = new SqlParameter("@retorno", SqlDbType.Int);
                    valor.Direction = ParameterDirection.Output;
                    myCommand.Parameters.Add(valor);

                    try
                    {
                        myConn.Open();
                        myCommand.ExecuteNonQuery();

                        int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                        if (resposta == 0)
                        {
                            lbl_erro.Text = "Brand with that name already exists!";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (resposta == 1)
                        {
                            lbl_erro.Text = "Brand updated successfully!";
                            lbl_erro.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, log, or display an error message
                        lbl_erro.Text = "An error occurred: " + ex.Message;
                        lbl_erro.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }


    }
}