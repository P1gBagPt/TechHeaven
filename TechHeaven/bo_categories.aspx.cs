using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class bo_categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<categorias> list = new List<categorias>();



                string query = @"
                SELECT c.category_name, COUNT(p.id_products) AS total_products
                FROM categories c
                LEFT JOIN products p ON c.id_category = p.category
                GROUP BY c.category_name;";

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand(query, myConn);


                myConn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var categoria = new categorias();

                    categoria.nome = dr.GetString(0);
                    categoria.totalProdutos = dr.GetInt32(1);

                    list.Add(categoria);
                }


                myConn.Close();
                Repeater1.DataSource = list;
                Repeater1.DataBind();


            }
            catch (Exception ex)
            {

            }

        }
    }
    public class categorias
    {
        public string nome { get; set; }
        public int totalProdutos { get; set; }
    }
}