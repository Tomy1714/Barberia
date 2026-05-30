using Newtonsoft.Json;
using LibPresentaciones.Interfaces;
using System.Text;

namespace LibPresentaciones.Implementaciones
{
    public class Comunicaciones : IComunicaciones
    {
        public async Task<Dictionary<string, object>> EjecutarConsultar(Dictionary<string, object> datos)
        {
            var Url = datos["Url"].ToString();
            datos.Remove("Url");

            var httpCliente = new HttpClient();
            httpCliente.Timeout = new TimeSpan(0, 4, 0);

            var message = await httpCliente.GetAsync(Url);

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error de comunicacion con el API");

            var resp = await message.Content.ReadAsStringAsync();
            httpCliente.Dispose();

            return new Dictionary<string, object>() { { "Valor", Limpiar(resp) } };
        }

        public async Task<Dictionary<string, object>> EjecutarGuardar(Dictionary<string, object> datos)
        {
            var Url = datos["Url"].ToString();
            datos.Remove("Url");

            var stringData = datos.ContainsKey("Entidad")
                ? JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var message = await httpClient.PostAsync(Url, body);

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error al guardar en el API");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose();

            return new Dictionary<string, object>() { { "Valor", Limpiar(resp) } };
        }

        public async Task<Dictionary<string, object>> EjecutarModificar(Dictionary<string, object> datos)
        {
            var Url = datos["Url"].ToString();
            datos.Remove("Url");

            var stringData = datos.ContainsKey("Entidad")
                ? JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var message = await httpClient.PutAsync(Url, body);

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error al modificar en el API");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose();

            return new Dictionary<string, object>() { { "Valor", Limpiar(resp) } };
        }

        public async Task<Dictionary<string, object>> EjecutarEliminar(Dictionary<string, object> datos)
        {
            var Url = datos["Url"].ToString();
            datos.Remove("Url");

            var stringData = datos.ContainsKey("Entidad")
                ? JsonConvert.SerializeObject(datos["Entidad"]) : "{}";

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var request = new HttpRequestMessage(HttpMethod.Delete, Url)
            {
                Content = new StringContent(stringData,
                              System.Text.Encoding.UTF8,
                              "application/json")
            };

            var message = await httpClient.SendAsync(request);

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error al eliminar en el API");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose();

            return new Dictionary<string, object>() { { "Valor", Limpiar(resp) } };
        }

        public async Task<Dictionary<string, object>> EjecutarCalculo(Dictionary<string, object> datos)
        {
            var Url = datos["Url"].ToString();
            datos.Remove("Url");

            var stringData = datos.ContainsKey("Entidad")
                ? JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var message = await httpClient.PostAsync(Url, body);

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error en calculo del API");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose();

            return new Dictionary<string, object>() { { "Valor", Limpiar(resp) } };
        }

        private string Limpiar(string resp)
        {
            return resp.Replace("\\r\\n", "")
                .Replace("\r\n", "")
                .Replace("\\", "\"")
                .Replace("\"", "\"")
                .Replace("\"", "'")
                .Replace("'[", "[")
                .Replace("]'", "]")
                .Replace("'{'", "{'")
                .Replace("'}'", "'}")
                .Replace("}'", "}")
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("    ", "")
                .Replace("'{", "{")
                .Replace("\"", "")
                .Replace("  ", "")
                .Replace("null", "''");
        }
    }
}
