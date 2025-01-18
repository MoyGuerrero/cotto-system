
namespace cotto_system.interfaces
{
    public interface IRepositorioGuardarImagen
    {
        Task<string> GuardarImagen(IFormFile file, string nameFolder);
    }
}
