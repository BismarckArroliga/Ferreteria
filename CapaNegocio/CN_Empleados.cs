using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
  public class CN_Empleados
    {
        CD_Empleados objEmpleados = new CD_Empleados();

        public DataTable Listar()
        {
            return objEmpleados.Listar();
        }

        public DataTable Cargos()
        {
            return objEmpleados.Cargos();
        }

        public void Insertar(string nombre, string apellido, string correo, string telefono, string direccion, string cargo)
        {
            objEmpleados.Insertar(nombre, apellido, correo, telefono, direccion, int.Parse(cargo));
        }

        public void Eliminar(string id)
        {
            objEmpleados.Eliminar(int.Parse(id));
        }

        public void Actualizar(string id,  string nombre, string apellido, string correo, string telefono, string direccion, string cargo)
        {
            objEmpleados.Actualizar(int.Parse(id), nombre, apellido, correo, telefono, direccion, int.Parse(cargo));
        }

        public bool Buscar(string id)
        {
            return objEmpleados.Buscar(int.Parse(id));
        }
    }
}







