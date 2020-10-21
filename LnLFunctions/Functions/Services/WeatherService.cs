namespace LnL.Functions.Services
{
    using System.Threading.Tasks;
    using Database.Context;
    using Database.Models;
    using Interface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

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
            var weatherForecast = await weatherDb.WeatherForecast.FirstOrDefaultAsync(forecast => forecast.City.ToLower() == city.ToLower());
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