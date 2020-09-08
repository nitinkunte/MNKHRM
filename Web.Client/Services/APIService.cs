using System;
using System.Threading.Tasks;
using Web.DTO.Data;
using System.Net.Http;

namespace Web.Client.Services
{
    public interface IAPIService
    {
        Task<UserRoleModel> GetUserRole(string userName);
        Task<UserRoleModel> LoginAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
    }

    public class APIService : IAPIService
    {
        private readonly IHttpService http;
        private readonly string URL_SESSION = "api/session/";
        private readonly string URL_EMPLOYEE = "api/employees/";

        public APIService(IHttpService http)
        {
            this.http = http;
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var url = $"{URL_EMPLOYEE }GetEmployee/{employeeId}";
            var ret = await http.GetFromJsonAsync<EmployeeModel>(url);
            return ret;
        }

        public async Task<UserRoleModel> GetUserRole(string userName)
        {
            return await http.GetAsync<UserRoleModel>($"{URL_EMPLOYEE }getrole");
        }

        public async Task<UserRoleModel> LoginAsync()
        {
            return await http.GetAsync<UserRoleModel>($"{URL_SESSION }login");
        }
    }
}
