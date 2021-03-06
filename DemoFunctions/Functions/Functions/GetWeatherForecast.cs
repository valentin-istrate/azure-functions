namespace LnL.Functions.Functions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Services.Interface;
    using System.Threading.Tasks;
    using Database.Models;

    public class GetWeatherForecast
    {
        private readonly ILogger<GetWeatherForecast> logger;
        private readonly IWeatherService weatherService;

        public GetWeatherForecast(ILogger<GetWeatherForecast> logger,
            IWeatherService weatherService)
        {
            this.logger = logger;
            this.weatherService = weatherService;
        }


        [FunctionName(nameof(GetWeatherForecast))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "weather/{city:alpha}")]
            HttpRequest req,
            string city)
        {
            logger.LogInformation($"Requesting forecast for {city}.");

            WeatherForecast forecast = await weatherService.GetForecastAsync(city);

            return new OkObjectResult(forecast);
        }
    }
}