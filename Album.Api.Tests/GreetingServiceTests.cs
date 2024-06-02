using Xunit;
using System.Net;

namespace Album.Api.Tests
{
    public class TestGreetingService
    {
        public GreetingService greetingService = new();
        [Fact]
        public void TestGreetingNamed()
        {
            MessageModel result = greetingService.GetGreeting("Ceyda");
            Assert.StartsWith("Hello CEYDA", result.message);
        }

        [Fact]
        public void TestGreetingNull()
        {
            MessageModel result = greetingService.GetGreeting(null);
            Assert.StartsWith("Hello World", result.message);
        }

        [Fact]
        public void TestGreetingEmpty()
        {
            MessageModel result = greetingService.GetGreeting();
            Assert.StartsWith("Hello World", result.message);
        }
    }
}