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
        private readonly IRepositorioCatalogos repositorioClientes;

        public CatalogosController(IRepositorioCatalogos repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }


        [HttpPost]
        [Route("agregar_cliente")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(Clientes clientes)
        {
            try
            {
                await repositorioClientes.addClient(clientes);

                return Ok(new Success<object>(true, "Cliente agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success<Object>(true, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("grados_clasificacion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetGradosClasificacion()
        {
            try
            {
                var grados_clasificacion = await repositorioClientes.getGradosClasificacion();
                return Ok(new SuccessWithData<object>(true, "Datos de obtenidos con éxito.", (int)HttpStatusCode.OK, grados_clasificacion));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success<object>(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
