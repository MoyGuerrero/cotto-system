﻿using cotto_system.interfaces;
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
        private readonly IWebHostEnvironment env;

        public CatalogosController(IRepositorioCatalogos repositorioClientes, IWebHostEnvironment env)
        {
            this.repositorioCatalogos = repositorioClientes;
            this.env = env;
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

        [HttpPost]
        [Route("agregar_proveedor")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostProveedor(Proveedor proveedor)
        {
            try
            {
                await repositorioCatalogos.addProveedor(proveedor);

                return Ok(new Success(true, $"El proveedor {proveedor.nombre } se agregado con éxito.", (int)HttpStatusCode.OK));
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

        [HttpGet]
        [Route("perfilventaenc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Route("descargar_plantilla_gc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Download()
        {
            try
            {
                string filePath = Path.Combine(env.ContentRootPath, "Plantillas", "plantilla_carga_gradosclasif.xltx");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new Success(false, $"El archivo plantilla_carga_gradosclasif no fue encontrado.", (int)HttpStatusCode.NotFound));
                }

                byte[] fileByte = System.IO.File.ReadAllBytes(filePath);


                return File(fileByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "plantilla_carga_gradosclasif.xltx");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("getClientes/{idcliente:int}/{nombre}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    }
}
