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
    //[Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly ISchedulingService _scheduler;

        public EmployeeController(ILogger<EmployeeController> logger, ISchedulingService scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        [HttpPost]
        [Route("employee")]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddEmployee(Lib.DAL.Employee employee)
        {
            return Ok(await _scheduler.AddEmployeeAsync(employee));
        }

        [HttpGet]
        [Route("employee")]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _scheduler.GetEmployeesAsync(includeDeleted: true));
        }

        [HttpGet]
        [Route("employee/{employeeID:int}")]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployee(int employeeID)
        {
            if (employeeID <= 0) return BadRequest();

            var employee = await _scheduler.GetEmployeeAsync(employeeID);
            if (employee == null) return BadRequest();

            return Ok(employee);
        }

        [HttpPut]
        [Route("employee")]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmployee(Lib.DAL.Employee employee)
        {
            if (employee == null) return BadRequest();
            if (employee.EmployeeID <= 0) return BadRequest();

            var updatedEmployee = await _scheduler.UpdateEmployeeAsync(employee);
            if (updatedEmployee == null) return BadRequest();

            return Ok(updatedEmployee);
        }
    }
}
