using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LineaIII.Controllers.Alumnos
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlumnosController : Controller
    {
        private readonly DBContext _context;

        public AlumnosController(DBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Listar")]
        public IActionResult listar()
        {
            return Ok(_context.Alumno.ToList());
        }
        [HttpGet]
        [Route("Buscar")]
        public IActionResult buscar([FromBody] Alumno alumno)
        {
            Alumno alumnoB = _context.Alumno.Where(u => u.Id == alumno.Id).FirstOrDefault();
            if (alumnoB == null)
            {
                return Ok("Alumno no existe");
            }
            else
            {
                return Ok(alumnoB);
            }
        }

        [HttpPost]
        [Route("Modificar")]
        public IActionResult edit([FromBody] Alumno alumno)
        {
            Alumno alumnoM = _context.Alumno.Where(u => u.Id == alumno.Id).FirstOrDefault();
            if (alumnoM == null)
            {
                return Ok("Alumno no existe");
            }
            else
            {
                alumnoM.Nombre = alumno.Nombre;
                alumnoM.Apellido1 = alumno.Apellido1;
                alumnoM.Apellido2 = alumno.Apellido2;
                //_context.Alumnos.Attach(Alumno);
                _context.Entry(alumnoM).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Alumno modificado exitosamente");
            }
        }
        [HttpPut]
        [Route("Agregar")]
        public IActionResult add(Alumno alumno)
        {
            Alumno alumnoA = _context.Alumno.FirstOrDefault(x => x.Id == alumno.Id);
            if (alumnoA != null)
            {
                return Ok();
            }
            else
            {
                return Ok("Alumno no encontrado");
            }
        }
    }
}
