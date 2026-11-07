using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class FacturaMapper
    {
        private static FacturaMapper _instance;

        private FacturaMapper()
        {

        }

        public static FacturaMapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FacturaMapper();
                }

                return _instance;
            }
        }

        public Factura Obtener(int Id)
        {
            string query = @"SELECT f.Id AS FacturaId,f.FechaCreacion,u.Id AS UsuarioId,u.Nombre AS NombreUsuario,u.Apellido AS ApellidoUsuario,
                            m.Id AS MedioPagoId,m.Nombre AS MedioPago,s.Titulo,fd.SubscripcionId,s.Descripcion,s.Precio,fd.Id AS FacturaDetalleId
                            FROM Factura f
                            INNER JOIN FacturaDetalle fd ON fd.FacturaId=f.Id
                            INNER JOIN Usuario u ON u.Id=f.UsuarioId
                            INNER JOIN MedioPago m ON m.Id=f.MedioPagoId
                            INNER JOIN Subscripcion s ON s.Id=fd.SubscripcionId
                            WHERE f.Id=@Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id),
            };

            DataTable table = SqlHelper.Obtener(query, parameters.ToArray());
            if (table == null || table.Rows.Count == 0)
            {
                return null;
            }

            Factura factura = new Factura()
            {
                Id = Id,
                Fecha = DateTime.Parse(table.Rows[0]["FechaCreacion"].ToString()),
                Usuario = new Usuario() { Id = int.Parse(table.Rows[0]["UsuarioId"].ToString()), Nombre = table.Rows[0]["NombreUsuario"].ToString(), Apellido = table.Rows[0]["ApellidoUsuario"].ToString() },
                MedioPago = new MedioPago() { Id = int.Parse(table.Rows[0]["MedioPagoId"].ToString()), Nombre = table.Rows[0]["FechaCreacion"].ToString() }
            };

            List<FacturaDetalle> detalles = new List<FacturaDetalle>();
            foreach (DataRow row in table.Rows)
            {
                Subscripcion subscripcion = new Subscripcion();
                subscripcion.Titulo = row["Titulo"].ToString();
                subscripcion.Descripcion = row["Descripcion"].ToString();
                subscripcion.Precio = decimal.Parse(row["Precio"].ToString());

                FacturaDetalle detalle = new FacturaDetalle();
                detalle.Subscripcion = subscripcion;

                detalles.Add(detalle);
            }
            factura.Detalles = detalles;

            return factura;
        }

        public List<Factura> Obtener()
        {
            string query = @"SELECT f.Id AS FacturaId,f.FechaCreacion,u.Id AS UsuarioId,u.Nombre AS NombreUsuario,u.Apellido AS ApellidoUsuario,
                            m.Id AS MedioPagoId,m.Nombre AS MedioPago,s.Titulo,fd.SubscripcionId,s.Descripcion,s.Precio,fd.Id AS FacturaDetalleId
                            FROM Factura f
                            INNER JOIN FacturaDetalle fd ON fd.FacturaId=f.Id
                            INNER JOIN Usuario u ON u.Id=f.UsuarioId
                            INNER JOIN MedioPago m ON m.Id=f.MedioPagoId
                            INNER JOIN Subscripcion s ON s.Id=fd.SubscripcionId
                            ORDER BY f.FechaCreacion DESC";
            DataTable table = SqlHelper.Obtener(query, null);

            if (table == null || table.Rows.Count == 0)
            {
                return new List<Factura>();
            }

            Dictionary<int, Factura> facturasDictionary =
                new Dictionary<int, Factura>();

            List<Factura> facturas = new List<Factura>();

            foreach (DataRow row in table.Rows)
            {
                int facturaId = Convert.ToInt32(row["FacturaId"]);

                Factura factura;

                if (!facturasDictionary.TryGetValue(facturaId, out factura))
                {
                    factura = new Factura()
                    {
                        Id = facturaId,

                        Fecha = Convert.ToDateTime(row["FechaCreacion"]),

                        Usuario = new Usuario()
                        {
                            Id = Convert.ToInt32(row["UsuarioId"]),
                            Nombre = row["NombreUsuario"].ToString(),
                            Apellido = row["ApellidoUsuario"].ToString()
                        },

                        MedioPago = new MedioPago()
                        {
                            Id = Convert.ToInt32(row["MedioPagoId"]),
                            Nombre = row["MedioPago"].ToString()
                        },

                        Detalles = new List<FacturaDetalle>()
                    };

                    facturasDictionary.Add(facturaId, factura);
                    facturas.Add(factura);
                }

                Subscripcion subscripcion = new Subscripcion()
                {
                    Id = Convert.ToInt32(row["SubscripcionId"]),
                    Titulo = row["Titulo"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(row["Precio"])
                };

                FacturaDetalle detalle = new FacturaDetalle()
                {
                    Id = Convert.ToInt32(row["FacturaDetalleId"]),
                    Subscripcion = subscripcion
                };

                factura.Detalles.Add(detalle);
            }

            return facturas;
        }

        public int Guardar(Factura factura)
        {
            string queryFactura = "INSERT INTO Factura (FechaCreacion,UsuarioId,CarritoId,MedioPagoId) OUTPUT INSERTED.Id " +
                "VALUES (@FechaCreacion,@UsuarioId,@CarritoId,@MedioPagoId)";
            SqlParameter[] parametersFactura = new SqlParameter[]
            {
                new SqlParameter("@FechaCreacion", factura.Fecha),
                new SqlParameter("@UsuarioId", factura.Usuario.Id),
                new SqlParameter("@CarritoId", factura.Carrito.Id),
                new SqlParameter("@MedioPagoId", factura.MedioPago.Id)
            };

            factura.Id = SqlHelper.Insertar(queryFactura, parametersFactura);
            if (factura.Id == 0)
            {
                return 0;
            }

            // Ahora inserto los detalles
            foreach (FacturaDetalle detalle in factura.Detalles)
            {
                string queryDetalle = "INSERT INTO FacturaDetalle (FacturaId,SubscripcionId) OUTPUT INSERTED.Id " +
                    "VALUES (@FacturaId, @SubscripcionId)";
                SqlParameter[] parametersDetalle = new SqlParameter[]
                {
                    new SqlParameter("@FacturaId", factura.Id),
                    new SqlParameter("@SubscripcionId", detalle.Subscripcion.Id),
                    //new SqlParameter("@Precio", detalle.Precio)
                };

                detalle.Id = SqlHelper.Insertar(queryDetalle, parametersDetalle);
                if (detalle.Id == 0)
                {
                    return 0;
                }
            }

            return factura.Id;
        }
    }
}
