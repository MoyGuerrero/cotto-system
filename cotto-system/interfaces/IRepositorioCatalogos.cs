using cotto_system.Modelos;

namespace cotto_system.interfaces
{
    public interface IRepositorioCatalogos
    {
        Task addClient(Clientes clientes);
        Task<IEnumerable<GetClases>> getClases();
        Task<IEnumerable<getGradosCalificacion>> getGradosClasificacion();
    }
}
