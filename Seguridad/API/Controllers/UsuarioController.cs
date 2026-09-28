using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioController
    {

        private readonly IUsuarioFlujo _usuarioFlujo;
        public UsuarioController(IUsuarioFlujo usuarioFlujo)
        {
            _usuarioFlujo = usuarioFlujo;
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRequest usuario)
        {
            if(await _usuarioFlujo.ObtenerUsuario(usuario.Correo) != null)
                return BadRequest("El correo ya se encuentra registrado");
            var respuesta = await _usuarioFlujo.CrearUsuario(usuario);
            return Ok(respuesta);
        }
    }
}
