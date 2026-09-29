using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> ObtenerUsuarioPorId(Guid id);
        Task<IActionResult> CrearUsuario(UsuarioRequest usuario);
    }
}
