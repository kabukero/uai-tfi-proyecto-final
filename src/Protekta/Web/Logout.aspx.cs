using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;

namespace Web
{
    public partial class Logout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Resetear variable de sesion usuario logueado
            UsuarioLogueado = null;

            // Configurar idioma predeterminado nuevamente
            Session["UsuarioIdioma"] = SistemaConfiguracion.IdiomaPredeterminado;

            // Redireccionar a la pagina home
            Response.Redirect("~/Default.aspx");
        }
    }
}