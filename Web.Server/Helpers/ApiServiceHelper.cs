using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.DTO.Data;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Web.Server.Helpers
{
    public interface IAPIService
    {
        Task<UserRoleModel> GetUserRole(string userName);
        Task<UserRoleModel> LoginAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
    }

    public class APIService : IAPIService
    {
        private readonly HttpClient http;
        private readonly TokenProvider tokenProvider;

        private readonly AzureAd appSettings;
        private readonly string URL_SESSION = "api/session/";
        private readonly string URL_EMPLOYEE = "api/employees/";

        public APIService(HttpClient http
                        , TokenProvider tokenProvider
                        , IOptions<AzureAd> iOptions)
        {
            this.http = http;
            this.tokenProvider = tokenProvider;
            this.appSettings = iOptions.Value;
            var token = GetTokenWithAudienceChanged(tokenProvider.AccessToken);
            this.http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var url = $"{URL_EMPLOYEE }GetEmployee/{employeeId}";
            var ret = await http.GetFromJsonAsync<EmployeeModel>(url);
            return ret;
        }

        public async Task<UserRoleModel> GetUserRole(string userName)
        {
            return await http.GetFromJsonAsync<UserRoleModel>($"{URL_EMPLOYEE }getrole");
        }

        public async Task<UserRoleModel> LoginAsync()
        {
            return await http.GetFromJsonAsync<UserRoleModel>($"{URL_SESSION }login");
        }

        private string GetTokenWithAudienceChanged(string token)
        {
            string ret = string.Empty;
            var tokken = new JwtSecurityToken(token);
            if (null != tokken)
            {
                Console.WriteLine($"Actor = {tokken.Actor ?? string.Empty}");
                Console.WriteLine($"Audiences = {string.Join(",", tokken.Audiences)}");
                string ty = string.Empty;
                string val = string.Empty;
                foreach (var claim in tokken.Claims)
                {
                    ty = claim.Type ?? string.Empty;
                    val = claim.Value ?? string.Empty;
                    Console.WriteLine($"Type = {ty} <==> Value = {val}");
                }

                Console.WriteLine($"Issuer = {tokken.Issuer ?? string.Empty}");
                Console.WriteLine($"Subject = {tokken.Subject ?? string.Empty}");
                Console.WriteLine($"Valid From = {tokken.ValidFrom.ToLongDateString() ?? string.Empty}");
                Console.WriteLine($"Valid To = {tokken.ValidTo.ToLongDateString() ?? string.Empty}");

                //var newToken = new JwtSecurityToken(
                //    issuer: tokken.Issuer,
                //    audience: "edec3924-b0a3-4d90-b83c-820eab840075",
                //    claims: tokken.Claims,
                //    notBefore: tokken.ValidFrom,
                //    expires: tokken.ValidTo,
                //    signingCredentials: tokken.SigningCredentials
                //    );

                var config = appSettings;
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                            .WithClientSecret(config.ClientSecret)
                            .WithAuthority(new Uri($"{config.Instance}{config.TenantId}"))

                            .Build();


                string[] ResourceIds = new string[] { config.ApplicationIDURI };

                AuthenticationResult result = null;
                try
                {
                    result = app.AcquireTokenForClient(ResourceIds).ExecuteAsync().Result;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Token acquired \n");
                    Console.WriteLine(result.AccessToken);
                    Console.ResetColor();
                }
                catch (MsalClientException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }

                ret = result.AccessToken;
            }


            return ret;
        }


    }
}
