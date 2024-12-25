using cotto_system.Modelos;

namespace cotto_system.interfaces
{
    public interface IRepositorioCatalogos
    {
        Task<int> addClases(AddClases addClases);
        Task addClient(Clientes clientes);
        Task<int> addGradosClasificacion(AddGradosCalificacion addGradosCalificacion);
        Task<List<int>> AddPerfilDeduccionDet(List<AddPerfilDeduccionesDet> addPerfilDeduccionesDet);
        Task<int> AddPerfilDeducciones(AddPerfilesDeducciones addPerfilDeducciones);
        Task<List<int>> AddPerfilVentaDet(List<AddPerfilVentaDet> addPerfilVentaDets);
        Task<int> AddPerfilVentaEnc(AddPerfilVentaEnc addPerfilVentaEnc);
        Task addProveedor(Proveedor proveedor);
        Task<int> AddValorUnidad(AddVentaUnidad addVentaUnidad);
        Task<IEnumerable<GetClases>> getClases();
        Task<IEnumerable<Clientes>> GetClientes(int idcliente, string nombre);
        Task<IEnumerable<getGradosCalificacion>> getGradosClasificacion();
        Task<IEnumerable<AddPerfilDeduccionesDet>> GetPerfillesDeduccionesDet(int idperfilenc);
        Task<IEnumerable<PerfilMicVentaEnc>> GetPerfilMicVentaEnc(int posicion);
        Task<IEnumerable<PerfilVentaDet>> getPerfilVentaDet(int idperfilenc);
        Task<IEnumerable<PerfilVentaEnc>> getPerfilVentaEnc();
        Task<IEnumerable<Clientes>> GetProveedor(int Idcomprador, string nombre);
        Task<IEnumerable<UnidadVenta>> getUnidadVenta();
    }
}
