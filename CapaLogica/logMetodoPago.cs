using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logMetodoPago
    {
        private static readonly logMetodoPago _instancia = new logMetodoPago();
        public static logMetodoPago Instancia
        {
            get { return logMetodoPago._instancia; }
        }

        // Método para obtener los métodos de pago desde la capa de datos
        public DataTable ObtenerMetodosPago()
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datMetodoPago.Instancia.ObtenerMetodosPago();
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logMetodoPago.ObtenerMetodosPago(): " + ex.Message);
                return null;
            }
        }
        public string ObtenerNombreMetodoPago(int idMetodoPago)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datMetodoPago.Instancia.ObtenerNombreMetodoPago(idMetodoPago);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logMetodoPago.ObtenerNombreMetodoPago(): " + ex.Message);
                return string.Empty;
            }
        }



    }
}
