using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace Web
{
    public partial class SiteMaster : MasterPage
    {
        private IdiomaManager manager = new IdiomaManager();
        private IntegridadDatosManager integridadDatosManager = new IntegridadDatosManager();
        private UsuarioManager usuarioManager = new UsuarioManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIdiomas();
            }

            // mostrar el nombre y apellido del usuario logueado
            MostrarUsuarioLogueado();

            // validar permisos para mostrar / ocultar links navegacion
            MostrarOcultarLinkNavegacion();
        }

        private void MostrarOcultarLinkNavegacion()
        {
            // validar si tiene permisos para acceder a la bitacora
            if (Session["UsuarioLogueado"] == null)
            {
                return;
            }

            HyperLinkBitacora.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.BITACORA);
            HyperLinkCarrito.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.CARRITO);
            HyperLinkProveedor.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_PROVEEDOR);
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["UsuarioIdioma"] = ddlLanguage.SelectedValue;
            Response.Redirect(Request.RawUrl);
        }

        private void MostrarUsuarioLogueado()
        {
            HyperLinkUsuarioLogin.Text = Session["UsuarioLogueado"] == null ? "" : ((Usuario)Session["UsuarioLogueado"]).ToString();
        }

        private void CargarIdiomas()
        {
            List<Idioma> items = manager.Obtener();
            ddlLanguage.DataSource = items;
            ddlLanguage.DataTextField = "Nombre";
            ddlLanguage.DataValueField = "Codigo";
            ddlLanguage.DataBind();
            ddlLanguage.SelectedValue = Session["UsuarioIdioma"] == null ? SistemaConfiguracion.IdiomaPredeterminado : Session["UsuarioIdioma"].ToString();
        }
    }
}
