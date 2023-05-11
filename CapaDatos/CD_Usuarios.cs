using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaComun.Cache;
namespace CapaDatos
{
    public class CD_Usuarios:CD_Conexion
    {
        public bool Login(string usuario, string contrasena)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();

                using (var cmd = new SqlCommand("sp_usuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", "L");
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if(reader.Read())
                        {
                            CM_LoginDatosCache.Nombre = reader.GetString(0);
                            CM_LoginDatosCache.Apellido = reader.GetString(1);
                            CM_LoginDatosCache.Cargo = reader.GetString(2);
                            CM_LoginDatosCache.Correo = reader.GetString(3);
                        }

                        return true;
                    }
                    else
                        return false;
                }
            }
        }


        public bool Buscar(int id)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_Usuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", "B");
                    cmd.Parameters.AddWithValue("@empleado", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        public void Insertar(string nombre, string contrasena, int id)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_Usuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", "I");
                    cmd.Parameters.AddWithValue("@usuario", nombre);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@empleado", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Actualizar(string nombre, string contrasena, int idEmpleado)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_Usuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", "U");
                    cmd.Parameters.AddWithValue("@usuario", nombre);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@empleado", idEmpleado);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
