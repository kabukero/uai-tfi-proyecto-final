using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class CarritoMapper
    {
        private static CarritoMapper _instance;

        private CarritoMapper()
        {

        }

        public static CarritoMapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CarritoMapper();
                }

                return _instance;
            }
        }

        public Carrito ObtenerCarrito(int UsuarioId, string UsuarioSessionId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UsuarioId", UsuarioId),
                new SqlParameter("@UsuarioSessionId", UsuarioSessionId),
            };

            string query = @"SELECT c.Id AS CarritoId,c.FechaCreacion,cd.Id AS CarritoDetalleId,
                            cd.SubscripcionId,s.Titulo,s.Descripcion,s.Precio
                            FROM Carrito c
                            INNER JOIN CarritoDetalle cd ON cd.CarritoId = c.Id
                            INNER JOIN Subscripcion s ON s.Id = cd.SubscripcionId
                            WHERE c.UsuarioId=@UsuarioId AND c.UsuarioSessionId=@UsuarioSessionId
                            ORDER BY s.Titulo";

            DataTable tabla = SqlHelper.Obtener(query, parameters.ToArray());

            if (tabla == null || tabla.Rows.Count == 0)
                return null;

            List<CarritoDetalle> detalles = new List<CarritoDetalle>();

            Carrito carrito = new Carrito()
            {
                Usuario = UsuarioMapper.Instance.Obtener(UsuarioId),
                UsuarioSessionId = UsuarioSessionId
            };

            foreach (DataRow fila in tabla.Rows)
            {
                carrito.Id = int.Parse(fila["CarritoId"].ToString());
                carrito.FechaCreacion = DateTime.Parse(fila["FechaCreacion"].ToString());

                Subscripcion subscripcion = new Subscripcion()
                {
                    Id = int.Parse(fila["SubscripcionId"].ToString()),
                    Titulo = fila["Titulo"].ToString(),
                    Descripcion = fila["Descripcion"].ToString(),
                    Precio = decimal.Parse(fila["Precio"].ToString())
                };

                CarritoDetalle carritoDetalle = new CarritoDetalle
                {
                    Id = int.Parse(fila["CarritoDetalleId"].ToString()),
                    Cantidad = 1,
                    Precio = int.Parse(fila["Precio"].ToString()),
                    Subscripcion = subscripcion
                };


                detalles.Add(carritoDetalle);
            }

            carrito.Detalles = detalles;

            return carrito;
        }

        public Carrito ObtenerCarritoSesion(string UsuarioSessionId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UsuarioSessionId", UsuarioSessionId));

            string query = "SELECT Id FROM Carrito WHERE UsuarioSessionId=@UsuarioSessionId";
            DataTable tabla = SqlHelper.Obtener(query, parameters.ToArray());

            if (tabla == null || tabla.Rows.Count == 0)
                return null;

            DataRow fila = tabla.Rows[0];
            Carrito carrito = new Carrito();
            carrito.Id = int.Parse(fila["Id"].ToString());
            return carrito;
        }

        public bool Existe(string UsuarioSessionId)
        {
            string query = "SELECT COUNT(*) FROM Carrito WHERE UsuarioSessionId=@UsuarioSessionId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UsuarioSessionId", UsuarioSessionId)
            };

            return SqlHelper.ObtenerValor<int>(query, parameters) > 0;
        }


        public void AgregarDetalle(Carrito carrito)
        {
            string query = string.Empty;

            if (Existe(carrito.UsuarioSessionId))
            {
                Carrito carritoActual = ObtenerCarritoSesion(carrito.UsuarioSessionId);
                carrito.Id = carritoActual.Id;
            }
            else
            {
                query = "INSERT INTO Carrito (FechaCreacion,UsuarioId,UsuarioSessionId) OUTPUT INSERTED.Id" +
                         " VALUES (@FechaCreacion,@UsuarioId,@UsuarioSessionId)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FechaCreacion", carrito.FechaCreacion),
                    new SqlParameter("@UsuarioId", carrito.Usuario.Id),
                    new SqlParameter("@UsuarioSessionId", carrito.UsuarioSessionId)
                };

                carrito.Id = SqlHelper.Insertar(query, parameters);
            }

            foreach (CarritoDetalle detalle in carrito.Detalles)
            {
                if (detalle.Id == 0)
                {
                    query = "INSERT INTO CarritoDetalle (CarritoId,SubscripcionId)" +
                             " VALUES (@CarritoId,@SubscripcionId)";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@CarritoId", carrito.Id),
                    new SqlParameter("@SubscripcionId", detalle.Subscripcion.Id),
                    //new SqlParameter("@Cantidad", detalle.Cantidad),
                    //new SqlParameter("@Precio", detalle.Precio)
                    };

                    SqlHelper.Ejecutar(query, parameters);
                }
            }
        }

        public void EliminarDetalle(CarritoDetalle detalle)
        {
            string query = "DELETE FROM CarritoDetalle WHERE Id=@Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", detalle.Id)
            };
            SqlHelper.Ejecutar(query, parameters);
        }
    }
}
