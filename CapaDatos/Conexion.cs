using System;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        private static readonly Conexion _instancia = new Conexion();
        public static Conexion Instancia
        {
            get { return Conexion._instancia; }
        }

        private string connectionString;

        public Conexion()
        {
            string machineName = Environment.MachineName;

            // Concatena el machineName en la cadena de conexión
            connectionString = $"Server={machineName}\\SQLEXPRESS;Database=BD_CaldosAlex;Integrated Security=True;";
        }

        public string obtenerConexion()
        {
            return connectionString;
        }
    }

}
