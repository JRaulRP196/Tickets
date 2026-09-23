using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface ITicketController
    {

        Task<IActionResult> ObtenerTicket([FromRoute] Guid id);
        Task<IActionResult> ObtenerTickets();
        Task<IActionResult> ObtenerTicketsPendientes();
        Task<IActionResult> ObtenerTicketsAsignados([FromRoute] Guid idSoporte);
        Task<IActionResult> ObtenerTicketsCreados([FromRoute] Guid idEmisor);
        Task<IActionResult> Agregar([FromBody] TicketRequest ticket);
        Task<IActionResult> Editar([FromBody] TicketRequest ticket, [FromRoute]  Guid id);
        Task<IActionResult> Eliminar([FromRoute]  Guid id);

    }
}
