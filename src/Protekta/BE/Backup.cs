using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Backup
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreArchivo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
