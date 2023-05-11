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
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            CN_Factura objFactura = new CN_Factura();
            dvgReportes.DataSource = objFactura.ReportesVentas(dateTimePicker1.Text, dateTimePicker2.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
