using Abstracciones.Interfaces.DA;
using Npgsql;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        public Task<NpgsqlConnection> ObtenerRepositorio()
        {
            throw new NotImplementedException();
        }
    }
}
