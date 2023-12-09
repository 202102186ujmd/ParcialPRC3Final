using BecarioAPI.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Inyección de dependencias
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;   
        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [HttpPost("Login")]
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
        public ActionResult AddUsuario([FromBody] Becas.Shared.Usuario usuario)
        {
            try
            {
                //validar que el usuario no exista
                if (_usuarioRepository.GetUsuario(usuario.Email) != null)
                {
                    throw new Exception("El usuario ya existe");
                }
                _usuarioRepository.AddUsuario(usuario);
                return Ok("Usuario Agragado Exitosamente");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddUsuario)}: " + ex.Message);
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("EliminarUsuario/{email}")]
        public ActionResult DeleteUsuario(string email)
        {
            try
            {
                //validar que el usuario exista
                var usuario = _usuarioRepository.GetUsuario(email);
                if(usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }
                _usuarioRepository.DeleteUsuario(email);
                return Ok("Usuario Eliminado Exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteUsuario)}: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetUsuario")]
        public ActionResult<Becas.Shared.Usuario> GetUsuario(string email)
        {
            try
            {
                //validar que el usuario exista
                var usuario = _usuarioRepository.GetUsuario(email);
                if(usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }
                return Ok(usuario);
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
               //validar que existan usuarios
                var usuarios = _usuarioRepository.GetUsuarios();
                if(usuarios.Count == 0)
                {
                    throw new Exception("No existen usuarios");
                }
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetUsuarios)}: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
