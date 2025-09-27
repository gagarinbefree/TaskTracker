using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "dev-identifier"),
                new Claim(ClaimTypes.Name, "developer@example.com"),
                new Claim(ClaimTypes.Email, "developer@example.com"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim("dev_mode", "true")
            };

            var secret = _configuration["Jwt:Secret"];
            if (secret == null)
                return NotFound("JWT security key");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return Ok("Bearer " + new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
