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
    public partial class frmProductos : Form
    {
        CN_Productos objProductos = new CN_Productos();

        public frmProductos()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarEstados()
        {
            cmbEstados.DataSource = objProductos.Estados();
            cmbEstados.DisplayMember = "Estado";
            cmbEstados.ValueMember = "Id";
        }

        private void ListarProductos()
        {
           dvgProductos.DataSource = objProductos.Listar();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            CargarEstados();
            ListarProductos();
            
            // Texto en mayusculas al momento de escribir
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            txtCodigo.Focus();

            dvgProductos.Columns[5].DefaultCellStyle.Format = "0.00";
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "" || txtNombre.Text == "" || txtDescripcion.Text == "" || txtStock.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            objProductos.Insertar(txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, txtStock.Text, txtPrecio.Text, cmbEstados.SelectedValue.ToString());
            MessageBox.Show("Nuevo producto registrado: " + txtNombre.Text);
            ListarProductos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "" || txtNombre.Text == "" || txtDescripcion.Text == "" || txtStock.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            objProductos.Actualizar(txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, txtStock.Text, txtPrecio.Text, cmbEstados.SelectedValue.ToString());
            MessageBox.Show("Producto actualizado correctamente: " + txtNombre.Text);
            ListarProductos();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dvgProductos.DataSource = objProductos.Buscar(txtCodigoBuscar.Text, txtCodigoBuscar.Text);
        }

        
        private void txtCodigoBuscar_TextChanged(object sender, EventArgs e)
        {
            if(txtCodigoBuscar.Text == "")
            {
                ListarProductos();

            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtStock.Clear();
            txtPrecio.Text = "0.00";
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
            if(txtPrecio.Text == "0.00")
            {
            txtPrecio.Text = "";

            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {

            if (txtPrecio.Text == "")
            {
                txtPrecio.Text = "0.00";

            }
        }

        private void txtCodigoBuscar_Enter(object sender, EventArgs e)
        {
            if(txtCodigoBuscar.Text == "Nombre o Codigo")
            {
                txtCodigoBuscar.Text = "";
            }
        }

        private void txtCodigoBuscar_Leave(object sender, EventArgs e)
        {
            if(txtCodigoBuscar.Text == "")
            {
                txtCodigoBuscar.Text = "Nombre o Codigo";
            }
        }

        private void dvgProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if(i >= 0)
            {
                txtCodigo.Text = dvgProductos.Rows[i].Cells["Codigo"].Value.ToString();
                txtNombre.Text = dvgProductos.Rows[i].Cells["Producto"].Value.ToString();
                txtDescripcion.Text = dvgProductos.Rows[i].Cells["descripcion"].Value.ToString();
                txtStock.Text = dvgProductos.Rows[i].Cells["Stock"].Value.ToString();
                txtPrecio.Text = dvgProductos.Rows[i].Cells["Precio"].Value.ToString();

                if(dvgProductos.Rows[i].Cells["Estado"].Value.ToString() == "1")
                {
                    cmbEstados.SelectedIndex = 0;
                } else
                {
                    cmbEstados.SelectedIndex = 1;
                }

            }

        }
    }
}










