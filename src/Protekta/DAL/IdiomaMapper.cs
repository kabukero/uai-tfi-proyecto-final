using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class IdiomaMapper
    {
        public List<Idioma> Obtener()
        {

            string query = "SELECT Id,Nombre,Codigo FROM Idioma ORDER BY Nombre";

            DataTable tabla = SqlHelper.Obtener(query, null);

            if (tabla == null || tabla.Rows.Count == 0)
                return null;

            List<Idioma> items = new List<Idioma>();

            foreach (DataRow fila in tabla.Rows)
            {
                Idioma item = new Idioma()
                {
                    Id = int.Parse(fila["Id"].ToString()),
                    Nombre = fila["Nombre"].ToString(),
                    Codigo = fila["Codigo"].ToString(),
                };
                items.Add(item);
            }

            return items;
        }
    }
}
