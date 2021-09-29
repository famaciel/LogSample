using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogSample.Api.Controllers
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

        private readonly ILogger _custLogger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoggerFactory loggerFact)
        {
            _logger = logger;
            _custLogger = loggerFact.CreateLogger("OtherCategoryLogger");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("*** Get Ini ***");

            _custLogger.LogInformation("I am here too!");

            // EventID
            var eventID = 3;
            _logger.LogInformation(eventID, "Tech : {tech}", "webapi");

            try
            {
                // var routeInfo = ControllerContext.ToString()
                // _logger.LogInformation(routeInfo);

                var rng = new Random();

                var test = rng.Next(1, 3);

                _logger.LogDebug($"The test is {test}");
                _logger.LogDebug($"Exception == {test == 2}");

                // using (_logger.BeginScope(new Dictionary<string, object>
                // {
                //     ["dateTime"] = DateTime.Now,
                //     ["test"] = test
                // }
                // ))
                //     _logger.LogInformation("Log With Scope");

                using (_logger.BeginScope("using block message"))
                {
                    _logger.LogInformation("inside using");
                    if (test == 2)
                        _logger.LogWarning("Is equal two");
                }

                if (test == 2)
                {
                    _logger.LogWarning("Exception is comming");
                    throw new Exception("Ops...");
                }

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (System.Exception exc)
            {
                _logger.LogWarning(exc, "Exception found");
                _logger.LogError(exc.Message);
                return null;
            }
            finally
            {
                _logger.LogInformation("Get End");
            }
        }
    }
}
