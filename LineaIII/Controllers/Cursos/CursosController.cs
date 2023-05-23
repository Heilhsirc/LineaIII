using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LineaIII.Controllers.Cursos
{
    [ApiController]
    [Route("api/[controller]")]
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
        [Route("Buscar/{id}")]
        public IActionResult buscar([FromRoute] int id)
        {
            Curso cursoB = _context.Curso.Where(u => u.Id == id).FirstOrDefault();
            if (cursoB == null)
            {
                return Ok("Curso no existe");
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
            Response response = new Response();
            Curso cursoE = _context.Curso.Where(u => u.Id == curso.Id).FirstOrDefault();
            if (cursoE == null)
            {

                response.Message = "Curso no existe";
                response.Id = 1;
                return Ok(response);
            }
            else
            {
                cursoE.Nombre = curso.Nombre;
                cursoE.Creditos = curso.Creditos;
                //_context.Usuarios.Attach(usuario);
                _context.Entry(cursoE).State = EntityState.Modified;
                _context.SaveChanges();
                response.Message = "Curso modificado exitosamente";
                response.Id = 2;
                return Ok(response);
            }
        }
        [HttpPut]
        [Route("Agregar")]
        public IActionResult add(Curso curso)
        {
            Response response = new Response();
            Curso cursoA = _context.Curso.FirstOrDefault(x => x.Codigo.Equals(curso.Codigo));
            if (cursoA == null)
            {
                _context.Curso.Add(curso);
                _context.SaveChanges();
                response.Message = "Curso agregado correctamente";
                response.Id = 2;
                return Ok(response);
            }
            else
            {
                response.Message = "Error al agregar el curso, el codigo del curso ya esta registrado";
                response.Id = 1;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult eliminar([FromRoute] int id)
        {
            Response response = new Response();
            Curso cursoE = _context.Curso.FirstOrDefault(c => c.Id == id);
            if (cursoE != null)
            {
                _context.Curso.Remove(cursoE);
                _context.SaveChanges();
                response.Message = "Curso eliminado correctamente";
                response.Id = 3;
                return Ok(response);
            }
            else
            {
                response.Message = "Error al eliminar el curso";
                response.Id = 4;
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("BuscarXAlumno/{id}")]
        public IActionResult buscarxalumno([FromRoute] int id)
        {
            Response response = new Response();
            var cursos = (from t in _context.Tabcxa
                          join a in _context.Alumno on t.UsuarioId equals a.Id
                          join c in _context.Curso on t.CursoId equals c.Id

                            where a.Id == id && t.Is_active.Equals('1')

                            select new
                            {
                                c.Nombre,
                                c.Codigo,
                                c.Id
                            }).ToList();
            return Ok(cursos);
        }
    }
}
