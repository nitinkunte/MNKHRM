using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.DTO.Data;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

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

        private readonly string URL_SESSION = "api/session/";
        private readonly string URL_EMPLOYEE = "api/employees/";

        public APIService(HttpClient http, TokenProvider tokenProvider)
        {
            this.http = http;
            this.tokenProvider = tokenProvider;

        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var rst = tokenProvider;
            var aa = tokenProvider.AccessToken;
            var abc = http.DefaultRequestHeaders;
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
    }
}
