using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class bo_newsletter : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 10;
        public static string search;
        public static string query = @"
            SELECT
                n.id_newsletter,
                n.title,
                n.news,
                n.creation_date,
                n.status,
                u.firstName AS fistNameUser,
                u.lastName AS lastNameUser
            FROM
                newsletter n
            JOIN
                users u ON n.userID = u.id;
        ";
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
            try
            {
                BindDataIntoRepeater(query);
            }
            catch (Exception ex)
            {
            }
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

        // Bind PagedDataSource into Repeater
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

            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            HandlePaging();
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

        private void GetTitleAndNews(int newsId, out string title, out string news)
        {
            title = string.Empty;
            news = string.Empty;

            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString()))
            {
                con.Open();

                string query = "SELECT title, news FROM newsletter WHERE id_newsletter = @newsId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@newsId", newsId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            title = reader["title"].ToString();
                            news = reader["news"].ToString();
                        }
                    }
                }
            }
        }


        protected void lb_send_email_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Send")
            {
                int newsId = Convert.ToInt32(e.CommandArgument);

                string title, news;
                GetTitleAndNews(newsId, out title, out news);

                // Obtenha os endereços de e-mail dos usuários com newsletter = 1
                List<string> emailList = GetEmailsForNewsletterSubscribers();

                // Componha e envie os e-mails
                foreach (string email in emailList)
                {
                    SendEmail(email, newsId);
                }
            }
        }

        private List<string> GetEmailsForNewsletterSubscribers()
        {
            List<string> emailList = new List<string>();

            using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString()))
            {
                con.Open();

                string query = "SELECT email FROM users WHERE newsletter = 1";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string email = reader["email"].ToString();
                            emailList.Add(email);
                        }
                    }
                }
            }

            return emailList;
        }

        private void SendEmail(string emailAddress, int newsId)
        {
            try
            {
                // Obter os campos title e news do registro na tabela newsletter
                string title, news;
                GetTitleAndNews(newsId, out title, out news);

                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                // Use your SMTP configuration from AppSettings
                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                mail.To.Add(new MailAddress(emailAddress));

                // Utilizar o título como assunto do e-mail
                mail.Subject = title;

                mail.IsBodyHtml = true;

                // Utilizar a notícia como corpo do e-mail
                mail.Body = news;

                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                servidor.EnableSsl = true;

                servidor.Send(mail);

                // Adicionar qualquer lógica ou registro adicional aqui
            }
            catch (Exception ex)
            {
                // Lidar com exceções, registar erros, etc.
                // Exemplo: LogException(ex);
            }
        }



        protected void lb_activate_deactivate_Command(object sender, CommandEventArgs e)
        {
            // Obtenha o ID do produto a partir dos dados do item atual (usando Eval, por exemplo).
            //int produtoID = Convert.ToInt32(Eval("id_produto"));
            if (e.CommandName == "AtivarDesativar")
            {
                int produtoID = Convert.ToInt32(e.CommandArgument);

                // Crie uma conexão com o banco de dados.
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString()))
                {
                    // Abra a conexão.
                    con.Open();

                    // Consulta SQL para atualizar o estado do produto.
                    string querySet = "UPDATE newsletter SET status = CASE WHEN status = 1 THEN 0 ELSE 1 END WHERE id_newsletter = @newsletterID";

                    // Crie e configure o comando SQL.
                    using (SqlCommand cmd = new SqlCommand(querySet, con))
                    {
                        cmd.Parameters.AddWithValue("@newsletterID", produtoID);

                        // Execute a consulta.
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verifique se a consulta foi executada com sucesso.
                        if (rowsAffected > 0)
                        {
                            // Atualize a interface do usuário para refletir a mudança no estado do produto, se necessário.
                            BindDataIntoRepeater(query);

                        }
                    }
                }
            }
        }
    }
}