using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class PermisoManager
    {
        public Permiso Obtener(string nombre)
        {
            return PermisoMapper.Instance.ObtenerPorNombre(nombre);
        }
    }
}
