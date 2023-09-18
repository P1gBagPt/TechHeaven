using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            string email_user = Master.DecryptString(Request.QueryString["m_uti"]);

            MySqlConnection myConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

            MySqlCommand myCommand = new MySqlCommand();

            myCommand.Parameters.AddWithValue("@user_email", email_user);

            myCommand.CommandText = "account_activation";
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Connection = myConn;
            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();
        }
    }
}