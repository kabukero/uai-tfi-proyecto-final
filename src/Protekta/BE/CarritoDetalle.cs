using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CarritoDetalle
    {
        public int Id { get; set; }
        public Subscripcion Subscripcion { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
    }
}
