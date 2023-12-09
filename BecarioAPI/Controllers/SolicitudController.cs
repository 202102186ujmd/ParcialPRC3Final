using BecarioAPI.Models.Interfaces;
using Becas.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {

        //Inyección de dependencias
        #region Dependencias
        private readonly ISolicitudesRepository _solicitudesRepository;
        private readonly ILogger<SolicitudController> _logger;   
        public SolicitudController(ISolicitudesRepository solicitudesRepository, ILogger<SolicitudController> logger)
        {
            _solicitudesRepository = solicitudesRepository;
            _logger = logger;
        }
        #endregion

        [HttpPost("AgragarSolicitud")]
        public ActionResult AgregarSolicitud([FromBody] Solicitud solicitud)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _solicitudesRepository.AddSolicitud(solicitud);
                    return Ok("Datos de solicitud invalidos");
                }
                return BadRequest("Datos de solicitud invalidos");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(AgregarSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("ActualizarSolicitud")]
        public ActionResult ActualizarSolicitud(int IdSolicitud,[FromBody] Solicitud solicitud)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _solicitudesRepository.UpdateSolicitud(IdSolicitud, solicitud);
                    return Ok("Datos de solicitud invalidos");
                }
                return BadRequest("Datos de solicitud invalidos");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(ActualizarSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("EliminarSolicitud/{id}")]
        public ActionResult EliminarSolicitud(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _solicitudesRepository.DeleteSolicitud(id);
                    return Ok("Datos de solicitud invalidos");
                }
                return BadRequest("Datos de solicitud invalidos");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(EliminarSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ObtenerSolicitud/{id}")]
        public ActionResult ObtenerSolicitud(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var solicitud = _solicitudesRepository.GetSolicitud(id);
                    return Ok(solicitud);
                }
                return BadRequest("Datos de solicitud invalidos");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("ObtenerSolicitudes")]
        public ActionResult ObtenerSolicitudes()
        {
            try
            {
                //Validar que existan solicitudes
                var solicitudes = _solicitudesRepository.GetSolicitudes();
                if (solicitudes.Count == 0)
                {
                    //si no existen, lanzar excepcion
                    return NotFound("No se encontraron solicitudes");
                }
                //si existen, devolverlas
                return Ok(solicitudes);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerSolicitudes)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
    }
}
