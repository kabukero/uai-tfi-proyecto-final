using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UsuarioMapper
    {
        public Usuario Obtener(string email)
        {
            string query = "SELECT Id,Email,Nombre,Apellido,Password,Activo,DVH,IdIdioma FROM Usuario " +
            "WHERE Email=@email";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email", email)
            };

            DataTable table = SqlHelper.Obtener(query, parameters);
            if (table == null || table.Rows.Count == 0)
            {
                return null;
            }

            var usuario = new Usuario()
            {
                Id = int.Parse(table.Rows[0]["Id"].ToString()),
                Email = table.Rows[0]["Email"].ToString(),
                Nombre = table.Rows[0]["Nombre"].ToString(),
                Apellido = table.Rows[0]["Apellido"].ToString(),
                Password = table.Rows[0]["Password"].ToString(),
                Activo = bool.Parse(table.Rows[0]["Activo"].ToString()),
                DVH = table.Rows[0]["DVH"].ToString(),
                IdIdioma = int.Parse(table.Rows[0]["IdIdioma"].ToString())
            };

            return usuario;
        }
    }
}
