namespace Demo.DurableFunction.Functions.Orchestrators
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Activities;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Models;

    public class GreetingOrchestrator
    {
        [FunctionName(nameof(GreetingOrchestrator))]
        public async Task<List<string>> RunOrchestratorAsync(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            Person input = context.GetInput<Person>();

            var outputs = new List<string>
            {
                await context.CallActivityAsync<string>(nameof(HelloActivity), input),
                await context.CallActivityAsync<string>(nameof(HowAreYouActivity), input),
                await context.CallActivityAsync<string>(nameof(GoodbyeActivity), input)
            };

            return outputs;
        }
    }
}