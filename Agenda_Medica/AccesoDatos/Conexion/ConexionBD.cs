using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Conexion
{
    public class ConexionBD
    {
        private readonly string cadenaConexion =
           "Server=localhost;Database=AgendaMedica_Final;Uid=root;Pwd=a123456789;";

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }

        public bool ProbarConexion()
        {
            try
            {
                using (MySqlConnection conexion = ObtenerConexion())
                {
                    conexion.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
