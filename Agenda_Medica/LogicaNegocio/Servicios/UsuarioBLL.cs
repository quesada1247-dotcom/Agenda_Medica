
using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Data;
using System.Security.Cryptography;

namespace LogicaNegocio.Servicios
{
    public class UsuarioBLL
    {
        // Objeto para acceder a los datos de los usuarios.
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        // Cantidad de iteraciones para proteger las contraseñas.
        private const int Iteraciones = 100000;

        // =====================================================
        // 1. VALIDAR EL INICIO DE SESION
        // =====================================================
        public Usuario ValidarInicioSesion(
            string nombreUsuario,
            string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("Ingrese el nombre de usuario.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Ingrese la contraseña.");

            // Buscar el usuario en la base de datos.
            Usuario usuario =
                usuarioDAL.ObtenerPorUsuario(nombreUsuario.Trim());

            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            // Comprobar que la cuenta esté activa.
            if (!usuario.Estado)
                throw new Exception("El usuario se encuentra inactivo.");

            // Comprobar la contraseña.
            if (!VerificarContrasena(contrasena, usuario.Contrasena))
                throw new Exception("Usuario o contraseña incorrectos.");

            return usuario;
        }

        // =====================================================
        // 2. GENERAR HASH SEGURO PARA LAS CONTRASEÑAS
        // =====================================================
        public static string GenerarHash(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("La contraseña no puede estar vacía.");

            // Crear un salt aleatorio.
            byte[] salt = new byte[16];

            using (var generador = RandomNumberGenerator.Create())
            {
                generador.GetBytes(salt);
            }

            // Generar el hash mediante PBKDF2.
            byte[] hash;

            using (var derivador = new Rfc2898DeriveBytes(
                contrasena, salt, Iteraciones))
            {
                hash = derivador.GetBytes(32);
            }

            // Guardar algoritmo, iteraciones, salt y hash.
            return "PBKDF2$" + Iteraciones + "$" +
                Convert.ToBase64String(salt) + "$" +
                Convert.ToBase64String(hash);
        }

        // =====================================================
        // 3. VERIFICAR LA CONTRASEÑA
        // =====================================================
        private bool VerificarContrasena(
            string contrasena,
            string hashGuardado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hashGuardado))
                    return false;

                string[] partes = hashGuardado.Split('$');

                if (partes.Length != 4 || partes[0] != "PBKDF2")
                    return false;

                int iteraciones = Convert.ToInt32(partes[1]);

                if (iteraciones < 10000 || iteraciones > 1000000)
                    return false;

                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] hashOriginal = Convert.FromBase64String(partes[3]);

                if (salt.Length != 16 || hashOriginal.Length != 32)
                    return false;

                byte[] hashCalculado;

                using (var derivador = new Rfc2898DeriveBytes(
                    contrasena, salt, iteraciones))
                {
                    hashCalculado = derivador.GetBytes(32);
                }

                // Comparar los hashes.
                int diferencia = 0;

                for (int i = 0; i < hashOriginal.Length; i++)
                {
                    diferencia |= hashOriginal[i] ^ hashCalculado[i];
                }

                return diferencia == 0;
            }
            catch
            {
                return false;
            }
        }

        // =====================================================
        // 4. REGISTRAR UN NUEVO USUARIO
        // =====================================================
        public bool Registrar(Usuario usuario, string contrasena)
        {
            if (usuario == null)
                throw new Exception("Los datos del usuario son obligatorios.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("Ingrese el nombre del usuario.");

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new Exception("Ingrese un nombre de usuario.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Ingrese una contraseña.");

            if (contrasena.Length < 8)
                throw new Exception(
                    "La contraseña debe tener al menos 8 caracteres.");

            if (usuario.IdRol <= 0)
                throw new Exception("Seleccione un rol para el usuario.");

            // Comprobar que el nombre de usuario no esté repetido.
            Usuario existente =
                usuarioDAL.ObtenerPorUsuario(usuario.NombreUsuario.Trim());

            if (existente != null)
                throw new Exception(
                    "El nombre de usuario ya está registrado.");

            // Limpiar los datos.
            usuario.Nombre = usuario.Nombre.Trim();
            usuario.NombreUsuario = usuario.NombreUsuario.Trim();

            // Convertir la contraseña en un hash seguro.
            usuario.Contrasena = GenerarHash(contrasena);

            // Los usuarios nuevos se registran activos.
            usuario.Estado = true;

            bool registrado = usuarioDAL.Registrar(usuario);

            if (!registrado)
                throw new Exception("No se pudo registrar el usuario.");

            return true;
        }

        // =====================================================
        // 5. LISTAR TODOS LOS USUARIOS
        // =====================================================
        public DataTable ListarUsuarios()
        {
            return usuarioDAL.ListarUsuarios();
        }

        // =====================================================
        // 6. BUSCAR USUARIOS
        // =====================================================
        public DataTable BuscarUsuarios(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return usuarioDAL.ListarUsuarios();

            return usuarioDAL.BuscarUsuarios(texto.Trim());
        }

        // =====================================================
        // 7. MODIFICAR LOS DATOS DE UN USUARIO
        // =====================================================
        public bool Modificar(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Seleccione un usuario.");

            if (usuario.IdUsuario <= 0)
                throw new Exception("El usuario seleccionado no es válido.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("Ingrese el nombre del usuario.");

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new Exception("Ingrese el nombre de usuario.");

            if (usuario.IdRol <= 0)
                throw new Exception("Seleccione un rol.");

            // Limpiar los datos.
            usuario.Nombre = usuario.Nombre.Trim();
            usuario.NombreUsuario = usuario.NombreUsuario.Trim();

            // Comprobar que el nombre de usuario no pertenezca
            // a otra cuenta.
            Usuario existente =
                usuarioDAL.ObtenerPorUsuario(usuario.NombreUsuario);

            if (existente != null &&
                existente.IdUsuario != usuario.IdUsuario)
            {
                throw new Exception(
                    "Ese nombre de usuario ya pertenece a otra cuenta.");
            }

            bool modificado = usuarioDAL.Modificar(usuario);

            if (!modificado)
                throw new Exception(
                    "No se pudieron modificar los datos del usuario.");

            return true;
        }

        // =====================================================
        // 8. DESACTIVAR UN USUARIO
        // =====================================================
        public bool Desactivar(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new Exception("Seleccione un usuario válido.");

            bool desactivado = usuarioDAL.Desactivar(idUsuario);

            if (!desactivado)
                throw new Exception("No se pudo desactivar el usuario.");

            return true;
        }

        // =====================================================
        // 9. LISTAR LOS ROLES DISPONIBLES
        // =====================================================
        public DataTable ListarRoles()
        {
            return usuarioDAL.ListarRoles();
        }
    }
}
