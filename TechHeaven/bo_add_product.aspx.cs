using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class bo_add_product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_enviar_Click(object sender, EventArgs e)
        {
            Stream imgStream = fu_imagem.PostedFile.InputStream;
            int imgTamanho = fu_imagem.PostedFile.ContentLength;

            string contentType = fu_imagem.PostedFile.ContentType;

            byte[] imgBinary = new byte[imgTamanho];

            imgStream.Read(imgBinary, 0, imgTamanho);

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "insert_product";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@name", tb_nome.Text);
            myCommand.Parameters.AddWithValue("@description", tb_descricao.Text);
            myCommand.Parameters.AddWithValue("@code", tb_numero_artigo.Text);
            myCommand.Parameters.AddWithValue("@price", tb_preco.Text);
            myCommand.Parameters.AddWithValue("@stock", tb_stock.Text);
            myCommand.Parameters.AddWithValue("@category", ddl_categoria.SelectedValue);
            myCommand.Parameters.AddWithValue("@image", imgBinary);
            myCommand.Parameters.AddWithValue("@ct", contentType);
            myCommand.Parameters.AddWithValue("@status", 1);
            myCommand.Parameters.AddWithValue("@brand", ddl_marca.SelectedValue);

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
                lbl_erro.Text = "Este produto já existe no sistema";
                lbl_erro.ForeColor = System.Drawing.Color.Red;
            }
            else if (resposta == 1)
            {
                lbl_erro.Text = "Produto registado com sucesso";
                lbl_erro.ForeColor = System.Drawing.Color.Green;
                //Response.Redirect("bo_produtos.aspx");
            }

            myConn.Close();
        }
    }
}