using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class bo_reviews : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 10;
        public static string search;
        public static string query = "SELECT r.id_review, r.title, r.review, r.classification, r.status, u.firstName, u.lastName, u.id as userID, p.name as productName, p.id_products as productID " +
                                      "FROM review_classification r " +
                                      "LEFT JOIN users u ON r.userId = u.id " +
                                      "LEFT JOIN products p ON r.productID = p.id_products";


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

        protected void lb_activate_deactivate_Command(object sender, CommandEventArgs e)
        {
            // Obtenha o ID do produto a partir dos dados do item atual (usando Eval, por exemplo).
            //int produtoID = Convert.ToInt32(Eval("id_produto"));
            if (e.CommandName == "AtivarDesativar")
            {
                int reviewID = Convert.ToInt32(e.CommandArgument);

                // Crie uma conexão com o banco de dados.
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["techeavenConnectionString"].ToString()))
                {
                    // Abra a conexão.
                    con.Open();

                    // Consulta SQL para atualizar o estado do produto.
                    string querySet = "UPDATE review_classification SET status = CASE WHEN status = 1 THEN 0 ELSE 1 END WHERE id_review = @reviewID";

                    // Crie e configure o comando SQL.
                    using (SqlCommand cmd = new SqlCommand(querySet, con))
                    {
                        cmd.Parameters.AddWithValue("@reviewID", reviewID);

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


        // Get data from database/repository
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


    }
}