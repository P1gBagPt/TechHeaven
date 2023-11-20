using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace TechHeaven
{
    public partial class bo_add_category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_add_category_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "add_category";

                myCommand.Connection = myConn;

                string input = tb_nome.Text.ToLower(); // Convert to lowercase
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo; // You can change "en-US" to the appropriate culture if needed
                string capitalizedInput = textInfo.ToTitleCase(input);
                myCommand.Parameters.AddWithValue("@nome", capitalizedInput);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);

                myConn.Open();
                myCommand.ExecuteNonQuery();

                int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                if (resposta == 0)
                {
                    lbl_erro.Text = "Category already exists";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
                else if (resposta == 1)
                {
                    lbl_erro.Text = "Category added successfully";
                    lbl_erro.ForeColor = System.Drawing.Color.Green;
                    //Response.Redirect("bo_produtos.aspx");
                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }
        }
    }
}