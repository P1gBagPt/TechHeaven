using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class add_card : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_card_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "insert_card";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["userId"]));
                myCommand.Parameters.AddWithValue("@name", card_name.Text);
                myCommand.Parameters.AddWithValue("@number", card_number.Text);
                myCommand.Parameters.AddWithValue("@cvv", Convert.ToInt32(cvv.Text));
                myCommand.Parameters.AddWithValue("@valid", Convert.ToString(expiration.Text));
                myCommand.Parameters.AddWithValue("@cardTypeID", DropDownList1.SelectedValue);
               


                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();

                Response.Redirect("account.aspx");

            }
            catch (Exception ex)
            {
                lbl_erro.Visible = true;
                lbl_erro.Text = ex.ToString();
            }
        }
    }
}