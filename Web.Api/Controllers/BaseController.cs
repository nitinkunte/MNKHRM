using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Services;
using Web.DTO.Data;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowEveryone")]
    public class BaseController : ControllerBase
    {
        protected readonly IEmployeeService employeeService;

        public BaseController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        private string _signInUserId;
        protected string SignInUserId
        {
            get
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrWhiteSpace(_signInUserId))
                    throw new ValidationException("You haven't logged into system yet.");
                return userId;
            }
            set { _signInUserId = value; }
        }

        protected bool IsUserLoggedIn()
        {
            var userId = GetCurrentUserId();
            return (!string.IsNullOrWhiteSpace(userId));
        }

        protected BadRequestObjectResult HandleException(Exception exception)
        {

            if (exception is ValidationException)
            {
                var responseData = ResponseData.Create(false, exception.Message);
                return BadRequest(responseData);
            }

            return BadRequest("Unhandled error has occured. Please try again. If you face this error frequently please contact support.");
        }


        protected string GetCurrentUserId()
        {
            if (!string.IsNullOrWhiteSpace(_signInUserId)) return _signInUserId;
            var claim = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                _signInUserId = claim.Value;
                return _signInUserId;
            }
            return string.Empty;
        }
    }


}
