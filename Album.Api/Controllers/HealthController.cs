using Microsoft.AspNetCore.Mvc;

namespace Album.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("health")]
        public IActionResult CheckHealth()
        {
            return Ok("Healthy");
        }
    }
}