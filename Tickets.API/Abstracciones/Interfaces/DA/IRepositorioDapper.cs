using Npgsql;

namespace Abstracciones.Interfaces.DA
{
    public interface IRepositorioDapper
    {

        NpgsqlConnection ObtenerRepositorio();

    }
}
