using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports.DTO;
using Sports.Model;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly DB _context;
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _env;

        public PlayerController(DB context, EmailService emailService,IWebHostEnvironment env)
        {
            _context = context;
            _emailService = emailService;
            _env = env;
        }

        [Authorize]
        [HttpPost("Add-Players")]
        public async Task<IActionResult> AddPlayers([FromForm] PlayerDTO playerDTO)
        {
            try
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Players");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string playerImageFileName = $"{Guid.NewGuid()}_{playerDTO.URLImage!.FileName}";
                string playerImagePath = Path.Combine(uploadsFolder, playerImageFileName);
                using (var stream = new FileStream(playerImagePath, FileMode.Create))
                {
                    await playerDTO.URLImage.CopyToAsync(stream);
                }
                string safeImageUrl = Uri.EscapeUriString($"{Request.Scheme}://{Request.Host}/Players/{playerImageFileName}");

                string passportFileName = $"{Guid.NewGuid()}_{playerDTO.URLPassport!.FileName}";
                string passportPath = Path.Combine(uploadsFolder, passportFileName);
                using (var stream = new FileStream(passportPath, FileMode.Create))
                {
                    await playerDTO.URLPassport.CopyToAsync(stream);
                }
                string safePassportUrl = Uri.EscapeUriString($"{Request.Scheme}://{Request.Host}/Players/{passportFileName}");

                Player player = new Player
                {
                    PLayerName = playerDTO.PLayerName!,
                    BirthDate = playerDTO.BirthDate,
                    Possition = playerDTO.Possition,
                    NumberShirt = playerDTO.NumberShirt!,
                    URLImage = safeImageUrl,
                    URLPassport = safePassportUrl,
                    Nationality = playerDTO.Nationality!,
                    category = playerDTO.category!,
                    AcademyId = int.Parse(User.FindFirstValue("Id")!)
                };

                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();

                var academy = await _context.Academies.FindAsync(player.AcademyId);

                var body = $@"
<!DOCTYPE html>
<html lang=""ar"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>طلب تسجيل لاعب جديد</title>
</head>
<body style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f6f8; color: #333; margin: 0; padding: 20px; direction: rtl;"">
    <div style=""max-width: 700px; margin: auto; background-color: #ffffff; border-radius: 10px; padding: 25px; box-shadow: 0 2px 8px rgba(0,0,0,0.05);"">
        <h2 style=""font-size: 18px; color: #2c3e50; margin-bottom: 10px;"">📝 طلب تسجيل لاعب جديد</h2>
        <p style=""font-size: 14px; margin: 10px 0;"">تم تقديم طلب لتسجيل لاعب جديد، يرجى مراجعة البيانات التالية:</p>

        <table style=""width: 100%; border-collapse: collapse; margin-top: 20px;"">
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">اسم اللاعب</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">{player.PLayerName}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">الجنسية</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">{player.Nationality}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">تاريخ الميلاد</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">{player.BirthDate:yyyy-MM-dd}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">المركز</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">{player.Possition ?? "—"}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">رقم القميص</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">{player.NumberShirt}</td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">صورة اللاعب</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">
                    <img src=""{safeImageUrl}"" alt=""صورة اللاعب"" style=""max-width: 150px; height: auto; border-radius: 6px;"">
                </td>
            </tr>
            <tr>
                <td style=""padding: 10px; border: 1px solid #ddd;"">صورة جواز السفر</td>
                <td style=""padding: 10px; border: 1px solid #ddd;"">
                    <img src=""{safePassportUrl}"" alt=""جواز السفر"" style=""max-width: 150px; height: auto; border-radius: 6px;"">
                </td>
            </tr>
        </table>

        <div style=""margin-top: 25px; text-align: center;"">
            <a href=""https://backend.quattrogcc.ae/api/approve-Player/{player.id}"" target=""_blank""
               style=""display: inline-block; background-color: #27ae60; color: white; padding: 10px 20px;
                      font-size: 14px; border-radius: 6px; font-weight: normal; text-decoration: none;
                      margin: 5px;"">
                ✅ قبول الطلب
            </a>
            <a href=""https://backend.quattrogcc.ae/api/reject-Player/{player.id}"" target=""_blank""
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
                await _emailService.SendEmailAsync("info@quattrogcc.ae", "طلب تسجيل لاعب جديد", body);
                return Ok("تم إرسال طلب التسجيل بنجاح. في انتظار مراجعة وموافقة الأكاديمية على طلب انضمامك كلاعب");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return BadRequest($"Error adding players: {inner}");
            }
        }

        [HttpGet("Get-All-Players")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var players = await _context.Players.ToListAsync();
                if (players == null || !players.Any())
                {
                    return Ok(new string[] { });
                }
                return Ok(players);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving players: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Player-By-Id")]
        public async Task<IActionResult> GetPlayerById()
        {
            try
            {
                var Id = int.Parse(User.FindFirstValue("Id")!);
                var playeres = await _context.Players.Where(x=>x.AcademyId == Id && x.Statu == true).ToListAsync();
                List<GetPlayersDTO> playerDTOs = new List<GetPlayersDTO>();
                if (playeres.Any())
                {
                    foreach(var player in playeres)
                    {
                        var academy = await _context.Academies.Where(x=>x.Id == player.AcademyId).FirstOrDefaultAsync();
                        playerDTOs.Add(new GetPlayersDTO
                        {
                            PLayerName = player.PLayerName,
                            Possition = player.Possition,
                            URLPassport = player.URLPassport,
                            URLImage = player.URLImage,
                            category = player.category,
                            AcademyName = academy!.AcademyName,
                            NumberShirt = player.NumberShirt,
                            Nationality = player.Nationality,
                            BirthDate = player.BirthDate.ToString("yyyy-MM-dd"),
                            PlayerID = player.id
                        });
                    }
                    return Ok(playerDTOs);
                }

                return Ok(new string[] { });
            }
            catch (Exception)
            {
                return BadRequest("حدث خطأ");
            }
        }

        [Authorize]
        [HttpPost("Update-Player/{id}")]
        public async Task<IActionResult> UpdatePlayerById(int id, [FromForm] PlayerDTO playerDTO)
        {
            try
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);
                var player = await _context.Players
                    .Where(x => x.id == id && x.AcademyId == academyId && x.Statu == true)
                    .FirstOrDefaultAsync();

                if (player == null)
                    return NotFound("اللاعب غير موجود أو غير مفعل");

                player.PLayerName = playerDTO.PLayerName!;
                player.BirthDate = playerDTO.BirthDate;
                player.Possition = playerDTO.Possition;
                player.NumberShirt = playerDTO.NumberShirt!;
                player.Nationality = playerDTO.Nationality!;

                // تجهيز مجلد الحفظ
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Players");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // تحديث صورة اللاعب (إن وُجدت)
                if (playerDTO.URLImage != null)
                {
                    string playerImageFileName = $"{Guid.NewGuid()}_{playerDTO.URLImage.FileName}";
                    string playerImagePath = Path.Combine(uploadsFolder, playerImageFileName);
                    using (var stream = new FileStream(playerImagePath, FileMode.Create))
                    {
                        await playerDTO.URLImage.CopyToAsync(stream);
                    }
                    string safeImageUrl = Uri.EscapeUriString($"{Request.Scheme}://{Request.Host}/Players/{playerImageFileName}");
                    player.URLImage = safeImageUrl;
                }

                // تحديث جواز السفر (إن وُجد)
                if (playerDTO.URLPassport != null)
                {
                    string passportFileName = $"{Guid.NewGuid()}_{playerDTO.URLPassport.FileName}";
                    string passportPath = Path.Combine(uploadsFolder, passportFileName);
                    using (var stream = new FileStream(passportPath, FileMode.Create))
                    {
                        await playerDTO.URLPassport.CopyToAsync(stream);
                    }
                    string safePassportUrl = Uri.EscapeUriString($"{Request.Scheme}://{Request.Host}/Players/{passportFileName}");
                    player.URLPassport = safePassportUrl;
                }

                _context.Players.Update(player);
                await _context.SaveChangesAsync();

                return Ok("تم تعديل بيانات اللاعب بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ أثناء التعديل: {ex.Message}");
            }
        }


        [Authorize]
        [HttpDelete("Delete-Player/{id}")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);
                if (player == null)
                {
                    return NotFound("Player not found.");
                }
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return Ok("Player deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting player: {ex.Message}");
            }
        }
    }
}
