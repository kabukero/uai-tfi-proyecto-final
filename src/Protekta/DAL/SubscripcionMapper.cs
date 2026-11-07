using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class SubscripcionMapper
    {
        public List<Subscripcion> ObtenerSubscripciones()
        {

            string query = "SELECT Id,Titulo,Descripcion,Precio,Activo" +
                            " FROM Subscripcion ORDER BY Titulo";

            DataTable tabla = SqlHelper.Obtener(query, null);

            if (tabla == null || tabla.Rows.Count == 0)
                return null;

            List<Subscripcion> items = new List<Subscripcion>();

            foreach (DataRow fila in tabla.Rows)
            {
                Subscripcion item = new Subscripcion()
                {
                    Id = int.Parse(fila["Id"].ToString()),
                    Titulo = fila["Titulo"].ToString(),
                    Descripcion = fila["Descripcion"].ToString(),
                    Precio = decimal.Parse(fila["Precio"].ToString())
                };
                items.Add(item);
            }

            return items;
        }
    }
}
