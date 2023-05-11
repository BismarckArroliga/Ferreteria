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
    public partial class frmEmpleados : Form
    {
        CN_Empleados objEmpleados = new CN_Empleados();
        public frmEmpleados()
        {
            InitializeComponent();
        }

        private void MostrarCargos()
        {
            cmbCargos.DataSource = objEmpleados.Cargos();
            cmbCargos.DisplayMember = "Cargo";
            cmbCargos.ValueMember = "Id";
        }

        private void ListarEmpleados()
        {
            CN_Empleados objEmpleado = new CN_Empleados();
            dvgEmpleados.DataSource = objEmpleado.Listar();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            MostrarCargos();
            ListarEmpleados();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == "" || txtApellido.Text == "" || txtDireccion.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }

            objEmpleados.Insertar(txtNombre.Text, txtApellido.Text, txtCorreo.Text, txtTelefono.Text, txtDireccion.Text, cmbCargos.SelectedValue.ToString());
            MessageBox.Show("Nuevo empleado registrado: " + txtNombre.Text);
            ListarEmpleados();   
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtIdEmpleado.Text == "")
            {
                MessageBox.Show("Debes ingresar el id del empleado para eliminar");
                return;
            }
            if (MessageBox.Show("Eliminar empleado?", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                 objEmpleados.Eliminar(txtIdEmpleado.Text);
                 ListarEmpleados();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDireccion.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios");
                return;
            }
            objEmpleados.Actualizar(txtIdEmpleado.Text, txtNombre.Text, txtApellido.Text, txtCorreo.Text, txtTelefono.Text, txtDireccion.Text, cmbCargos.SelectedValue.ToString());
            MessageBox.Show("Empleado actualizado correctamente: " + txtNombre.Text);
            ListarEmpleados();
        }

        private void btnCrearLogin_Click(object sender, EventArgs e)
        {
            if(txtUsuario.Text == "" || txtContrasena.Text == "" || txtIdEmpleadoLogin.Text == "")
            {
                MessageBox.Show("Datos invalidos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            } else
            {
                CN_Usuarios objUsuario = new CN_Usuarios();
                var usuario = objUsuario.Buscar(txtIdEmpleadoLogin.Text);
                var empelado = objEmpleados.Buscar(txtIdEmpleadoLogin.Text);

                if (usuario)
                {
                    MessageBox.Show("Este usuario ya existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIdEmpleadoLogin.Clear();
                }
                else if (empelado)
                {
                    objUsuario.Insertar(txtUsuario.Text, txtContrasena.Text, txtIdEmpleadoLogin.Text);
                    MessageBox.Show("Usuario creado correctamento");

                }
                else
                    MessageBox.Show("Id Empleado incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizalogin_Click(object sender, EventArgs e)
        {
           if(txtUsuario.Text != ""|| txtContrasena.Text != "" || txtIdEmpleadoLogin.Text != "")
            {
                CN_Usuarios objUsuario = new CN_Usuarios();
                objUsuario.Actualizar(txtUsuario.Text, txtContrasena.Text, txtIdEmpleadoLogin.Text);
                MessageBox.Show("Usuario actualiazado correctamento");
            } else
            {
                MessageBox.Show("Datos invalidos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdEmpleado.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreo.Clear();
            txtTelefono.Text = "+505";
            txtDireccion.Clear();


            // Login
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtIdEmpleadoLogin.Clear();
        }

        private void dvgEmpleados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if(i >= 0)
            {
                txtIdEmpleado.Text = dvgEmpleados.Rows[i].Cells["Id"].Value.ToString();
                txtNombre.Text = dvgEmpleados.Rows[i].Cells["Nombre"].Value.ToString();
                txtApellido.Text = dvgEmpleados.Rows[i].Cells["Apellido"].Value.ToString();
                txtCorreo.Text = dvgEmpleados.Rows[i].Cells["Correo"].Value.ToString();
                txtTelefono.Text = dvgEmpleados.Rows[i].Cells["Telefono"].Value.ToString();
                txtDireccion.Text = dvgEmpleados.Rows[i].Cells["Direccion"].Value.ToString();

                if (dvgEmpleados.Rows[i].Cells["Cargo"].Value.ToString() == "Empleado") 
                {
                    cmbCargos.SelectedIndex = 1;
                } else {
                    cmbCargos.SelectedIndex = 0;
                
                }
            }
        }

        private void txtIdEmpleadoLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtIdEmpleado.Text.Trim().Length == 0 && e.KeyChar.ToString() == "")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == "")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtIdEmpleadoLogin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


















