namespace Demo.GreetingDurableFunction.Functions.Triggers
{
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;
    using Orchestrators;

    public class TimerStarter
    {
        private readonly ILogger log;

        public TimerStarter(ILogger<TimerStarter> log)
        {
            this.log = log;
        }
        [FunctionName(nameof(TimerStarter))]
        public async Task RunAsync(
            [TimerTrigger("0,30 * * * * *")] TimerInfo myTimer,
            [DurableClient] IDurableOrchestrationClient orchestrator)
        {
            var person = new Person
            {
                FirstName = "John",
                LastName = "Timer"
            };
            log.LogInformation($"Starting timed greeting for {person}");

            string instanceId = await orchestrator.StartNewAsync(nameof(LoggingOrchestrator), person);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
        }
    }
}