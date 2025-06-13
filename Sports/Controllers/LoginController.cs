using Microsoft.AspNetCore.Mvc;
using Sports.DTO;
using Sports.Model;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DB _context;
        private readonly Token token;

        public LoginController(DB context, Token token)
        {
            _context = context;
            this.token = token;
        }

        [HttpPost("Login-Academy")]
        public async Task<IActionResult> LoginAcademy(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.AcademyEmail) || string.IsNullOrEmpty(loginDTO.AcademyPassword))
                {
                    return BadRequest("Invalid login credentials.");
                }
                var user = _context.Academies.FirstOrDefault(u => u.AcademyEmail == loginDTO.AcademyEmail);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.AcademyPassword, user.AcademyPassword))
                {
                    return Unauthorized("Invalid username or password.");
                }
                var Token = await token.GenerateToken(user);
                return Ok(Token);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error during login: {ex.Message}");
            }
        }
    }
}
