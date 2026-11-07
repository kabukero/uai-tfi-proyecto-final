using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;
using BE;
using BLL;
using Resources;

namespace Web
{
    public partial class _Default : BasePage
    {
        private CarritoManager carritoManager = new CarritoManager();
        private UsuarioManager usuarioManager = new UsuarioManager();
        private bool MostrarCarritoLink = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetMensajeBienvenida();

            if (!IsPostBack)
            {
                CargarPlanes();
            }

            //if(Session["UsuarioLogueado"] != null)
            //{
            //    MostrarCarritoLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_ORDEN);
            //}
        }

        public void CargarPlanes()
        {
            SubscripcionWS ws = new SubscripcionWS();
            string xmlPlanes = ws.ObtenerSubscripionesXml();
            List<Subscripcion> subscripciones = new List<Subscripcion>();

            // Utilizo el xml dentro del string para crear un documento XPath
            StringReader sr = new StringReader(xmlPlanes);
            XPathDocument xPathDocument = new XPathDocument(sr);

            XPathNavigator xNavegador = xPathDocument.CreateNavigator();
            XPathNodeIterator iterador;
            iterador = xNavegador.Select("Subscripciones/Subscripcion");

            // Reconstruyo los objetos Subscripcion iterando con el iterador de XPath
            // y obteniendo los valores de los nodos.
            while (iterador.MoveNext())
            {
                subscripciones.Add(new Subscripcion()
                {
                    Id = int.Parse(iterador.Current.SelectSingleNode("Id").Value),
                    Titulo = iterador.Current.SelectSingleNode("Titulo").Value,
                    Descripcion = iterador.Current.SelectSingleNode("Descripcion").Value,
                    Precio = decimal.Parse(iterador.Current.SelectSingleNode("Precio").Value),
                });
            }

            sr.Close();

            rptSubscripciones.DataSource = subscripciones;
            rptSubscripciones.DataBind();
        }

        private void SetMensajeBienvenida()
        {
            bool mostrarBotonComprar = false;
            if(UsuarioLogueado == null)
            {
                // Mostrar mensaje de bienvenida
                PnlMensajeBienvenida.Visible = false;
                //BtnPlanPrevencionTradicional.Visible = false;
            }
            else
            {
                // Mostrar mensaje de bienvenida
                PnlMensajeBienvenida.Visible = true;
                LblMensajeBienvenida.Text = string.Format(Labels.Default_TextoBienvenida, UsuarioLogueado.ToString(), UsuarioLogueado.Permisos == null || UsuarioLogueado.Permisos.Count == 0 ? "" : UsuarioLogueado.Permisos[0].Nombre);
                mostrarBotonComprar = true;
            }

            // Mostrar / Ocultar Botones Comprar si el usuario esta logueado o no
            //BtnPlanPrevencionTradicional.Visible = mostrarBotonComprar;
            //BtnPlanGestionDigital.Visible = mostrarBotonComprar;
            //BtnPlanPrevencionIA.Visible = mostrarBotonComprar;
        }

        protected void BtnAgregarPlan_Command(object sender, CommandEventArgs e)
        {
            Carrito carrito = carritoManager.ObtenerCarrito(UsuarioLogueado.Id, Session.SessionID);
            if (carrito == null)
            {
                carrito = new Carrito
                {
                    FechaCreacion = DateTime.Now,
                    Usuario = UsuarioLogueado,
                    UsuarioSessionId = Session.SessionID
                };
            }

            int planId = int.Parse(e.CommandArgument.ToString());
            if (!carrito.Detalles.Any(d => d.Subscripcion.Id == planId))
            {
                Subscripcion subscripcion = new Subscripcion
                {
                    Id = planId,
                    Precio = 300
                };
                CarritoDetalle detalle = new CarritoDetalle
                {
                    Subscripcion = subscripcion,
                    Cantidad = 1
                };

                carrito.Detalles.Add(detalle);
                carritoManager.AgregarDetalle(carrito);
            }

            Response.Redirect("ListadoCarrito.aspx");
        }

        protected void rptSubscripciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnAgregarPlan =  (Button)e.Item.FindControl("BtnAgregarPlan");

                if (btnAgregarPlan != null)
                {
                    if (Session["UsuarioLogueado"] != null)
                    {
                        MostrarCarritoLink = usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.ABM_ORDEN);
                    }

                    btnAgregarPlan.Visible = MostrarCarritoLink;
                }
            }
        }
    }
}
