using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase, ITicketController
    {

        private readonly ITicketFlujo _ticketFlujo;

        public TicketController(ITicketFlujo ticketFlujo)
        {
            _ticketFlujo = ticketFlujo;
        }
        [Authorize(Roles = "2, 1")]
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] TicketRequest ticket)
        {
            return Ok(await _ticketFlujo.Agregar(ticket));
        }
        [Authorize(Roles = "2, 1")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar([FromBody] TicketRequest ticket, [FromRoute] Guid id)
        {
            return Ok(await _ticketFlujo.Editar(ticket, id));
        }
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid id)
        {
            return Ok(await _ticketFlujo.Eliminar(id));
        }
        [HttpGet("{id}")]
        public Task<IActionResult> ObtenerTicket([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
        [HttpGet("Tickets")]
        public Task<IActionResult> ObtenerTickets()
        {
            throw new NotImplementedException();
        }
        [HttpGet("Tickets/Asignados/{idSoporte}")]
        public Task<IActionResult> ObtenerTicketsAsignados([FromRoute] Guid idSoporte)
        {
            throw new NotImplementedException();
        }
        [HttpGet("Tickets/Creados/{idEmisor}")]
        public Task<IActionResult> ObtenerTicketsCreados([FromRoute] Guid idEmisor)
        {
            throw new NotImplementedException();
        }
        [HttpGet("Tickets/Pendientes")]
        public Task<IActionResult> ObtenerTicketsPendientes()
        {
            throw new NotImplementedException();
        }
    }
}
