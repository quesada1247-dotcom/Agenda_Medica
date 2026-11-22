
using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Security.Cryptography;

namespace LogicaNegocio.Servicios
{
    public class UsuarioBLL
    {
        // Crear el objeto para acceder a los datos de los usuarios.
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        // Cantidad de iteraciones utilizadas para proteger las contraseñas.
        private const int Iteraciones = 100000;

        // Validar el inicio de sesión del usuario.
        public Usuario ValidarInicioSesion(
            string nombreUsuario,
            string contrasena)
        {
            // Comprobar que el nombre de usuario no esté vacío.
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("Ingrese el nombre de usuario.");

            // Comprobar que la contraseña no esté vacía.
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Ingrese la contraseña.");

            // Buscar el usuario en la base de datos.
            Usuario usuario =
                usuarioDAL.ObtenerPorUsuario(nombreUsuario.Trim());

            // Comprobar si el usuario existe.
            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            // Comprobar si la cuenta está activa.
            if (!usuario.Estado)
                throw new Exception("El usuario se encuentra inactivo.");

            // Verificar que la contraseña sea correcta.
            if (!VerificarContrasena(contrasena, usuario.Contrasena))
                throw new Exception("Usuario o contraseña incorrectos.");

            // Devolver los datos del usuario autenticado.
            return usuario;
        }

        // Generar un hash seguro para almacenar contraseñas.
        public static string GenerarHash(string contrasena)
        {
            // Validar que la contraseña tenga contenido.
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("La contraseña no puede estar vacía.");

            // Crear un salt aleatorio de 16 bytes.
            byte[] salt = new byte[16];

            using (var generador = RandomNumberGenerator.Create())
            {
                generador.GetBytes(salt);
            }

            // Generar el hash de la contraseña mediante PBKDF2.
            byte[] hash;

            using (var derivador = new Rfc2898DeriveBytes(
                contrasena, salt, Iteraciones))
            {
                hash = derivador.GetBytes(32);
            }

            // Devolver el algoritmo, las iteraciones, el salt y el hash.
            return "PBKDF2$" + Iteraciones + "$" +
                Convert.ToBase64String(salt) + "$" +
                Convert.ToBase64String(hash);
        }

        // Comprobar si la contraseña ingresada es correcta.
        private bool VerificarContrasena(
            string contrasena,
            string hashGuardado)
        {
            try
            {
                // Comprobar que exista un hash almacenado.
                if (string.IsNullOrWhiteSpace(hashGuardado))
                    return false;

                // Separar los componentes del hash almacenado.
                string[] partes = hashGuardado.Split('$');

                if (partes.Length != 4 || partes[0] != "PBKDF2")
                    return false;

                // Recuperar la cantidad de iteraciones.
                int iteraciones = Convert.ToInt32(partes[1]);

                if (iteraciones < 10000 || iteraciones > 1000000)
                    return false;

                // Recuperar el salt y el hash original.
                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] hashOriginal = Convert.FromBase64String(partes[3]);

                if (salt.Length != 16 || hashOriginal.Length != 32)
                    return false;

                // Generar un nuevo hash con la contraseña ingresada.
                byte[] hashCalculado;

                using (var derivador = new Rfc2898DeriveBytes(
                    contrasena, salt, iteraciones))
                {
                    hashCalculado = derivador.GetBytes(32);
                }

                // Comparar ambos hashes sin detenerse en la primera diferencia.
                int diferencia = 0;

                for (int i = 0; i < hashOriginal.Length; i++)
                {
                    diferencia |= hashOriginal[i] ^ hashCalculado[i];
                }

                return diferencia == 0;
            }
            catch
            {
                // Si ocurre algún error, la contraseña no se considera válida.
                return false;
            }
        }

        // Registrar un nuevo usuario en el sistema.
        public bool Registrar(Usuario usuario, string contrasena)
        {
            // Validar que se hayan proporcionado los datos del usuario.
            if (usuario == null)
                throw new Exception("Los datos del usuario son obligatorios.");

            // Validar el nombre completo.
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("Ingrese el nombre del usuario.");

            // Validar el nombre de usuario.
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new Exception("Ingrese un nombre de usuario.");

            // Validar la contraseña.
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("Ingrese una contraseña.");

            // Exigir una contraseña de al menos 8 caracteres.
            if (contrasena.Length < 8)
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");

            // Validar que se haya seleccionado un rol.
            if (usuario.IdRol <= 0)
                throw new Exception("Seleccione un rol para el usuario.");

            // Comprobar si el nombre de usuario ya está registrado.
            Usuario existente =
                usuarioDAL.ObtenerPorUsuario(usuario.NombreUsuario.Trim());

            if (existente != null)
                throw new Exception("El nombre de usuario ya está registrado.");

            // Limpiar los espacios de los datos de texto.
            usuario.Nombre = usuario.Nombre.Trim();
            usuario.NombreUsuario = usuario.NombreUsuario.Trim();

            // Convertir la contraseña original en un hash seguro.
            usuario.Contrasena = GenerarHash(contrasena);

            // Establecer el usuario como activo.
            usuario.Estado = true;

            // Enviar los datos a la capa de acceso a datos.
            bool registrado = usuarioDAL.Registrar(usuario);

            // Comprobar si el registro se realizó correctamente.
            if (!registrado)
                throw new Exception("No se pudo registrar el usuario.");

            // Confirmar que el registro fue exitoso.
            return true;
        }
    }
}
