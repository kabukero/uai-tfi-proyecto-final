using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using BE;

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

        protected override void InitializeCulture()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo(Session["UsuarioIdioma"] == null ? SistemaConfiguracion.IdiomaPredeterminado : Session["UsuarioIdioma"].ToString());
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            base.InitializeCulture();
        }
    }
}
