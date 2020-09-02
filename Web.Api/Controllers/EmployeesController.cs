using System;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Services;
using Web.DTO.Data;
using Web.DTO.NetStd.Data;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowEveryOne")]
    //[Authorize]
    public class EmployeesController : BaseController
    {
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetRole()
        {
            var user = base.GetCurrentUserId();
            UserRole ret = new UserRole { UserName = user };
            if (user.StartsWith("nitin"))
            {
                ret.RoleName = "Super User";
                return Ok(ret);
            }
            else
            {
                return Ok(ret);
            }
        }



        /// <summary>
        /// Returns all employees in the system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var emp = await employeeService.GetEmployeesAsync();

                var responseData = ResponseData.Create(true, string.Empty, emp);
                //var abc = System.Text.Json.JsonSerializer.Serialize(responseData);
                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Returns single employee based on employee Id. No details are returned
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetEmployee/{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            try
            {
                var emp = await employeeService.GetEmployeeByIdAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, emp);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Returns list of addresses for the employee - Home & Other
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetAddresses/{employeeId}")]
        public async Task<IActionResult> GetAddresses(int employeeId)
        {
            try
            {
                var ret = await employeeService.GetAddressesAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// returns job related information for the employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetEmploymentInfo/{employeeId}")]
        public async Task<IActionResult> GetEmploymentInfo(int employeeId)
        {
            try
            {
                var ret = await employeeService.GetEmploymentInfoAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// returns list of emergency contacts for the employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetEmergencyContact/{employeeId}")]
        public async Task<IActionResult> GetEmergencyContact(int employeeId)
        {
            try
            {
                var ret = await employeeService.GetEmergencyContactsAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// returns immigration details for the employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetImmigration/{employeeId}")]
        public async Task<IActionResult> GetImmigration(int employeeId)
        {
            try
            {
                var ret = await employeeService.GetImmigrationAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Returns employee with all details such as Address, Emergency Contact, Job Info & Immigration
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("GetDetails/{employeeId}")]
        public async Task<IActionResult> GetDetails(int employeeId)
        {
            try
            {
                var emp = await employeeService.GetEmployeeByIdAsync(employeeId);
                var addresses = await employeeService.GetAddressesAsync(employeeId);
                if (addresses?.Count > 0)
                {
                    emp.AddressHome = addresses.FirstOrDefault(x => x.AddressType == 10);
                    emp.AddressOther = addresses.FirstOrDefault(x => x.AddressType == 20);
                }
                var contacts = await employeeService.GetEmergencyContactsAsync(employeeId);
                if (contacts?.Count > 0)
                {
                    emp.EmergencyContact1 = contacts.FirstOrDefault();
                    emp.EmergencyContact2 = contacts.LastOrDefault();
                }

                emp.EmploymentInfo = await employeeService.GetEmploymentInfoAsync(employeeId);
                emp.Immigration = await employeeService.GetImmigrationAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, emp);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Insert/Update employee and all related classes like EmploymentInfo, Address, EmergencyContact, Immigration if they are sent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveEmployee")]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var emp = await employeeService.SaveEmployeeAsync(model);

                var responseData = ResponseData.Create(true, string.Empty, emp);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Insert/Update Address. EmployeeId and AddressType must be greater than zero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveAddress")]
        public async Task<IActionResult> SaveAddress([FromBody] Address model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (model?.EmployeeId < 0) return NotFound("Invalid EmployeeId");
                if (model?.AddressType < 0) return NotFound("Invalid AddressType");

                var ret = await employeeService.SaveAddressAsync(model);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Insert/Update  Job Info. EmployeeId must be greater than zero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveEmploymentInfo")]
        public async Task<IActionResult> SaveEmploymentInfo([FromBody] EmploymentInfo model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (model?.EmployeeId < 0) return NotFound("Invalid EmployeeId");

                var ret = await employeeService.SaveEmploymentInfoAsync(model);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Insert/Update  immigration. EmployeeId must be greater than zero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveImmigration")]
        public async Task<IActionResult> SaveImmigration([FromBody] Immigration model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (model?.EmployeeId < 0) return NotFound("Invalid EmployeeId");

                var ret = await employeeService.SaveImmigrationAsync(model);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Insert/Update  Emergency Contact. EmployeeId and RelationshipStatus must be greater than zero
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveEmergencyContact")]
        public async Task<IActionResult> SaveEmergencyContact([FromBody] EmergencyContact model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (model?.EmployeeId < 0) return NotFound("Invalid EmployeeId");
                if (model?.RelationshipStatus < 0) return NotFound("Invalid RelationshipStatus");

                var ret = await employeeService.SaveEmergencyContactAsync(model);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// Soft delete for employee - just the IsActive flag is set to false. 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var ret = await employeeService.DeleteEmployeeAsync(employeeId);

                var responseData = ResponseData.Create(true, string.Empty, ret);

                return Ok(responseData);
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

    }
}
