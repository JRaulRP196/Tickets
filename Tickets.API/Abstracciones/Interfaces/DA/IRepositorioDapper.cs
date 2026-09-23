using Npgsql;

namespace Abstracciones.Interfaces.DA
{
    public interface IRepositorioDapper
    {

        Task<NpgsqlConnection> ObtenerRepositorio();

    }
}
