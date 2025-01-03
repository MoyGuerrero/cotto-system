using cotto_system.interfaces;
using cotto_system.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cotto_system.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/catalogos")]
    public class CatalogosController : ControllerBase
    {
        private readonly IRepositorioCatalogos repositorioCatalogos;
        private readonly IWebHostEnvironment env;

        public CatalogosController(IRepositorioCatalogos repositorioClientes, IWebHostEnvironment env)
        {
            this.repositorioCatalogos = repositorioClientes;
            this.env = env;
        }


        [HttpPost]
        [Route("agregar_cliente")]
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

        [HttpPost]
        [Route("agregar_proveedor")]
        public async Task<IActionResult> PostProveedor(Proveedor proveedor)
        {
            try
            {
                await repositorioCatalogos.addProveedor(proveedor);

                return Ok(new Success(true, $"El proveedor {proveedor.nombre} se agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(true, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_grados_clasificacion")]
        public async Task<IActionResult> PostGradosClasificacion(AddGradosCalificacion addGradosCalificacion)
        {
            try
            {
                var id = await repositorioCatalogos.addGradosClasificacion(addGradosCalificacion);

                if (id == addGradosCalificacion.Idgradosclasificacion)
                {
                    return Ok(new Success(true, $"El dato con el id {id} se ha actualizado con éxito.", (int)HttpStatusCode.OK));
                }

                return Ok(new Success(true, $"El dato con el id {id} se ha agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_clase")]
        public async Task<IActionResult> AddClase(AddClases addClases)
        {
            var id = await repositorioCatalogos.addClases(addClases);

            if (addClases.idclasesenc == id)
            {
                return Ok(new Success(true, $"El dato con el id {id} se ha actualizado con éxito.", (int)HttpStatusCode.OK));
            }

            return Ok(new Success(true, $"El dato con el id {id} se ha agregado con éxito.", (int)HttpStatusCode.OK));
        }

        [HttpGet]
        [Route("grados_clasificacion")]
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

        [HttpGet]
        [Route("perfilventaenc")]
        public async Task<IActionResult> GetPerfilVentaEnc()
        {
            try
            {
                var perfilventaenc = await repositorioCatalogos.getPerfilVentaEnc();
                return Ok(new SuccessWithData<object>(true, "Success", (int)HttpStatusCode.OK, perfilventaenc));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("perfilventadet/{idperfilenc:int}")]
        public async Task<IActionResult> GetPerfilVentaDet(int idperfilenc)
        {
            try
            {
                if (idperfilenc < 0)
                {
                    return BadRequest(new Success(false, "Error en la ruta, favor verificarla.", (int)HttpStatusCode.BadRequest));
                }

                var perfilventadet = await repositorioCatalogos.getPerfilVentaDet(idperfilenc);
                return Ok(new SuccessWithData<object>(true, "Sucess", (int)HttpStatusCode.OK, perfilventadet));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("unidad_venta")]
        public async Task<IActionResult> GetUnidadVenta()
        {
            try
            {
                var unidadVenta = await repositorioCatalogos.getUnidadVenta();
                return Ok(new SuccessWithData<object>(true, "Success", (int)HttpStatusCode.OK, unidadVenta));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_unidad_venta")]
        public async Task<IActionResult> AddUnidadVenta(AddVentaUnidad addVentaUnidad)
        {
            try
            {
                var id = await repositorioCatalogos.AddValorUnidad(addVentaUnidad);

                return Ok(new Success(true, $"La unidad {id} se ha agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }


        [HttpPost]
        [Route("agregar_perfil_venta_enc")]
        public async Task<IActionResult> AddPerfilVentaEnc(AddPerfilVentaEnc addPerfilVentaEnc)
        {
            try
            {
                var id = await repositorioCatalogos.AddPerfilVentaEnc(addPerfilVentaEnc);

                return Ok(new SuccessWithID(true, $"El perfil con el ID {id} se ha agregado con éxito.", (int)HttpStatusCode.OK, id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_perfil_venta_det")]
        public async Task<IActionResult> AddPerfilVentaDet(List<AddPerfilVentaDet> addPerfilVentaDets)
        {
            try
            {
                var ids = await repositorioCatalogos.AddPerfilVentaDet(addPerfilVentaDets);

                return Ok(new Success(true, $"Success", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("descargar_plantilla/{nombre_archivo}")]
        public IActionResult Download(string nombre_archivo)
        {
            try
            {
                string filePath = Path.Combine(env.ContentRootPath, "Plantillas", $"{nombre_archivo}1.xlsx");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new Success(false, $"El archivo {nombre_archivo} no fue encontrado.", (int)HttpStatusCode.NotFound));
                }

                byte[] fileByte = System.IO.File.ReadAllBytes(filePath);


                return File(fileByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{nombre_archivo}.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("getClientes/{idcliente:int}/{nombre}")]
        public async Task<IActionResult> GetClientes(int idcliente, string nombre)
        {
            try
            {
                var clientes = await repositorioCatalogos.GetClientes(idcliente, nombre);

                if (clientes is null)
                {
                    return NotFound(new Success(false, "El cliente no existe.", (int)HttpStatusCode.NotFound));
                }

                return Ok(new SuccessWithData<object>(true, "Success", (int)HttpStatusCode.OK, clientes));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("getProveedor/{Idcomprador:int}/{nombre}")]
        public async Task<IActionResult> GetProveedor(int Idcomprador, string nombre)
        {
            try
            {
                var clientes = await repositorioCatalogos.GetProveedor(Idcomprador, nombre);

                if (clientes is null)
                {
                    return NotFound(new Success(false, "El proveedor no existe.", (int)HttpStatusCode.NotFound));
                }

                return Ok(new SuccessWithData<object>(true, "Success", (int)HttpStatusCode.OK, clientes));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("obtener_perfiles_deduccion/{posicion:int}")]
        public async Task<IActionResult> GetPerfilesMicVentaEnc(int posicion)
        {
            try
            {
                var perfilesMicVentaEnc = await repositorioCatalogos.GetPerfilMicVentaEnc(posicion);

                return Ok(new SuccessWithData<object>(true, "success", (int)HttpStatusCode.OK, perfilesMicVentaEnc));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_perfiles_deduccion/{position:int}")]
        public async Task<IActionResult> AddPerfilDeduccion(AddPerfilesDeducciones addPerfilesDeducciones, int position)
        {
            try
            {
                var id = await repositorioCatalogos.AddPerfilDeducciones(addPerfilesDeducciones, position);

                return Ok(new SuccessWithID(true, $"El perfil con el id {id} se agregado con éxito.", (int)HttpStatusCode.OK, id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_perfiles_deduccion_enc/{position:int}")]
        public async Task<IActionResult> AddPerfilDeduccionDet([FromBody] List<AddPerfilDeduccionesDet> addPerfilDeduccionesDets, int position)
        {
            try
            {
                var id = await repositorioCatalogos.AddPerfilDeduccionDet(addPerfilDeduccionesDets, position);

                return Ok(new Success(true, $"El perfil se agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("agregar_perfiles_deduccion_enc_uhml")]
        public async Task<IActionResult> AddPerfilUHML([FromBody] List<PerfilUHMLVentaDet> perfiluhml)
        {
            try
            {
                var id = await repositorioCatalogos.AddPerfilVentaUHMLDets(perfiluhml);

                return Ok(new Success(true, $"El perfil se agregado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("get_perfiles_deducciones/{idperfilenc:int}/{position:int}")]
        public async Task<IActionResult> GetPerfilesDeduccionesDet(int idperfilenc, int position)
        {
            try
            {
                if (position == 3)
                {
                    var perfilesUHML = await repositorioCatalogos.GetPerfilUHMLVentaDet(idperfilenc);
                    return Ok(new SuccessWithData<object>(true, "Datos cargado con éxito.", (int)HttpStatusCode.OK, perfilesUHML));
                }
                var perfiles = await repositorioCatalogos.GetPerfillesDeduccionesDet(idperfilenc, position);

                return Ok(new SuccessWithData<object>(true, "Datos cargado con éxito.", (int)HttpStatusCode.OK, perfiles));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete]
        [Route("{position:int}/eliminar_perfil/{idperfilenc:int}")]
        public async Task<IActionResult> DeletePerfil(int position, int idperfilenc)
        {
            try
            {
                await repositorioCatalogos.DeletePerfil(idperfilenc, position);
                return Ok(new Success(true, $"El perfil con el id {idperfilenc} se ha eliminado con éxito.", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete]
        [Route("eliminar_grados")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await repositorioCatalogos.DeleteGrados();

                return Ok(new Success(true, "Success", (int)HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }
    }
}
