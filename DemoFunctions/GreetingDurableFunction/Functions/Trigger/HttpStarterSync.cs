namespace Demo.GreetingDurableFunction.Functions.Trigger
{
    using Demo.GreetingDurableFunction.Functions.Orchestrator;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Services.Interface;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;

    public class HttpStarterSync
    {
        private readonly IPersonService personService;
        private readonly ILogger<HttpStarterSync> log;

        private const int TimeoutInSeconds = 300;

        public HttpStarterSync(IPersonService personService, ILogger<HttpStarterSync> log)
        {
            this.personService = personService;
            this.log = log;
        }

        [FunctionName(nameof(HttpStarterSync))]
        public async Task<HttpResponseMessage> HttpStartAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient orchestrator)
        {
            Person person = personService.GetPersonFromRequestAsync(req);
            string instanceId = await orchestrator.StartNewAsync(nameof(GreetingOrchestrator), person);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return await orchestrator.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId,
                TimeSpan.FromSeconds(TimeoutInSeconds));
        }
    }
}