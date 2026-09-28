using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Npgsql;

namespace DA
{
    public class TicketDA : ITicketDA
    {

        private readonly NpgsqlConnection _connection;
        private readonly IRepositorioDapper _repositorioDapper;

        public TicketDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _connection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(TicketRequest ticket)
        {
            string query = "SELECT fn_agregar_ticket(@p_id, @p_asunto, @p_estado, @p_descripcion, @p_id_emisor, @p_id_soporte)";
            var respuesta = await _connection.ExecuteScalarAsync<Guid>(query, new
            {
                p_id = Guid.NewGuid(),
                p_asunto = ticket.Asunto,
                p_estado = ticket.Estado,
                p_descripcion = ticket.Descripcion,
                p_id_emisor = ticket.IdEmisor,
                p_id_soporte = ticket.IdSoporte
            });
            return respuesta;
        }

        public async Task<Guid> Editar(TicketRequest ticket, Guid id)
        {
            string query ="SELECT fn_editar_ticket(@p_id, @p_asunto, @p_estado, @p_descripcion, @p_id_emisor, @p_id_soporte)";
            var respuesta = await _connection.ExecuteScalarAsync<Guid>(query, new
            {
                p_id = id,
                p_asunto = ticket.Asunto,
                p_estado = ticket.Estado,
                p_descripcion = ticket.Descripcion,
                p_id_emisor = ticket.IdEmisor,
                p_id_soporte = ticket.IdSoporte
            });
            return respuesta;
        }

        public async Task<Guid> Eliminar(Guid id)
        {
            string query = "SELECT fn_eliminar_ticket(@p_id)";
            var respuesta = await _connection.ExecuteScalarAsync<Guid>(query, new { p_id = id });
            return respuesta;
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
