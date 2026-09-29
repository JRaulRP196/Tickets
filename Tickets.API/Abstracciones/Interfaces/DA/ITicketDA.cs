using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ITicketDA
    {

        Task<TicketResponse> ObtenerTicket(Guid id);
        Task<IEnumerable<TicketResponse>> ObtenerTickets();
        Task<IEnumerable<TicketResponse>> ObtenerTicketsPendientes();
        Task<IEnumerable<TicketResponse>> ObtenerTicketsAsignados(Guid idSoporte);
        Task<IEnumerable<TicketResponse>> ObtenerTicketsCreados(Guid idEmisor);
        Task<Guid> Agregar(TicketRequest ticket);
        Task<Guid> Editar(TicketRequest ticket, Guid id);
        Task<Guid> Eliminar(Guid id);

    }
}
