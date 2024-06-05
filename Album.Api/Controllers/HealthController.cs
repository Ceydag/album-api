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
             try {
                return Ok("Healthy");
            } catch {
                return Ok("Not healthy");
            }
        }
    }
}