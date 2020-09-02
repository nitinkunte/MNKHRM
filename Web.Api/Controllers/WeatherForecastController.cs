using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowEveryone")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            if (User.Claims.FirstOrDefault(c => (c.Type == "http://schemas.microsoft.com/identity/claims/scope") && (c.Value == "SuperAdmin Users weatherforecast.get")) == null)
            {
                var abc = User.Claims.FirstOrDefault(c => (c.Type == "http://schemas.microsoft.com/identity/claims/scope"));
                throw new UnauthorizedAccessException();
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
