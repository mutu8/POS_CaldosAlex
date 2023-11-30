using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logCaja
    {
        private static readonly logCaja _instancia = new logCaja();
        public static logCaja Instancia
        {
            get { return logCaja._instancia; }
        }

        public DataTable ObtenerDatosCaja()
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datCaja.Instancia.ObtenerDatosCaja();
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.ObtenerDatosCaja(): " + ex.Message);
                return null;
            }
        }
        // Nuevo método para insertar una nueva caja
        public void InsertarNuevaCaja(DateTime fechaApertura)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                datCaja.Instancia.InsertarNuevaCaja(fechaApertura);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.InsertarNuevaCaja(): " + ex.Message);
            }
        }
        public int ObtenerIdCajaAbierta()
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datCaja.Instancia.ObtenerIdCajaAbierta();
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.ObtenerIdCajaAbierta(): " + ex.Message);
                return -1; // Valor predeterminado si ocurre un error
            }
        }
        public void CerrarCaja(int idCaja)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                datCaja.Instancia.CerrarCaja(idCaja);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.CerrarCaja(): " + ex.Message);
            }
        }
        public bool ExistenCajasAbiertas()
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datCaja.Instancia.ExistenCajasAbiertas();
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.ExistenCajasAbiertas(): " + ex.Message);
                return false; // Valor predeterminado si ocurre un error
            }
        }
        // Nuevo método para insertar un gasto en una caja específica
        public void InsertarGasto(int idCaja, decimal montoGasto)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                datCaja.Instancia.InsertarGasto(idCaja, montoGasto);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.InsertarGasto(): " + ex.Message);
            }
        }
        public bool CajaTieneGastosRegistrados(int idCaja)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datCaja.Instancia.CajaTieneGastosRegistrados(idCaja);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.CajaTieneGastosRegistrados(): " + ex.Message);
                return false; // Valor predeterminado si ocurre un error
            }
        }
        public bool CajaEstaAbierta(int idCaja)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datCaja.Instancia.CajaEstaAbierta(idCaja);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.CajaEstaAbierta(): " + ex.Message);
                return false; // Valor predeterminado si ocurre un error
            }
        }
        public decimal ObtenerGastoCajaPorId(int idCaja)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datCaja.Instancia.ObtenerGastoCajaPorId(idCaja);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logCaja.ObtenerGastoCajaPorId(): " + ex.Message);
                return 0; // Valor predeterminado si ocurre un error
            }
        }

    }

}
