using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase, IAutenticacionController
    {

        private readonly IAutenticacionFlujo _autenticacionFlujo;

        public AutenticacionController(IAutenticacionFlujo autenticacionFlujo)
        {
            _autenticacionFlujo = autenticacionFlujo;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            return Ok(await _autenticacionFlujo.Login(login));
        }
    }
}
