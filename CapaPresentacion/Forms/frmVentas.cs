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
    public partial class frmVentas : Form
    {
        CN_Ventas objVentas = new CN_Ventas();

        public frmVentas()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarEmpleados()
        {
            cmbEmpleados.DataSource = objVentas.Empleados();
            cmbEmpleados.DisplayMember = "Nombre";
            cmbEmpleados.ValueMember = "Id";
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void btnBuscarClientes_Click(object sender, EventArgs e)
        {
            using (var Modal = new md_Clientes())
            {
                var rest = Modal.ShowDialog();
                if (rest == DialogResult.OK)
                {
                    txtIdCliente.Text = Modal.Cliente.Id;
                    txtNombreCom.Text = Modal.Cliente.Nombre + " " + Modal.Cliente.Apellido;
                }
            }
        }

        private void btnBuscarProductos_Click(object sender, EventArgs e)
        {
            using (var Modal = new md_Productos())
            {
                var rest = Modal.ShowDialog();
                if (rest == DialogResult.OK)
                {
                    txtProducto.Text = Modal.Productos.Nombre;
                    txtCodigo.Text = Modal.Productos.Codigo;
                    txtStock.Text = Modal.Productos.Stock;
                    txtPrecio.Text = Modal.Productos.PrecioVenta;
                    txtUnidades.Select();
                }
            }
        }


        private string ColorError = "#E73E32"; 
        private string ColorBien = "#DCDCDC";

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if(txtProducto.Text == "" || txtCodigo.Text == "" ||  txtUnidades.Text == "")
            {
                MessageBox.Show("Debes ingresar la información completa de la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal precio = decimal.Parse(txtPrecio.Text);
            bool productoExiste = false;

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtUnidades.Text))
            {
                txtUnidades.BackColor = ColorTranslator.FromHtml(ColorError);
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnidades.Text = "";
                txtUnidades.Select();
                return;
            }
            else
            {
            }

            foreach (DataGridViewRow fila in dvgVenta.Rows)
            {
                if (fila.Cells["Codigo"].Value.ToString() == txtCodigo.Text)
                {
                    productoExiste = true;
                    MessageBox.Show("Venta existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }

            if (productoExiste == false)
            {

                CN_Ventas objVentas = new CN_Ventas();
                objVentas.RestarStock(txtUnidades.Text, txtCodigo.Text);
 
                    dvgVenta.Rows.Add(new object[] {
                         txtProducto.Text,
                         txtCodigo.Text,
                         decimal.Parse(txtPrecio.Text).ToString("0"),
                         txtUnidades.Text,
                         (decimal.Parse(txtPrecio.Text)* (decimal.Parse(txtUnidades.Text))).ToString("0")
                    });
                
               
                CalcularTotal();
                Limpiar();
            }
        }




        //Método calcular Total
        private void CalcularTotal()
        {
            decimal total = 0;
            if (dvgVenta.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dvgVenta.Rows)
                {
                    total += Convert.ToDecimal(fila.Cells["Total"].Value.ToString());
                }
                txtTotalPagar.Text = total.ToString("0");
            }
        }

        private void txtUnidades_TextChanged(object sender, EventArgs e)
        {
            // Resetear color unidades 
            txtUnidades.BackColor = ColorTranslator.FromHtml(ColorBien);

            if (txtUnidades.Text != "")
            {
                decimal total = 0;
                total += decimal.Parse(txtUnidades.Text) * decimal.Parse(txtPrecio.Text);
                txtTotal.Text = total.ToString("0");
            }
            else
            {
                txtTotal.Text = "0";
            }
        }


        private void Limpiar()
        {
            // Productos
            txtProducto.Clear();
            txtCodigo.Clear();
            txtStock.Clear();
            txtUnidades.Clear();
            txtPrecio.Clear();
            txtTotal.Text = "0";

            txtPagaCon.Clear();
            txtCambio.Clear();
        }

        private void dvgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgVenta.Columns[e.ColumnIndex].Name == "Delete")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    CN_Ventas objVentas = new CN_Ventas();
                    string stock = dvgVenta.Rows[index].Cells["Unidades"].Value.ToString();
                    string codigo = dvgVenta.Rows[index].Cells["Codigo"].Value.ToString();
                    objVentas.SumarStock(stock, codigo);

                    dvgVenta.Rows.RemoveAt(index);
                    CalcularTotal();
                }

                if (dvgVenta.RowCount == 0)
                {
                    txtTotalPagar.Text = "0";
                }
            }
        }

        private void txtUnidades_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            } else
            {
                if(txtUnidades.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                } else
                {
                    if(Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    } else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

 
        private void txtPagaCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtUnidades.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
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

        
        private void calcularCambio()
        {
           if(txtPagaCon.Text != "")
            {
                decimal totalPagar = decimal.Parse(txtTotalPagar.Text);
                decimal pagaCom = decimal.Parse(txtPagaCon.Text);

                if (pagaCom < totalPagar)
                {
                    txtCambio.Text = "0";
                }
                else
                {
                    txtCambio.Text = ((pagaCom) - (totalPagar)).ToString("0");
                }
            } else
            {
                txtCambio.Text = "0";
            }

        }

        private void txtPagaCon_TextChanged(object sender, EventArgs e)
        {
 
                calcularCambio();
           
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if(txtIdCliente.Text == "")
            {
                MessageBox.Show("Debes ingresar la informacion completa del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPagaCon.Text == "")
            {
                MessageBox.Show("Debes ingresar el paga con", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dvgVenta.Rows.Count < 1)
            {
                MessageBox.Show("Debes ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            DataTable DetalleFactura = new DataTable();

            DetalleFactura.Columns.Add("Codigo", typeof(string));
            DetalleFactura.Columns.Add("Precio", typeof(float));
            DetalleFactura.Columns.Add("Unidades", typeof(Int32));
            DetalleFactura.Columns.Add("Total", typeof(float));

            foreach (DataGridViewRow row in dvgVenta.Rows)
            {
                DetalleFactura.Rows.Add(new object[] {
                    row.Cells["Codigo"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Unidades"].Value.ToString(),
                    row.Cells["Total"].Value.ToString()

                });
            }

            calcularCambio();
            objVentas.CrearVenta(txtIdCliente.Text, cmbEmpleados.SelectedValue.ToString(), txtPagaCon.Text, txtCambio.Text, txtTotalPagar.Text, DetalleFactura);

            if (MessageBox.Show("Nueva factura generada", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK )
            {
                dvgVenta.Rows.Clear();
                txtTotalPagar.Text = "0";
                txtCambio.Clear();
                txtPagaCon.Clear();
            }
           
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCom.Clear();
            txtIdCliente.Clear();
            txtProducto.Clear();
            txtCodigo.Clear();
            txtStock.Clear();
            txtUnidades.Clear();
            txtPrecio.Clear();
            txtTotal.Text = "0";
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}














