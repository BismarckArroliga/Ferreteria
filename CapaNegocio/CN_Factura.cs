using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
   public class CN_Factura
    {
        CD_Factura objFactura = new CD_Factura();

        public DataTable Listar()
        {
            return objFactura.Listar();
        }

        public DataTable ReportesVentas(string fechaInicio, string fechaFin)
        {
           return  objFactura.ReportesVentas(fechaInicio, fechaFin);
        }
    }
}
