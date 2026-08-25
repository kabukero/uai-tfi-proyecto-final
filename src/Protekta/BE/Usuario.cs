using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Activo { get; set; }
        public int IdIdioma { get; set; }
        public string DVH { get; set; }
        public Idioma Idioma { get; set; }
        public List<Permiso> Permisos { get; set; } = new List<Permiso>();

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
