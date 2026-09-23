using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ITicketDA
    {

        Task<TicketResponse> ObtenerTicket(Guid id);
        Task<List<TicketResponse>> ObtenerTickets();
        Task<List<TicketResponse>> ObtenerTicketsPendientes();
        Task<List<TicketResponse>> ObtenerTicketsAsignados(Guid idSoporte);
        Task<List<TicketResponse>> ObtenerTicketsCreados(Guid idEmisor);
        Task<Guid> Agregar(TicketRequest ticket);
        Task<Guid> Editar(TicketRequest ticket, Guid id);
        Task<Guid> Eliminar(Guid id);

    }
}
