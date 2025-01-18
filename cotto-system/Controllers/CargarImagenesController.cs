using cotto_system.interfaces;
using cotto_system.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cotto_system.Controllers
{
    [ApiController]
    [Route("api/uploadImage")]
    public class CargarImagenesController : ControllerBase
    {
        private readonly IRepositorioGuardarImagen repositorioGuardarImagen;

        public CargarImagenesController(IRepositorioGuardarImagen repositorioGuardarImagen)
        {
            this.repositorioGuardarImagen = repositorioGuardarImagen;
        }
        [HttpPost]
        [Route("{nameFolder}")]
        public async Task<IActionResult> Post(IFormFile file,string nameFolder)
        {
            try
            {
                if (file is null || file.Length == 0)
                {
                    return BadRequest(new Success(false, "Favor de enviar imagen", (int)HttpStatusCode.BadRequest));
                }

                //Extensiones permitidas para el avatar
                string[] extensiones = { ".jpg", ".jpeg", ".png" };

                string extension = Path.GetExtension(file.FileName);

                if (!extensiones.Contains(extension.ToLower()))
                {
                    return BadRequest(new Success(false, "Favor de seleccionar una imagen valida con extensiones .jpg,.jpeg,.png", (int)HttpStatusCode.BadRequest));
                }

                var nameFile = await repositorioGuardarImagen.GuardarImagen(file,nameFolder);
                return Ok(new Success(true, nameFile, (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
