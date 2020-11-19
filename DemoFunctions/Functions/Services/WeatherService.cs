namespace LnL.Functions.Services
{
    using System;
    using Database.Context;
    using Database.Models;
    using Interface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class WeatherService : IWeatherService
    {
        private readonly ILogger logger;
        private readonly WeatherDbContext weatherDb;

        public WeatherService(WeatherDbContext weatherDb, ILogger<WeatherService> logger)
        {
            this.logger = logger;
            this.weatherDb = weatherDb;
        }

        public async Task<WeatherForecast> GetForecastAsync(string city)
        {
            WeatherForecast weatherForecast = await weatherDb.WeatherForecast
                .LastOrDefaultAsync(forecast => string.Equals(forecast.City, city, StringComparison.CurrentCultureIgnoreCase));
            return weatherForecast;
        }

        public async Task StoreForecastAsync(WeatherForecast forecast)
        {
            logger.LogDebug("Adding new forecast");
            await weatherDb.WeatherForecast.AddAsync(forecast);
            await weatherDb.SaveChangesAsync();
        }
    }
}