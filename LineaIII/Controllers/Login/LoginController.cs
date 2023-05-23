using LineaIII.Data;
using LineaIII.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;

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
                Error error = new Error();
                error.Message = "Usuario o contrasenia incorrecta";
                return BadRequest(error);
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
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddSeconds(1)

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(tokenConfig);

                return Ok(token);

            }
        }

        [HttpPost]
        [Route("Recuperar")]
        public IActionResult recuperar([FromBody] RecuperarRequest email)
        {
            Usuario user = _context.Usuarios.FirstOrDefault(x => x.Email.Equals(email.Correo));
            if (user == null)
            {
                return BadRequest("No se encontro ningun usuario asociado a ese correo");    
            }
            else
            {
                try
                {
                    return Ok(user);
                }
                catch (Exception e)
                {

                    return BadRequest();
                }

            }
        }
 
    }

}
