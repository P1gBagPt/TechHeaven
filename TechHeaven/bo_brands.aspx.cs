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
    public partial class bo_brands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<marcas> list = new List<marcas>();

                string query = @"
SELECT b.brand_name, COUNT(p.id_products) AS total_products
FROM brands b
LEFT JOIN products p ON b.id_brand = p.brand
GROUP BY b.brand_name;";

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand(query, myConn);


                myConn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var marca = new marcas();

                    marca.nome = dr.GetString(0);
                    marca.totalMarcasProdutos = dr.GetInt32(1);

                    list.Add(marca);
                }

                myConn.Close();
                Repeater1.DataSource = list;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public class marcas
        {
            public string nome { get; set; }
            public int totalMarcasProdutos { get; set; }
        }

    }
}