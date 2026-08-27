using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

namespace Web
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMensajeBienvenida();
        }

        private void SetMensajeBienvenida()
        {
            if(UsuarioLogueado == null)
            {
                PnlMensajeBienvenida.Visible = false;
            }
            else
            {
                PnlMensajeBienvenida.Visible = true;
                LblMensajeBienvenida.Text = string.Format(Labels.Default_TextoBienvenida, UsuarioLogueado.ToString(), UsuarioLogueado.Permisos == null || UsuarioLogueado.Permisos.Count == 0 ? "" : UsuarioLogueado.Permisos[0].Nombre);
            }
        }
    }
}
