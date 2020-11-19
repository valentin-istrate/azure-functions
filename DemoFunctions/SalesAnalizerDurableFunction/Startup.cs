using Demo.SalesAnalyzerDurableFunction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Demo.SalesAnalyzerDurableFunction
{
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Interface;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();

            builder.Services.AddScoped<ICsvParser,CsvParser>();
            builder.Services.AddScoped<ISalesAnalyzer,SalesAnalyzer>();
        }
    }
}