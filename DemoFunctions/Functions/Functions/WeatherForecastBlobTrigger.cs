namespace LnL.Functions.Functions
{
    using Database.Models;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Services.Interface;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class WeatherForecastBlobTrigger
    {
        private readonly ILogger<WeatherForecastBlobTrigger> logger;
        private readonly IWeatherService weatherService;

        public WeatherForecastBlobTrigger(
            ILogger<WeatherForecastBlobTrigger> logger,
            IWeatherService weatherService)
        {
            this.logger = logger;
            this.weatherService = weatherService;
        }

        [FunctionName(nameof(WeatherForecastBlobTrigger))]
        public async Task Run([BlobTrigger("forecast-reports/{name}", Connection = "AzureWebJobsStorage")]
            Stream weatherReport,
            string name)
        {
            logger.LogInformation($"Report {name} was uploaded to blob. Updating database.");
            try
            {
                var forecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(weatherReport);
                await weatherService.StoreForecastAsync(forecast);
            }
            catch (JsonException exception)
            {
                logger.LogError("Could not deserialize report.", exception);
            }
        }
    }
}