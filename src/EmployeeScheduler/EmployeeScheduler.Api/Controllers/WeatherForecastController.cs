using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IPasswordValidationService _passwordService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPasswordValidationService passwordService)
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
        public IActionResult Get([FromBody]string password)
        {
            // TODO: This isn't getting hit, I'm doing something wrong.
            if (!_passwordService.AdminPasswordIsValid(password)) return Unauthorized();

            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
