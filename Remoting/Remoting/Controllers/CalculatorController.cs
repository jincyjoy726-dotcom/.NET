using Microsoft.AspNetCore.Mvc;

namespace Remoting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("add")]
        public ActionResult<int> Add(int a, int b) => Ok(a + b);

        [HttpGet("subtract")]
        public ActionResult<int> Subtract(int a, int b) => Ok(a - b);
    }
}