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

        public PlayerController(DB context, EmailService emailService, IWebHostEnvironment env)
        {
            _context = context;
            _emailService = emailService;
            _env = env;
        }

        [Authorize]
        [HttpPost("Add-Players")]
        public async Task<IActionResult> AddPlayers([FromForm] List<PlayerDTO> players)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("لا يمكن تحديد هوية المستخدم.");

                int academyId = int.Parse(userId);

                if (players == null || players.Count == 0)
                    return BadRequest("لم يتم استلام أي لاعبين.");

                string uploadsFolder = Path.Combine(_env.WebRootPath, "Players");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var playersToAdd = new List<Player>();

                foreach (var playerDTO in players)
                {
                    if (playerDTO.URLImage == null || playerDTO.URLPassport == null)
                        return BadRequest("الصورة أو جواز السفر غير موجود.");

                    if (string.IsNullOrWhiteSpace(playerDTO.PLayerName) ||
                        string.IsNullOrWhiteSpace(playerDTO.NumberShirt) ||
                        string.IsNullOrWhiteSpace(playerDTO.Nationality) ||
                        string.IsNullOrWhiteSpace(playerDTO.category))
                        return BadRequest("بعض البيانات الأساسية ناقصة.");

                    // حفظ صورة اللاعب
                    string playerImageFileName = $"{Guid.NewGuid()}.jpg";
                    string playerImagePath = Path.Combine(uploadsFolder, playerImageFileName);
                    using (var stream = new FileStream(playerImagePath, FileMode.Create))
                    {
                        await playerDTO.URLImage.CopyToAsync(stream);
                    }
                    string safeImageUrl = $"{Request.Scheme}://{Request.Host}/Players/{playerImageFileName}";

                    // حفظ صورة جواز السفر
                    string passportFileName = $"{Guid.NewGuid()}.jpg";
                    string passportPath = Path.Combine(uploadsFolder, passportFileName);
                    using (var stream = new FileStream(passportPath, FileMode.Create))
                    {
                        await playerDTO.URLPassport.CopyToAsync(stream);
                    }
                    string safePassportUrl = $"{Request.Scheme}://{Request.Host}/Players/{passportFileName}";

                    // إنشاء كائن اللاعب
                    Player player = new Player
                    {
                        PLayerName = playerDTO.PLayerName,
                        BirthDate = playerDTO.BirthDate,
                        Possition = playerDTO.Possition,
                        NumberShirt = playerDTO.NumberShirt,
                        URLImage = safeImageUrl,
                        URLPassport = safePassportUrl,
                        Nationality = playerDTO.Nationality,
                        category = playerDTO.category,
                        AcademyId = academyId,
                        Location = "Admin"
                    };

                    playersToAdd.Add(player);
                }

                if (playersToAdd.Count == 0)
                    return BadRequest("لم يتم تجهيز أي لاعب للحفظ.");

                await _context.Players.AddRangeAsync(playersToAdd);
                var change = await _context.SaveChangesAsync();

                if (change == 0)
                    return Ok("لم يتم حفظ البيانات");

                return Ok("تم تسجيل اللاعبين بنجاح.");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return BadRequest($"خطأ أثناء تسجيل اللاعبين: {inner}");
            }
        }



        [Authorize]
        [HttpPost("Add-Technical-administrative")]
        public async Task<IActionResult> AddTechnicalAdministrative([FromForm] TechnicalAdministrativeDTO technicalAdministrativeDTO)
        {
            try
            {
                // التحقق إن الصورة موجودة
                if (technicalAdministrativeDTO.URLImage == null || technicalAdministrativeDTO.URLImage.Length == 0)
                {
                    return BadRequest("الصورة المطلوبة غير موجودة.");
                }

                // تحديد مكان تخزين الصور داخل wwwroot
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Technicalandadministrative");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // جلب رقم الأكاديمية من بيانات المستخدم المسجل دخوله
                var academyId = int.Parse(User.FindFirstValue("Id")!);

                // التحقق من امتداد الصورة
                var fileExtension = Path.GetExtension(technicalAdministrativeDTO.URLImage.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("نوع الملف غير مدعوم. فقط الصور مسموحة (jpg - png - gif).");
                }

                // حفظ الصورة باسم فريد داخل المجلد
                // حفظ صورة الشخص
                string imageFileName = $"{Guid.NewGuid()}_{technicalAdministrativeDTO.URLImage.FileName}";
                string imagePath = Path.Combine(uploadsFolder, imageFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await technicalAdministrativeDTO.URLImage.CopyToAsync(stream);
                }
                string safeImageUrl = $"{Request.Scheme}://{Request.Host}/Technicalandadministrative/{imageFileName}";

                // حفظ صورة الجواز
                string passportFolder = Path.Combine(_env.WebRootPath, "Passports");
                if (!Directory.Exists(passportFolder))
                    Directory.CreateDirectory(passportFolder);

                string passportFileName = $"{Guid.NewGuid()}_{technicalAdministrativeDTO.URLPassport!.FileName}";
                string passportPath = Path.Combine(passportFolder, passportFileName);
                using (var stream = new FileStream(passportPath, FileMode.Create))
                {
                    await technicalAdministrativeDTO.URLPassport.CopyToAsync(stream);
                }
                string safePassportUrl = $"{Request.Scheme}://{Request.Host}/Passports/{passportFileName}";
                // إنشاء كائن المسؤول الفني/الإداري
                var technicalAdministrative = new Tech_admin
                {
                    FullName = technicalAdministrativeDTO.FullName!,
                    AcademyId = academyId,
                    URLImage = safeImageUrl,
                    URLPassport = safePassportUrl,
                    AcademyName = technicalAdministrativeDTO.AcademyName!,
                    attribute = technicalAdministrativeDTO.attribute,
                };

                // الحفظ في قاعدة البيانات
                await _context.Tech_admins.AddAsync(technicalAdministrative);
                await _context.SaveChangesAsync();

                // الرد برسالة نجاح + الكائن
                return Ok("تم تسجيل المسؤول الفني بنجاح");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return BadRequest($"خطأ أثناء تسجيل المسؤول الفني: {inner}");
            }
        }

        [Authorize]
        [HttpGet("Get-All-Players-By-Academy")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var Role = User.FindFirstValue("Role");
                if (Role == "Academy")
                {
                    var academyId = int.Parse(User.FindFirstValue("Id")!);
                    var players = await _context.Players.Where(x => x.AcademyId == academyId && x.Statu == true).ToListAsync();
                    return Ok(players);
                }
                else if (Role == "Admin")
                {
                    var players = await _context.Players.Where(x=>x.Location == "Admin" || (x.Location == "Academy" && x.Statu==true)).ToListAsync();
                    return Ok(players);
                }
                return BadRequest("Role not found");
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
                var playeres = await _context.Players.Where(x => x.AcademyId == Id && x.Statu == true).ToListAsync();
                List<GetPlayersDTO> playerDTOs = new List<GetPlayersDTO>();
                if (playeres.Any())
                {
                    foreach (var player in playeres)
                    {
                        var academy = await _context.Academies.Where(x => x.Id == player.AcademyId).FirstOrDefaultAsync();
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
                    .Where(x => x.id == id && x.AcademyId == academyId && x.Statu == false)
                    .FirstOrDefaultAsync();

                if (player == null)
                    return NotFound("اللاعب غير موجود أو غير مفعل");

                player.PLayerName = playerDTO.PLayerName!;
                player.BirthDate = playerDTO.BirthDate;
                player.Possition = playerDTO.Possition;
                player.NumberShirt = playerDTO.NumberShirt!;
                player.Nationality = playerDTO.Nationality!;
                player.category = playerDTO.category!;
                player.Location = "Admin"; // تحديد الموقع كـ Admin

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

                return Ok(new { message = "تم تعديل بيانات اللاعب بنجاح" });
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


        [HttpGet("Public-Goals-Report/{category}")]
        public async Task<IActionResult> GetTopScorers(string category)
        {
            try
            {
                var topScorers = await (
                    from goal in _context.goalsReports
                    join player in _context.Players
                        on goal.PlayerName equals player.PLayerName
                    where player.Statu == true && player.category == category
                    group new { goal, player } by new
                    {
                        player.PLayerName,
                        player.AcademyId,
                        player.Possition,
                        player.NumberShirt,
                        goal.AcadamyName
                    } into g
                    select new
                    {
                        PlayerName = g.Key.PLayerName,
                        AcademyName = g.Key.AcadamyName,
                        Position = g.Key.Possition,
                        NumberShirt = g.Key.NumberShirt,
                        GoalsCount = g.Count()
                    }
                )
                .OrderByDescending(x => x.GoalsCount)
                .ToListAsync();
                return Ok(topScorers);
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ أثناء جلب قائمة الهدافين: {ex.Message}");
            }
        }

        [HttpGet("Public-Cards-Report/{category}")]
        public async Task<IActionResult> CardsReportByType(string category)
        {
            try
            {
                var cardsReport = await (
                    from card in _context.CardsReports
                    join player in _context.Players 
                        on card.PlayerName equals player.PLayerName
                    where player.Statu == true && player.category == category
                    group card by new { card.PlayerName, card.AcadamyName } into groupData
                    select new
                    {
                        PlayerName = groupData.Key.PlayerName,
                        AcademyName = groupData.Key.AcadamyName,
                        YellowCards = groupData.Count(c => c.CardType.ToLower() == "yellow"),
                        RedCards = groupData.Count(c => c.CardType.ToLower() == "red"),
                        TotalCards = groupData.Count()
                    }
                )
                .OrderByDescending(x => x.TotalCards)
                .ToListAsync();
                return Ok(cardsReport);
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ أثناء جلب قائمة الإنذارات: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Private-Cards-Report/{category}")]
        public async Task<IActionResult> GetPrivateCardsReport(string category)
        {
            var academyId = int.Parse(User.FindFirstValue("Id")!);

            var report = await (
                from player in _context.Players
                where player.AcademyId == academyId && player.Statu == true && player.category == category
                join card in _context.CardsReports
                    on player.PLayerName equals card.PlayerName into playerCards
                from card in playerCards.DefaultIfEmpty()
                group card by new { player.PLayerName } into g
                select new
                {
                    PlayerName = g.Key.PLayerName,
                    YellowCards = g.Count(x => x != null && x.CardType.ToLower() == "yellow"),
                    RedCards = g.Count(x => x != null && x.CardType.ToLower() == "red"),
                    TotalCards = g.Count(x => x != null)
                }
            ).ToListAsync();

            return Ok(report);
        }

        [Authorize]
        [HttpGet("Private-Goals-Report/{category}")]
        public async Task<IActionResult> GetPrivateGoalsReport(string category)
        {
            var academyId = int.Parse(User.FindFirstValue("Id")!);

            var goalsReport = await (
                from player in _context.Players
                where player.AcademyId == academyId && player.Statu == true && player.category == category
                join goal in _context.goalsReports
                    on player.PLayerName equals goal.PlayerName into playerGoals
                from goal in playerGoals.DefaultIfEmpty()
                group goal by new { player.PLayerName, player.Possition, player.NumberShirt } into g
                select new
                {
                    PlayerName = g.Key.PLayerName,
                    Position = g.Key.Possition,
                    GoalsCount = g.Count(x => x != null),
                    NumberShirt = g.Key.NumberShirt
                }
            ).ToListAsync();

            return Ok(goalsReport);
        }


        //[Authorize]
        //[HttpGet("Active-Players")]
        //public async Task<IActionResult> GetActivePlayers()
        //{
        //    var Role = User.FindFirstValue("Role");
        //    if (Role == "Academy") {
        //    var academyId = int.Parse(User.FindFirstValue("Id")!); // استخراج الأكاديمية من الـ token

        //    var players = await _context.Players
        //        .Where(p => p.AcademyId == academyId && p.Statu == true)
        //        .Select(p => new
        //        {
        //            id = p.id,
        //            PlayerName = p.PLayerName,
        //            Nationality = p.Nationality,
        //            BirthDate = p.BirthDate.ToString("yyyy-MM-dd"),
        //            Position = p.Possition,
        //            NumberShirt = p.NumberShirt,
        //            URLImage = p.URLImage,
        //            URLPassport = p.URLPassport,
        //            Category = p.category
        //        })
        //        .ToListAsync();

        //    return Ok(players);
        //}
        //    else if(Role == "Admin")
        //    {

        //        var players = await _context.Players
        //            .Where(p=>p.Statu == true)
        //            .Select(p => new
        //            {
        //                id = p.id,
        //                PlayerName = p.PLayerName,
        //                Nationality = p.Nationality,
        //                BirthDate = p.BirthDate.ToString("yyyy-MM-dd"),
        //                Position = p.Possition,
        //                NumberShirt = p.NumberShirt,
        //                URLImage = p.URLImage,
        //                URLPassport = p.URLPassport,
        //                Category = p.category
        //            })
        //            .ToListAsync();

        //        return Ok(players);
        //    }
        //    return BadRequest("Role not found");
        //}

        //[Authorize]
        //[HttpGet("Not-Active-Players")]
        //public async Task<IActionResult> GetNotActivePlayers()
        //{
        //    var academyId = int.Parse(User.FindFirstValue("Id")!); // استخراج الأكاديمية من الـ token
        //    var Role = User.FindFirstValue("Role");
        //    if (Role == "Academy")
        //    {
        //        var players = await _context.Players
        //            .Where(p => p.AcademyId == academyId && p.Statu == false && p.Location == "Academy")
        //            .Select(p => new
        //            {
        //                Id = p.id,
        //                PlayerName = p.PLayerName,
        //                Nationality = p.Nationality,
        //                BirthDate = p.BirthDate.ToString("yyyy-MM-dd"),
        //                Position = p.Possition,
        //                NumberShirt = p.NumberShirt,
        //                URLImage = p.URLImage,
        //                URLPassport = p.URLPassport,
        //                Category = p.category,
        //                Notes = p.Notes
        //            })
        //            .ToListAsync();

        //        return Ok(players);
        //    }
        //    else if (Role == "Admin")
        //    {
        //        var players = await _context.Players
        //           .Where(p => p.AcademyId == academyId && p.Statu == false && p.Location == "Admin")
        //           .Select(p => new
        //           {
        //               Id = p.id,
        //               PlayerName = p.PLayerName,
        //               Nationality = p.Nationality,
        //               BirthDate = p.BirthDate.ToString("yyyy-MM-dd"),
        //               Position = p.Possition,
        //               NumberShirt = p.NumberShirt,
        //               URLImage = p.URLImage,
        //               URLPassport = p.URLPassport,
        //               Category = p.category,
        //               Notes = p.Notes
        //           })
        //           .ToListAsync();

        //        return Ok(players);
        //    }
        //    return BadRequest("Role not found");
        //}


    }
}
