using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {

        Task<UsuarioResponse> ObtenerUsuario(string correo); 
        Task<Guid> CrearUsuario (UsuarioRequest usuario);

    }
}
