using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class BitacoraMapper
    {
        private UsuarioMapper usuarioMapper = new UsuarioMapper();
        public void Alta(Bitacora bitacora)
        {
            string query = "INSERT INTO Bitacora (Descripcion,FechaEvento,UsuarioId,BitacoraTipoEventoId) OUTPUT INSERTED.Id " +
                "VALUES (@Descripcion,@FechaEvento,@UsuarioId,@BitacoraTipoEventoId)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Descripcion", bitacora.Descripcion),
                new SqlParameter("@FechaEvento", bitacora.FechaEvento),
                new SqlParameter("@UsuarioId", bitacora.Usuario.Id),
                new SqlParameter("@BitacoraTipoEventoId", bitacora.BitacoraTipoEvento.Id)
            };

            SqlHelper.Ejecutar(query, parameters);
        }

        public List<Bitacora> Obtener()
        {
            List<Bitacora> bitacora = new List<Bitacora>();
            string query = @"SELECT b.Id,b.Descripcion,b.FechaEvento,b.UsuarioId,b.BitacoraTipoEventoId,
                            t.Id,t.Nombre
                            FROM Bitacora b INNER JOIN BitacoraTipoEvento t ON t.Id=b.BitacoraTipoEventoId
                            ORDER BY b.FechaEvento DESC";
            DataTable table = SqlHelper.Obtener(query, null);
            if (table == null)
            {
                return null;
            }

            foreach (DataRow row in table.Rows)
            {
                bitacora.Add(new Bitacora
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Descripcion = row["Descripcion"].ToString(),
                    FechaEvento = DateTime.Parse(row["FechaEvento"].ToString()),
                    Usuario = usuarioMapper.Obtener(int.Parse(row["UsuarioId"].ToString())),
                    BitacoraTipoEvento = new BitacoraTipoEvento() { Id = int.Parse(row["Id"].ToString()), Nombre = row["Nombre"].ToString() }
                });
            }
            return bitacora;
        }
    }
}
