using cotto_system.Modelos;
using cotto_system.Modelos.ComercializacionModel;

namespace cotto_system.interfaces
{

    public interface IRepositorioComercializacion
    {
        Task<int> addCalculoCompraDet(AddCalculocompradet addCalculocompradet);
        Task<int> addCalculoCompraEnc(AddCalculocompraenc addCalculocompraenc);
        Task<IEnumerable<GetCalculocompraenc>> GetCalculoCompraEnc();
    }

}
