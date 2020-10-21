namespace LnL.Functions.Services.Interface
{
    using Database.Models;

    public interface IWeatherSensorService
    {
        WeatherForecast GetForecast(string city);
    }
}