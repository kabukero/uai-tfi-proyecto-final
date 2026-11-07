using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class MedioPagoMapper
    {
        private static MedioPagoMapper _instance;

        private MedioPagoMapper()
        {

        }

        public static MedioPagoMapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MedioPagoMapper();
                }

                return _instance;
            }
        }

        public List<MedioPago> Obtener()
        {

            string query = "SELECT Id,Nombre FROM MedioPago ORDER BY Nombre";

            DataTable tabla = SqlHelper.Obtener(query, null);

            if (tabla == null || tabla.Rows.Count == 0)
                return null;

            List<MedioPago> items = new List<MedioPago>();

            foreach (DataRow fila in tabla.Rows)
            {
                MedioPago item = new MedioPago()
                {
                    Id = int.Parse(fila["Id"].ToString()),
                    Nombre = fila["Nombre"].ToString(),
                };
                items.Add(item);
            }

            return items;
        }
    }
}
