namespace LnL.Functions.Services.Interface
{
    using System.Threading.Tasks;
    using Database.Models;

    public interface IWeatherService
    {
        Task<WeatherForecast> GetForecastAsync(string city);
        Task StoreForecastAsync(WeatherForecast forecast);
    }
}