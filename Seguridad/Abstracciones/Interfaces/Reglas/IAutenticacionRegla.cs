using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IAutenticacionRegla
    {
        Task<Token> Login(Login login);
    }
}
