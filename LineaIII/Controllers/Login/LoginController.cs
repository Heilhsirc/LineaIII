using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LineaIII.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        [HttpGet(Name = "Login")]
        public IActionResult login()
        {
            return Ok("Ya estoy funcionando");
        }
    }
}
