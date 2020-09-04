using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.SessionStorage;
using Web.Client.Services;
using System.Net.Http.Json;

namespace Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");


            builder.Services.AddScoped(sp =>
            {
                var apiUrl = new Uri(builder.Configuration["ApiBaseURL"]);
                return new HttpClient() { BaseAddress = apiUrl };
            });
            //            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMsalAuthentication(options =>
            {
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://mnkhrm/Users");
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            });

            builder.Services.AddBlazoredSessionStorage(config =>
                            config.JsonSerializerOptions.WriteIndented = true);


            builder.Services
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<IAPIService, APIService>();


            await builder.Build().RunAsync();
        }


    }
}
