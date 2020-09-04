using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Services;

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
    }
}
