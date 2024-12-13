using cotto_system.interfaces;
using cotto_system.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace cotto_system.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IConfiguration configuration;

        public UsuarioController(IRepositorioUsuario repositorioUsuario, IConfiguration configuration)
        {
            this.repositorioUsuario = repositorioUsuario;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("agregar_usuario")]
        public async Task<IActionResult> AddUsuario([FromBody] AddUsuario usuario)
        {
            try
            {
                await repositorioUsuario.AddUsuario(usuario);
                return Ok(new ResponseData<object>(true, "Cliente agregado con éxito.", (int)HttpStatusCode.OK, new { }, ""));
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var usuarioBd = await repositorioUsuario.Login(login.Usuario);

                if (usuarioBd.Validacion == 0)
                {
                    return NotFound(new ResponseData<object>(false, "El usuario no existe.", (int)HttpStatusCode.NotFound, new { }, ""));
                }


                if (!repositorioUsuario.verifyPassword(usuarioBd.Clave, login.Clave))
                {
                    return BadRequest(new ResponseData<object>(false, "Las credenciales son incorrectas.", (int)HttpStatusCode.BadRequest, new { }, ""));
                }

                return Ok(new ResponseData<object>(true, "Bienvenido.", (int)HttpStatusCode.OK, usuarioBd, Token(usuarioBd)));
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }

        }

        [HttpGet]
        [Route("renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RenovarToken()
        {

            try
            {
                var usuarioClaim = HttpContext.User.Claims.Where(claim => claim.Type == "usuario").FirstOrDefault();


                var usuarioBd = await repositorioUsuario.Login(usuarioClaim.Value);

                return Ok(new ResponseData<object>(true, "Success", (int)HttpStatusCode.OK, usuarioBd, Token(usuarioBd)));
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, new Success(false, ex.Message, (int)HttpStatusCode.InternalServerError));
            }



        }
        private string Token(GetUsuario usuario)
        {

            var claims = new List<Claim>() {
            new Claim("usuario",usuario.Usuario)
            };

            var llaveSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT"]));
            var creds = new SigningCredentials(llaveSecret, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
