
using AccesoDatos.Conexion;
using Modelos.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AccesoDatos.Datos
{
    public class UsuarioDAL
    {
        // Objeto para obtener la conexión con MySQL.
        private ConexionBD conexionBD = new ConexionBD();

        // =====================================================
        // 1. BUSCAR UN USUARIO POR SU NOMBRE DE USUARIO
        // =====================================================
        public Usuario ObtenerPorUsuario(string nombreUsuario)
        {
            Usuario usuario = null;

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

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@usuario", nombreUsuario);

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

            return usuario;
        }

        // =====================================================
        // 2. REGISTRAR UN NUEVO USUARIO
        // =====================================================
        public bool Registrar(Usuario usuario)
        {
            string consulta = @"
                INSERT INTO Usuarios
                (nombre, usuario, correo, contrasena, id_rol, estado)
                VALUES
                (@nombre, @usuario, @correo, @contrasena, @idRol, @estado)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@nombre", usuario.Nombre);

                    comando.Parameters.AddWithValue(
                        "@usuario", usuario.NombreUsuario);

                    comando.Parameters.AddWithValue(
                        "@correo",
                        string.IsNullOrWhiteSpace(usuario.Correo)
                        ? (object)DBNull.Value
                        : usuario.Correo);

                    comando.Parameters.AddWithValue(
                        "@contrasena", usuario.Contrasena);

                    comando.Parameters.AddWithValue(
                        "@idRol", usuario.IdRol);

                    comando.Parameters.AddWithValue(
                        "@estado", usuario.Estado);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // =====================================================
        // 3. LISTAR TODOS LOS USUARIOS
        // =====================================================
        public DataTable ListarUsuarios()
        {
            DataTable tabla = new DataTable();

            string consulta = @"
                SELECT
                    u.id_usuario AS ID,
                    u.nombre AS Nombre,
                    u.usuario AS Usuario,
                    u.correo AS Correo,
                    r.nombre AS Rol,
                    u.estado AS Estado
                FROM Usuarios u
                INNER JOIN Roles r ON u.id_rol = r.id_rol
                ORDER BY u.id_usuario DESC";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlDataAdapter adaptador =
                    new MySqlDataAdapter(consulta, conexion))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        // =====================================================
        // 4. BUSCAR USUARIOS POR NOMBRE, USUARIO O CORREO
        // =====================================================
        public DataTable BuscarUsuarios(string texto)
        {
            DataTable tabla = new DataTable();

            string consulta = @"
                SELECT
                    u.id_usuario AS ID,
                    u.nombre AS Nombre,
                    u.usuario AS Usuario,
                    u.correo AS Correo,
                    r.nombre AS Rol,
                    u.estado AS Estado
                FROM Usuarios u
                INNER JOIN Roles r ON u.id_rol = r.id_rol
                WHERE u.nombre LIKE @texto
                   OR u.usuario LIKE @texto
                   OR u.correo LIKE @texto
                ORDER BY u.id_usuario DESC";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@texto", "%" + texto + "%");

                    using (MySqlDataAdapter adaptador =
                        new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        // =====================================================
        // 5. MODIFICAR LOS DATOS DE UN USUARIO
        // =====================================================
        public bool Modificar(Usuario usuario)
        {
            string consulta = @"
                UPDATE Usuarios
                SET
                    nombre = @nombre,
                    usuario = @usuario,
                    correo = @correo,
                    id_rol = @idRol,
                    estado = @estado
                WHERE id_usuario = @idUsuario";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@nombre", usuario.Nombre);

                    comando.Parameters.AddWithValue(
                        "@usuario", usuario.NombreUsuario);

                    comando.Parameters.AddWithValue(
                        "@correo",
                        string.IsNullOrWhiteSpace(usuario.Correo)
                        ? (object)DBNull.Value
                        : usuario.Correo);

                    comando.Parameters.AddWithValue(
                        "@idRol", usuario.IdRol);

                    comando.Parameters.AddWithValue(
                        "@estado", usuario.Estado);

                    comando.Parameters.AddWithValue(
                        "@idUsuario", usuario.IdUsuario);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // =====================================================
        // 6. DESACTIVAR UN USUARIO
        // =====================================================
        public bool Desactivar(int idUsuario)
        {
            string consulta = @"
                UPDATE Usuarios
                SET estado = 0
                WHERE id_usuario = @idUsuario";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idUsuario", idUsuario);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // =====================================================
        // 7. LISTAR LOS ROLES DISPONIBLES
        // =====================================================
        public DataTable ListarRoles()
        {
            DataTable tabla = new DataTable();

            string consulta = @"
                SELECT
                    id_rol,
                    nombre
                FROM Roles
                ORDER BY id_rol";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlDataAdapter adaptador =
                    new MySqlDataAdapter(consulta, conexion))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
    }
}
