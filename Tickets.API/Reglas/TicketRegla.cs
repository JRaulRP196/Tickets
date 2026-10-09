using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;

namespace Reglas
{
    public class TicketRegla : ITicketRegla
    {

        private readonly ITicketDA _ticketDA;
        private readonly IUsuarioServicios _usuarioServicios;

        public TicketRegla(ITicketDA ticketDA, IUsuarioServicios usuarioServicios)
        {
            _ticketDA = ticketDA;
            _usuarioServicios = usuarioServicios;
        }

        public async Task<TicketDetalle> ObtenerTicket(Guid id)
        {
            var ticket = await _ticketDA.ObtenerTicket(id);
            if (ticket == null)
                return null;
            var emisor = await _usuarioServicios.ObtenerUsuario(ticket.IdEmisor);
            var soporte = await _usuarioServicios.ObtenerUsuario(ticket.IdSoporte);
            return new TicketDetalle
            {
                Id = ticket.Id,
                Asunto = ticket.Asunto,
                Estado = ticket.Estado,
                Descripcion = ticket.Descripcion,
                IdEmisor = ticket.IdEmisor,
                IdSoporte = ticket.IdSoporte,
                FechaCreacion = ticket.FechaCreacion,
                Emisor = emisor?.Nombre ?? "Usuario no encontrado",
                Soporte = soporte?.Nombre ?? "Sin asignar"
            };
        }
    }
}
