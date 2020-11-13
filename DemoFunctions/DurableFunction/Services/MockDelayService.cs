namespace Demo.GreetingDurableFunction.Services
{
    using System;
    using System.Threading.Tasks;
    using Interface;
    using Microsoft.Extensions.Logging;

    internal class MockDelayService : IMockDelayService
    {
        private readonly ILogger log;
        private readonly Random randomGenerator;

        public MockDelayService(ILogger<MockDelayService> log)
        {
            this.log = log;
            this.randomGenerator = new Random();
        }

        public async Task MockDelayAsync(int minDelay, int maxDelay)
        {
            int delay = randomGenerator.Next(minDelay, maxDelay);
            log.LogDebug($"Mocking delay for {delay} ms.");
            await Task.Delay(delay);
        }
    }
}