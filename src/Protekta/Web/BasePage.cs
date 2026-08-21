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
                return Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
            }
            set
            {
                Session["UsuarioLogueado"] = value;
            }
        }

        protected override void InitializeCulture()
        {
            string language = "en-US";

            if (Session["UsuarioIdioma"] != null)
            {
                language = Session["UsuarioIdioma"].ToString();
            }

            CultureInfo culture = CultureInfo.GetCultureInfo(language);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.InitializeCulture();
        }
    }
}