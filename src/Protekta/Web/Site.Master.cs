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
            // validar si el usuario esta logueado
            if (Session["UsuarioLogueado"] == null)
            {
                return;
            }

            // validar roles y permisos del usuario logueado
            bool MostrarBitacoraLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.GESTION_BITACORA);
            bool MostrarCarritoLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_ORDEN);
            bool MostrarProveedorLink  = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_PROVEEDOR);
            bool MostrarClienteLink  = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_CLIENTE);
            bool MostrarIncidenteLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_INCIDENTE);
            bool MostrarInventarioLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_INVENTARIO);
            bool MostrarBackupLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.GESTION_BACKUP);

            HyperLinkBitacora.Visible = MostrarBitacoraLink;
            HyperLinkCarrito.Visible = MostrarCarritoLink;
            HyperLinkProveedor.Visible = MostrarProveedorLink;
            HyperLinkCliente.Visible = MostrarClienteLink;
            HyperLinkIncidentes.Visible = MostrarIncidenteLink;
            HyperLinkInventario.Visible = MostrarInventarioLink;
            HyperLinkBackup.Visible = MostrarBackupLink;

            //HyperLinkCarrito.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_ORDEN);
            //HyperLinkProveedor.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_PROVEEDOR);
            //HyperLinkCliente.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_CLIENTE);
            //HyperLinkIncidentes.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_INCIDENTE);
            //HyperLinkInventario.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_INVENTARIO);
            //HyperLinkBackup.Visible = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.GESTION_BACKUP);
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
            // cargar el dropdownlist idiomas con los lenguajes disponibles en el sistema
            List<Idioma> items = manager.Obtener();
            ddlLanguage.DataSource = items;
            ddlLanguage.DataTextField = "Nombre";
            ddlLanguage.DataValueField = "Codigo";
            ddlLanguage.DataBind();
            ddlLanguage.SelectedValue = Session["UsuarioIdioma"] == null ? SistemaConfiguracion.IdiomaPredeterminado : Session["UsuarioIdioma"].ToString();
        }
    }
}
