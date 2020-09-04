﻿using System;
using System.Threading.Tasks;
using Web.DTO.Data;

namespace Web.Client.Services
{
    public interface IAPIService
    {
        Task<UserRoleModel> GetUserRole(string userName);
    }

    public class APIService : IAPIService
    {
        private readonly IHttpService http;

        public APIService(IHttpService http)
        {
            this.http = http;
        }

        public async Task<UserRoleModel> GetUserRole(string userName)
        {
            return await http.Get<UserRoleModel>("api/employees/getrole");
        }
    }
}
