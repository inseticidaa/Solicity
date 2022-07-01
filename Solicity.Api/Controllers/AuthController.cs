using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Solicity.Domain.DTOs;
using Solicity.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Solicity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IConfiguration _configuration;

        public AuthController(IConfiguration config, IAuthService authService)
        {
            _configuration = config;
            _authService = authService;

        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserCreationDTO userCreationDTO)
        {
            try
            {
                var user = await _authService.RegisterAsync(userCreationDTO);

                var token = GenerateToken(user);

                return CreatedAtAction(null, new { Token = token, user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserCredentialDTO userCredentialDTO)
        {
            try
            {
                var user = await _authService.AuthenticateAsync(userCredentialDTO);

                var token = GenerateToken(user);

                return Ok(new { Token = token, user });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        private string GenerateToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
