using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

namespace DAL
{
    class SqlHelper
    {
        private const string CONN_STRING_PRINCIPAL_KEY = "Principal";
        private const string CONN_STRING_BITACORA_KEY = "Bitacora";
        private const string CONN_STRING_MASTER_KEY = "Master";

        public static DataTable Obtener(string query, SqlParameter[] parameters)
        {
            return Obtener(query, parameters, CONN_STRING_PRINCIPAL_KEY);
        }

        public static DataTable ObtenerBitacora(string query, SqlParameter[] parameters)
        {
            return Obtener(query, parameters, CONN_STRING_BITACORA_KEY);
        }

        private static DataTable Obtener(string query, SqlParameter[] parameters, string connStringKey)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringKey].ConnectionString;
            SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection
                {
                    ConnectionString = connString
                },
                CommandText = query,
                CommandType = CommandType.Text,
            };
            if (parameters != null && parameters.Count() > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            DataSet ds = new DataSet();
            cmd.Connection.Open();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(ds);
            }
            cmd.Connection.Close();

            return ds.Tables[0];
        }

        public static T ObtenerValor<T>(string query, SqlParameter[] parameters)
        {
            string connString = ConfigurationManager.ConnectionStrings[CONN_STRING_PRINCIPAL_KEY].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.Connection.Open();
                object value = command.ExecuteScalar();
                T valor = value is DBNull || value == null ? default : (T)Convert.ChangeType(value, typeof(T));
                command.Connection.Close();
                return valor;
            }
        }

        public static void EjecutarEnMaster(string query, SqlParameter[] parameters)
        {
            Ejecutar(query, parameters, CONN_STRING_MASTER_KEY);
        }

        public static void Ejecutar(string query, SqlParameter[] parameters)
        {
            Ejecutar(query, parameters, CONN_STRING_PRINCIPAL_KEY);
        }

        private static void Ejecutar(string query, SqlParameter[] parameters, string connStringKey)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringKey].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public static int Insertar(string query, SqlParameter[] parameters)
        {
            return Insertar(query, parameters, CONN_STRING_PRINCIPAL_KEY);
        }

        public static int InsertarBitacora(string query, SqlParameter[] parameters)
        {
            return Insertar(query, parameters, CONN_STRING_BITACORA_KEY);
        }

        private static int Insertar(string query, SqlParameter[] parameters, string connStringKey)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringKey].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters);
                command.Connection.Open();
                int idInsertado = (int)command.ExecuteScalar();
                command.Connection.Close();
                return idInsertado;
            }
        }
    }
}