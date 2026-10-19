using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System.Net.Http;

namespace Servicios
{
    public class N8nServicio : IN8nServicios
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public N8nServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task EnviarNotificacion()
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiN8n", "EnviarNotificacion");
            var servicioN8n = _httpClient.CreateClient("ServicioN8n");
            var respuesta = await servicioN8n.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
        }
    }
}
