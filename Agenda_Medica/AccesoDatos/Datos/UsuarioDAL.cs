
using AccesoDatos.Conexion;
using Modelos.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace AccesoDatos.Datos
{
    public class UsuarioDAL
    {
        // Crear el objeto encargado de obtener la conexión MySQL.
        private ConexionBD conexionBD = new ConexionBD();

        // Buscar un usuario por su nombre de usuario.
        public Usuario ObtenerPorUsuario(string nombreUsuario)
        {
            Usuario usuario = null;

            // Consulta para obtener los datos del usuario y su rol.
            string consulta = @"
                SELECT
                    u.id_usuario,
                    u.nombre,
                    u.usuario,
                    u.correo,
                    u.contrasena,
                    u.id_rol,
                    u.estado,
                    r.nombre AS nombre_rol
                FROM Usuarios u
                INNER JOIN Roles r ON u.id_rol = r.id_rol
                WHERE u.usuario = @usuario
                LIMIT 1";

            // Obtener y abrir la conexión con MySQL.
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                // Preparar el comando SQL.
                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    // Enviar el nombre de usuario como parámetro.
                    comando.Parameters.AddWithValue("@usuario", nombreUsuario);

                    // Leer el resultado de la consulta.
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            usuario = new Usuario();

                            usuario.IdUsuario =
                                Convert.ToInt32(lector["id_usuario"]);

                            usuario.Nombre = lector["nombre"].ToString();

                            usuario.NombreUsuario =
                                lector["usuario"].ToString();

                            usuario.Correo =
                                lector["correo"] == DBNull.Value
                                ? ""
                                : lector["correo"].ToString();

                            usuario.Contrasena =
                                lector["contrasena"].ToString();

                            usuario.IdRol =
                                Convert.ToInt32(lector["id_rol"]);

                            usuario.NombreRol =
                                lector["nombre_rol"].ToString();

                            usuario.Estado =
                                Convert.ToBoolean(lector["estado"]);
                        }
                    }
                }
            }

            // Devolver el usuario encontrado o null.
            return usuario;
        }

        // Registrar un nuevo usuario en la base de datos.
        public bool Registrar(Usuario usuario)
        {
            // Consulta SQL para insertar un nuevo usuario.
            string consulta = @"
                INSERT INTO Usuarios
                (nombre, usuario, correo, contrasena, id_rol, estado)
                VALUES
                (@nombre, @usuario, @correo, @contrasena, @idRol, @estado)";

            // Obtener y abrir la conexión con MySQL.
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                // Crear el comando para ejecutar la consulta.
                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    // Asignar los datos del usuario.
                    comando.Parameters.AddWithValue("@nombre", usuario.Nombre);

                    comando.Parameters.AddWithValue(
                        "@usuario", usuario.NombreUsuario);

                    // Guardar NULL si el correo está vacío.
                    comando.Parameters.AddWithValue("@correo",
                        string.IsNullOrWhiteSpace(usuario.Correo)
                        ? (object)DBNull.Value
                        : usuario.Correo);

                    // Guardar la contraseña previamente convertida a hash.
                    comando.Parameters.AddWithValue(
                        "@contrasena", usuario.Contrasena);

                    // Asignar el rol y el estado del usuario.
                    comando.Parameters.AddWithValue("@idRol", usuario.IdRol);

                    comando.Parameters.AddWithValue("@estado", usuario.Estado);

                    // Ejecutar el INSERT y comprobar si fue exitoso.
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
