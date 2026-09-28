using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class TicketFlujo : ITicketFlujo
    {

        private readonly ITicketDA _ticketDA;

        public TicketFlujo(ITicketDA ticketDA)
        {
            _ticketDA = ticketDA;
        }

        public async Task<Guid> Agregar(TicketRequest ticket)
        {
            return await _ticketDA.Agregar(ticket);
        }

        public async Task<Guid> Editar(TicketRequest ticket, Guid id)
        {
            return await _ticketDA.Editar(ticket, id);
        }

        public async Task<Guid> Eliminar(Guid id)
        {
            return await _ticketDA.Eliminar(id);
        }

        public Task<TicketResponse> ObtenerTicket(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketResponse>> ObtenerTickets()
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketResponse>> ObtenerTicketsAsignados(Guid idSoporte)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketResponse>> ObtenerTicketsCreados(Guid idEmisor)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketResponse>> ObtenerTicketsPendientes()
        {
            throw new NotImplementedException();
        }
    }
}
