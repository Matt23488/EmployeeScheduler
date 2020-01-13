using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeScheduler.Api.Filters
{
    public class RequiresTokenAttribute : ActionFilterAttribute
    {
        public const string TOKEN_HEADER_KEY = "authentication-token";

        //public int 

        //private IAuthService _authService;

        //public RequiresTokenAttribute(IAuthService authService)
        //{
        //    _authService = authService;
        //}

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as ControllerBase;

            var headers = context.HttpContext.Request.Headers;
            if (!headers.ContainsKey(TOKEN_HEADER_KEY))
            {
                context.Result = controller.Unauthorized();
                return;
            }

            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var token = headers[TOKEN_HEADER_KEY].First();
            if (!await authService.ValidateTokenAsync(ipAddress, token))
            {
                context.Result = controller.Unauthorized();
                return;
            }
        }
    }
}
