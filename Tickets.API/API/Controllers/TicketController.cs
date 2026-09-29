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
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] TicketRequest ticket)
        {
            return Ok(await _ticketFlujo.Agregar(ticket));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar([FromBody] TicketRequest ticket, [FromRoute] Guid id)
        {
            if (await _ticketFlujo.ObtenerTicketBase(id) == null)
                return BadRequest("El usuario no existe");
            return Ok(await _ticketFlujo.Editar(ticket, id));
        }
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid id)
        {
            if (await _ticketFlujo.ObtenerTicketBase(id) == null)
                return BadRequest("El usuario no existe");
            return Ok(await _ticketFlujo.Eliminar(id));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTicket([FromRoute] Guid id)
        {
            var ticket = await _ticketFlujo.ObtenerTicket(id);
            if (ticket == null)
                return NotFound("Ticket no encontrado");
            return Ok(ticket);
        }
        [Authorize(Roles = "1")]
        [HttpGet("Tickets")]
        public async Task<IActionResult> ObtenerTickets()
        {
            var tickets = await _ticketFlujo.ObtenerTickets();
            if (!tickets.Any())
                return NoContent();
            return Ok(tickets);
        }
        [Authorize(Roles = "1")]
        [HttpGet("Tickets/Asignados/{idSoporte}")]
        public async Task<IActionResult> ObtenerTicketsAsignados([FromRoute] Guid idSoporte)
        {
            var tickets = await _ticketFlujo.ObtenerTicketsAsignados(idSoporte);
            if (!tickets.Any())
                return NoContent();
            return Ok(tickets);
        }
        [Authorize(Roles = "2")]
        [HttpGet("Tickets/Creados/{idEmisor}")]
        public async Task<IActionResult> ObtenerTicketsCreados([FromRoute] Guid idEmisor)
        {
            var tickets = await _ticketFlujo.ObtenerTicketsCreados(idEmisor);
            if (!tickets.Any())
                return NoContent();
            return Ok(tickets);
        }
        [Authorize(Roles = "1")]
        [HttpGet("Tickets/Pendientes")]
        public async Task<IActionResult> ObtenerTicketsPendientes()
        {
            var tickets = await _ticketFlujo.ObtenerTicketsPendientes();
            if (!tickets.Any())
                return NoContent();
            return Ok(tickets);
        }
    }
}
