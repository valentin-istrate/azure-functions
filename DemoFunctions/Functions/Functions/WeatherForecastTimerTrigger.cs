namespace LnL.Functions.Functions
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.Models;

    public class WeatherForecastTimerTrigger
    {
        private readonly ILogger<WeatherForecastTimerTrigger> logger;
        private readonly IWeatherSensorService weatherSensorService;
        private readonly IWeatherService weatherService;

        public WeatherForecastTimerTrigger(
            ILogger<WeatherForecastTimerTrigger> logger,
            IWeatherSensorService weatherSensorService,
            IWeatherService weatherService)
        {
            this.logger = logger;
            this.weatherSensorService = weatherSensorService;
            this.weatherService = weatherService;
        }


        // NCRONTAB timer format
        // execute every 30 seconds
        [FunctionName(nameof(WeatherForecastTimerTrigger))]
        public async Task Run([TimerTrigger("0,30 * * * * *")] TimerInfo myTimer)
        {
            string city = GetRandomCity();
            logger.LogInformation($"Getting temperature for {city}.");

            WeatherForecast forecast = weatherSensorService.GetForecast(city);
            await weatherService.StoreForecastAsync(forecast);
        }

        private string GetRandomCity()
        {
            var random = new Random();

            var cities = new List<string>
            {
                "Brasov",
                "Constanta",
                "Bucuresti"
            };

            return cities[random.Next(0, cities.Count)];
        }
    }
}