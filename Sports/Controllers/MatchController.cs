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
    public class MatchController : ControllerBase
    {
        private readonly DB _context;

        public MatchController(DB context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("Add-Match")]
        public async Task<IActionResult> AddMatch(MatchDTO matchDTO)
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
                    HomeTeam = matchDTO.HomeTeam,
                    AwayTeam = matchDTO.AwayTeam,
                    Date = matchDTO.Date,
                    Time = matchDTO.Time,
                    Stadium = matchDTO.Stadium,
                    MatchStatus = matchDTO.MatchStatus,
                    AcademyId = int.Parse(User.FindFirstValue("Id")!)
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

        [HttpGet("Get-Matches")]
        public async Task<IActionResult> GetMatches()
        {
            try
            {
                var matches = await _context.Matches.ToListAsync();
                if (matches == null || !matches.Any())
                {
                    return Ok(new string[] {});
                }
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Match/{id}")]
        public async Task<IActionResult> GetMatch(int id)
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var match = await _context.Matches.Where(x => x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();
                if (match == null)
                {
                    return NotFound($"Match with ID {id} not found.");
                }
                return Ok(match);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving match: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Update-Match/{id}")]
        public async Task<IActionResult> UpdateMatch(int id, MatchDTO matchDTO)
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var match = await _context.Matches.Where(x => x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();
                if (match == null)
                {
                    return NotFound($"Match with ID {id} not found.");
                }
                match.Category = matchDTO.Category;
                match.HomeTeam = matchDTO.HomeTeam;
                match.AwayTeam = matchDTO.AwayTeam;
                match.Date = matchDTO.Date;
                match.Time = matchDTO.Time;
                match.Stadium = matchDTO.Stadium;
                match.MatchStatus = matchDTO.MatchStatus;
                match.HomeTeamScore = matchDTO.HomeTeamScore!;
                match.AwayTeamScore = matchDTO.AwayTeamScore!;
                _context.Matches.Update(match);
                await _context.SaveChangesAsync();
                return Ok("Match updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating match: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("Delete-Match/{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var match = await _context.Matches.Where(x => x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();
                if (match == null)
                {
                    return NotFound($"Match with ID {id} not found.");
                }
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
                return Ok("Match deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting match: {ex.Message}");
            }
        }

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

        [Authorize]
        [HttpGet("Get-Matches-By-Academy")]
        public async Task<IActionResult> GetMatchByAcademy()
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var matches = await _context.Matches.Where(x => x.AcademyId == AcademyId).ToListAsync();
                if (matches == null || !matches.Any())
                {
                    return Ok(new string[] {});
                }
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }

        [HttpGet("Matches-Table")]
        public async Task<IActionResult> MatchesTable()
        {
            try
            {
                var academies = await _context.Academies.ToListAsync();
                var matches = await _context.Matches.ToListAsync();

                var stats = academies.Select(academy =>
                {
                    var academyId = academy.Id;

                    var homeMatches = matches.Where(m => m.AcademyId == academyId);
                    var totalMatches = homeMatches.Count();
                    var wins = homeMatches.Count(m => int.Parse(m.HomeTeamScore) > int.Parse(m.AwayTeamScore));
                    var losses = homeMatches.Count(m => int.Parse(m.HomeTeamScore) < int.Parse(m.AwayTeamScore));
                    var draws = homeMatches.Count(m => int.Parse(m.HomeTeamScore) == int.Parse(m.AwayTeamScore));
                    var goalsFor = homeMatches.Sum(m => int.Parse(m.HomeTeamScore));
                    var goalsAgainst = homeMatches.Sum(m => int.Parse(m.AwayTeamScore));
                    var difference = goalsFor - goalsAgainst;

                    return new
                    {
                        AcademyName = academy.AcademyName,
                        MatchesPlayed = totalMatches,
                        Wins = wins,
                        Losses = losses,
                        Draws = draws,
                        GoalsFor = goalsFor,
                        GoalsAgainst = goalsAgainst,
                        GoalDifference = difference,
                        Points = (wins * 3) + (draws * 1)
                    };
                })
               .OrderByDescending(a => a.Points) // ⬅️ ترتيب تنازلي
                .ThenByDescending(a => a.GoalDifference) // ⬅️ لو فيه تساوي بالنقاط، يرتّب حسب فارق الأهداف
               .ToList();

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving matches: {ex.Message}");
            }
        }
    }
}
