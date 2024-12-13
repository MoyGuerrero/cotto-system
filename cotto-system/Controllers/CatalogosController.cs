using cotto_system.interfaces;
using cotto_system.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cotto_system.Controllers
{
    [ApiController]
    [Route("api/catalogos")]
    public class CatalogosController : Controller
    {
        private readonly IRepositorioCatalogos repositorioCatalogos;

        public CatalogosController(IRepositorioCatalogos repositorioClientes)
        {
            this.repositorioCatalogos = repositorioClientes;
        }


        [HttpPost]
        [Route("agregar_cliente")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(Clientes clientes)
        {
            try
            {
                await repositorioCatalogos.addClient(clientes);

                return Ok(new Success(true, "Cliente agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(true, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("grados_clasificacion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetGradosClasificacion()
        {
            try
            {
                var grados_clasificacion = await repositorioCatalogos.getGradosClasificacion();
                return Ok(new SuccessWithData<object>(true, "Datos de obtenidos con éxito.", (int)HttpStatusCode.OK, grados_clasificacion));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("clases")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetClases()
        {
            try
            {
                var clases = await repositorioCatalogos.getClases();
                return Ok(new SuccessWithData<object>(true, "Success", (int)HttpStatusCode.OK, clases));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
