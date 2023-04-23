using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LineaIII.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        
        [HttpGet]
        [Route("Login")]
        public IActionResult login()
        {
            return Ok("Ya estoy funcionando");
        }
    }
}
