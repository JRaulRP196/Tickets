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
        private readonly IN8nServicios _n8nServicios;

        public TicketRegla(ITicketDA ticketDA, IUsuarioServicios usuarioServicios, IN8nServicios n8nServicios)
        {
            _ticketDA = ticketDA;
            _usuarioServicios = usuarioServicios;
            _n8nServicios = n8nServicios;
        }

        public async Task<Guid> Agregar(TicketRequest ticket)
        {
            var id = await _ticketDA.Agregar(ticket);
            try
            {
                await _n8nServicios.EnviarNotificacion();
            }
            catch (HttpRequestException)
            {
                // El ticket ya fue creado; una falla de n8n no debe revertir ni romper la operación.
            }
            return id;
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
