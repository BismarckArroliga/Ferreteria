using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
   public class CD_DetalleFactura: CD_Conexion
    {
        public DataTable ListarDetalles(int idFactura)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_detalle_factura", conexion))
                {
                    DataTable tabla = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", "L");
                    cmd.Parameters.AddWithValue("@factura", idFactura);
                    SqlDataReader reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
    }
}
