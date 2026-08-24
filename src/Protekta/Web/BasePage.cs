using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using BE;
using BLL;

namespace Web
{
    public class BasePage : Page
    {
        public Usuario UsuarioLogueado
        {
            get
            {
                return Session["UsuarioLogueado"] == null ? null : (Usuario)Session["UsuarioLogueado"];
            }
            set
            {
                Session["UsuarioLogueado"] = value;
            }
        }

        protected void ValidarExisteSesionLogin()
        {
            if (UsuarioLogueado == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected override void InitializeCulture()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo(Session["UsuarioIdioma"] == null ? SistemaConfiguracion.IdiomaPredeterminado : Session["UsuarioIdioma"].ToString());
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            base.InitializeCulture();
        }
    }
}
