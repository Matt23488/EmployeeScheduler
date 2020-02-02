using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeScheduler.Api.Filters;
using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeScheduler.Api.Controllers
{
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly ISchedulingService _scheduler;

        public ScheduleController(ILogger<ScheduleController> logger, ISchedulingService scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        [HttpPut]
        [Route("schedule")]
        [RequiresToken(Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SaveSchedule(Lib.DAL.ScheduleWeek schedule)
        {
            return Ok(await _scheduler.SaveScheduleAsync(schedule));
        }

        [HttpGet]
        [Route("schedule/{scheduleID:long}")]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSchedule(long scheduleID)
        {
            if (scheduleID <= 0) return BadRequest();

            return Ok(await _scheduler.GetScheduleAsync(scheduleID));
        }
    }
}