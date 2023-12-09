using BecarioAPI.Models.Interfaces;
using Becas.Shared;
using Microsoft.EntityFrameworkCore;

namespace BecarioAPI.Models.Repositories
{
    public class SolicitudesRepository : ISolicitudesRepository
    {
        //Inyeccion de dependencias
        #region Dependencias
        private readonly BecarioDBContext _db;
        private readonly ILogger<SolicitudesRepository> _logger;
        public SolicitudesRepository(BecarioDBContext db, ILogger<SolicitudesRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        #endregion


        #region Metodos
        public void AddSolicitud(Solicitud solicitud)
        {
            try
            {
                //validar que el solicitante exista
                var solicitanteExistente = _db.Solicitantes.Find(solicitud.IdSolicitante);
                if (solicitanteExistente != null)
                {
                    //validar que no exista una solicitud del mismo solicitante
                    var solicitudExistente = _db.Solicitudes.Where(s => s.IdSolicitante == solicitud.IdSolicitante).FirstOrDefault();
                    if (solicitudExistente == null)
                    {
                        _db.Solicitudes.Add(solicitud);
                        _db.SaveChanges();
                        return;
                    }
                    throw new Exception("Ya existe una solicitud para este solicitante");
                }
                throw new Exception("El solicitante no existe");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddSolicitud)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSolicitud(int id)
        {
            try
            {
                //validar que la solicitud exista
                var solicitudExistente = _db.Solicitudes.Find(id);
                if (solicitudExistente != null)
                {
                    //si existe, eliminarla
                    _db.Solicitudes.Remove(solicitudExistente);
                    _db.SaveChanges();
                    return;
                }
                //si no existe, lanzar excepcion
                throw new Exception("La solicitud no existe");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteSolicitud)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Solicitud GetSolicitud(int id)
        {
            try
            {
                //validar que la solicitud exista
                var solicitudExistente = _db.Solicitudes.Find(id);
                if (solicitudExistente != null)
                {
                    //si existe, devolverla
                    return solicitudExistente;
                }
                //si no existe, lanzar excepcion
                throw new Exception("La solicitud no existe");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetSolicitud)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Solicitud> GetSolicitudes()
        {
            try
            {
                //Validar que existan solicitudes
                var solicitudes = _db.Solicitudes.ToList();
                if (solicitudes.Count > 0)
                {
                    //si existen, devolverlas
                    return solicitudes;
                }
                //si no existen, lanzar excepcion
                throw new Exception("No existen solicitudes");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetSolicitudes)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSolicitud(int IdSolicitud, Solicitud solicitud)
        {
            try
            {
                //validar que la solicitud exista
                var solicitudExistente = _db.Solicitudes.Find(IdSolicitud);
                //validar si se encontro la solicitud
                if (solicitudExistente != null)
                {
                    //Actualizar los datos de la solicitud
                    solicitudExistente.IdSolicitante = solicitud.IdSolicitante > 0 ? solicitud.IdSolicitante : solicitudExistente.IdSolicitante;
                    solicitudExistente.FechadeCreacion = solicitud.FechadeCreacion != default ? solicitud.FechadeCreacion : solicitudExistente.FechadeCreacion;
                    solicitudExistente.IdEstado = solicitud.IdEstado > 0 ? solicitud.IdEstado : solicitudExistente.IdEstado;

                    //Guardar los cambios
                    _db.Entry(solicitudExistente).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    //si no existe, lanzar excepcion
                    throw new Exception("La solicitud no existe");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateSolicitud)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
