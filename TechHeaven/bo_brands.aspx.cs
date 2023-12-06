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
SELECT b.id_brand, b.brand_name, COUNT(p.id_products) AS total_products
FROM brands b
LEFT JOIN products p ON b.id_brand = p.brand
GROUP BY b.id_brand, b.brand_name;";

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand(query, myConn);


                myConn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var marca = new marcas();

                    marca.id_brand = dr.GetInt32(0);
                    marca.nome = dr.GetString(1);
                    marca.totalMarcasProdutos = dr.GetInt32(2);

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

        protected void edit_product_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int brandId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"bo_edit_brands.aspx?brandId={brandId}");
            }
        }

        public class marcas
        {
            public int id_brand {  get; set; }
            public string nome { get; set; }
            public int totalMarcasProdutos { get; set; }
        }

    }
}