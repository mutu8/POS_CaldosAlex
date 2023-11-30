using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class ConvertidorMonto
    {
        private static readonly string[] Unidades = { "Cero", "Un", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve" };
        private static readonly string[] Decenas = { "Diez", "Veinte", "Treinta", "Cuarenta", "Cincuenta", "Sesenta", "Setenta", "Ochenta", "Noventa" };
        private static readonly string[] DiezAVeinte = { "Diez", "Once", "Doce", "Trece", "Catorce", "Quince", "Dieciséis", "Diecisiete", "Dieciocho", "Diecinueve" };

        public static string ConvertirMontoEnPalabras(decimal monto)
        {
            if (monto == 0)
            {
                return "Cero nuevos soles con 00 centavos";
            }

            int parteEntera = (int)Math.Floor(monto);
            int parteDecimal = (int)Math.Round((monto - parteEntera) * 100);

            string parteEnteraEnPalabras = ConvertirParteEnteraEnPalabras(parteEntera);
            string parteDecimalEnPalabras = ConvertirParteDecimalEnPalabras(parteDecimal);

            string resultado = $"{parteEnteraEnPalabras} nuevos soles con {parteDecimalEnPalabras} centavos";
            return resultado;
        }

        private static string ConvertirParteEnteraEnPalabras(int parteEntera)
        {
            if (parteEntera < 10)
            {
                return Unidades[parteEntera];
            }
            else if (parteEntera >= 10 && parteEntera < 20)
            {
                return DiezAVeinte[parteEntera - 10];
            }
            else
            {
                int unidad = parteEntera % 10;
                int decena = parteEntera / 10;

                if (unidad == 0)
                {
                    return Decenas[decena - 1];
                }
                else
                {
                    return $"{Decenas[decena - 1]} y {Unidades[unidad]}";
                }
            }
        }

        private static string ConvertirParteDecimalEnPalabras(int parteDecimal)
        {
            if (parteDecimal == 0)
            {
                return "00";
            }
            else if (parteDecimal < 10)
            {
                return Unidades[parteDecimal];
            }
            else
            {
                int unidad = parteDecimal % 10;
                int decena = parteDecimal / 10;

                if (unidad == 0)
                {
                    return $"{Decenas[decena - 1]}";
                }
                else
                {
                    return $"{Decenas[decena - 1]} y {Unidades[unidad]}";
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            decimal monto = 80.00m;
            string montoEnPalabras = ConvertidorMonto.ConvertirMontoEnPalabras(monto);
            Console.WriteLine(montoEnPalabras);
        }
    }

}
