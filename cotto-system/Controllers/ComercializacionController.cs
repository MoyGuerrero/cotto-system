using cotto_system.interfaces;
using cotto_system.Modelos;
using cotto_system.Modelos.ComercializacionModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cotto_system.Controllers
{
    [ApiController]
    [Route("api/comercializacion")]
    public class ComercializacionController : ControllerBase
    {
        private readonly IRepositorioComercializacion repositorioComercializacion;
        private readonly IWebHostEnvironment env;
        public ComercializacionController(IRepositorioComercializacion repositorioComercializacion, IWebHostEnvironment env)
        {
            this.repositorioComercializacion = repositorioComercializacion;
            this.env = env;
        }

        [HttpPost]
        [Route("agregar_calculocompra")]
        public async Task<IActionResult> PostCalculocompraenc(AddCalculocompraenc addcalculocompraenc)
        {
            try
            {
                var idcalculocompranec = await repositorioComercializacion.addcalculocompraenc(addcalculocompraenc);

                if (idcalculocompranec == addcalculocompraenc.idcalculo)
                {
                    return Ok(new Success(true, $"La Compra con el id {idcalculocompranec} se ha actualizado con éxito.", (int)HttpStatusCode.OK));
                }

                return Ok(new Success(true, $"La Compra con el id {idcalculocompranec} se ha agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }
        [HttpGet]
        [Route("getCalculoCompra/{idcalculocompraenc:int}/{nombre}")]
        public async Task<IActionResult> getCalculocompraenc(int idcalculocompraenc, string nombre)
        {
            try
            {
                var compras = await repositorioComercializacion.getCalculocompraenc(idcalculocompraenc, nombre);

                if (compras is null)
                {
                    return NotFound(new Success(false, "El cliente no existe.", (int)HttpStatusCode.NotFound));
                }

                return Ok(new SuccessWithData<object>(true, "Success", (int)HttpStatusCode.OK, compras));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
