using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class donecheckout : System.Web.UI.Page
    {
        public static int id_user;

        protected void Page_Load(object sender, EventArgs e)
        {
            id_user = Convert.ToInt32(Session["userId"].ToString());
            try
            {
                int encomenda_id = (int)Session["EncomendaID"];

                string connectionString = ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ToString();

                using (SqlConnection myConn = new SqlConnection(connectionString))
                {
                    myConn.Open();

                    // Consulta para obter o email do utilizador
                    string queryEmail = "SELECT u.email FROM users u INNER JOIN orders o ON u.id = o.userID WHERE o.id_order = @encomenda_id";
                    using (SqlCommand cmdEmail = new SqlCommand(queryEmail, myConn))
                    {
                        cmdEmail.Parameters.AddWithValue("@encomenda_id", encomenda_id);
                        string email = cmdEmail.ExecuteScalar().ToString();
                        lbl_email_utilizador.Text = email;
                        lbl_email_utilizador.ForeColor = Color.Green;
                    }

                    // Número da encomenda já disponível em encomenda_id
                    lbl_num_encomenda.Text = encomenda_id.ToString();
                    lbl_num_encomenda.ForeColor = Color.Green;
                }

            }
            catch (Exception ex)
            {
                lbl_num_encomenda.Text = ex.Message;


            }
        }
    }
}