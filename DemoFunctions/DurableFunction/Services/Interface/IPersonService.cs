namespace Demo.GreetingDurableFunction.Services.Interface
{
    using System.Net.Http;
    using Models;

    public interface IPersonService
    {
        Person GetPersonFromRequestAsync(HttpRequestMessage req);
    }
}