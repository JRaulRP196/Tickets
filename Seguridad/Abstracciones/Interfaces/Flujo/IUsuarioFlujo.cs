using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<Guid> CrearUsuario(UsuarioRequest usuario);
        Task<UsuarioResponse> ObtenerUsuario(string correo);
    }
}
