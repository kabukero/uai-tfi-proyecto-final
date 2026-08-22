using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BE;

namespace DAL
{
    public class DigitoVerificadorMapper
    {
        private const string DV_KEY = "Hl2NspLLkg";

        public void ActualizarDV(Usuario usuario)
        {
            // Primero el DVH
            string dvh = ObtenerDVH(usuario);

            string query = "UPDATE Usuario SET DVH = @dvh WHERE Id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@dvh", dvh),
                new SqlParameter("@id", usuario.Id)
            };
            SqlHelper.Ejecutar(query, parameters);

            // Ahora el DVV
            RecalcularDVV("DVH", "Usuario", "Id");
        }

        private string ObtenerDVH(Usuario usuario)
        {
            string registro = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                usuario.Id,
                usuario.Email,
                usuario.Password,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Activo,
                usuario.IdIdioma);
            return CalcularDV(registro);
        }

        private string CalcularDV(string registro)
        {
            byte[] data = Encoding.ASCII.GetBytes(registro + DV_KEY);
            data = new SHA256Managed().ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        private void RecalcularDVV(string columnaDVH, string tabla, string columnaOrden)
        {
            string query = string.Format("SELECT {0} FROM {1} ORDER BY {2}", columnaDVH, tabla, columnaOrden);

            StringBuilder sb = new StringBuilder();
            DataTable table = SqlHelper.Obtener(query, new SqlParameter[0]);
            table.Select().ToList().ForEach(r => sb.Append(r[columnaDVH].ToString()));

            string dvv = CalcularDV(sb.ToString());

            query = "UPDATE DigitoVerificadorVertical SET DVV = @dvv WHERE NombreTabla = @tabla";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@dvv", dvv),
                new SqlParameter("@tabla", tabla)
            };
            SqlHelper.Ejecutar(query, parameters);
        }

        public VerificarIntegridadRespuesta VerificarIntegridad()
        {
            string query;
            DataTable tabla;
            StringBuilder sbDvhs = new StringBuilder();
            VerificarIntegridadRespuesta respuesta = null;

            #region Usuario
            // obtener todos los usuarios de la base de datos
            query = "SELECT Id,Email,Password,Nombre,Apellido,Activo,DVH,IdIdioma FROM Usuario ORDER BY Id";
            tabla = SqlHelper.Obtener(query, null);
            if (tabla == null)
            {
                return null;
            }

            respuesta = new VerificarIntegridadRespuesta();
            respuesta.Mensajes = new List<string>();
            foreach (DataRow row in tabla.Rows)
            {
                Usuario usuario = new Usuario
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Activo = bool.Parse(row["Activo"].ToString()),
                    IdIdioma = int.Parse(row["IdIdioma"].ToString())
                };

                string dvhBD = row["DVH"].ToString();
                string dvhCalculado = ObtenerDVH(usuario);
                if (dvhBD != dvhCalculado)
                {
                    respuesta.Mensajes.Add(string.Format("El usuario con el ID {0} fue modificado externamente.", usuario.Id));
                    respuesta.HayErrores = true;
                }
                sbDvhs.Append(dvhBD.ToString());
            }

            // Chequear DVV
            string dvvCalculado = CalcularDV(sbDvhs.ToString());
            query = "SELECT DVV FROM DigitoVerificadorVertical WHERE NombreTabla = 'Usuario'";
            string dvvBD = SqlHelper.ObtenerValor<string>(query, null);

            if (dvvCalculado != dvvBD)
            {
                respuesta.Mensajes.Add("Un registro de la tabla Usuario fue eliminado externamente.");
                respuesta.HayErrores = true;
            }
            #endregion

            return respuesta;
        }
    }
}
