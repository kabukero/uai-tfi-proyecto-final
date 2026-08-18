using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class IdiomaManager
    {
        private IdiomaMapper mapper = new IdiomaMapper();

        public List<Idioma> Obtener()
        {
            return mapper.Obtener();
        }
    }
}
