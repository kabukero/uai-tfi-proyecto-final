using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;

namespace Web
{
    public class BasePage : Page
    {
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