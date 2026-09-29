using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IUsuarioServicios
    {

        Task<UsuarioResponse> ObtenerUsuario(Guid id);

    }
}
