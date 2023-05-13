using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return Ok(usuario);
            }
        }

        [HttpPost]
        [Route("Close")]
        public IActionResult close([FromBody] Usuario user)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Id == user.Id);
            if (usuario == null)
            {
                return BadRequest("Usuario no existe");
            }
            else
            {
                Security reg = _context.Security.FirstOrDefault(x => x.UsuarioId == usuario.Id);
                reg.Token = "";
                //_context.Usuarios.Attach(usuario);
                _context.Entry(reg).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Se cerro la sesion satisfactoriamente");
                
            }
        }
    }
}
