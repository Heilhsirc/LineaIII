using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using LineaIII.Modelo;
using System.Text;

namespace LineaIII.Controllers.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _token;

        public AuthenticationController(IConfiguration config)
        {
            _token = config.GetSection("settings").GetSection("secretKey").ToString();
        }

        [HttpPost]
        [Route("Auth")]
        public IActionResult Auth([FromBody] Auth request)
        {
            if (request.User.Equals("Sofia") && request.TranKey.Equals("1234")) { 
            var keyBytes = Encoding.ASCII.GetBytes(_token);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.User));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(tokenConfig);
                return StatusCode(StatusCodes.Status200OK, new { token = token});
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { token = "" });
            }
        }

    }
}
