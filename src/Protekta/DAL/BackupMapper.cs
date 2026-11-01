using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Configuration;

namespace DAL
{
    public class BackupMapper
    {
        private static BackupMapper _instance;

        private BackupMapper()
        {

        }

        public static BackupMapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BackupMapper();
                }

                return _instance;
            }
        }

        public void Alta(Backup backup)
        {
            string query = "INSERT INTO BackupSistema (FechaCreacion,NombreArchivo,UsuarioId) " +
                "VALUES (@FechaCreacion,@NombreArchivo,@UsuarioId)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FechaCreacion", backup.FechaCreacion),
                new SqlParameter("@NombreArchivo", backup.NombreArchivo),
                new SqlParameter("@UsuarioId", backup.Usuario.Id),
            };

            SqlHelper.Ejecutar(query, parameters);
        }

        public List<Backup> Obtener()
        {
            List<Backup> backup = new List<Backup>();
            string query = @"SELECT b.Id,b.FechaCreacion,b.NombreArchivo,
                            b.UsuarioId,u.Nombre,u.Apellido
                            FROM BackupSistema b INNER JOIN Usuario u ON u.Id=b.UsuarioId
                            ORDER BY b.FechaCreacion DESC";
            DataTable table = SqlHelper.Obtener(query, null);
            if (table == null)
            {
                return null;
            }

            foreach (DataRow row in table.Rows)
            {
                backup.Add(
                    new Backup
                    {
                        Id = int.Parse(row["Id"].ToString()),
                        FechaCreacion = DateTime.Parse(row["FechaCreacion"].ToString()),
                        NombreArchivo = row["NombreArchivo"].ToString(),
                        Usuario = new Usuario()
                        {
                            Id = int.Parse(row["UsuarioId"].ToString()),
                            Nombre = row["Nombre"].ToString(),
                            Apellido = row["Apellido"].ToString()
                        }
                    }
                 );
            }
            return backup;
        }

        public void HacerBackup(string ubicacion)
        {
            string nombreBd = ConfigurationManager.AppSettings.Get("nombreBdPrincipal");
            string query = "BACKUP DATABASE " + nombreBd + " TO DISK = @ubicacion";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ubicacion", ubicacion)
            };
            SqlHelper.Ejecutar(query, parameters);
        }
    }
}
