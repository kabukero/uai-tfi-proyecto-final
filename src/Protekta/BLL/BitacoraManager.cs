using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BitacoraManager
    {
        private BitacoraMapper mapper = new BitacoraMapper();

        public void Alta(Bitacora bitacora)
        {
            mapper.Alta(bitacora);
        }

        public List<Bitacora> Obtener()
        {
            return mapper.Obtener();
        }
    }
}
