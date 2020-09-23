using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.DTO.Data;
using Web.DTO.Enums;

namespace Web.Api.Services
{

    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployeesAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        Task<List<AddressModel>> GetAddressesAsync(int employeeId);
        Task<List<EmergencyContactModel>> GetEmergencyContactsAsync(int employeeId);
        Task<EmploymentInfoModel> GetEmploymentInfoAsync(int employeeId);
        Task<ImmigrationModel> GetImmigrationAsync(int employeeId);

        /// <summary>
        /// Saves the employee and all related classes such as addresses, job info etc
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<EmployeeModel> SaveEmployeeAsync(EmployeeModel employee);
        Task<AddressModel> SaveAddressAsync(AddressModel address);
        Task<EmergencyContactModel> SaveEmergencyContactAsync(EmergencyContactModel emergencyContact);
        Task<EmploymentInfoModel> SaveEmploymentInfoAsync(EmploymentInfoModel EmploymentInfo);
        Task<ImmigrationModel> SaveImmigrationAsync(ImmigrationModel immigration);

        Task<EmployeeModel> DeleteEmployeeAsync(int employeeId);

    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            return await dbContext.Employees
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var ret = await dbContext.Employees
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == employeeId);
            return ret;
        }

        public async Task<List<AddressModel>> GetAddressesAsync(int employeeId)
        {
            var ret = await dbContext.Addresses
                            .Where(x => x.EmployeeId == employeeId)
                            .AsNoTracking()
                            .ToListAsync();
            return ret;
        }

        public async Task<ImmigrationModel> GetImmigrationAsync(int employeeId)
        {
            var ret = await dbContext.Immigrations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            return ret;
        }

        public async Task<List<EmergencyContactModel>> GetEmergencyContactsAsync(int employeeId)
        {
            var ret = await dbContext.EmergencyContacts
                            .Where(x => x.EmployeeId == employeeId)
                            .AsNoTracking()
                            .ToListAsync();
            return ret;
        }

        public async Task<EmploymentInfoModel> GetEmploymentInfoAsync(int employeeId)
        {
            var ret = await dbContext.EmploymentInfos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            return ret;
        }

        public async Task<EmployeeModel> SaveEmployeeAsync(EmployeeModel employee)
        {
            EmployeeModel ret = null;
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

        public async Task<AddressModel> SaveAddressAsync(AddressModel model)
        {
            AddressModel ret = null;
            if ((model?.EmployeeId > 0) && (AddressTypeEnums.IsValid(model.AddressType)))
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

        public async Task<EmergencyContactModel> SaveEmergencyContactAsync(EmergencyContactModel model)
        {
            EmergencyContactModel ret = null;
            if ((model?.EmployeeId > 0) && (model?.RelationshipStatusId > 0))
            {
                var emergency = await dbContext.EmergencyContacts
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.EmployeeId == model.EmployeeId && x.RelationshipStatusId == model.RelationshipStatusId);
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

        public async Task<EmploymentInfoModel> SaveEmploymentInfoAsync(EmploymentInfoModel model)
        {
            EmploymentInfoModel ret = null;
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

        public async Task<ImmigrationModel> SaveImmigrationAsync(ImmigrationModel model)
        {
            ImmigrationModel ret = null;
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

        public async Task<EmployeeModel> DeleteEmployeeAsync(int employeeId)
        {
            EmployeeModel ret = await dbContext.Employees
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



        private async Task<EmployeeModel> UpdateEmployeeDetailsAsync(EmployeeModel employee)
        {
            EmployeeModel ret = null;
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
