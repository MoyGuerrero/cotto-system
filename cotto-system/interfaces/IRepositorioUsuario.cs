using cotto_system.Modelos.CatalogoModelo;

namespace cotto_system.interfaces
{
    public interface IRepositorioUsuario
    {
        Task AddUsuario(AddUsuario usuario);
        string Encritptar(string password);
        bool verifyPassword(string hashPassword, string password);
        Task<GetUsuario> Login(string Usuario);
    }
}
