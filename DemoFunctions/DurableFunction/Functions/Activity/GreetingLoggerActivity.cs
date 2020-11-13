namespace Demo.GreetingDurableFunction.Functions.Activity
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    public class GreetingLoggerActivity
    {
        private readonly ILogger log;

        public GreetingLoggerActivity(ILogger<GreetingLoggerActivity> log)
        {
            this.log = log;
        }

        [FunctionName(nameof(GreetingLoggerActivity))]
        public void LogGreetings([ActivityTrigger] IList<string> greetings)
        {
            log.LogInformation("The following greetings have been generated:");
            log.LogInformation(string.Join(Environment.NewLine, greetings));
        }
    }
}