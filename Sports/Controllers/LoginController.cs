using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    return BadRequest("جميع الحقول مطلوبة");
                }
                var user = await _context.Academies.Where(u => u.AcademyEmail == loginDTO.AcademyEmail).FirstOrDefaultAsync();

                if (user == null || string.IsNullOrEmpty(user.AcademyPassword) 
                    ||!BCrypt.Net.BCrypt.Verify(loginDTO.AcademyPassword, user.AcademyPassword) )
                {
                    return Unauthorized("خطأ في اسم المستخدم او كلمة المرور");
                }
                if (user.Statue == false)
                {
                    return Unauthorized("لم يتم الموافقة علي تسجيل الأكادمية");
                }
                var Token = await token.GenerateToken(user);
                return Ok(Token);
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ اثناء تسجيل الدخول: {ex.Message}");
            }
        }
    }
}
