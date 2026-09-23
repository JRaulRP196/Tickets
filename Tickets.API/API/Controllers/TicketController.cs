using Abstracciones.Interfaces.API;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase, ITicketController
    {
        public Task<IActionResult> Agregar([FromBody] TicketRequest ticket)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Editar([FromBody] TicketRequest ticket, [FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Eliminar([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ObtenerTicket([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ObtenerTickets()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ObtenerTicketsAsignados([FromRoute] Guid idSoporte)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ObtenerTicketsCreados([FromRoute] Guid idEmisor)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ObtenerTicketsPendientes()
        {
            throw new NotImplementedException();
        }
    }
}
