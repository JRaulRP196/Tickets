using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;

namespace Flujo
{
    public class TicketFlujo : ITicketFlujo
    {

        private readonly ITicketDA _ticketDA;
        private readonly ITicketRegla _ticketRegla;

        public TicketFlujo(ITicketDA ticketDA, ITicketRegla ticketRegla)
        {
            _ticketDA = ticketDA;
            _ticketRegla = ticketRegla;
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

        public async Task<TicketDetalle> ObtenerTicket(Guid id)
        {
            return await _ticketRegla.ObtenerTicket(id);
        }

        public async Task<TicketResponse> ObtenerTicketBase(Guid id)
        {
            return await _ticketDA.ObtenerTicket(id);
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTickets()
        {
            return await _ticketDA.ObtenerTickets();
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTicketsAsignados(Guid idSoporte)
        {
            return await _ticketDA.ObtenerTicketsAsignados(idSoporte);
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTicketsCreados(Guid idEmisor)
        {
            return await _ticketDA.ObtenerTicketsCreados(idEmisor);
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTicketsPendientes()
        {
            return await _ticketDA.ObtenerTicketsPendientes();
        }
    }
}
