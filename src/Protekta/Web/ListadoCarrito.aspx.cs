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
    public partial class ListadoCarrito : BasePage
    {
        CarritoManager manager = new CarritoManager();
        MedioPagoManager medioPagoManager = new MedioPagoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsuarioLogueado == null)
                Response.Redirect("Default.aspx");

            if (!IsPostBack)
            {
                CargarGvCarrito();
                CargarMedioPagoss();
            }
        }

        private void CargarMedioPagoss()
        {
            ddlMedioPago.DataSource = medioPagoManager.Obtener();
            ddlMedioPago.DataValueField = "Id";
            ddlMedioPago.DataTextField = "Nombre";
            ddlMedioPago.DataBind();
        }

        private void CargarGvCarrito()
        {
            Carrito carrito = manager.ObtenerCarrito(UsuarioLogueado.Id, Session.SessionID);
            if (carrito != null && carrito.Detalles != null && carrito.Detalles.Count > 0)
            {
                GridViewCarrito.DataSource = carrito.Detalles;
                GridViewCarrito.DataBind();
                GridViewCarrito.Visible = true;
                LblTotal.Text = "Total: $" + carrito.Detalles.Sum(d => d.Precio);
                LblTotal.Visible = true;
                BtnFinalizarCompra.Visible = true;
                LblResultado.Visible = false;
            }
            else
            {
                GridViewCarrito.Visible = false;
                LblTotal.Visible = false;
                BtnFinalizarCompra.Visible = false;
                LblResultado.Visible = true;
            }
        }

        protected void BtnFinalizarCompra_Click(object sender, EventArgs e)
        {
            Carrito carrito = manager.ObtenerCarrito(UsuarioLogueado.Id, Session.SessionID);
            MedioPago medioPago = new MedioPago() { Id = int.Parse(ddlMedioPago.SelectedValue.ToString()) };
            int facturaId = manager.FinalizarCompra(carrito, medioPago);
            if (carrito != null && facturaId != 0)
            {
                Response.Redirect("~/ListadoFactura.aspx");
            }
            else
            {
                // Mostrar que hubo error
                FailureText.Text = "Ocurrió un error al generar la factura";
                ErrorMessage.Visible = true;
            }
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "EliminarDetalle")
                return;
            CarritoDetalle detalle = new CarritoDetalle()
            {
                Id = Convert.ToInt32(e.CommandArgument)
            };
            manager.EliminarDetalle(detalle);
            CargarGvCarrito();
        }
    }
}
