namespace Demo.GreetingDurableFunction.Services.Interface
{
    using System.Threading.Tasks;

    public interface IMockDelayService
    {
        Task MockDelayAsync(int minDelay, int maxDelay);
    }
}