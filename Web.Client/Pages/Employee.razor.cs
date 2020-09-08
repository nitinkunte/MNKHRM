using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Web.Client.Services;
using Web.DTO.Data;

namespace Web.Client.Pages
{
    public partial class Employee
    {
        [Inject]
        protected IAPIService api { get; set; }

        private EmployeeModel employeeModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                employeeModel = await api.GetEmployeeByIdAsync(1);
            }
            catch (Exception ex)
            {
                employeeModel = new EmployeeModel();
            }

        }

    }
}
