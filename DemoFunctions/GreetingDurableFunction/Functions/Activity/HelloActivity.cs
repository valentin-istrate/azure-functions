namespace Demo.GreetingDurableFunction.Functions.Activity
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Interface;
    using System.Threading.Tasks;

    public class HelloActivity
    {
        private readonly ILogger log;
        private readonly IMockDelayService mockDelayService;

        public HelloActivity(IMockDelayService mockDelayService, ILogger<HelloActivity> log)
        {
            this.log = log;
            this.mockDelayService = mockDelayService;
        }

        [FunctionName(nameof(HelloActivity))]
        public async Task<string> SayHelloAsync([ActivityTrigger] Person person)
        {
            await mockDelayService.MockDelayAsync(500, 1000);
            log.LogInformation($"Saying hello to {person}.");
            return $"Hello {person}!";
        }
    }
}