using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LineaIII.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly DBContext _context;

        public UsuarioController(DBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Listar")]
        public IActionResult listar()
        {
            return Ok(_context.Usuarios.ToList());
        }
        [HttpGet]
        [Route("Buscar")]
        public IActionResult buscar([FromBody] Usuario user)
        {
            Usuario usuario = _context.Usuarios.Where(u => u.Id == user.Id).FirstOrDefault();
            if (usuario == null)
            {
                return Ok("Usuario no existe");
            }
            else
            {
                return Ok(usuario);
            }
        }

        [HttpPost]
        [Route("Modificar")]
        public IActionResult edit([FromBody] Usuario user)
        {
            Usuario usuario = _context.Usuarios.Where(u => u.Id == user.Id).FirstOrDefault();
            if (usuario == null)
            {
                return Ok("Usuario no existe");
            }
            else
            {
                usuario.Nombre = user.Nombre;
                usuario.Email = user.Email;
                //_context.Usuarios.Attach(usuario);
                _context.Entry(usuario).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Usuario modificado exitosamente");
            }
        }
        [HttpPut]
        [Route("Agregar")]
        public IActionResult add(Usuario user)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Id == user.Id);
            if (usuario != null)
            {
                return Ok();
            }
            else
            {
                return Ok("Usuario no encontrado");
            }
        }
    }
}
