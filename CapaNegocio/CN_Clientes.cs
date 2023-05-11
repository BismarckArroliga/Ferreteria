using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
   public class CN_Clientes
    {
        CD_Clientes objClientes = new CD_Clientes();

        public DataTable Listar()
        {
            return objClientes.Listar();
        }

        public DataTable Buscar(string id)
        {
            return objClientes.Buscar(int.Parse(id));
        }


        public void Insertar(string nombre, string apellido, string cedula, string telefono, string direccion)
        {
            objClientes.Insertar(nombre, apellido, cedula, telefono, direccion);
        }

        public void Actualizar( string id, string nombre, string apellido, string cedula, string telefono, string direccion)
        {
            objClientes.Actualizar(int.Parse(id),nombre, apellido, cedula, telefono, direccion);
        }

    }
}
