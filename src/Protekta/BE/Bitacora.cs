using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bitacora
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEvento { get; set; }
        public BitacoraTipoEvento BitacoraTipoEvento { get; set; }
        public Usuario Usuario { get; set; }

        public string UsuarioLogin
        {

            get
            {
                return Usuario.ToString();
            }
        }

        public string TipoEvento
        {
            get
            {
                return BitacoraTipoEvento.Nombre;
            }
        }
    }
}
