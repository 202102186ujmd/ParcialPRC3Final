namespace BecarioAPI.Models.Interfaces
{
    public interface ISolicitantesRepository
    {
        public void AddSolicitante(Becas.Shared.Solicitante solicitante);
        public void UpdateSolicitante(int IdSolicitante, Becas.Shared.Solicitante solicitante);
        public void DeleteSolicitante(int id);
        public Becas.Shared.Solicitante GetSolicitante(int id);
        public List<Becas.Shared.Solicitante> GetSolicitantes();
    }
}
