using cotto_system.Modelos;
using cotto_system.Modelos.ComercializacionModel;

namespace cotto_system.interfaces
{

        public interface IRepositorioComercializacion
        {
        Task<int> addcalculocompraenc(AddCalculocompraenc addCalculocompraenc);
        Task<List<int>> addcalculocompradet(AddCalculocompradet addCalculocompradet);
        Task<IEnumerable<GetCalculocompraenc>> getCalculocompraenc(int idcalculocompraenc, string nombre);
        Task<IEnumerable<GetCalculocompradet>> getCalculocompradet(int idcalculocompraenc);
    }

}
