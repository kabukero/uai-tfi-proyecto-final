using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class PermisoMapper
    {
        private static PermisoMapper _instance;

        private PermisoMapper()
        {

        }

        public static PermisoMapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PermisoMapper();
                }

                return _instance;
            }
        }

        public List<Permiso> ObtenerPorUsuario(int usuarioId)
        {
            string query = "SELECT PermisoId FROM UsuarioPermiso WHERE UsuarioId = @usuarioId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@usuarioId", usuarioId)
            };

            DataTable table = SqlHelper.Obtener(query, parameters);
            List<Permiso> permisos = new List<Permiso>();
            foreach (DataRow row in table.Rows)
            {
                Permiso permiso = ObtenerPermiso(int.Parse(row["PermisoId"].ToString()), 1);
                permisos.Add(permiso);
            }
            return permisos;
        }

        private Permiso ObtenerPermiso(int permisoId, int profundidad)
        {
            Permiso permiso;
            List<int> hijosIds = new List<int>();
            if (profundidad < 10)
            {
                hijosIds = ObtenerPermisosHijos(permisoId);
            }

            if (hijosIds.Any())
            {
                // Es un permiso compuesto
                permiso = new PermisoCompuesto();
                foreach (int hijoId in hijosIds)
                {
                    Permiso permisoHijo = ObtenerPermiso(hijoId, profundidad + 1);
                    permiso.AgregarPermisoHijo(permisoHijo);
                }
            }
            else
            {
                // Es un permiso simple
                permiso = new PermisoSimple();
            }

            CompletarPermiso(permiso, permisoId);

            return permiso;
        }

        private List<int> ObtenerPermisosHijos(int permisoId)
        {
            string query = "SELECT pp.PermisoId " +
                    "FROM Permiso p " +
                    "INNER JOIN PermisoPermiso pp ON p.Id = pp.PermisoPadreId " +
                    "WHERE pp.PermisoPadreId = @permisoPadreId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@permisoPadreId", permisoId)
            };

            DataTable table = SqlHelper.Obtener(query, parameters);
            return table.Select().Select(r => int.Parse(r["PermisoId"].ToString())).ToList();
        }

        private void CompletarPermiso(Permiso permiso, int permisoId)
        {
            string query = "SELECT Nombre FROM permiso WHERE Id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", permisoId)
            };

            DataTable table = SqlHelper.Obtener(query, parameters);
            if (table.Rows.Count > 0)
            {
                permiso.Id = permisoId;
                permiso.Nombre = table.Rows[0]["Nombre"].ToString();
            }
        }

        public Permiso ObtenerPorNombre(string nombre)
        {
            string query = "SELECT Id FROM Permiso WHERE Nombre = @nombre";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre)
            };

            DataTable table = SqlHelper.Obtener(query, parameters);
            Permiso permiso = null;
            if (table != null && table.Rows.Count > 0)
            {
                permiso = ObtenerPermiso(int.Parse(table.Rows[0]["id"].ToString()), 1);
            }

            return permiso;
        }
    }
}
