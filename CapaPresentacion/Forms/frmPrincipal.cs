using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaComun.Cache;

namespace CapaPresentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }


        private Form Accion = null;
        private void AbrirFormulario(Form Formulario)
        {
            if(Accion != null)
                Accion.Close();
            
            Accion = Formulario;
            Formulario.TopLevel = false;
            Formulario.Dock = DockStyle.Fill; 
            Formulario.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(Formulario);
            panelMain.Tag = Formulario;
            Formulario.BringToFront();   
            Formulario.Show();
        }


        #region Efecto Mover
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            Mover();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int
       wparam, int lparam);

        public void Mover()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion


        private void CargarLoginDatos()
        {
            lblNombre.Text = CM_LoginDatosCache.Nombre + " " + CM_LoginDatosCache.Apellido;
            lblCargo.Text = CM_LoginDatosCache.Cargo;
            lblCorreo.Text = CM_LoginDatosCache.Correo;

            if (CM_LoginDatosCache.Cargo == "Empleado")
            {
                btnEmpleados.Hide();
            }

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            CargarLoginDatos();
        }

        private void timerHoraFecha_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmEmpleados());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientes());

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmProductos());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmVentas());
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmFactura());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmReportes());
        }
    }
}




