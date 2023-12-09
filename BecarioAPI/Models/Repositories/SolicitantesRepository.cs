using BecarioAPI.Models.Interfaces;
using Becas.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BecarioAPI.Models.Repositories
{
    public class SolicitantesRepository : ISolicitantesRepository
    {
        //Inyeccion de dependencias
        #region Dependencias
        private readonly BecarioDBContext _db;
        private readonly ILogger<SolicitantesRepository> _logger;
        public SolicitantesRepository(BecarioDBContext db, ILogger<SolicitantesRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        #endregion


        #region Metodos
        public void AddSolicitante(Solicitante solicitante)
        {
            try
            {
                //Validar que el solicitante no exista
                var solicitanteExistente = _db.Solicitantes.FirstOrDefault(x => x.NombreCompleto == solicitante.NombreCompleto);
                if (solicitanteExistente != null)
                {
                    throw new Exception("El solicitante ya existe");
                }
                //Agregar solicitante
                _db.Solicitantes.Add(solicitante);
                _db.SaveChanges();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(AddSolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSolicitante(int id)
        {
            try
            {
                //buscar solicitante que se quiere eliminar
                var solicitanteExistente = _db.Solicitantes.Find(id);
                if (solicitanteExistente != null)
                {
                    //Eliminar solicitante
                    _db.Solicitantes.Remove(solicitanteExistente);
                    _db.SaveChanges();
                }
                throw new Exception("El solicitante no existe");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(DeleteSolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Solicitante GetSolicitante(int id)
        {
            try
            {
                //buscar solicitante que se quiere obtener
                var solicitante = _db.Solicitantes.Find(id);
                //validar que el solicitante exista
                if (solicitante != null)
                {
                    //si existe, retornarlo
                    return solicitante;
                }
                //si no existe, lanzar excepcion
                throw new Exception("El solicitante no existe");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetSolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Solicitante> GetSolicitantes()
        {
            try
            {
                //Validar que existan solicitantes
                var solicitantes = _db.Solicitantes.ToList();
                if (solicitantes != null)
                {
                    //si existen, retornarlos
                    return solicitantes;
                }
                //si no existen, lanzar excepcion
                throw new Exception("No existen solicitantes");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(GetSolicitantes)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSolicitante(int IdSolicitante, Solicitante solicitante)
        {
            try
            {
                //buscar solicitante que se quiere actualizar
                var solicitanteExistente = _db.Solicitantes.Find(IdSolicitante);
                if (solicitanteExistente != null)
                {
                    //Actualizar los campos que se desean nada mas (no todos)
                    solicitanteExistente.IdSolicitante = IdSolicitante;
                    solicitanteExistente.NombreCompleto = solicitante.NombreCompleto;
                    solicitanteExistente.FechaNacimiento = solicitante.FechaNacimiento != default ? solicitante.FechaNacimiento : solicitanteExistente.FechaNacimiento;
                    solicitanteExistente.EstadoCivil = solicitante.EstadoCivil ?? solicitanteExistente.EstadoCivil;
                    solicitanteExistente.Dui = solicitante.Dui ?? solicitanteExistente.Dui;
                    solicitanteExistente.Pasaporte = solicitante.Pasaporte ?? solicitanteExistente.Pasaporte;
                    solicitanteExistente.FechaExpedicionPasaporte = solicitante.FechaExpedicionPasaporte != default ? solicitante.FechaExpedicionPasaporte : solicitanteExistente.FechaExpedicionPasaporte;
                    solicitanteExistente.LugardeExpedicion = solicitante.LugardeExpedicion ?? solicitanteExistente.LugardeExpedicion;
                    solicitanteExistente.Direccion = solicitante.Direccion ?? solicitanteExistente.Direccion;
                    solicitanteExistente.Email = solicitante.Email ?? solicitanteExistente.Email;
                    solicitanteExistente.Telefono = solicitante.Telefono ?? solicitanteExistente.Telefono;
                    
                    //Guardar cambios
                    _db.Entry(solicitanteExistente).State = EntityState.Modified;
                    _db.SaveChanges();
                    //Devolver mensaje de exito
                    
                }
                else
                {
                    //si no existe, lanzar excepcion
                    throw new Exception("El solicitante no existe");
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error en {nameof(UpdateSolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
