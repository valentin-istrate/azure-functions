namespace Demo.GreetingDurableFunction.Functions.Activity
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Interface;
    using System.Threading.Tasks;

    public class GoodbyeActivity
    {
        private readonly ILogger log;
        private readonly IMockDelayService mockDelayService;

        public GoodbyeActivity(IMockDelayService mockDelayService, ILogger<GoodbyeActivity> log)
        {
            this.log = log;
            this.mockDelayService = mockDelayService;
        }

        [FunctionName(nameof(GoodbyeActivity))]
        public async Task<string> SayGoodbyeAsync([ActivityTrigger] Person person)
        {
            await mockDelayService.MockDelayAsync(500, 3000);
            log.LogInformation($"Saying goodbye to {person}!");
            return $"Goodbye {person}!";
        }
    }
}