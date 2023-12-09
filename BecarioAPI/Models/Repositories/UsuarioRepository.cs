using BecarioAPI.Models.Interfaces;
using Becas.Shared;

namespace BecarioAPI.Models.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //Inyección de dependencias
        #region Dependencias
        private readonly BecarioDBContext _db;
        private readonly ILogger<UsuarioRepository> _logger;
        public UsuarioRepository(BecarioDBContext db, ILogger<UsuarioRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        #endregion
        public void AddUsuario(Usuario usuario)
        {
            try
            {
                //Validar que el usuario no exista
                var usuarioExistente = _db.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);
                if (usuarioExistente != null)
                {
                    throw new Exception("El usuario ya existe");
                }
                //agregar usuario
                _db.Usuarios.Add(usuario);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddUsuario)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUsuario(string email)
        {
            try
            {
                //Validar que el usuario exista
                var usuarioExistente = _db.Usuarios.FirstOrDefault(u => u.Email == email);
                if (usuarioExistente == null)
                {
                    throw new Exception("El usuario no existe");
                }
                //Eliminar usuario
                _db.Usuarios.Remove(usuarioExistente);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteUsuario)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Usuario GetUsuario(string email)
        {
            try
            {
                //Validar que el usuario exista
                var usuarioExistente = _db.Usuarios.FirstOrDefault(u => u.Email == email);
                if (usuarioExistente == null)
                {
                    throw new Exception("El usuario no existe");
                }
                //Retornar usuario
                return usuarioExistente;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetUsuario)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Usuario> GetUsuarios()
        {
            try
            {
                //validar que existan usuarios
                var usuarios = _db.Usuarios.ToList();
                if (usuarios == null)
                {
                    throw new Exception("No existen usuarios");
                }
                //Retornar usuarios
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetUsuarios)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> LoginStatus(string username, string password)
        {
            try
            {
                //buscar el usuario
                var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == username);
                if (usuario != null)
                {
                    //validar contraseña
                    if (usuario.Contrasena == password)
                    {
                        return Task.FromResult(true);
                    }
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(LoginStatus)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
