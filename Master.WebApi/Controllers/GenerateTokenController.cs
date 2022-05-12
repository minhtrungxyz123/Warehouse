using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Master.WebApi.Controllers
{
    [Route("generate-token")]
    [ApiController]
    public class GenerateTokenController : ControllerBase
    {
        private static string Secret = "0342699952";

        private readonly IConfiguration _configuration;

        public GenerateTokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("GenerateToken")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GenerateToken(string secret)
        {
            if (secret is null)
            {
                return Ok("Non Author");
            }

            if (secret.Equals(Secret))
            {
                var authClaims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name,"GenerateToken"),
                                        new Claim("IdSession", Guid.NewGuid().ToString())
                                    };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(100),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                });
            }
            return Ok("Non Author");
        }
    }
}