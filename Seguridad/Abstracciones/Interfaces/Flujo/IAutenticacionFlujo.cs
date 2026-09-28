using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IAutenticacionFlujo
    {

        Task<Token> Login(Login login);

    }
}
