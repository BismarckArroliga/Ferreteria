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
using CapaComun.Cache;

namespace CapaPresentacion
{
    public partial class md_Clientes : Form
    {

        public Clientes Cliente { get; set; }

        public md_Clientes()
        {
            InitializeComponent();
        }

        private void md_Clientes_Load(object sender, EventArgs e)
        {
            CN_Clientes obClientes = new CN_Clientes();

            dvgClientes.AutoGenerateColumns = false;
            dvgClientes.ColumnCount = 3;
            dvgClientes.DataSource = obClientes.Listar();
        }

        private void dvgClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int IdColum = e.ColumnIndex;
            if (i >= 0 && IdColum >= 0)
            {
                Cliente = new Clientes()
                {
                    Id = dvgClientes.Rows[i].Cells["Id"].Value.ToString(),
                    Nombre = dvgClientes.Rows[i].Cells["Nombre"].Value.ToString(),
                    Apellido = dvgClientes.Rows[i].Cells["Apellido"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CN_Clientes obClientes = new CN_Clientes();
            dvgClientes.DataSource = obClientes.Buscar(txtCodigoBuscar.Text);
        }

        private void txtCodigoBuscar_TextChanged(object sender, EventArgs e)
        {
            CN_Clientes obClientes = new CN_Clientes();
            dvgClientes.DataSource = obClientes.Listar();
        }

        private void txtCodigoBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtCodigoBuscar.Text.Trim().Length == 0 && e.KeyChar.ToString() == "")
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
    }
}


