using Abstracciones.Interfaces.DA;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;
        public RepositorioDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("SeguridadBD"));
        }
        public NpgsqlConnection ObtenerRepositorio()
        {
            return _connection;
        }
    }
    
    }

