using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Npgsql;
using Dapper;

namespace DA
{
    public class RolDA : IRolDA
    {

        private readonly IRepositorioDapper _repositorioDapper;
        private readonly NpgsqlConnection _connection;

        public RolDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _connection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<Rol>> ObtenerRoles()
        {
            string query = "SELECT * FROM obtener_roles()";
            var respuesta = await _connection.QueryAsync<Rol>(query);
            return respuesta;
        }
    }
}
