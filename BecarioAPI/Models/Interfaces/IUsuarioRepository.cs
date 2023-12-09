namespace BecarioAPI.Models.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<bool> LoginStatus(String username, String password);
        public void AddUsuario(Becas.Shared.Usuario usuario);
        public void DeleteUsuario(string email);

        public List<Becas.Shared.Usuario> GetUsuarios();
        public Becas.Shared.Usuario GetUsuario(string email);

    }
}
