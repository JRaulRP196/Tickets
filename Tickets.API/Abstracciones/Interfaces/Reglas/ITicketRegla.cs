using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Reglas
{
    public interface ITicketRegla
    {
        Task<Guid> Agregar(TicketRequest ticket);
        Task<TicketDetalle> ObtenerTicket(Guid id);
    }
}
