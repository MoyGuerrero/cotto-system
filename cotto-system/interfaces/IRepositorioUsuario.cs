using cotto_system.Modelos;

namespace cotto_system.interfaces
{
    public interface IRepositorioUsuario
    {
        Task AddUsuario(AddUsuario usuario);
        Task<AddUsuario> Login(string usuario);
    }
}
