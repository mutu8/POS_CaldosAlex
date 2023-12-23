using System;
using System.Data;
using System.Data.SqlClient;
using CapaeEntidad; // Asegúrate de importar el espacio de nombres de la capa de entidad

namespace CapaDatos
{
    public class datUsuarios
    {

        // Actualiza los detalles de conexión según tu servidor de Azure SQL
        private string connectionString = Conexion.Instancia.obtenerConexion();

        #region Singleton   
        // Patrón de Diseño Singleton
        private static readonly datUsuarios _instancia = new datUsuarios();
        public static datUsuarios Instancia
        {
            get { return datUsuarios._instancia; }
        }
        #endregion

        public entUsuarios ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        entUsuarios usuario = new entUsuarios
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            Contraseña = reader["Contraseña"].ToString()
                            // Mapear otras propiedades según sea necesario
                        };
                        return usuario;
                    }
                    return null; // Usuario no encontrado
                }
            }
        }
        public void EstablecerEstadoLogeado(int usuarioID, bool Logeado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Usuarios SET Logeado = @Logeado WHERE ID = @UsuarioID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    command.Parameters.AddWithValue("@Logeado", Logeado);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int ObtenerIdUsuarioLogeado(string nombreUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Logeado = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    object result = command.ExecuteScalar();

                    // Verificar si el resultado no es nulo antes de convertir
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }

                    return -1; // Si no se encuentra ningún usuario logeado con ese nombre
                }
            }
        }


        public void EstablecerEstadoDeslogeado(string NombreUsuario, bool Logeado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Usuarios SET Logeado = @Logeado WHERE NombreUsuario = @NombreUsuario";
                using (SqlCommand command = new SqlCommand(query, connection))
                {   
                    command.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Logeado", Logeado);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
