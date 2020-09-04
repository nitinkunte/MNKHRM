using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Web.Client.Helpers
{
    public interface IApiHelper
    {
        Task<HttpResponseMessage> GetAsync(string url, string token);
        Task<T> GetFromJsonAsync<T>(string url, string token);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient client;
        private string baseUrl = string.Empty;
        public ApiHelper(IConfiguration config, HttpClient client)
        {
            var appSettings = config.Get<AppSettings>();
            baseUrl = appSettings.ApiBaseURL;
            this.client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url, string token)
        {
            HttpResponseMessage ret = null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var fullUrl = $"{baseUrl}/api/{url}";
            ret = await client.GetAsync(fullUrl);
            return ret;
        }

        public async Task<T> GetFromJsonAsync<T>(string url, string token)
        {
            T ret;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var fullUrl = $"{baseUrl}/api/{url}";
            ret = await client.GetFromJsonAsync<T>(fullUrl);
            return ret;
        }

    }
}
