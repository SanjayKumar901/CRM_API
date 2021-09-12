using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.CommonMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public class abc
        {
            public int sum(int i,int j)
            {
                return (i + j);
            }
        }
        public class bcd:abc
        {
            public int sum(int i, int j)
            {
                return (i - j);
            }
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            
            //string pass = Common.Decrypt("OAHw5qTqVQoD5dShdjb6ow==");
            abc b = new bcd();
            b.sum(4, 8);
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
}
