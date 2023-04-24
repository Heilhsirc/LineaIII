using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LineaIII.Controllers.Cursos
{
    public class CursosController : Controller
    {
        private readonly DBContext _context;

        public CursosController(DBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Listar")]
        public IActionResult listar()
        {
            return Ok(_context.Curso.ToList());
        }
        [HttpGet]
        [Route("Buscar")]
        public IActionResult buscar([FromBody] Curso curso)
        {
            Curso cursoB = _context.Curso.Where(u => u.Id == curso.Id).FirstOrDefault();
            if (cursoB == null)
            {
                return Ok("Usuario no existe");
            }
            else
            {
                return Ok(cursoB);
            }
        }

        [HttpPost]
        [Route("Modificar")]
        public IActionResult edit([FromBody] Curso curso)
        {
            Curso cursoE = _context.Curso.Where(u => u.Id == curso.Id).FirstOrDefault();
            if (cursoE == null)
            {
                return Ok("Usuario no existe");
            }
            else
            {
                cursoE.Nombre = curso.Nombre;
                cursoE.Creditos = curso.Creditos;
                //_context.Usuarios.Attach(usuario);
                _context.Entry(cursoE).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Usuario modificado exitosamente");
            }
        }
        [HttpPut]
        [Route("Agregar")]
        public IActionResult add(Curso curso)
        {
            Curso cursoA = _context.Curso.FirstOrDefault(x => x.Id == curso.Id);
            if (cursoA != null)
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
