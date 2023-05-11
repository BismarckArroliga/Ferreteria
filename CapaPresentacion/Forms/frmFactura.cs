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
    public partial class frmFactura : Form
    {
        CN_Factura objFactura = new CN_Factura();

        public frmFactura()
        {
            InitializeComponent();
        }

        private void MostarFacturas()
        {
            CN_Factura objFacturas = new CN_Factura();
            dvgFacturas.DataSource = objFacturas.Listar();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            MostarFacturas();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dvgFacturas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CN_DetalleFactura obDetalleFactura = new CN_DetalleFactura();

            int i = e.RowIndex;

            if(i >= 0 )
            {
                dataGridView1.DataSource = obDetalleFactura.ListarDetalles(dvgFacturas.Rows[i].Cells["Id"].Value.ToString());

            }

        }
    }
}
