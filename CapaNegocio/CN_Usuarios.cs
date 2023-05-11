using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        CD_Usuarios objUsuarios = new CD_Usuarios();

        public bool Login(string usuario, string contrasena)
        {
            return objUsuarios.Login(usuario, contrasena);
        }

        public void Insertar(string usuario, string contrasena, string id)
        {
            objUsuarios.Insertar(usuario, contrasena, int.Parse(id));
        }

        public void Actualizar(string usuario, string contrasena, string id)
        {
            objUsuarios.Actualizar(usuario, contrasena, int.Parse(id));
        }

        public bool Buscar(string id)
        {
            return objUsuarios.Buscar(int.Parse(id));
        }
    }
}
