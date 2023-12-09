using BecarioAPI.Authorization;
using BecarioAPI.Models.Interfaces;
using Becas.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitantesController : ControllerBase
    {

        //Inyección de dependencias
        #region Dependencias
        private readonly ISolicitantesRepository _solicitantesRepository;
        private readonly ILogger<SolicitantesController> _logger;   
        public SolicitantesController(ISolicitantesRepository solicitantesRepository, ILogger<SolicitantesController> logger)
        {
            _solicitantesRepository = solicitantesRepository;
            _logger = logger;
        }
        #endregion

        
        [HttpGet("ObetenesTodosSolicitantes")]
        public ActionResult GetSolicitantes()
        {
            try
            {
                //Validar que existan solicitantes
                var solicitantes = _solicitantesRepository.GetSolicitantes();
                if(solicitantes.Count == 0)
                {
                    return NotFound("No existen solicitantes");
                }
                return Ok(solicitantes);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetSolicitantes)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [HttpGet("ObtenerSolicitantePorId/{id}")]
        public ActionResult GetSolicitanteById(int id)
        {
            try
            {
                //validar que el campo id no sea 0
                if(id == 0)
                {
                    return BadRequest("El id no puede ser 0");
                }
                //Validar que exista el solicitante
                var solicitante = _solicitantesRepository.GetSolicitante(id);
                if(solicitante == null)
                {
                    return NotFound("El solicitante no existe");
                }
                return Ok(solicitante);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetSolicitanteById)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AgregarSolicitante")]
        public ActionResult AddSolicitante(Solicitante solicitante)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Datos del solicitante invalidos");
                }
                //Agregar solicitante
                _solicitantesRepository.AddSolicitante(solicitante);
                return Ok("Solicitante agregado correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddSolicitante)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("ActualizarSolicitante/{IdSolicitante}")]
        public ActionResult UpdateSolicitante(int IdSolicitante,[FromBody]Solicitante solicitante)
        {
            try
            {
                //validar que el solicitante exita antes de actualizarlo
                var solicitanteExistente = _solicitantesRepository.GetSolicitante(IdSolicitante);
                if(solicitanteExistente == null)
                {
                    return NotFound("El solicitante no existe");
                }
                //Actualizar solicitante
                _solicitantesRepository.UpdateSolicitante(IdSolicitante, solicitante);
                return Ok("Solicitante actualizado correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateSolicitante)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("EliminarSolicitante/{IdSolicitante}")]
        public ActionResult DeleteSolicitante(int IdSolicitante)
        {
            try
            {
                //validar si el campo esta vacio
                if (IdSolicitante == 0)
                {
                    return BadRequest("El id del solicitante es requerido");
                }
                //validar que el solicitante exita antes de eliminarlo
                var solicitanteExistente = _solicitantesRepository.GetSolicitante(IdSolicitante);
                if(solicitanteExistente == null)
                {
                    return NotFound($"El solicitante con el id {IdSolicitante} no existe");
                }
                //Eliminar solicitante
                _solicitantesRepository.DeleteSolicitante(IdSolicitante);
                return Ok("Solicitante eliminado correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteSolicitante)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
