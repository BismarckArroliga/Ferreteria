using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_DetalleFactura
    {
        CD_DetalleFactura objDetalleFactura = new CD_DetalleFactura();

        public DataTable ListarDetalles(string IdFactura)
        {
            return objDetalleFactura.ListarDetalles(int.Parse(IdFactura));
        }

    }
}
