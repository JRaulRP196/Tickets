using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {

        private readonly IUsuarioDA _seguridadDA;

        private readonly IContrasenaRegla _contrasenaRegla;

        public UsuarioFlujo(IUsuarioDA seguridadDA, IContrasenaRegla contrasenaRegla)
        {
            _seguridadDA = seguridadDA;
            _contrasenaRegla = contrasenaRegla;
        }

        public async Task<Guid> CrearUsuario(UsuarioRequest usuario)
        {
            // PasswordHash llega con la contraseña en texto plano (por HTTPS); se guarda su hash.
            usuario.PasswordHash = _contrasenaRegla.Hashear(usuario.PasswordHash);
            return await _seguridadDA.CrearUsuario(usuario);
        }

        public async Task<UsuarioResponse> ObtenerUsuario(string correo)
        {
            return await _seguridadDA.ObtenerUsuario(correo);
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid id)
        {
            return await _seguridadDA.ObtenerUsuarioPorId(id);
        }
    }
}
