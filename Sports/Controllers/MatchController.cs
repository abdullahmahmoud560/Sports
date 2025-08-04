using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports.DTO;
using Sports.Model;
using System.Security.Claims;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly DB _context;

        public MatchController(DB context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("Add-Match")]
        public async Task<IActionResult> AddMatch([FromBody]MatchDTO matchDTO)
        {
            try
            {
                if (matchDTO == null)
                {
                    return BadRequest("Match data is required.");
                }

                Match match = new Match
                {
                    Category = matchDTO.Category,
                    HomeTeamName = matchDTO.HomeTeamName,
                    AwayTeamName = matchDTO.AwayTeamName,
                    Date = matchDTO.Date,
                    Time = matchDTO.Time,
                    Stadium = matchDTO.Stadium,
                    MatchStatus = matchDTO.MatchStatus,
                    HomeTeamId = matchDTO.HomeTeamId,
                    AwayTeamId = matchDTO.AwayTeamId,
                };
                await _context.Matches.AddAsync(match);
                await _context.SaveChangesAsync();
                return Ok("Match added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding match: {ex.Message}");
            }
        }

        //جدول المباريات
        [HttpGet("Get-Matches/{category}")]
        public async Task<IActionResult> GetMatches(string category)
        {
            try
            {
                var today = DateTime.Now;

                var matches = await _context.Matches
                    .Where(x =>
                        x.Category == category &&
                        x.Date >= today &&
                        x.MatchStatus != "Completed")
                    .ToListAsync();
                var teamIds = matches
                    .SelectMany(m => new[] { m.HomeTeamId, m.AwayTeamId })
                    .Distinct()
                    .ToList();

                var logos = await _context.Academies
                    .Where(a => teamIds.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, a => a.LogoURL);

                var result = matches.Select(match => new
                {
                    match.HomeTeamName,
                    match.AwayTeamName,
                    match.Date,
                    match.Stadium,
                    match.MatchStatus,
                    match.HomeTeamScore,
                    match.AwayTeamScore,
                    HomeLogo = logos.TryGetValue(match.HomeTeamId, out var homeLogo) ? homeLogo : null,
                    AwayLogo = logos.TryGetValue(match.AwayTeamId, out var awayLogo) ? awayLogo : null
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Match-schedule-results/{category}")]
        public async Task<IActionResult> GetMatchesByCategory(string category)
        {
            try
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);

                // هات الماتشات الخاصة بالأكاديمية والكاتيجوري
                var matches = await _context.Matches
                    .Where(m => m.Category == category && 
                                (m.HomeTeamId == academyId || m.AwayTeamId == academyId))
                    .Select(m => new
                    {
                        m.MatchStatus,
                        m.Date,
                        m.Time,
                        m.Stadium,
                        m.HomeTeamName,
                        m.AwayTeamName,
                        m.HomeTeamScore,
                        m.AwayTeamScore,
                        m.HomeTeamId,
                        m.AwayTeamId
                    })
                    .ToListAsync();

                // هات IDs بتاعة الأكاديميات
                var academyIds = matches
                    .SelectMany(m => new[] { m.HomeTeamId, m.AwayTeamId })
                    .Distinct()
                    .ToList();

                // هات اللوجوهات
                var academyLogos = await _context.Academies
                    .Where(a => academyIds.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, a => a.LogoURL);

                // دمج اللوجوهات جوه كل ماتش
                var result = matches.Select(m => new
                {
                    m.MatchStatus,
                    m.Date,
                    m.Time,
                    m.Stadium,
                    m.HomeTeamName,
                    m.AwayTeamName,
                    m.HomeTeamScore,
                    m.AwayTeamScore,
                    HomeTeamLogo = academyLogos.ContainsKey(m.HomeTeamId) ? academyLogos[m.HomeTeamId] : null,
                    AwayTeamLogo = academyLogos.ContainsKey(m.AwayTeamId) ? academyLogos[m.AwayTeamId] : null
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        //[Authorize]
        //[HttpGet("Get-Match/{id}")]
        //public async Task<IActionResult> GetMatch(int id)
        //{
        //    try
        //    {
        //        var AcademyId = int.Parse(User.FindFirstValue("Id")!);
        //        var match = await _context.Matches.Where(x => x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();
        //        if (match == null)
        //        {
        //            return NotFound($"Match with ID {id} not found.");
        //        }
        //        return Ok(match);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error retrieving match: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("Update-Match/{id}")]
        //public async Task<IActionResult> UpdateMatch(int id, MatchDTO matchDTO)
        //{
        //    try
        //    {
        //        var AcademyId = int.Parse(User.FindFirstValue("Id")!);
        //        var match = await _context.Matches.Where(x => x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();
        //        if (match == null)
        //        {
        //            return NotFound($"Match with ID {id} not found.");
        //        }
        //        match.Category = matchDTO.Category;
        //        match.HomeTeam = matchDTO.HomeTeam;
        //        match.AwayTeam = matchDTO.AwayTeam;
        //        match.Date = matchDTO.Date;
        //        match.Time = matchDTO.Time;
        //        match.Stadium = matchDTO.Stadium;
        //        match.MatchStatus = matchDTO.MatchStatus;
        //        match.HomeTeamScore = matchDTO.HomeTeamScore!;
        //        match.AwayTeamScore = matchDTO.AwayTeamScore!;
        //        _context.Matches.Update(match);
        //        await _context.SaveChangesAsync();
        //        return Ok("Match updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error updating match: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpDelete("Delete-Match/{id}")]
        //public async Task<IActionResult> DeleteMatch(int id)
        //{
        //    try
        //    {
        //        var AcademyId = int.Parse(User.FindFirstValue("Id")!);
        //        var match = await _context.Matches.Where(x => x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();
        //        if (match == null)
        //        {
        //            return NotFound($"Match with ID {id} not found.");
        //        }
        //        _context.Matches.Remove(match);
        //        await _context.SaveChangesAsync();
        //        return Ok("Match deleted successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error deleting match: {ex.Message}");
        //    }
        //}

        [HttpGet("Count-Matches")]
        public async Task<IActionResult> CountMatches()
        {
            try
            {
                var matches = await _context.Matches.CountAsync();
                var categories = await _context.Matches.Select(m => m.Category).Distinct().CountAsync();
                var mathesToday = await _context.Matches.CountAsync(m => m.Date.HasValue && m.Date.Value.Date == DateTime.UtcNow.Date);
                var completedMatches = await _context.Matches.CountAsync(m => m.MatchStatus == "Completed");
                return Ok(new { TotalMatches = matches, Categories = categories, MatchesToday = mathesToday, CompletedMatches = completedMatches });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error counting matches: {ex.Message}");
            }
        }

        //[Authorize]
        //[HttpGet("Get-Matches-By-Academy")]
        //public async Task<IActionResult> GetMatchByAcademy()
        //{
        //    try
        //    {
        //        var AcademyId = int.Parse(User.FindFirstValue("Id")!);
        //        var matches = await _context.Matches.Where(x => x.AcademyId == AcademyId).ToListAsync();
        //        if (matches == null || !matches.Any())
        //        {
        //            return Ok(new string[] {});
        //        }
        //        return Ok(matches);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error retrieving matches: {ex.Message}");
        //    }
        //}


        //جدول ترتيب الفرق
        [HttpGet("Matches-Table/{category}")]
        public async Task<IActionResult> MatchesTable(string category)
        {
            try
            {
                List<Academy> academies = new List<Academy>();
                if (category == "U12")
                    academies = await _context.Academies
                        .Where(x => x.under12 == true && x.Role == "Academy")
                        .ToListAsync();
                else if (category == "U14")
                    academies = await _context.Academies
                        .Where(x => x.under14 == true && x.Role == "Academy")
                        .ToListAsync();
                else if (category == "U16")
                    academies = await _context.Academies
                        .Where(x => x.under16 == true && x.Role == "Academy")
                        .ToListAsync();

                var matches = await _context.Matches
                    .Where(x => x.MatchStatus == "Completed")
                    .ToListAsync();

                var stats = academies.Select(academy =>
                {
                    var academyId = academy.Id;

                    var allMatches = matches
                        .Where(m => m.HomeTeamId == academyId || m.AwayTeamId == academyId)
                        .ToList();

                    var wins = 0;
                    var draws = 0;
                    var losses = 0;
                    var goalsFor = 0;
                    var goalsAgainst = 0;

                    foreach (var match in allMatches)
                    {
                        int homeScore = int.TryParse(match.HomeTeamScore, out var hs) ? hs : 0;
                        int awayScore = int.TryParse(match.AwayTeamScore, out var ascore) ? ascore : 0;

                        bool isHome = match.HomeTeamId == academyId;

                        int scored = isHome ? homeScore : awayScore;
                        int conceded = isHome ? awayScore : homeScore;

                        goalsFor += scored;
                        goalsAgainst += conceded;

                        if (homeScore == awayScore)
                            draws++;
                        else if ((isHome && homeScore > awayScore) || (!isHome && awayScore > homeScore))
                            wins++;
                        else
                            losses++;
                    }

                    var totalMatches = allMatches.Count();
                    var goalDifference = goalsFor - goalsAgainst;
                    var points = (wins * 3) + draws;

                    return new
                    {
                        AcademyName = academy.AcademyName,
                        MatchesPlayed = totalMatches,
                        Wins = wins,
                        Draws = draws,
                        Losses = losses,
                        GoalsFor = goalsFor,
                        GoalsAgainst = goalsAgainst,
                        GoalDifference = goalDifference,
                        Points = points
                    };
                })
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference)
                .ToList();

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        //نتائج المباريات
        [HttpGet("Get-Score-Matches")]
        public async Task<IActionResult> GetScoreMatches()
        {
            try
            {
                var matches = await _context.Matches
                    .Where(x =>  x.MatchStatus == "Completed")
                    .ToListAsync();

                var teamIds = matches
                    .SelectMany(m => new[] { m.HomeTeamId, m.AwayTeamId })
                    .Distinct()
                    .ToList();

                var logos = await _context.Academies
                    .Where(a => teamIds.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, a => a.LogoURL);

                var result = matches.Select(match => new 
                {
                    Category = match.Category,
                    HomeTeamName = match.HomeTeamName,
                    AwayTeamName = match.AwayTeamName,
                    Date = match.Date,
                    Time = match.Time,
                    Stadium = match.Stadium,
                    MatchStatus = match.MatchStatus,
                    HomeTeamScore = match.HomeTeamScore,
                    AwayTeamScore = match.AwayTeamScore,
                    HomeLogo = logos.TryGetValue(match.HomeTeamId, out var homeLogo) ? homeLogo : null,
                    AwayLogo = logos.TryGetValue(match.AwayTeamId, out var awayLogo) ? awayLogo : null
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Add-Matches-Report")]
        public async Task<IActionResult> AddReport([FromBody] MatchReportDto dto)
        {
            try
            {
                var match = await _context.Matches.FindAsync(dto.MatchId);
                if (match == null)
                    return NotFound("Match not found");

                // Map Players
                var playerReports = dto.Players.Select(p => new PlayersReport
                {
                    PlayerName = p.PlayerName,
                    Position = p.Position,
                    Essential = p.Essential,
                    Reserve = p.Reserve,
                    Notes = p.Notes,
                    MatchId = dto.MatchId
                }).ToList();

                // Map Goals
                var goalsReports = dto.Goals.Select(g => new GoalsReports
                {
                    PlayerName = g.PlayerName,
                    AcadamyName = g.AcadamyName,
                    Minute = g.Minute,
                    Notes = g.Notes,
                    MatchId = dto.MatchId
                }).ToList();

                // Map Cards
                var cardsReports = dto.Cards.Select(c => new CardsReports
                {
                    PlayerName = c.PlayerName,
                    AcadamyName = c.AcadamyName,
                    CardType = c.CardType,
                    Notes = c.Notes,
                    MatchId = dto.MatchId
                }).ToList();

                // Map Staff
                var staffReports = dto.Staff.Select(s => new StaffReport
                {
                    TechName = s.TechName,
                    Role = s.Role,
                    Notes = s.Notes,
                    MatchId = dto.MatchId
                }).ToList();

                // Add to DB
                await _context.playersReports.AddRangeAsync(playerReports);
                await _context.goalsReports.AddRangeAsync(goalsReports);
                await _context.CardsReports.AddRangeAsync(cardsReports);
                await _context.StaffReport.AddRangeAsync(staffReports);

                await _context.SaveChangesAsync();

                return Ok("تم إضافة تقارير المباراة بنجاح");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Score-Matches-By-Academy")]
        public async Task<IActionResult> GetScoreMatchesByAcademy()
        {
            try
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);

                // IDs للمباريات اللي ليها تقارير
                var reportedMatchIds = new HashSet<int>();

                reportedMatchIds.UnionWith(await _context.CardsReports.Select(r => r.MatchId).ToListAsync());
                reportedMatchIds.UnionWith(await _context.goalsReports.Select(r => r.MatchId).ToListAsync());
                reportedMatchIds.UnionWith(await _context.playersReports.Select(r => r.MatchId).ToListAsync());
                reportedMatchIds.UnionWith(await _context.Tech_AdminReports.Select(r => r.MatchId).ToListAsync());

                // الماتشات المنتهية الخاصة بالأكاديمية واللي مافيهاش تقارير
                var matches = await _context.Matches
                    .Where(x =>
                        x.MatchStatus == "Completed" &&
                        (x.HomeTeamId == academyId || x.AwayTeamId == academyId) &&
                        !reportedMatchIds.Contains(x.Id))
                    .ToListAsync();

                var teamIds = matches
                    .SelectMany(m => new[] { m.HomeTeamId, m.AwayTeamId })
                    .Distinct()
                    .ToList();

                var logos = await _context.Academies
                    .Where(a => teamIds.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, a => a.LogoURL);

                var result = matches.Select(match => new
                {
                    Id = match.Id,
                    Category = match.Category,
                    HomeTeamName = match.HomeTeamName,
                    AwayTeamName = match.AwayTeamName,
                    Date = match.Date,
                    Time = match.Time,
                    Stadium = match.Stadium,
                    MatchStatus = match.MatchStatus,
                    HomeTeamScore = match.HomeTeamScore,
                    AwayTeamScore = match.AwayTeamScore,
                    HomeLogo = logos.TryGetValue(match.HomeTeamId, out var homeLogo) ? homeLogo : null,
                    AwayLogo = logos.TryGetValue(match.AwayTeamId, out var awayLogo) ? awayLogo : null
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Show-Reports")]
        public async Task<IActionResult> GetShowReports()
        {
            try
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);

                // IDs للمباريات اللي ليها تقارير
                var reportedMatchIds = new HashSet<int>();

                reportedMatchIds.UnionWith(await _context.CardsReports.Select(r => r.MatchId).ToListAsync());
                reportedMatchIds.UnionWith(await _context.goalsReports.Select(r => r.MatchId).ToListAsync());
                reportedMatchIds.UnionWith(await _context.playersReports.Select(r => r.MatchId).ToListAsync());
                reportedMatchIds.UnionWith(await _context.Tech_AdminReports.Select(r => r.MatchId).ToListAsync());

                // الماتشات المنتهية الخاصة بالأكاديمية واللي ليها تقارير
                var matches = await _context.Matches
                    .Where(x =>
                        x.MatchStatus == "Completed" &&
                        (x.HomeTeamId == academyId || x.AwayTeamId == academyId) &&
                        reportedMatchIds.Contains(x.Id)) // <-- التغيير هنا
                    .ToListAsync();

                var teamIds = matches
                    .SelectMany(m => new[] { m.HomeTeamId, m.AwayTeamId })
                    .Distinct()
                    .ToList();

                var logos = await _context.Academies
                    .Where(a => teamIds.Contains(a.Id))
                    .ToDictionaryAsync(a => a.Id, a => a.LogoURL);

                var result = matches.Select(match => new
                {
                    Id = match.Id,
                    Category = match.Category,
                    HomeTeamName = match.HomeTeamName,
                    AwayTeamName = match.AwayTeamName,
                    Date = match.Date,
                    Time = match.Time,
                    Stadium = match.Stadium,
                    MatchStatus = match.MatchStatus,
                    HomeTeamScore = match.HomeTeamScore,
                    AwayTeamScore = match.AwayTeamScore,
                    HomeLogo = logos.TryGetValue(match.HomeTeamId, out var homeLogo) ? homeLogo : null,
                    AwayLogo = logos.TryGetValue(match.AwayTeamId, out var awayLogo) ? awayLogo : null
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Details-Reports/{matchId}")]
        public async Task<IActionResult> GetDetailsReports(int matchId)
        {
            try
            {
                var cards = await _context.CardsReports
                    .Where(r => r.MatchId == matchId)
                    .ToListAsync();

                var goals = await _context.goalsReports
                    .Where(r => r.MatchId == matchId)
                    .ToListAsync();

                var players = await _context.playersReports
                    .Where(r => r.MatchId == matchId)
                    .ToListAsync();

                var technical = await _context.Tech_AdminReports
                    .Where(r => r.MatchId == matchId)
                    .ToListAsync();

                var result = new
                {
                    Cards = cards,
                    Goals = goals,
                    Players = players,
                    Technical = technical
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving match reports: {ex.Message}");
            }
        }


    }
}
