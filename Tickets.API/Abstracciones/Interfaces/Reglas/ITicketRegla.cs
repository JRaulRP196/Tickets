using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Reglas
{
    public interface ITicketRegla
    {
        Task<TicketDetalle> ObtenerTicket(Guid id);
    }
}
