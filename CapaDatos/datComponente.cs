using CapaeEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDatos
{
    public class datComponente
    {
        // Actualiza los detalles de conexión según tu servidor de Azure SQL
        private string connectionString = Conexion.Instancia.obtenerConexion();
        //private string connectionString = "Server=34.176.49.57;Database=bd_caldosAlex;User Id=sqlserver;Password=@hV\"1%`(o63_/7V:;";
        //User ID=adminsql;Password=clave123-;";

        #region Singleton
        // Patrón de Diseño Singleton
        private static readonly datComponente _instancia = new datComponente();
        public static datComponente Instancia
        {
            get { return datComponente._instancia; }
        }
        #endregion

        public decimal ObtenerPrecioComponenteDesdeBD(int idComponente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string obtenerPrecioQuery = $"SELECT precioComponente FROM Componentes WHERE idComponente = {idComponente}";

                using (SqlCommand obtenerPrecioCommand = new SqlCommand(obtenerPrecioQuery, connection))
                {
                    object result = obtenerPrecioCommand.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal precio))
                    {
                        return precio;
                    }
                }
            }

            // Si no se puede obtener el precio, devolvemos 0 o el valor predeterminado que desees.
            return 0;
        }

        public int ObtenerIdDesdeBaseDeDatos(string descripcionComponente)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta para obtener el ID del componente basado en la descripción
                    string consulta = "SELECT idComponente FROM Componentes WHERE TRIM(descripcionComponente) = TRIM(@Descripcion)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Parámetro para evitar SQL Injection
                        comando.Parameters.AddWithValue("@Descripcion", descripcionComponente);

                        // Ejecutar la consulta y obtener el ID
                        object resultado = comando.ExecuteScalar();

                        // Verificar si se obtuvo un resultado
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            return Convert.ToInt32(resultado);
                        }
                        else
                        {
                            // Manejar el caso donde no se encuentra el componente
                            return -1; // O algún valor que indique que no se encontró
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores en la conexión o la consulta
                MessageBox.Show("Error al obtener el ID del componente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }


        public bool ActualizarComponente(int idComponente, string nuevaDescripcion, decimal nuevoPrecio)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta para actualizar los datos del componente
                    string consulta = @"
                UPDATE Componentes
                SET descripcionComponente = @NuevaDescripcion,
                    precioComponente = @NuevoPrecio
                WHERE idComponente = @ID";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Parámetros para la consulta
                        comando.Parameters.AddWithValue("@NuevaDescripcion", nuevaDescripcion);
                        comando.Parameters.AddWithValue("@NuevoPrecio", nuevoPrecio);
                        comando.Parameters.AddWithValue("@ID", idComponente);

                        // Ejecutar la consulta
                        comando.ExecuteNonQuery();

                        // Realizar un commit explícito si es necesario
                        // conexion.Commit();  // Descomentar si es necesario
                    }
                }

                return true; // Si la actualización se realizó con éxito
            }
            catch (Exception ex)
            {
                // Manejar el error (puedes agregar un MessageBox aquí para depuración)
                Console.WriteLine("Error al actualizar el componente: " + ex.Message);
                return false;
            }
        }



    }
}
