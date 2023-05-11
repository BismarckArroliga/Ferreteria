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
    public partial class md_Productos : Form
    {
        public CM_Productos Productos{ get; set; }
        public md_Productos()
        {
            InitializeComponent();
        }

        private void md_Productos_Load(object sender, EventArgs e)
        {
            CN_Productos objProductos = new CN_Productos();
            dvgProductos.AutoGenerateColumns = false;
            dvgProductos.ColumnCount = 5;
            dvgProductos.Columns[3].DefaultCellStyle.Format = "0.00";
            dvgProductos.DataSource = objProductos.ListarActivos();
        }

        private void dvgProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int idColm = e.ColumnIndex;

            if(i >= 0 && idColm >= 0)
            {
                Productos = new CM_Productos()
                {
                    Nombre = dvgProductos.Rows[i].Cells["Producto"].Value.ToString(),
                    Codigo = dvgProductos.Rows[i].Cells["Codigo"].Value.ToString(),
                    Stock = dvgProductos.Rows[i].Cells["Stock"].Value.ToString(),
                    PrecioVenta = dvgProductos.Rows[i].Cells["Precio"].Value.ToString()
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CN_Productos objProductos = new CN_Productos();
            dvgProductos.DataSource = objProductos.Buscar(txtCodigoBuscar.Text,txtCodigoBuscar.Text);
 
        }

        private void txtCodigoBuscar_TextChanged(object sender, EventArgs e)
        {
            if(txtCodigoBuscar.Text == "")
            {
                CN_Productos objProductos = new CN_Productos();
                dvgProductos.DataSource = objProductos.Listar();
            }
        }
    }
}













