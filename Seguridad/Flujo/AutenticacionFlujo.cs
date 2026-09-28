using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;

namespace Flujo
{
    public class AutenticacionFlujo : IAutenticacionFlujo
    {

        private readonly IAutenticacionRegla _autenticacionRegla;

        public AutenticacionFlujo(IAutenticacionRegla autenticacionRegla)
        {
            _autenticacionRegla = autenticacionRegla;
        }

        public async Task<Token> Login(Login login)
        {
            return await _autenticacionRegla.Login(login);
        }
    }
}
