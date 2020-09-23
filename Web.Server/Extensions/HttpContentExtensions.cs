using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace Web.Server.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
            };
            var json = await content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}