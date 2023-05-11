using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
   public class CN_Ventas
    {
        CD_Ventas objVentas = new CD_Ventas();

        public DataTable Empleados()
        {
            return objVentas.Empleados();
        }

        public void RestarStock(string stock, string codigo)
        {
           objVentas.RestarStock(int.Parse(stock), codigo);
        }

        public void SumarStock(string stock, string codigo)
        {
            objVentas.SumarStock( int.Parse(stock), codigo);
        }

        public void CrearVenta(string cliente, string empleado, string montoPago, string montoCambio, string montoTotal, DataTable DetalleFactura)
        {
             objVentas.CrearVenta(int.Parse(cliente), int.Parse(empleado),float.Parse(montoPago), float.Parse(montoCambio), float.Parse(montoTotal), DetalleFactura);
        
        }

    }
}









