namespace LnL.Functions.Services
{
    using System;
    using System.Collections.Generic;
    using Database.Models;
    using Interface;
    using Microsoft.Extensions.Logging;

    public class WeatherSensorService : IWeatherSensorService
    {
        private readonly ILogger<WeatherSensorService> logger;
        private readonly Random randomGenerator;

        public WeatherSensorService(ILogger<WeatherSensorService> logger)
        {
            this.logger = logger;
            this.randomGenerator = new Random();
        }

        public WeatherForecast GetForecast(string city)
        {
            logger.LogDebug("Creating random forecast");
            return new WeatherForecast
            {
                City = city,
                Description = GetRandomDescription(),
                Humidity = randomGenerator.Next(0, 90),
                Temperature = randomGenerator.Next(4, 16),
                Time = DateTime.Now
            };
        }

        private string GetRandomDescription()
        {
            var descriptions = new List<string>
            {
                "Partly Cloudy",
                "Rain",
                "Sunny",
                "Balmy"
            };

            return descriptions[randomGenerator.Next(0, descriptions.Count)];
        }
    }
}