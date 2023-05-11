using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
   public abstract class CD_Conexion
    {
        private readonly string connetion;

        public CD_Conexion ()
        {
            connetion = "Data Source=(local);Initial Catalog=BD_Ferreteria;Integrated Security=True";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connetion);
        }
    }
}
