using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Carrito Carrito { get; set; }
        public Usuario Usuario { get; set; }
        public MedioPago MedioPago { get; set; }
        public List<FacturaDetalle> Detalles { get; set; }
    }
}
