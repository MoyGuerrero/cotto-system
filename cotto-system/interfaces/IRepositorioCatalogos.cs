using cotto_system.Modelos;

namespace cotto_system.interfaces
{
    public interface IRepositorioCatalogos
    {
        Task addClient(Clientes clientes);
        Task<List<int>> AddPerfilVentaDet(List<AddPerfilVentaDet> addPerfilVentaDets);
        Task<int> AddPerfilVentaEnc(AddPerfilVentaEnc addPerfilVentaEnc);
        Task addProveedor(Proveedor proveedor);
        Task<int> AddValorUnidad(AddVentaUnidad addVentaUnidad);
        Task<IEnumerable<GetClases>> getClases();
        Task<IEnumerable<Clientes>> GetClientes(int idcliente, string nombre);
        Task<IEnumerable<getGradosCalificacion>> getGradosClasificacion();
        Task<IEnumerable<PerfilVentaDet>> getPerfilVentaDet(int idperfilenc);
        Task<IEnumerable<PerfilVentaEnc>> getPerfilVentaEnc();
        Task<IEnumerable<Clientes>> GetProveedor(int Idcomprador, string nombre);
        Task<IEnumerable<UnidadVenta>> getUnidadVenta();
    }
}
