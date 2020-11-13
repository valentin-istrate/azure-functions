namespace Demo.GreetingDurableFunction.Services
{
    using Constants;
    using Extensions;
    using Interface;
    using Models;
    using System.Net.Http;

    internal class PersonService : IPersonService
    {
        public Person GetPersonFromRequestAsync(HttpRequestMessage req)
        {
            return new Person
            {
                FirstName = req.RequestUri.TryGetQueryValue(RequestConstants.FirstName, "first-name-not-set"),
                LastName = req.RequestUri.TryGetQueryValue(RequestConstants.LastName, "last-name-not-set")
            };
        }
    }
}