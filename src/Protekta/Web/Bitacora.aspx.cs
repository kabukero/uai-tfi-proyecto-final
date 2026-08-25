using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace Web
{
    public partial class Bitacora : BasePage
    {
        private BitacoraManager manager = new BitacoraManager();
        private UsuarioManager usuarioManager = new UsuarioManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            // validar si el usuario esta logueado
            ValidarExisteSesionLogin();

            // validar si el usuario tiene permisos
            if(!usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.BITACORA))
            {
                Response.Redirect("Default.aspx");
            }

            if(!IsPostBack)
            {
                CargarBitacoraEventos();
            }
        }

        private void CargarBitacoraEventos()
        {
            GridViewBitacora.DataSource = manager.Obtener();
            GridViewBitacora.DataBind();
        }

        protected void GridViewBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBitacora.PageIndex = e.NewPageIndex;

            CargarBitacoraEventos();
        }
    }
}
