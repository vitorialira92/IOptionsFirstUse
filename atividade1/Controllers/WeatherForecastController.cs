using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace atividade1.Controllers
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

        private readonly IOptionsMonitor<WeatherOptions> _options;


        public WeatherForecastController(IOptionsMonitor<WeatherOptions> options, ILogger<WeatherForecastController> logger)
        {
            _options = options;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var fixo = new WeatherForecast { 
                Date = DateOnly.FromDateTime(DateTime.Now), 
                TemperatureC = _options.CurrentValue.Temperature, 
                Summary = "Warm" 
            };

            var fixedList = new List<WeatherForecast> { fixo };

            Random random = new Random();

            var randomList = Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = random.Next(-20, 38),
                Summary = Summaries[random.Next(Summaries.Length)]
            })
            .ToArray();

            return _options.CurrentValue.UseFixedValue ? (fixedList) : (randomList);

        }
    }
}
