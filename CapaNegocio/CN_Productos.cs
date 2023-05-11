using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Productos
    {
        CD_Productos objProductos = new CD_Productos();

        public DataTable Estados()
        {
            return objProductos.Estados();
        }

        public DataTable Listar()
        {
            return objProductos.Listar();
        }

        public DataTable ListarActivos()
        {
            return objProductos.ListarActivos();
        }

        public DataTable Buscar( string codigo, string nombre)
        {
            return objProductos.Buscar(codigo, nombre);
        }

        public void Insertar(string codigo, string nombre, string descripcion, string stock, string precio, string estado)
        {
            objProductos.Insertar(codigo, nombre, descripcion, stock, precio, int.Parse(estado));
        }

        public void Actualizar(string codigo, string nombre, string descripcion, string stock, string precio, string estado)
        {
            objProductos.Actualizar(codigo, nombre, descripcion, stock, precio, int.Parse(estado));
        }
    }
}
