namespace Demo.GreetingDurableFunction.Functions.Trigger
{
    using Demo.GreetingDurableFunction.Functions.Orchestrator;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Services.Interface;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;

    public class HttpStarterAsync
    {
        private readonly IPersonService personService;
        private readonly ILogger<HttpStarterAsync> log;

        public HttpStarterAsync(IPersonService personService, ILogger<HttpStarterAsync> log)
        {
            this.personService = personService;
            this.log = log;
        }
        [FunctionName(nameof(HttpStarterAsync))]
        public async Task<HttpResponseMessage> HttpStartAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            Person person = personService.GetPersonFromRequestAsync(req);
            string instanceId = await starter.StartNewAsync(nameof(GreetingOrchestrator), person);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}