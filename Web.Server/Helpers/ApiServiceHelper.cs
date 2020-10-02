using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Web.DTO.Data;
using Web.Server.Extensions;

namespace Web.Server.Helpers
{
    public interface IAPIService
    {
        Task<UserRoleModel> GetUserRole(string userName);
        Task<UserRoleModel> LoginAsync();
        Task<List<EmployeeModel>> GetAllEmployeesAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        /// <summary>
        /// Returns employee with all details such as Address, Emergency Contact, Job Info & Immigration
        /// </summary>
        /// <param name="employeeId">Id for the given employee</param>
        /// <returns></returns>
        Task<EmployeeModel> GetEmployeeDetailsByIdAsync(int employeeId);

        /// <summary>
        /// Returns List of Addresses for this employee
        /// </summary>
        /// <param name="employeeId">Id for the given employee</param>
        /// <returns>Returns List of Addresses for this employee</returns>
        Task<List<AddressModel>> GetAddressesAsync(int employeeId);
        Task<EmploymentInfoModel> GetEmploymentInfoAsync(int employeeId);
        Task<ImmigrationModel> GetImmigrationInfoAsync(int employeeId);
        Task<List<EmergencyContactModel>> GetEmergencyContactAsync(int employeeId);

        /// <summary>
        /// Insert/Update employee and all related classes like EmploymentInfo, Address, EmergencyContact, Immigration if they are sent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<EmployeeModel> SaveEmployeeAsync(EmployeeModel employeeModel);
        Task<AddressModel> SaveAddressAsync(AddressModel addressModel);
        Task<EmploymentInfoModel> SaveEmploymentInfoAsync(EmploymentInfoModel employmentInfoModel);
        Task<ImmigrationModel> SaveImmigrationAsync(ImmigrationModel immigrationModel);
        Task<EmergencyContactModel> SaveEmergencyContactAsync(EmergencyContactModel emergencyContactModel);
    }

    public class APIService : IAPIService
    {
        private readonly HttpClient http;
        private readonly TokenProvider tokenProvider;

        private readonly AzureAd appSettings;
        private readonly string URL_SESSION = "api/session";
        private readonly string URL_EMPLOYEE = "api/employees";

