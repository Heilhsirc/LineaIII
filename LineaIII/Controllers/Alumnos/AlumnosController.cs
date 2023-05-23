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
        [Route("Buscar/{id}")]
        public IActionResult buscar([FromRoute] int id)
        {
            Alumno alumnoB = _context.Alumno.Where(u => u.Id == id).FirstOrDefault();
            if (alumnoB == null)
            {
                Response message = new Response();
                message.Message = "Alumno no existe";
                message.Id = 4;
                return BadRequest(message);
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
                Response message = new Response();
                message.Message = "Alumno no existe";
                message.Id = 4;
                return BadRequest(message);
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
            Alumno alumnoA = _context.Alumno.FirstOrDefault(x => x.Nif.Equals(alumno.Nif));
            Response response = new Response();
            if (alumnoA == null)
            {
                _context.Alumno.Add(alumno);
                _context.SaveChanges();
                response.Message = "Alumno agregado correctamente";
                response.Id = 2;
                return Ok(response);
            }
            else
            {
                response.Message = "Error al agregar, el nif ya esta registrado";
                response.Id = 1;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult eliminar([FromRoute] int id)
        {
            Response response = new Response();
            Alumno alumnoE = _context.Alumno.FirstOrDefault(c => c.Id == id);
            if (alumnoE != null)
            {
                _context.Alumno.Remove(alumnoE);
                _context.SaveChanges();
                response.Message = "Alumno eliminado correctamente";
                response.Id = 3;
                return Ok(response);
            }
            else
            {
                response.Message = "Error al eliminar el alumno";
                response.Id = 4;
                return BadRequest(response);
            }
        }
    }
}
