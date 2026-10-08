using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase, IRolController
    {

        private readonly IRolFlujo _rolFlujo;

        public RolController(IRolFlujo rolFlujo)
        {
            _rolFlujo = rolFlujo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ObtenerRoles()
        {
            var roles = await _rolFlujo.ObtenerRoles();
            if(!roles.Any())
                return NoContent();
            return Ok(roles);
        }

    }
}
