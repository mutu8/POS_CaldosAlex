using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datCaja
    {
        private string connectionString = Conexion.Instancia.obtenerConexion();

        #region Singleton
        private static readonly datCaja _instancia = new datCaja();
        public static datCaja Instancia
        {
            get { return datCaja._instancia; }
        }
        #endregion

        public DataTable ObtenerDatosCaja()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Caja ORDER BY FechaApertura DESC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);

                        // Agregar columnas separadas para fecha y hora de apertura
                        dataTable.Columns.Add("HoraApertura", typeof(string));
                        foreach (DataRow row in dataTable.Rows)
                        {
                            DateTime fechaApertura = Convert.ToDateTime(row["FechaApertura"]);
                            row["HoraApertura"] = fechaApertura.ToString("hh:mm tt").ToUpper(); // Formato de 12 horas con AM/PM en mayúsculas
                        }

                        // Agregar columnas separadas para fecha y hora de cierre
                        dataTable.Columns.Add("HoraCierre", typeof(string));
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row["FechaCierre"] != DBNull.Value)
                            {
                                DateTime fechaCierre = Convert.ToDateTime(row["FechaCierre"]);
                                row["HoraCierre"] = fechaCierre.ToString("hh:mm tt").ToUpper(); // Formato de 12 horas con AM/PM en mayúsculas
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Puedes manejar la excepción según tus necesidades
            }

            return dataTable;
        }



        public void InsertarNuevaCaja(DateTime fechaApertura)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Obtener el monto final de la caja anterior
                    string queryMontoFinal = "SELECT TOP 1 ISNULL(MontoFinal, 0) FROM Caja ORDER BY idCaja DESC";
                    decimal montoFinalCajaAnterior = 0;

                    using (SqlCommand commandMontoFinal = new SqlCommand(queryMontoFinal, connection))
                    {
                        object result = commandMontoFinal.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            montoFinalCajaAnterior = Convert.ToDecimal(result);
                        }
                    }

                    // Obtener los gastos de la caja anterior
                    string queryGastos = "SELECT TOP 1 ISNULL(Gastos, 0) FROM Caja ORDER BY idCaja DESC";
                    decimal gastosCajaAnterior = 0;

                    using (SqlCommand commandGastos = new SqlCommand(queryGastos, connection))
                    {
                        object result = commandGastos.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            gastosCajaAnterior = Convert.ToDecimal(result);
                        }
                    }

                    // Calcular el monto inicial considerando los gastos
                    decimal montoInicial = montoFinalCajaAnterior - gastosCajaAnterior;

                    // Insertar la nueva caja con el monto inicial calculado
                    string queryInsertarCaja = "INSERT INTO Caja (FechaApertura, EstadoCaja, MontoInicial) VALUES (@FechaApertura, 1, @MontoInicial);";

                    using (SqlCommand commandInsertarCaja = new SqlCommand(queryInsertarCaja, connection))
                    {
                        commandInsertarCaja.Parameters.AddWithValue("@FechaApertura", fechaApertura);
                        commandInsertarCaja.Parameters.AddWithValue("@MontoInicial", montoInicial);

                        // Ejecutar la consulta para insertar la nueva caja
                        commandInsertarCaja.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Puedes manejar la excepción según tus necesidades
            }
        }

        public int ObtenerIdCajaAbierta()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryObtenerIdCaja = "SELECT TOP 1 idCaja FROM Caja WHERE EstadoCaja = 1 ORDER BY idCaja DESC;";

                    using (SqlCommand commandObtenerIdCaja = new SqlCommand(queryObtenerIdCaja, connection))
                    {
                        object result = commandObtenerIdCaja.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }

                return -1; // Valor predeterminado si no se encuentra ningún ID de caja abierta
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la obtención del ID
                throw new Exception("Error al obtener el ID de la caja abierta: " + ex.Message);
            }
        }
        public void CerrarCaja(int idCaja)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryCerrarCaja = "UPDATE Caja SET EstadoCaja = 0 WHERE idCaja = @IdCaja";

                    using (SqlCommand commandCerrarCaja = new SqlCommand(queryCerrarCaja, connection))
                    {
                        commandCerrarCaja.Parameters.AddWithValue("@IdCaja", idCaja);

                        commandCerrarCaja.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                throw new Exception("Error en datCaja.CerrarCaja(): " + ex.Message);
            }
        }
        public bool ExistenCajasAbiertas()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryExistenCajasAbiertas = "SELECT COUNT(*) FROM Caja WHERE EstadoCaja = 1";

                    using (SqlCommand commandExistenCajasAbiertas = new SqlCommand(queryExistenCajasAbiertas, connection))
                    {
                        int cantidadCajasAbiertas = (int)commandExistenCajasAbiertas.ExecuteScalar();
                        return cantidadCajasAbiertas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                throw new Exception("Error en datCaja.ExistenCajasAbiertas(): " + ex.Message);
            }
        }
        public void InsertarGasto(int idCaja, decimal montoGasto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryInsertarGasto = "UPDATE Caja SET Gastos = ISNULL(Gastos, 0) + @MontoGasto WHERE idCaja = @IdCaja";

                    using (SqlCommand commandInsertarGasto = new SqlCommand(queryInsertarGasto, connection))
                    {
                        commandInsertarGasto.Parameters.AddWithValue("@MontoGasto", montoGasto);
                        commandInsertarGasto.Parameters.AddWithValue("@IdCaja", idCaja);

                        commandInsertarGasto.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                throw new Exception("Error en datCaja.InsertarGasto(): " + ex.Message);
            }
        }
        public bool CajaTieneGastosRegistrados(int idCaja)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryVerificarGastos = "SELECT Gastos FROM Caja WHERE idCaja = @IdCaja";

                    using (SqlCommand commandVerificarGastos = new SqlCommand(queryVerificarGastos, connection))
                    {
                        commandVerificarGastos.Parameters.AddWithValue("@IdCaja", idCaja);

                        object result = commandVerificarGastos.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            decimal gastos = Convert.ToDecimal(result);
                            return gastos > 0;
                        }
                    }
                }

                return false; // Valor predeterminado si no se encuentra ningún gasto
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la verificación de gastos
                throw new Exception("Error al verificar gastos de la caja: " + ex.Message);
            }
        }

        public bool CajaEstaAbierta(int idCaja)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT EstadoCaja FROM Caja WHERE idCaja = @IdCaja";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdCaja", idCaja);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int estadoCaja = Convert.ToInt32(result);
                            return estadoCaja == 1; // Si el estado es 1, la caja está abierta
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Puedes manejar la excepción según tus necesidades
            }

            return false; // En caso de error, asumimos que la caja no está abierta
        }
        public decimal ObtenerGastoCajaPorId(int idCaja)
        {
            decimal gasto = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Gastos FROM Caja WHERE idCaja = @IdCaja  ;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdCaja", idCaja);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            gasto = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el gasto de la caja: " + ex.Message);
                // Puedes manejar la excepción según tus necesidades
            }

            return gasto;
        }





    }

}
