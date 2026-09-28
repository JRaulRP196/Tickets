using Abstracciones.Interfaces.DA;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {

        private readonly NpgsqlConnection _connection;
        private readonly IConfiguration _configuration;

        public RepositorioDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("TicketsBD"));
        }

        public NpgsqlConnection ObtenerRepositorio()
        {
            return _connection;
        }
    }
}
