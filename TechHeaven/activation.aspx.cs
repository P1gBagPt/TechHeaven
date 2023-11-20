using MySql.Data.MySqlClient;
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
    public partial class activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["activation"] != null)
            {
                string email_user = Master.DecryptString(Request.QueryString["m_uti"]);

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "account_activation";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@email", email_user);

              
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
                lbl_nome.Text = email_user;
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}