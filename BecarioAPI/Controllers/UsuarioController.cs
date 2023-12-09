using BecarioAPI.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        //Inyeccion de dependencias
        #region Dependencias
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        #endregion

        [HttpPost]
        public ActionResult<bool> Login(string email, string password)
        {
            try
            {
                //validar que el usuario
                var result = _usuarioRepository.LoginStatus(email, password);
                return result.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Login)}: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AgregarUsuario")]
        public void AddUsuario(Becas.Shared.Usuario usuario)
        {
            try
            {
                _usuarioRepository.AddUsuario(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddUsuario)}: " + ex.Message);
            }
        }

        [HttpDelete("EliminarUsuario")]
        public void DeleteUsuario(string email)
        {
            try
            {
                _usuarioRepository.DeleteUsuario(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteUsuario)}: " + ex.Message);
            }
        }


        [HttpGet("GetUsuario")]
        public ActionResult<Becas.Shared.Usuario> GetUsuario(string email)
        {
            try
            {
                var usuario = _usuarioRepository.GetUsuario(email);
                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetUsuario)}: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsuarios")]
        public ActionResult<List<Becas.Shared.Usuario>> GetUsuarios()
        {
            try
            {
                var usuarios = _usuarioRepository.GetUsuarios();
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetUsuarios)}: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
       
    }
}
