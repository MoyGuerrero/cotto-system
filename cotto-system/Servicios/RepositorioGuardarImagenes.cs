using cotto_system.interfaces;
using cotto_system.Modelos;
using System.Net;

namespace cotto_system.Servicios
{
    public class RepositorioGuardarImagenes: IRepositorioGuardarImagen
    {

        public async Task<string> GuardarImagen(IFormFile file, string nameFolder)
        {
            string extension = Path.GetExtension(file.FileName);
            string newNameFile = $"{Guid.NewGuid()}{extension}";

            string uploadsFolder = Path.Combine("C:/inetpub/wwwroot", nameFolder);
            string filePath = Path.Combine(uploadsFolder, newNameFile);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return newNameFile;
        }
    }
}
