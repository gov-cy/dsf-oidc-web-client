using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{

    public class CallApiModel : PageModel
    {
        public string Json = string.Empty;
        private IConfiguration _configuration;

        public CallApiModel(IConfiguration config)
        {
            _configuration = config;
        }
        public async Task OnGet()
        {

            //Ignore SSL warnings (for testing only)
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();

            var endpoint = _configuration["ApiEndpoints:GetTodoItems"];
            var apiKey = _configuration["ApiClientKey"];
            //Add required Headers to the request
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("client-key", apiKey);

            //var content = await client.GetStringAsync("https://localhost:7086/api/v1/Identity/identity-echo");
            var content = await client.GetStringAsync(endpoint);

            var parsed = JsonDocument.Parse(content);
            var formatted = JsonSerializer.Serialize(parsed, new JsonSerializerOptions { WriteIndented = true });

            Json = formatted;
        }
    }
}
