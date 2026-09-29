using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;

namespace Servicios
{
    public class UsuarioServicio : IUsuarioServicios
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioServicio(IConfiguracion configuracion, IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UsuarioResponse> ObtenerUsuario(Guid id)
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiUsuarios", "ObtenerUsuario");
            var servicioRegistro = _httpClient.CreateClient("ServicioUsuario");
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
                servicioRegistro.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            var respuesta = await servicioRegistro.GetAsync(string.Format(endPoint, id));
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<UsuarioResponse>(resultado, opciones);
            return resultadoDeserializado;
        }
    }
}
