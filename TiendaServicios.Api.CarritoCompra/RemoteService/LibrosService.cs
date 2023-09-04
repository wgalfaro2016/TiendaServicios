using System.Text.Json;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;


namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                Guid id = LibroId;
                var client = _httpClient.CreateClient("Libros");
                var response = await client.GetAsync($"api/LibroMaterial/{id}");
                if (response.IsSuccessStatusCode) 
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);

            }
        }
    }
}
