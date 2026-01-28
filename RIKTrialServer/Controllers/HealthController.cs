using Microsoft.AspNetCore.Mvc;

namespace RIKTrialServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {
            return Ok();
        }
    }
}
