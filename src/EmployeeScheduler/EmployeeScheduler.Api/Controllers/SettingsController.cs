using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeScheduler.Api.Filters;
using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeScheduler.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSettings()
        {
            return Ok(await _settingsService.GetSettingsAsync());
        }

        [HttpPost]
        [RequiresToken(Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SaveSettings(Lib.DAL.AdminSettings settings)
        {
            return Ok(await _settingsService.SaveSettingsAsync(settings));
        }
    }
}