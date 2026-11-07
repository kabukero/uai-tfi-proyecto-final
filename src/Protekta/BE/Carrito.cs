using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Carrito
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<CarritoDetalle> Detalles { get; set; } = new List<CarritoDetalle>();
        public Usuario Usuario { get; set; }
        public string UsuarioSessionId { get; set; }
    }
}
