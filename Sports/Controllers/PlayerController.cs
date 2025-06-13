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

        public PlayerController(DB context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("Add-Players")]
        public async Task<IActionResult> AddPlayers(PlayerDTO playerDTO)
        {
            try
            {
                if (playerDTO == null)
                {
                    return BadRequest("Invalid player data.");
                }
                Player player = new Player
                {
                    PLayerName = playerDTO.PLayerName,
                    AcademyName = playerDTO.AcademyName,
                    Category = playerDTO.Category,
                    BirthDate = playerDTO.BirthDate,
                    Possition = playerDTO.Possition,
                    NumberShirt = playerDTO.NumberShirt,
                    Goals = playerDTO.Goals,
                    YellowCards = playerDTO.YellowCards,
                    RedCards = playerDTO.RedCards,
                    Nationality = playerDTO.Nationality,
                    AcademyId = int.Parse(User.FindFirstValue("Id")!)
                };
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();
                return Ok("Players added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding players: {ex.Message}");
            }
        }

        [HttpGet("Get-All-Players")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var players = await _context.Players.Where(x => x.AcademyId == AcademyId).ToListAsync();
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

        [HttpGet("Get-Player-By-Id/{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);
                if (player == null)
                {
                    return NotFound("Player not found.");
                }
                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving player: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Update-Player/{id}")]
        public async Task<IActionResult> UpdatePlayerById(int id, PlayerDTO playerDTO)
        {
            try
            {
                if (playerDTO == null)
                {
                    return BadRequest("Invalid academy data.");
                }
                var player = await _context.Players.FindAsync(id);

                if (player == null)
                {
                    return NotFound("player not found.");
                }

                player.BirthDate = playerDTO.BirthDate;
                player.Category = playerDTO.Category;
                player.RedCards = playerDTO.RedCards;
                player.PLayerName = playerDTO.PLayerName;
                player.Possition = playerDTO.Possition;
                player.AcademyName = playerDTO.AcademyName;
                player.NumberShirt = playerDTO.NumberShirt;
                player.Goals = playerDTO.Goals;
                player.YellowCards = playerDTO.YellowCards;

                _context.Players.Update(player);
                await _context.SaveChangesAsync();
                return Ok("Academy updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating player: {ex.Message}");
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

        [Authorize]
        [HttpGet("Get-Players-By-Academy")]
        public async Task<IActionResult> GetPlayersByAcademy()
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var players = await _context.Players.Where(x => x.AcademyId == AcademyId).ToListAsync();
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
    }
}
