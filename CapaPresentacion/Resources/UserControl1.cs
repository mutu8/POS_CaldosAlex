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

namespace POS
{
    public partial class UserControl1 : UserControl
    {
        // Declarar el evento ElementoSeleccionado
        public event EventHandler ElementoSeleccionado;

        public UserControl1()
        {
            InitializeComponent();
            ConfigurarAlineacionCentro(gunaShadowPanel1);

        }
        private void OnElementoSeleccionado()
        {
            ElementoSeleccionado?.Invoke(this, EventArgs.Empty);
        }
        public void ActualizarDatos(string nombre, decimal precio)
        {
            labelNombre.Text = nombre;
            labelPrecio.Text = precio.ToString("C");
        }

  
        private void GunaShadowPanel1_Click(object sender, EventArgs e)
        {
             if (gunaGradientButton_valid.Visible == false)
            {
                gunaGradientButton_valid.Visible = true;
                gunaLinePanel_valid.Visible = true;
            }
            else
            {
                gunaGradientButton_valid.Visible = false;
                gunaLinePanel_valid.Visible = false;
            }
            
        }

        private void GunaLinePanel_valid_Click(object sender, EventArgs e)
        {
            OnElementoSeleccionado();
            if (gunaGradientButton_valid.Visible == false)
            {
                gunaGradientButton_valid.Visible = true;
                gunaLinePanel_valid.Visible = true;
            }
            else
            {
                gunaGradientButton_valid.Visible = false;
                gunaLinePanel_valid.Visible = false;
            }
            
        }


        private void UserControl1_Click_1(object sender, EventArgs e)
        {
            if (gunaGradientButton_valid.Visible == false)
            {
                gunaGradientButton_valid.Visible = true;
                gunaLinePanel_valid.Visible = true;
            }
            else
            {
                gunaGradientButton_valid.Visible = false;
                gunaLinePanel_valid.Visible = false;
            }

        }
        public string Nombre
        {
            get { return labelNombre.Text; }
            set { labelNombre.Text = value; }
        }




        public decimal Precio
        {
            get
            {
                if (decimal.TryParse(labelPrecio.Text, out decimal precio))
                {
                    return precio;
                }
                else
                {
                    // Manejar el caso en que la conversión no sea exitosa
                    return 0; // O cualquier valor predeterminado que desees
                }
            }
            set
            {
                labelPrecio.Text = value.ToString("C");
            }
        }
        private void ConfigurarAlineacionCentro(GunaShadowPanel panel)
        {
            foreach (GunaLabel control in panel.Controls)
            {
                control.Anchor = AnchorStyles.None; // Elimina todas las configuraciones de Anchor existentes
                control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; // Establece la alineación al centro
                control.AutoSize = true; // Ajusta automáticamente el tamaño según el contenido del texto
            }
        }






    }
}
