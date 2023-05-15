using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using LineaIII.Modelo;
using System.Text;
using LineaIII.Data;
using Microsoft.EntityFrameworkCore;

namespace LineaIII.Controllers.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _token;
        private readonly DBContext _context;

        public AuthenticationController(IConfiguration config, DBContext context)
        {
            _context = context;
            _token = config.GetSection("settings").GetSection("secretKey").ToString();
        }

        [HttpPost]
        [Route("Auth")]
        public IActionResult Auth([FromBody] Auth request)
        {
            Usuario user = _context.Usuarios.FirstOrDefault(x => x.Username.Equals(request.User)&&x.Password.Equals(request.TranKey));
            if (user!=null) { 
            var keyBytes = Encoding.ASCII.GetBytes(_token);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.User));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(tokenConfig);
            Security reg = _context.Security.FirstOrDefault(x => x.UsuarioId == user.Id);
                if (reg!=null)
                {
                    reg.Token = token;
                    _context.Entry(reg).State = EntityState.Modified;
                    _context.SaveChanges();
                    var response = (from u in _context.Usuarios
                             join s in _context.Security on u.Id equals s.UsuarioId

                             where u.Id == user.Id

                             select new
                             {
                                u.Id, u.Username, u.Password, u.Email,u.Nombre,u.Rolid, s.Token

                             });

                    return Ok (response);
                }
                else
                {
                    reg.UsuarioId=user.Id;
                
                    _context.Security.Add(reg);
                    _context.SaveChanges();
                    var response = (from u in _context.Usuarios
                                    join s in _context.Security on u.Id equals s.UsuarioId

                                    where u.Id == user.Id

                                    select new
                                    {
                                        u.Id,
                                        u.Username,
                                        u.Password,
                                        u.Email,
                                        u.Nombre,
                                        u.Rolid,
                                        s.Token

                                    });
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest();
            }
           
        }

    }
}
