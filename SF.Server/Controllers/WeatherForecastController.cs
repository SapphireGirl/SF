using Microsoft.AspNetCore.Mvc;
using SF.Logger;

namespace SF.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ISFLogger sflogger) : ControllerBase
    {
        private readonly ISFLogger _sflogger = sflogger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;


        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _sflogger.LogInformation("GetWeatherForecast called");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
