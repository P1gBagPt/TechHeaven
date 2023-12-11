using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechHeaven
{
    public partial class master_page_admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogged"] == null || (int)Session["role"] != 1)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                lbl_username.Text = Session["user_username"].ToString();
                lbl_username2.Text = Session["user_username"].ToString();
            }
        }

       

        protected void lb_logout_Command(object sender, CommandEventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("main_page.aspx");
        }
    }
}