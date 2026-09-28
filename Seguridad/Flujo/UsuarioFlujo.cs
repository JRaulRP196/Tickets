using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {

        private readonly IUsuarioDA _seguridadDA;

        public UsuarioFlujo(IUsuarioDA seguridadDA)
        {
            _seguridadDA = seguridadDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioRequest usuario)
        {
            return await _seguridadDA.CrearUsuario(usuario);
        }

        public async Task<UsuarioResponse> ObtenerUsuario(string correo)
        {
            return await _seguridadDA.ObtenerUsuario(correo);
        }
    }
}
