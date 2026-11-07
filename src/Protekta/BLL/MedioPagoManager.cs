using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class MedioPagoManager
    {

        public List<MedioPago> Obtener()
        {
            return MedioPagoMapper.Instance.Obtener();
        }
    }
}
