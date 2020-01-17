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

        private readonly Lib.DAL.Roles[] _roles;

        public RequiresTokenAttribute(params Lib.DAL.Roles[] roles)
        {
            _roles = roles;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headers = context.HttpContext.Request.Headers;
            if (!headers.ContainsKey(TOKEN_HEADER_KEY))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var token = headers[TOKEN_HEADER_KEY].First();
            if (!await authService.ValidateTokenAsync(ipAddress, token, _roles))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
