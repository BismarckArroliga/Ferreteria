using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmClientes : Form
    {

        CN_Clientes objClientes = new CN_Clientes();

        public frmClientes()
        {
            InitializeComponent();
        }

        private void MostrarClientes ()
        {
            CN_Clientes objCliente = new CN_Clientes();
            dvgClientes.DataSource = objCliente.Listar();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == "" || txtApellido.Text == "" || txtCedula.Text == "" || txtDireccion.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            } 

            objClientes.Insertar(txtNombre.Text, txtApellido.Text, txtCedula.Text, txtTelefono.Text, txtDireccion.Text);
            MessageBox.Show("Nuevo cliente ingresado correctamente");
            MostrarClientes();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtCedula.Text == "" || txtDireccion.Text == "" || txtIdCliente.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            objClientes.Actualizar(txtIdCliente.Text, txtNombre.Text, txtApellido.Text, txtCedula.Text, txtTelefono.Text, txtDireccion.Text);
            MessageBox.Show("Cliente actualizado correctamente");
            MostrarClientes();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdCliente.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Text = "+505";
            txtDireccion.Clear();
        }

        private void dvgClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if(i >= 0)
            {
                txtIdCliente.Text = dvgClientes.Rows[i].Cells["Id"].Value.ToString();
                txtNombre.Text = dvgClientes.Rows[i].Cells["Nombre"].Value.ToString();
                txtApellido.Text = dvgClientes.Rows[i].Cells["Apellido"].Value.ToString();
                txtCedula.Text = dvgClientes.Rows[i].Cells["Cedula"].Value.ToString();
                txtTelefono.Text = dvgClientes.Rows[i].Cells["Telefono"].Value.ToString();
                txtDireccion.Text = dvgClientes.Rows[i].Cells["Direccion"].Value.ToString();
            }

        }
    }
}
