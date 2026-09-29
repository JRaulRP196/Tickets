using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ITicketFlujo
    {
        Task<TicketDetalle> ObtenerTicket(Guid id);
        Task<TicketResponse> ObtenerTicketBase(Guid id);
        Task<IEnumerable<TicketResponse>> ObtenerTickets();
        Task<IEnumerable<TicketResponse>> ObtenerTicketsPendientes();
        Task<IEnumerable<TicketResponse>> ObtenerTicketsAsignados(Guid idSoporte);
        Task<IEnumerable<TicketResponse>> ObtenerTicketsCreados(Guid idEmisor);
        Task<Guid> Agregar(TicketRequest ticket);
        Task<Guid> Editar(TicketRequest ticket, Guid id);
        Task<Guid> Eliminar(Guid id);
    }
}
