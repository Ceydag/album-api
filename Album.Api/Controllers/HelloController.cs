using Microsoft.AspNetCore.Mvc;

namespace AlbumApi.Controllers
{
    [Route("api/hello")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly GreetingService greetingService = new();

        private readonly ILogger<HelloController> _logger;

        public HelloController(ILogger<HelloController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public string GetGreeting([FromQuery] string? name = "")
        {
            var message = greetingService.GetGreeting(name);
            string log_message = $"GetGreeting({name}): {message.message}";
            _logger.LogInformation(log_message);
            return message.message;
        }
    }
}