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

        public async Task<TicketResponse> ObtenerTicket(Guid id)
        {
            string query = "SELECT * FROM obtener_ticket(@p_id)";
            var respuesta = await _connection.QueryFirstOrDefaultAsync<TicketResponse>(query, new { p_id = id });
            return respuesta;
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTickets()
        {
            string query = "SELECT * FROM obtener_tickets()";
            var respuesta = await _connection.QueryAsync<TicketResponse>(query);
            return respuesta;
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTicketsAsignados(Guid idSoporte)
        {
            string query = "SELECT * FROM obtener_tickets_asignados(@p_id)";
            var respuesta = await _connection.QueryAsync<TicketResponse>(query, new { p_id = idSoporte});
            return respuesta;
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTicketsCreados(Guid idEmisor)
        {
            string query = "SELECT * FROM obtener_tickets_creados(@p_id)";
            var respuesta = await _connection.QueryAsync<TicketResponse>(query, new { p_id = idEmisor });
            return respuesta;
        }

        public async Task<IEnumerable<TicketResponse>> ObtenerTicketsPendientes()
        {
            string query = "SELECT * FROM obtener_tickets_pendientes()";
            var respuesta = await _connection.QueryAsync<TicketResponse>(query);
            return respuesta;
        }
    }
}
