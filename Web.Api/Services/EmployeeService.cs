using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.DTO.Data;

namespace Web.Api.Services
{

    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<List<Address>> GetAddressesAsync(int employeeId);
        Task<List<EmergencyContact>> GetEmergencyContactsAsync(int employeeId);
        Task<EmploymentInfo> GetEmploymentInfoAsync(int employeeId);
        Task<Immigration> GetImmigrationAsync(int employeeId);

        /// <summary>
        /// Saves the employee and all related classes such as addresses, job info etc
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee> SaveEmployeeAsync(Employee employee);
        Task<Address> SaveAddressAsync(Address address);
        Task<EmergencyContact> SaveEmergencyContactAsync(EmergencyContact emergencyContact);
        Task<EmploymentInfo> SaveEmploymentInfoAsync(EmploymentInfo EmploymentInfo);
        Task<Immigration> SaveImmigrationAsync(Immigration immigration);

        Task<Employee> DeleteEmployeeAsync(int employeeId);

    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await dbContext.Employees
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var ret = await dbContext.Employees
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == employeeId);
            return ret;
        }

        public async Task<List<Address>> GetAddressesAsync(int employeeId)
        {
            var ret = await dbContext.Addresses
                            .Where(x => x.EmployeeId == employeeId)
                            .AsNoTracking()
                            .ToListAsync();
            return ret;
        }

        public async Task<Immigration> GetImmigrationAsync(int employeeId)
        {
            var ret = await dbContext.Immigrations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            return ret;
        }

        public async Task<List<EmergencyContact>> GetEmergencyContactsAsync(int employeeId)
        {
            var ret = await dbContext.EmergencyContacts
                            .Where(x => x.EmployeeId == employeeId)
                            .AsNoTracking()
                            .ToListAsync();
            return ret;
        }

        public async Task<EmploymentInfo> GetEmploymentInfoAsync(int employeeId)
        {
            var ret = await dbContext.EmploymentInfos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            return ret;
        }

        public async Task<Employee> SaveEmployeeAsync(Employee employee)
        {
            Employee ret = null;
            if (null != employee)
            {
                if (employee.Id <= 0) //insert
                {
                    await dbContext.Employees.AddAsync(employee);
                    await dbContext.SaveChangesAsync();
                }

                ret = await UpdateEmployeeDetailsAsync(employee);
            }
            return ret;
        }

        public async Task<Address> SaveAddressAsync(Address model)
        {
            Address ret = null;
            if ((model?.EmployeeId > 0) && (model?.AddressType > 0))
            {
                var addr = await dbContext.Addresses
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId && x.AddressType == model.AddressType);

                if (addr?.Id > 0) //update
                {
                    model.Id = addr.Id;
                    dbContext.Addresses.Update(model);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    await dbContext.Addresses.AddAsync(model);
                    await dbContext.SaveChangesAsync();
                }
                ret = model;
            }
            return ret;
        }

        public async Task<EmergencyContact> SaveEmergencyContactAsync(EmergencyContact model)
        {
            EmergencyContact ret = null;
            if ((model?.EmployeeId > 0) && (model?.RelationshipStatus > 0))
            {
                var emergency = await dbContext.EmergencyContacts
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId && x.RelationshipStatus == model.RelationshipStatus);
                if (emergency?.Id > 0) //update
                {
                    model.Id = emergency.Id;
                    dbContext.EmergencyContacts.Update(model);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    await dbContext.EmergencyContacts.AddAsync(model);
                    await dbContext.SaveChangesAsync();
                }
                ret = model;
            }
            return ret;
        }

        public async Task<EmploymentInfo> SaveEmploymentInfoAsync(EmploymentInfo model)
        {
            EmploymentInfo ret = null;
            if (model?.EmployeeId > 0)
            {
                var job = await dbContext.EmploymentInfos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId);
                if (job?.Id > 0) //update
                {
                    model.Id = job.Id;
                    dbContext.EmploymentInfos.Update(model);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    await dbContext.EmploymentInfos.AddAsync(model);
                    await dbContext.SaveChangesAsync();
                }
                ret = model;
            }
            return ret;
        }

        public async Task<Immigration> SaveImmigrationAsync(Immigration model)
        {
            Immigration ret = null;
            if (model?.EmployeeId > 0)
            {
                var immi = await dbContext.Immigrations
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId);
                if (immi?.Id > 0) //update
                {
                    model.Id = immi.Id;
                    dbContext.Immigrations.Update(model);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    await dbContext.Immigrations.AddAsync(model);
                    await dbContext.SaveChangesAsync();
                }
                ret = model;
            }
            return ret;
        }

        public async Task<Employee> DeleteEmployeeAsync(int employeeId)
        {
            Employee ret = await dbContext.Employees
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == employeeId);
            if (ret?.Id > 0)
            {
                ret.IsActive = false;
                dbContext.Employees.Update(ret);
                await dbContext.SaveChangesAsync();
            }
            return ret;
        }



        private async Task<Employee> UpdateEmployeeDetailsAsync(Employee employee)
        {
            Employee ret = null;
            if (employee?.Id > 0)
            {
                if (null != employee.AddressHome)
                {
                    employee.AddressHome.EmployeeId = employee.Id;
                    employee.AddressHome = await SaveAddressAsync(employee.AddressHome);
                    employee.AddressHomeId = (employee.AddressHome == null) ? 0 : employee.AddressHome.Id;
                }
                if (null != employee.AddressOther)
                {
                    employee.AddressOther.EmployeeId = employee.Id;
                    employee.AddressOther = await SaveAddressAsync(employee.AddressOther);
                    employee.AddressOtherId = (employee.AddressOther == null) ? 0 : employee.AddressOther.Id;
                }
                if (null != employee.EmploymentInfo)
                {
                    employee.EmploymentInfo.EmployeeId = employee.Id;
                    employee.EmploymentInfo = await SaveEmploymentInfoAsync(employee.EmploymentInfo);
                    employee.EmploymentInfoId = employee.EmploymentInfo.Id;
                }

                if (null != employee.EmergencyContact1)
                {
                    employee.EmergencyContact1.EmployeeId = employee.Id;
                    employee.EmergencyContact1 = await SaveEmergencyContactAsync(employee.EmergencyContact1);
                }
                if (null != employee.EmergencyContact2)
                {
                    employee.EmergencyContact2.EmployeeId = employee.Id;
                    employee.EmergencyContact2 = await SaveEmergencyContactAsync(employee.EmergencyContact2);
                }

                if (null != employee.Immigration)
                {
                    employee.Immigration.EmployeeId = employee.Id;
                    employee.Immigration = await SaveImmigrationAsync(employee.Immigration);
                    employee.ImmigrationId = (employee.Immigration == null) ? 0 : employee.Immigration.Id;
                }

                dbContext.Employees.Update(employee);
                await dbContext.SaveChangesAsync();
                ret = employee;
            }

            return ret;
        }


    }
}
