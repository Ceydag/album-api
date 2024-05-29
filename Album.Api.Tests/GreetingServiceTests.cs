using Xunit;
using Album.Api.Services;
using System.Net;

namespace Album.Api.Tests
{
    public class GreetingServiceTests
    {
        private readonly GreetingService _greetingService;

        public GreetingServiceTests()
        {
            _greetingService = new GreetingService();
        }

        [Fact]
        public void GetGreeting_WithGivenName_ReturnsHelloName()
        {
            string result = _greetingService.GetGreeting("Alice");
            Assert.Equal($"Hello, Alice! from {Dns.GetHostName()}", result);
        }

        [Theory]
        [InlineData(null, "Hello, World!")]
        [InlineData("", "Hello, World!")]
        [InlineData("   ", "Hello, World!")]
        public void GetGreeting_WithNullOrWhiteSpace_ReturnsHelloWorld(string name, string expected)
        {
            string result = _greetingService.GetGreeting(name);
            Assert.Equal(expected, result);
        }
    }
}