using Microsoft.AspNetCore.Mvc;

namespace LineaIII.Controllers.Alumno
{
    public class AlumnosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
