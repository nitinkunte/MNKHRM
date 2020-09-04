using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Services;
using Web.DTO;
using Web.DTO.Data;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionController : BaseController
    {

        public SessionController(IEmployeeService employeeService
                                 , ISessionService sessionService)
            : base(employeeService, sessionService)
        {

        }

        /// <summary>
        /// Get user roles
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("login")]
        public async Task<IActionResult> LoginAsync()
        {
            UserRoleModel ret = null;
            try
            {
                var userDetails = base.GetUserDetails();
                if (!string.IsNullOrWhiteSpace(userDetails?.UserName))
                {
                    var userName = userDetails.UserName;
                    var usr = await base.sessionService.GetUserRoleByUserNameAsync(userName);
                    if (null == usr)
                    {
                        usr = new UserRoleModel { UserName = userName, FirstName = userDetails.FirstName, LastName = userDetails.LastName, UpdateUser = userName };
                        if (userName.ToLower() == "nitin@mnkinfotech.com")
                        {
                            usr.RoleName = MNKConstants.ROLES.SUPER_ADMIN;
                        }
                        usr = await base.sessionService.SaveUserRoleAsync(usr);
                    }
                    await sessionService.LoginAsync(new SessionModel
                    {
                        UserName = userName,
                        UserOID = userDetails.UserOID,
                        IPAddress = userDetails.IpAddress,
                        IsActive = true,
                        UpdateUser = userName,

                    });

                    ret = usr;
                }

            }
            catch (Exception ex)
            {
                base.HandleException(ex);
            }

            return Ok(ret);
        }

        [Authorize]
        [HttpPost("changeRole")]
        public async Task<IActionResult> ChangeRoleAsync([FromBody] UserRoleModel model)
        {
            UserRoleModel ret = null;
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var userDetails = base.GetUserDetails();
                if (!string.IsNullOrWhiteSpace(userDetails?.UserName))
                {
                    var usr = await base.sessionService.GetUserRoleByUserNameAsync(userDetails.UserName);
                    if (!string.IsNullOrWhiteSpace(usr?.RoleName))
                    {
                        if ((usr.RoleName.ToLower() == MNKConstants.ROLES.SUPER_ADMIN)
                            || (usr.RoleName.ToLower() == MNKConstants.ROLES.ADMIN))
                        {
                            usr = await base.sessionService.SaveUserRoleAsync(model);
                        }
                    }
                    ret = usr;
                }
            }
            catch (Exception ex)
            {
                base.HandleException(ex);
            }

            return Ok(ret);
        }

    }
}
