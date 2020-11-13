namespace Demo.GreetingDurableFunction.Functions.Activity
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Interface;
    using System.Threading.Tasks;

    public class HowAreYouActivity
    {
        private readonly ILogger log;
        private readonly IMockDelayService mockDelayService;

        public HowAreYouActivity(IMockDelayService mockDelayService, ILogger<HowAreYouActivity> log)
        {
            this.log = log;
            this.mockDelayService = mockDelayService;
        }

        [FunctionName(nameof(HowAreYouActivity))]
        public async Task<string> AskHowAreYouAsync([ActivityTrigger] Person person)
        {
            await mockDelayService.MockDelayAsync(500, 3000);

            log.LogInformation($"Saying hello to {person}.");
            return $"How are you, {person}?";
        }
    }
}