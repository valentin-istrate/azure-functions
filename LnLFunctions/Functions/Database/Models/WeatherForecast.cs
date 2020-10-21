namespace LnL.Functions.Database.Models
{
    using System;

    public class WeatherForecast
    {
        public int Id { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}