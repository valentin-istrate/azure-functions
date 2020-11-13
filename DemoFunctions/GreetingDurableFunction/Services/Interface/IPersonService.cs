namespace Demo.GreetingDurableFunction.Services.Interface
{
    using Models;
    using System.Net.Http;

    public interface IPersonService
    {
        Person GetPersonFromRequestAsync(HttpRequestMessage req);
    }
}