namespace Demo.DurableFunction.Functions.Orchestrators
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Activities;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;

    public class LoggingOrchestrator
    {
        private readonly ILogger<LoggingOrchestrator> log;

        public LoggingOrchestrator(ILogger<LoggingOrchestrator> log)
        {
            this.log = log;
        }

        [FunctionName(nameof(LoggingOrchestrator))]
        public async Task RunOrchestratorAsync(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            Person input = context.GetInput<Person>();

            var outputs = new List<string>
            {
                await context.CallActivityAsync<string>(nameof(HelloActivity), input),
                await context.CallActivityAsync<string>(nameof(HowAreYouActivity), input),
                await context.CallActivityAsync<string>(nameof(GoodbyeActivity), input)
            };

            await context.CallActivityAsync(nameof(GreetingLoggerActivity), outputs);
        }
    }
}