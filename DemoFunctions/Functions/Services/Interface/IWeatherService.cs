namespace LnL.Functions.Services.Interface
{
    using Database.Models;
    using System.Threading.Tasks;

    public interface IWeatherService
    {
        Task<WeatherForecast> GetForecastAsync(string city);
        Task StoreForecastAsync(WeatherForecast forecast);
    }
}