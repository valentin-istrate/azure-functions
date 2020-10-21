using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(LnL.Functions.Startup))]

namespace LnL.Functions
{
    using System;
    using Constants;
    using Database.Context;
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Interface;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string sqlConnection = Environment.GetEnvironmentVariable(DbConstants.ConnectionStringKey);
            builder.Services.AddDbContext<WeatherDbContext>(
                options => options.UseSqlServer(sqlConnection ?? throw new InvalidOperationException()));

            builder.Services.AddLogging();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddScoped<IWeatherSensorService, WeatherSensorService>();
        }
    }
}