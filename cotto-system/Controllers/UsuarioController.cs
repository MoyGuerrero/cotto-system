using cotto_system.interfaces;
using cotto_system.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace cotto_system.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public UsuarioController(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        [HttpPost]
        //[Route("/")]
        public async Task<IActionResult> AddUsuario([FromBody] AddUsuario usuario)
        {
            await repositorioUsuario.AddUsuario(usuario);
            return Ok(new
            {
                error = false,
                msg = "Usuario registrado con exito."
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var usuario = await repositorioUsuario.Login(login.Usuario);

            if (usuario is null)
            {
                return BadRequest(new
                {
                    ok = true,
                    msg = "El usuario no es existe"
                });
            }


            var isCorrect = BCrypt.Net.BCrypt.Verify(login.Clave, usuario.Clave);

            if (!isCorrect)
            {
                return BadRequest(new
                {
                    ok = true,
                    msg = "Credenciales son incorrectas"
                });
            }

            return Ok(new
            {
                ok = false,
                msg = "Acceso concedido",
                token = "Aqui ira el token"
            });
        }
    }
}
