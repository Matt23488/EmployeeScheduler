using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken(RequestData data)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            var token = await _authService.GetTokenAsync(ipAddress.ToString(), data.password);
            if (token == null) return Unauthorized();

            return Ok(token);
        }

        public class RequestData
        {
            public string password { get; set; }
        }
    }
}
