using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
   public class CD_Factura: CD_Conexion
    {

        public DataTable Listar()
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();

                using (var cmd = new SqlCommand("sp_factura", conexion))
                {
                    DataTable tabla = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", "L");
                    SqlDataReader reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }

        public DataTable ReportesVentas(string fechaInicio, string fechaFin)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();

                using (var cmd = new SqlCommand("sp_reporteVentas", conexion))
                {
                    DataTable tabla = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    SqlDataReader reader = cmd.ExecuteReader();
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }
    }
}
