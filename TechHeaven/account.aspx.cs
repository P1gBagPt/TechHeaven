using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Serilog;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections;

namespace TechHeaven
{
    public partial class account : System.Web.UI.Page
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);

        //Wishlist
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 10;
        public static string query;
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["isLogged"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    LoadUserInfo();

                }
            }
            LoadUserInfo();
        }

        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoRepeater(query);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater(query);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater(query);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater(query);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater(query);
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = Color.FromName("#0398fc");
            lnkPage.ForeColor = Color.White;
        }

        public class addresses
        {
            public int id { get; set; }
            public string name { get; set; }

            public string address { get; set; }

            public string floor { get; set; }

            public string zipcode { get; set; }

            public string location { get; set; }

            public string city { get; set; }

            public string phone { get; set; }
        }

        public class cards
        {
            public int id { get; set; }

            public string name { get; set; }

            public string number { get; set; }

            public int cvv { get; set; }

            public string valid { get; set; }

            public int cardTypeID { get; set; }

            public int userId { get; set; }
            // Adicione a propriedade para armazenar o nome do tipo de cartão
            public string cardTypeName { get; set; }
        }




        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                //user_info_update
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "user_info_update";
                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userId", Session["userId"]);
                myCommand.Parameters.AddWithValue("@firstName", tb_first_name.Text);
                myCommand.Parameters.AddWithValue("@lastName", tb_last_name.Text);
                myCommand.Parameters.AddWithValue("@phoneNumber", tb_phoneNumber.Text);
                myCommand.Parameters.AddWithValue("@nif", Convert.ToInt32(tb_NIF.Text));


                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();


                lbl_sucesso.Enabled = true;
                lbl_sucesso.Visible = true;

                lbl_sucesso.Text = "Information updated successfully";
                lbl_sucesso.ForeColor = Color.Green;

            }
            catch (Exception ex)
            {
                lbl_sucesso.Text = ex.ToString();
            }
        }


        protected void lb_save_tfa_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "tfa")
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "update_tfa";

                if (lb_save_tfa.Text == "Setup")
                {
                    myCommand.Parameters.AddWithValue("@tfa", true);
                    lb_save_tfa.Text = "Remove";
                    lb_save_tfa.ForeColor = Color.Red;


                }
                else if (lb_save_tfa.Text == "Remove")
                {
                    myCommand.Parameters.AddWithValue("@tfa", false);
                    lb_save_tfa.Text = "Setup";
                    lb_save_tfa.ForeColor = Color.White;

                }

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["userId"]));

                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        protected void lb_save_news_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "news")
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "update_newsletter";

                if (lb_save_tfa.Text == "Setup")
                {
                    myCommand.Parameters.AddWithValue("@newsletter", true);
                    lb_save_news.Text = "Disable";
                    lb_save_news.ForeColor = Color.Red;


                }
                else if (lb_save_tfa.Text == "Remove")
                {
                    myCommand.Parameters.AddWithValue("@newsletter", false);
                    lb_save_news.Text = "Enable";
                    lb_save_news.ForeColor = Color.White;

                }

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["UserId"]));

                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        private void LoadUserInfo()
        {
            if (Session["userId"] != null)
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "user_info";
                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@userId", Session["userId"]);

                SqlParameter return_firstName = new SqlParameter();
                return_firstName.ParameterName = "@return_firstName";
                return_firstName.Direction = ParameterDirection.Output;
                return_firstName.SqlDbType = SqlDbType.VarChar;
                return_firstName.Size = 50;
                myCommand.Parameters.Add(return_firstName);


                SqlParameter return_lastName = new SqlParameter();
                return_lastName.ParameterName = "@return_lastName";
                return_lastName.Direction = ParameterDirection.Output;
                return_lastName.SqlDbType = SqlDbType.VarChar;
                return_lastName.Size = 50;
                myCommand.Parameters.Add(return_lastName);

                SqlParameter return_username = new SqlParameter();
                return_username.ParameterName = "@return_username";
                return_username.Direction = ParameterDirection.Output;
                return_username.SqlDbType = SqlDbType.VarChar;
                return_username.Size = 10;
                myCommand.Parameters.Add(return_username);



                SqlParameter return_phone = new SqlParameter();
                return_phone.ParameterName = "@return_phone";
                return_phone.Direction = ParameterDirection.Output;
                return_phone.SqlDbType = SqlDbType.VarChar;
                return_phone.Size = 100;
                myCommand.Parameters.Add(return_phone);


                SqlParameter return_nif = new SqlParameter();
                return_nif.ParameterName = "@return_nif";
                return_nif.Direction = ParameterDirection.Output;
                return_nif.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(return_nif);


                SqlParameter return_2fa = new SqlParameter();
                return_2fa.ParameterName = "@return_2fa";
                return_2fa.Direction = ParameterDirection.Output;
                return_2fa.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(return_2fa);


                SqlParameter return_newsletter = new SqlParameter();
                return_newsletter.ParameterName = "@return_newsletter";
                return_newsletter.Direction = ParameterDirection.Output;
                return_newsletter.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(return_newsletter);

                SqlParameter return_balance = new SqlParameter();
                return_balance.ParameterName = "@return_balance";
                return_balance.Direction = ParameterDirection.Output;
                return_balance.SqlDbType = SqlDbType.Decimal;
                myCommand.Parameters.Add(return_balance);


                SqlParameter return_total_address = new SqlParameter();
                return_total_address.ParameterName = "@return_total_address";
                return_total_address.Direction = ParameterDirection.Output;
                return_total_address.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(return_total_address);

                SqlParameter return_total_cards = new SqlParameter();
                return_total_cards.ParameterName = "@return_total_cards";
                return_total_cards.Direction = ParameterDirection.Output;
                return_total_cards.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(return_total_cards);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@return";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);


                myConn.Open();
                myCommand.ExecuteNonQuery();


                int respostaSP = Convert.ToInt32(myCommand.Parameters["@return"].Value);

                string respostafirstName = myCommand.Parameters["@return_firstName"].Value.ToString();
                string respostalastName = myCommand.Parameters["@return_lastName"].Value.ToString();
                string respostausername = myCommand.Parameters["@return_username"].Value.ToString();
                string respostaPhonenumber = myCommand.Parameters["@return_phone"].Value.ToString();

                object resposta_nifValue = myCommand.Parameters["@return_nif"].Value;

                int resposta_nif;
                if (resposta_nifValue != DBNull.Value)
                {
                    resposta_nif = Convert.ToInt32(resposta_nifValue);
                }
                else
                {
                    // Handle the case where the database value is NULL
                    resposta_nif = 0; // Set a default value or handle it as per your application logic
                }

                object tfa2Value = myCommand.Parameters["@return_2fa"].Value;
                bool tfa2;
                if (tfa2Value != DBNull.Value)
                {
                    tfa2 = Convert.ToBoolean(tfa2Value);
                }
                else
                {
                    // Handle the case where the database value is NULL
                    tfa2 = false; // Set a default value or handle it as per your application logic
                }

                object newsletterValue = myCommand.Parameters["@return_newsletter"].Value;
                bool respostaNewsletter;
                if (newsletterValue != DBNull.Value)
                {
                    respostaNewsletter = Convert.ToBoolean(newsletterValue);
                }
                else
                {
                    // Handle the case where the database value is NULL
                    respostaNewsletter = false; // Set a default value or handle it as per your application logic
                }

                decimal balanceValue = Convert.ToDecimal(myCommand.Parameters["@return_balance"].Value);

                int respostaAddress = Convert.ToInt32(myCommand.Parameters["@return_total_address"].Value);
                int respostaCards = Convert.ToInt32(myCommand.Parameters["@return_total_cards"].Value);


                myConn.Close();

                if (respostaAddress == 4)
                {
                    panel_add_address.Visible = false;
                }

                if (respostaCards == 5)
                {
                    panel2.Visible = false;
                }

                lbl_nameTop.Text = respostafirstName + " " + respostalastName + " Profile";
                lbl_balance.Text = balanceValue.ToString() + " €";
                tb_first_name.Text = respostafirstName;
                tb_last_name.Text = respostalastName;
                lbl_username.Text = respostausername;
                lbl_email.Text = Session["user_email"].ToString();
                tb_phoneNumber.Text = respostaPhonenumber;
                tb_NIF.Text = resposta_nif.ToString();

                if (respostaNewsletter)
                {
                    lb_save_news.Text = "Disable";
                    lb_save_news.ForeColor = Color.Red;
                }
                else
                {
                    lb_save_news.Text = "Enable";
                    lb_save_news.ForeColor = Color.White;
                }

                if (!tfa2)
                {
                    lb_save_tfa.Text = "Setup";
                    lb_save_tfa.ForeColor = Color.White;
                }
                else
                {
                    lb_save_tfa.Text = "Remove";
                    lb_save_tfa.ForeColor = Color.Red;
                }

                LoadUserAddresses();
                LoadUserCards();
                LoadUserWishlist();
            }
        }

        private void LoadUserWishlist()
        {
            try
            {
                string query = "SELECT w.*, p.name AS productName, p.price AS productPrice, p.image AS productImage, p.contenttype AS productImageContentType " +
                               "FROM wishlist w " +
                               "JOIN products p ON w.productID = p.id_products " +
                               "WHERE w.userID = " + Session["userId"].ToString();

                BindDataIntoRepeater(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void BindDataIntoRepeater(string query)
        {
            var dt = GetDataFromDb(query);
            _pgsource.DataSource = dt.DefaultView;
            _pgsource.AllowPaging = true;
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _pgsource.PageCount;
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;

            Repeater3.DataSource = _pgsource;
            Repeater3.DataBind();

            HandlePaging();
        }

        static DataTable GetDataFromDb(string query)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString());

            var da = new SqlDataAdapter(query, con);
            var dt = new DataTable();

            try
            {
                con.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        private void LoadUserAddresses()
        {
            try
            {
                List<addresses> lst_moradas = new List<addresses>();
                string query = "SELECT * FROM addresses WHERE status = 1 AND addresses.user_id = " + Session["userId"].ToString();

                using (SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCommand2 = new SqlCommand(query, myConn2))
                    {
                        myConn2.Open(); // Open the connection

                        using (SqlDataReader dr = myCommand2.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var moradas_address = new addresses();

                                moradas_address.id = Convert.ToInt32(dr["id_addresses"]);
                                moradas_address.name = dr["name"].ToString();
                                moradas_address.address = dr["address"].ToString();
                                moradas_address.floor = dr["floor"] is DBNull ? null : dr["floor"].ToString(); // Handle DBNull for nullable types
                                moradas_address.zipcode = dr["zipcode"].ToString();
                                moradas_address.location = dr["location"].ToString();
                                moradas_address.city = dr["city"].ToString();
                                moradas_address.phone = dr["phone"].ToString();

                                lst_moradas.Add(moradas_address);
                            }
                        }
                    }
                }

                Repeater1.DataSource = lst_moradas;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        private void LoadUserCards()
        {
            try
            {
                List<cards> lst_cards = new List<cards>();
                string query = "SELECT c.*, ct.name AS cardTypeName FROM cards c " +
                                       "JOIN card_type ct ON c.cardTypeID = ct.id_type_card " +
                                       "WHERE c.status = 1 AND c.userId = " + Session["userId"].ToString();

                using (SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    using (SqlCommand myCommand2 = new SqlCommand(query, myConn2))
                    {
                        myConn2.Open(); // Open the connection

                        using (SqlDataReader dr = myCommand2.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var cards_users = new cards();

                                cards_users.id = Convert.ToInt32(dr["id_card"]);
                                cards_users.name = dr["name"].ToString();
                                cards_users.number = dr["number"].ToString();
                                cards_users.cvv = Convert.ToInt32(dr["cvv"]);
                                cards_users.valid = dr["valid"].ToString();
                                cards_users.cardTypeID = Convert.ToInt32(dr["cardTypeID"]);
                                cards_users.userId = Convert.ToInt32(dr["userId"]);

                                cards_users.cardTypeName = dr["cardTypeName"].ToString();

                                lst_cards.Add(cards_users);
                            }
                        }
                    }
                }

                Repeater2.DataSource = lst_cards;
                Repeater2.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        public class WishlistItem
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int UserId { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
            public byte[] ProductImage { get; set; }
            public string ProductImageContentType { get; set; }
        }







        protected void lb_delete_address_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_address")
            {
                int addressId = Convert.ToInt32(e.CommandArgument);
                Console.WriteLine("Address ID: " + addressId); // Add this line
                // Crie uma conexão com o banco de dados.
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    // Abra a conexão.
                    con.Open();

                    // Consulta SQL para atualizar o estado do produto.
                    string query = "UPDATE addresses SET status = CASE WHEN status = 1 THEN 0 ELSE 1 END WHERE id_addresses = @addressID";

                    // Crie e configure o comando SQL.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@addressID", addressId);


                        // Execute a consulta.
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verifique se a consulta foi executada com sucesso.
                        if (rowsAffected > 0)
                        {
                            // Atualize a interface do usuário para refletir a mudança no estado do produto, se necessário.
                            LoadUserAddresses();

                        }
                    }
                }
            }
        }

        protected void lb_edit_address_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lb_delete_card_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_card")
            {
                int cardId = Convert.ToInt32(e.CommandArgument);
                Console.WriteLine("Address ID: " + cardId); // Add this line
                // Crie uma conexão com o banco de dados.
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TecHeavenConnectionString"].ConnectionString))
                {
                    // Abra a conexão.
                    con.Open();

                    // Consulta SQL para atualizar o estado do produto.
                    string query = "UPDATE cards SET status = CASE WHEN status = 1 THEN 0 ELSE 1 END WHERE id_card = @cardID";

                    // Crie e configure o comando SQL.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@cardID", cardId);


                        // Execute a consulta.
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verifique se a consulta foi executada com sucesso.
                        if (rowsAffected > 0)
                        {
                            // Atualize a interface do usuário para refletir a mudança no estado do produto, se necessário.
                            LoadUserCards();

                        }
                    }
                }
            }
        }

        protected void lb_edit_card_Command(object sender, CommandEventArgs e)
        {

        }
    }
}