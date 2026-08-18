using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }

            SalirDeLaPagina();
        }

        private void SalirDeLaPagina()
        {
            string urlRegreso = Request.QueryString["ReturnUrl"];
            if (string.IsNullOrEmpty(urlRegreso))
            {
                // No hay Url especificada, se redirecciona a la página principal
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect(urlRegreso);
            }
        }
    }
}
