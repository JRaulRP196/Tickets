using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;

namespace DA
{
    public class TicketDA : ITicketDA
    {
        public Task<Guid> Agregar(TicketRequest ticket)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Editar(TicketRequest ticket, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Eliminar(Guid id)
        {
            throw new NotImplementedException();
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
