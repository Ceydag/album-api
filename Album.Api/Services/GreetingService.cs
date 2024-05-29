using System.Net;

namespace Album.Api.Services
{
    public class GreetingService
    {
        public string GetGreeting(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Hello, World!";
            }
            else
            {
                return $"Hello, {name}! from {Dns.GetHostName()}"; 
            }
        }
    }
}