using Microsoft.AspNetCore.Mvc;

namespace LineaIII.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpGet(Name = "Login")]
        public IActionResult login()
        {
            return Ok("Ya estoy funcionando");
        }
    }
}
