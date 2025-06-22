using System.Security.AccessControl;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports.DTO;
using Sports.Model;
using static System.Net.WebRequestMethods;
using static Sports.DTO.GetAcademyDTO;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DB _context;
        private readonly IWebHostEnvironment _env;
        private EmailService _emailService;

        public RegisterController(DB context, IWebHostEnvironment env, EmailService emailService)
        {
            _context = context;
            _env = env;
            _emailService = emailService;
        }

        [HttpPost("Register-Academy")]
        public async Task<IActionResult> RegisterAcademy([FromForm] RegisterDTO registerDTO)
        {
            try
            {
                var isFound = await _context.Academies.AnyAsync(u => u.AcademyEmail == registerDTO.AcademyEmail);
                if (isFound)
                {
                    return BadRequest("البريد الإلكتروني موجود بالفعل");
                }
                if (registerDTO.Password != registerDTO.ConfirmPassword)
                {
                    return BadRequest("كلمة المرور وتأكيد كلمة المرور غير متطابقتين");
                }
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Logos");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string logoFileName = $"{Guid.NewGuid()}_{registerDTO.LogoURL!.FileName}";
                string logoPath = Path.Combine(uploadsFolder, logoFileName);

                using (var stream = new FileStream(logoPath, FileMode.Create))
                {
                    await registerDTO.LogoURL.CopyToAsync(stream);
                }

                string logoUrl = $"{Request.Scheme}://{Request.Host}/Logos/{logoFileName}";
                string safeUrl = Uri.EscapeUriString(logoUrl);

                Academy academy = new Academy
                {
                    AcademyName = registerDTO.AcademyName!,
                    AcademyEmail = registerDTO.AcademyEmail!,
                    AcademyPhone = registerDTO.AcademyPhone!,
                    AcademyCountry = registerDTO.AcademyCountry!,
                    academyManagerName = registerDTO.academyManagerName!,
                    AcademyPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                    LogoURL = safeUrl
                };

                await _context.Academies.AddAsync(academy);
                await _context.SaveChangesAsync();
                var body = $@"
<!DOCTYPE html>
<html lang=""ar"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>طلب تسجيل أكاديمية جديدة</title>
</head>
<body style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f6f8; color: #333; margin: 0; padding: 20px; direction: rtl;"">
    <div style=""max-width: 700px; margin: auto; background-color: #ffffff; border-radius: 10px; padding: 25px; box-shadow: 0 2px 8px rgba(0,0,0,0.05);"">
        <h2 style=""font-size: 18px; color: #2c3e50; margin-bottom: 10px;"">طلب تسجيل أكاديمية جديدة</h2>
        <p style=""font-size: 14px; margin: 10px 0;"">تم تقديم طلب لتسجيل أكاديمية جديدة، يرجى مراجعة البيانات التالية:</p>

        <table style=""width: 100%; border-collapse: collapse; margin-top: 20px;"">
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">اسم الأكاديمية</td>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">{registerDTO.AcademyName}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">البريد الإلكتروني</td>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">{registerDTO.AcademyEmail}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">رقم الهاتف</td>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">{registerDTO.AcademyPhone}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">الدولة</td>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">{registerDTO.AcademyCountry}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd; font-size: 14px;"">شعار الأكاديمية</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">
                    <img src=""{safeUrl}"" alt=""شعار الأكاديمية"" style=""max-width: 150px; height: auto; border-radius: 6px;"">
                </td>
            </tr>
        </table>

        <div style=""margin-top: 25px; text-align: center;"">
            <a href=""https://sports.runasp.net/api/Approve/{academy.Id}"" target=""_blank""
               style=""display: inline-block; background-color: #27ae60; color: white; padding: 10px 20px;
                      font-size: 14px; border-radius: 6px; font-weight: normal; text-decoration: none;
                      margin: 5px;"">
                ✔️ قبول الطلب
            </a>
            <a href=""https://sports.runasp.net/api/Reject/{academy.Id}"" target=""_blank""
               style=""display: inline-block; background-color: #c0392b; color: white; padding: 10px 20px;
                      font-size: 14px; border-radius: 6px; font-weight: normal; text-decoration: none;
                      margin: 5px;"">
                ❌ رفض الطلب
            </a>
        </div>

        <div style=""font-size: 13px; color: #777; margin-top: 30px; text-align: center;"">
            <p>مع تحيات فريق <strong>خليجية كواترو</strong></p>
            <p>
                <a href=""https://khalej-kotro.vercel.app"" target=""_blank"" style=""color: #3498db;"">خليجية كواترو</a><br/>
                الدعم: <a href=""mailto:support@quattrogcc.ae"" style=""color: #3498db;"">support@quattrogcc.ae</a>
            </p>
        </div>
    </div>
</body>
</html>";

                var sendEmail = await _emailService.SendEmailAsync("info@quattrogcc.ae", "طلب تسجيل أكادمية جديدة", body);
                if (!sendEmail.Equals("تم إرسال البريد الإلكتروني بنجاح"))
                {
                    return BadRequest("حدث خطأ أثناء إرسال البريد الإلكتروني");
                }
                return Ok("تم تقديم الطلب بنجاح ! سيتم مراجعة بياناتك والموافقة على طلب تسجيل الفريق في البطولة");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error registering academy: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-All-Academies")]
        public async Task<IActionResult> GetAllAcademies()
        {
            try
            {
                var academies = await _context.Academies
                    .Select(x => new AcademyDTO
                    {
                        Id = x.Id,
                        AcademyName = x.AcademyName,
                        AcademyCountry = x.AcademyCountry,
                        AcademyEmail = x.AcademyEmail,
                        AcademyPhone = x.AcademyPhone,
                        LogoURL = x.LogoURL,
                        AdditionalPhoneNumber = x.AdditionalPhoneNumber,
                        AdditionalEmail = x.AdditionalEmail,
                        Statue = x.Statue,
                        Role = x.Role,
                        TShirtColor = x.TShirtColor,
                        ShortColor = x.ShortColor,
                        ShoesColor = x.ShoesColor,
                        AdditionalTShirtColor = x.AdditionalTShirtColor,
                        AdditionalShortColor = x.AdditionalShortColor,
                        AdditionalShoesColor = x.AdditionalShoesColor,
                        Under12 = x.under12,
                        Under14 = x.under14,
                        Under16 = x.under16,
                        academyManagerName = x.academyManagerName
                    })
                    .ToListAsync();

                if (!academies.Any())
                    return Ok(new List<AcademyDTO>());

                return Ok(academies);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"حدث خطأ أثناء جلب البيانات: {ex.Message}" });
            }
        }


        [Authorize]
        [HttpGet("Get-Academy-By-Id/{id}")]
        public async Task<IActionResult> GetAcademies(int id)
        {
            try
            {
                var academy = await _context.Academies.FindAsync(id);
                if (academy == null)
                {
                    return NotFound("Academy not found.");
                }
                return Ok(academy);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving academy: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("Delete-Academy/{id}")]
        public async Task<IActionResult> DeleteAcademy(int id)
        {
            try
            {
                var academy = await _context.Academies.FindAsync(id);
                if (academy == null)
                {
                    return NotFound("Academy not found.");
                }
                _context.Academies.Remove(academy);
                _context.SaveChanges();
                return Ok("Academy deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting academy: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Update-Academy/{id}")]
        public async Task<IActionResult> UpdateAcademy(int id, [FromBody] UpdateAcademyDTO registerDTO)
        {
            try
            {
                var academy = await _context.Academies.FindAsync(id);
                if (academy == null)
                {
                    return NotFound("الأكاديمية غير موجودة");
                }
                academy.AdditionalPhoneNumber = registerDTO.AdditionalPhoneNumber;
                academy.AdditionalEmail = registerDTO.AdditionalEmail;
                academy.TShirtColor = registerDTO.TShirtColor;
                academy.ShortColor = registerDTO.ShortColor;
                academy.ShoesColor = registerDTO.ShoesColor;
                academy.AdditionalTShirtColor = registerDTO.AdditionalTShirtColor;
                academy.AdditionalShortColor = registerDTO.AdditionalShortColor;
                academy.AdditionalShoesColor = registerDTO.AdditionalShoesColor;
                academy.under12 = registerDTO.under12;
                academy.under14 = registerDTO.under14;
                academy.under16 = registerDTO.under16;

                _context.Academies.Update(academy);
                await _context.SaveChangesAsync();

                return Ok("تم تحديث بيانات الأكاديمية بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ أثناء التحديث: {ex.Message}");
            }
        }


        [Authorize]
        [HttpGet("Count-Academy")]
        public async Task<IActionResult> CountAcademy()
        {
            try
            {
                var academy = await _context.Academies.CountAsync();
                var country = await _context.Academies.Select(a => a.AcademyCountry).Distinct().CountAsync();

                return Ok(new { CountAcademy = academy, CountCountry = country });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error counting academies: {ex.Message}");
            }
        }


        [Authorize]
        [HttpGet("Get-Categories")]
        public async Task<IActionResult> getCategory()
        {
            try
            {
                var AcademyID = int.Parse(User.FindFirstValue("Id")!);
                var Category = await _context.Academies.Where(x=>x.Id == AcademyID).Select(g => new {g.under12 , g.under14 , g.under16}).ToListAsync();
                if (Category.Any())
                {
                    return Ok(Category);
                }
                return Ok(new string[] {});
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
