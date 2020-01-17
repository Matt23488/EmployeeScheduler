using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeScheduler.Api.Filters;
using EmployeeScheduler.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeScheduler.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthService _passwordService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuthService passwordService)
        {
            _logger = logger;
            _passwordService = passwordService;
        }

        [HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        public IActionResult Get()
        {
            // This should give 401
            //return Unauthorized();

            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpPost]
        [RequiresToken(Lib.DAL.Roles.User, Lib.DAL.Roles.Admin)]
        public async Task<IActionResult> Get(RequestData data)
        {
            //if (!_passwordService.AdminPasswordIsValid(data.password)) return Unauthorized();

            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }

        public class RequestData
        {
            public string password { get; set; }
        }
    }
}
