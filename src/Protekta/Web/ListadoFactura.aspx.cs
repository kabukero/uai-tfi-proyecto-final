using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace Web
{
    public partial class ListadoFactura : System.Web.UI.Page
    {
        CarritoManager manager = new CarritoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarFacturas();
            }
        }

        private void CargarFacturas()
        {
            GridViewFacturas.DataSource = manager.ObtenerFacturas();
            GridViewFacturas.DataBind();
        }

        protected void GridViewFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Factura factura = (Factura)e.Row.DataItem;
                GridView gvDetalle = (GridView)e.Row.FindControl("GridViewDetalleFactura");
                gvDetalle.DataSource = factura.Detalles;
                gvDetalle.DataBind();
            }
        }
    }
}
