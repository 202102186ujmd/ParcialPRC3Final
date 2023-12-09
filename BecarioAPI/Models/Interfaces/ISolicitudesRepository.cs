namespace BecarioAPI.Models.Interfaces
{
    public interface ISolicitudesRepository
    {
        public void AddSolicitud(Becas.Shared.Solicitud solicitud);
        public void UpdateSolicitud(int IdSolicitud,Becas.Shared.Solicitud solicitud);
        public void DeleteSolicitud(int id);
        public Becas.Shared.Solicitud GetSolicitud(int id);
        public List<Becas.Shared.Solicitud> GetSolicitudes();
    }
}
