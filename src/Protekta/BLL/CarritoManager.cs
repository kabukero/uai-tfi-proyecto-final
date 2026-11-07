using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class CarritoManager
    {
        public Carrito ObtenerCarrito(int usuarioId, string usuarioSessionId)
        {
            return CarritoMapper.Instance.ObtenerCarrito(usuarioId, usuarioSessionId);
        }

        public void AgregarDetalle(Carrito carrito)
        {
            CarritoMapper.Instance.AgregarDetalle(carrito);
        }

        public void EliminarDetalle(CarritoDetalle detalle)
        {
            CarritoMapper.Instance.EliminarDetalle(detalle);
        }

        public int FinalizarCompra(Carrito carrito, MedioPago medioPago)
        {
            Factura factura = new Factura
            {
                Fecha = DateTime.Now,
                Usuario = carrito.Usuario,
                Carrito = carrito,
                MedioPago = medioPago,
                Detalles = carrito.Detalles.Select(d => new FacturaDetalle
                {
                    Subscripcion = d.Subscripcion,
                    Precio = d.Precio
                }).ToList()
            };

            int facturaId = FacturaMapper.Instance.Guardar(factura);

            return facturaId;
        }

        public Factura ObtenerFactura(int Id)
        {
            return FacturaMapper.Instance.Obtener(Id);
        }

        public List<Factura> ObtenerFacturas()
        {
            return FacturaMapper.Instance.Obtener();
        }
    }
}
