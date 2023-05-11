using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LineaIII.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DBContext _context;

        public LoginController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult login([FromBody] Usuario user)
        {
            Usuario usuario = _context.Usuarios.Where(u => u.Password.Equals(user.Password) && u.Username.Equals(user.Username)).FirstOrDefault();
            if (usuario == null)
            {
                return BadRequest("Usuario no existe");
            }
            else
            {
                return Ok(true);
            }
        }
    }
}