        public APIService(HttpClient http, TokenProvider tokenProvider, IOptions<AzureAd> iOptions)
        {
            this.http = http;
            this.tokenProvider = tokenProvider;
            this.appSettings = iOptions.Value;
            var token = GetTokenWithAudienceChanged(tokenProvider.AccessToken);
            this.http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<EmployeeModel>> GetAllEmployeesAsync()
        {
            var ret = new List<EmployeeModel>();
            var url = $"{URL_EMPLOYEE }/GetAll";
            var response = await http.GetFromJsonAsync<ResponseData>(url);
            if ((response?.IsSuccess == true) && (null != response?.Data))
            {
                ret = response.Deserialize<List<EmployeeModel>>();
            }
            return ret;
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var ret = new EmployeeModel();
            var url = $"{URL_EMPLOYEE }/GetEmployee/{employeeId}";
            var response = await http.GetFromJsonAsync<ResponseData>(url);
            if ((response?.IsSuccess == true) && (null != response?.Data))
            {
                ret = response.Deserialize<EmployeeModel>();
            }
            return ret;
        }

        public async Task<EmployeeModel> GetEmployeeDetailsByIdAsync(int employeeId)
        {
            var url = $"{URL_EMPLOYEE }/GetDetails/{employeeId}";
            var ret = await http.GetFromJsonAsync<EmployeeModel>(url);
            return ret;
        }

        public async Task<List<AddressModel>> GetAddressesAsync(int employeeId)
        {
            var ret = new List<AddressModel>();
            var url = $"{URL_EMPLOYEE }/GetAddresses/{employeeId}";
            var response = await http.GetFromJsonAsync<ResponseData>(url);
            if ((response?.IsSuccess == true) && (null != response?.Data))
            {
                ret = response.Deserialize<List<AddressModel>>();
            }

            return ret;
        }

        public async Task<EmploymentInfoModel> GetEmploymentInfoAsync(int employeeId)
        {
            var ret = new EmploymentInfoModel();
            var url = $"{URL_EMPLOYEE }/GetEmploymentInfo/{employeeId}";
            var response = await http.GetFromJsonAsync<ResponseData>(url);
            if ((response?.IsSuccess == true) && (null != response?.Data))
            {
                ret = response.Deserialize<EmploymentInfoModel>();
            }
            return ret;
        }
        public async Task<ImmigrationModel> GetImmigrationInfoAsync(int employeeId)
        {
            var ret = new ImmigrationModel();
            var url = $"{URL_EMPLOYEE }/GetImmigration/{employeeId}";
            var response = await http.GetFromJsonAsync<ResponseData>(url);
            if ((response?.IsSuccess == true) && (null != response?.Data))
            {
                ret = response.Deserialize<ImmigrationModel>();
            }
            return ret;
        }

        public async Task<List<EmergencyContactModel>> GetEmergencyContactAsync(int employeeId)
        {
            var ret = new List<EmergencyContactModel>();
            var url = $"{URL_EMPLOYEE }/GetEmergencyContact/{employeeId}";
            var response = await http.GetFromJsonAsync<ResponseData>(url);
            if ((response?.IsSuccess == true) && (null != response?.Data))
            {
                ret = response.Deserialize<List<EmergencyContactModel>>();
            }
            return ret;
        }

        public async Task<UserRoleModel> GetUserRole(string userName)
        {
            return await http.GetFromJsonAsync<UserRoleModel>($"{URL_EMPLOYEE }/getrole");
        }

        public async Task<UserRoleModel> LoginAsync()
        {
            return await http.GetFromJsonAsync<UserRoleModel>($"{URL_SESSION }/login");
        }

        public async Task<EmployeeModel> SaveEmployeeAsync(EmployeeModel employeeModel)
        {
            EmployeeModel ret = new EmployeeModel();
            var url = $"{URL_EMPLOYEE }/SaveEmployee";
            var response = await http.PostAsJsonAsync<EmployeeModel>(url, employeeModel);
            if (response?.IsSuccessStatusCode == true)
            {
                var res = await response.Content.ReadAsAsync<ResponseData>();
                if (res.IsSuccess)
                {
                    ret = res.Deserialize<EmployeeModel>();
                }
            }
            return ret;
        }

        public async Task<AddressModel> SaveAddressAsync(AddressModel addressModel)
        {
            AddressModel ret = new AddressModel();
            var url = $"{URL_EMPLOYEE }/SaveAddress";
            var response = await http.PostAsJsonAsync<AddressModel>(url, addressModel);
            if (response?.IsSuccessStatusCode == true)
            {
                var res = await response.Content.ReadAsAsync<ResponseData>();
                if (res.IsSuccess)
                {
                    ret = res.Deserialize<AddressModel>();
                }
            }
            return ret;
        }

        public async Task<EmploymentInfoModel> SaveEmploymentInfoAsync(EmploymentInfoModel employmentInfoModel)
        {
            EmploymentInfoModel ret = new EmploymentInfoModel();
            var url = $"{URL_EMPLOYEE }/SaveEmploymentInfo";
            var response = await http.PostAsJsonAsync<EmploymentInfoModel>(url, employmentInfoModel);
            if (response?.IsSuccessStatusCode == true)
            {
                var res = await response.Content.ReadAsAsync<ResponseData>();
                if (res.IsSuccess)
                {
                    ret = res.Deserialize<EmploymentInfoModel>();
                }
            }
            return ret;
        }

        public async Task<ImmigrationModel> SaveImmigrationAsync(ImmigrationModel immigrationModel)
        {
            ImmigrationModel ret = new ImmigrationModel();
            var url = $"{URL_EMPLOYEE }/SaveEmploymentInfo";
            var response = await http.PostAsJsonAsync<ImmigrationModel>(url, immigrationModel);
            if (response?.IsSuccessStatusCode == true)
            {
                var res = await response.Content.ReadAsAsync<ResponseData>();
                if (res.IsSuccess)
                {
                    ret = res.Deserialize<ImmigrationModel>();
                }
            }
            return ret;
        }

        public async Task<EmergencyContactModel> SaveEmergencyContactAsync(EmergencyContactModel emergencyContactModel)
        {
            EmergencyContactModel ret = new EmergencyContactModel();
            var url = $"{URL_EMPLOYEE }/SaveEmploymentInfo";
            var response = await http.PostAsJsonAsync<EmergencyContactModel>(url, emergencyContactModel);
            if (response?.IsSuccessStatusCode == true)
            {
                var res = await response.Content.ReadAsAsync<ResponseData>();
                if (res.IsSuccess)
                {
                    ret = res.Deserialize<EmergencyContactModel>();
                }
            }
            return ret;
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
                // foreach (var claim in tokken.Claims)
                // {
                //     ty = claim.Type ?? string.Empty;
                //     val = claim.Value ?? string.Empty;
                //     Console.WriteLine($"Type = {ty} <==> Value = {val}");
                // }

                // Console.WriteLine($"Issuer = {tokken.Issuer ?? string.Empty}");
                // Console.WriteLine($"Subject = {tokken.Subject ?? string.Empty}");
                // Console.WriteLine($"Valid From = {tokken.ValidFrom.ToLongDateString() ?? string.Empty}");
                // Console.WriteLine($"Valid To = {tokken.ValidTo.ToLongDateString() ?? string.Empty}");

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
                    // Console.ForegroundColor = ConsoleColor.Green;
                    // Console.WriteLine("Token acquired \n");
                    // Console.WriteLine(result.AccessToken);
                    // Console.ResetColor();
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