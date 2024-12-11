using cotto_system.interfaces;
using cotto_system.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
            await repositorioUsuario.AddUsuario(usuario);
            return Ok(new
            {
                msg = "Usuario registrado con exito."
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var usuarioBd = await repositorioUsuario.Login(login.Usuario);

            if (usuarioBd.Validacion == 0)
            {
                return NotFound(new
                {
                    msg = "El usuario " + login.Usuario + " no exite"
                });
            }


            if (!repositorioUsuario.verifyPassword(usuarioBd.Clave, login.Clave))
            {
                return BadRequest(new
                {
                    msg = "Las credenciales no son correctas"
                });
            }

            return Ok(new
            {
                usuarioBd,
                token = Token(usuarioBd)
            });
        }

        [HttpGet]
        [Route("renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RenovarToken()
        {
            var usuarioClaim = HttpContext.User.Claims.Where(claim => claim.Type == "usuario").FirstOrDefault();


          var usuarioBd =  await repositorioUsuario.Login(usuarioClaim.Value);

            return Ok(new
            {
                token = Token(usuarioBd),
                usuarioBd
            });


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
