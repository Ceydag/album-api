using Microsoft.AspNetCore.Mvc;
using Album.Api.Services;

namespace Album.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly GreetingService _greetingService;

        public HelloController(GreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        [HttpGet("hello")]
        public IActionResult GetHello([FromQuery] string name)
        {
            string message = _greetingService.GetGreeting(name);
            return Ok(new { Message = message });
        }
    }
}