using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class bo_add_newsletter : System.Web.UI.Page
    {
        public static int id_user;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_add_newsletter_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "insert_newsletter";

                myCommand.Connection = myConn;

                id_user = Convert.ToInt32(Session["userId"].ToString());

                myCommand.Parameters.AddWithValue("@userID", id_user);
                myCommand.Parameters.AddWithValue("@title", tb_title.Text);
                myCommand.Parameters.AddWithValue("@news", tb_news.Text);



                myConn.Open();
                myCommand.ExecuteNonQuery();

               
                myConn.Close();

                lbl_erro.Text = "Category added successfully";
                lbl_erro.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }
        }
    }
}