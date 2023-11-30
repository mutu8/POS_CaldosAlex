using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;

namespace POS
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            NumericUpDownC.ValueChanged += (sender, e) => OnCantidadChanged();
        }

        public string Nombre
        {
            get { return lblNombre.Text; }
            set { lblNombre.Text = value; }
        }

        private int cantidad;
        public int Cantidad
        {
            get { return (int)NumericUpDownC.Value; }
            set
            {
                cantidad = value;
                NumericUpDownC.Value = value;
                OnCantidadChanged();
            }
        }

        private decimal precio;
        public decimal Precio
        {
            get { return precio; }
            set
            {
                precio = value;
                lblPrecio.Text=precio.ToString();
                CalcularImporte();
            }
        }

        private decimal importe;
        public decimal Importe
        {
            get { return importe; }
            set { importe = value; CalcularImporte(); }
        }
        public string horaRegistrada 
        {
            get { return lblHoraIngresada.Text; }
            set { lblHoraIngresada.Text = value; }
        }

        private void CalcularImporte()
        {
            importe = Cantidad * Precio;
            OnCantidadChanged();  // Notificar el cambio de cantidad para que el formulario pueda actualizar el importe total
        }

        public event EventHandler EliminarClick;

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarClick?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CantidadChanged;
        private void OnCantidadChanged()
        {
            CantidadChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
