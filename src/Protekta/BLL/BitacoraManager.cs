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
        private BitacoraMapper mapper = BitacoraMapper.Instance;

        public int Alta(Bitacora bitacora)
        {
            return mapper.Alta(bitacora);
        }

        public List<Bitacora> Obtener()
        {
            return mapper.Obtener();
        }
    }
}
