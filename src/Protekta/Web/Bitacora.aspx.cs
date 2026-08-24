using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web
{
    public partial class Bitacora : BasePage
    {
        private BitacoraManager manager = new BitacoraManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarExisteSesionLogin();

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
