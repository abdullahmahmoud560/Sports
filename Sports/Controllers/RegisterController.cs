using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports.DTO;
using Sports.Model;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DB _context;

        public RegisterController(DB context)
        {
            _context = context;
        }

        [HttpPost("Register-Academy")]
        public async Task<IActionResult> RegisterAcademy(RegisterDTO registerDTO)
        {
            try
            {
                if (registerDTO == null)
                {
                    return BadRequest("Invalid academy data.");
                }
                var isFound = await _context.Academies.AnyAsync(u => u.AcademyEmail == registerDTO.AcademyEmail);
                if (isFound)
                {
                    return BadRequest("Academy with this email already exists.");
                }
                Academy academy = new Academy
                {
                    AcademyName = registerDTO.AcademyName,
                    AcademyEmail = registerDTO.AcademyEmail,
                    AcademyPhone = registerDTO.AcademyPhone,
                    AcademyCity = registerDTO.AcademyCity,
                    AcademyCountry = registerDTO.AcademyCountry,
                    AcademyPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.AcademyPassword),
                    Coordinator = registerDTO.Coordinator
                };
                await _context.Academies.AddAsync(academy);
                await _context.SaveChangesAsync();
                return Ok("Academy registered successfully.");
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
                var academies = await _context.Academies.ToListAsync();
                if (academies.Count == 0)
                {
                    return NotFound("No academies found.");
                }
                return Ok(academies);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving academies: {ex.Message}");
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
        public async Task<IActionResult> UpdateAcademy(int id, RegisterDTO registerDTO)
        {
            try
            {
                if (registerDTO == null)
                {
                    return BadRequest("Invalid academy data.");
                }
                var academy = await _context.Academies.FindAsync(id);
                if (academy == null)
                {
                    return NotFound("Academy not found.");
                }
                academy.AcademyName = registerDTO.AcademyName;
                academy.AcademyEmail = registerDTO.AcademyEmail;
                academy.AcademyPhone = registerDTO.AcademyPhone;
                academy.AcademyCity = registerDTO.AcademyCity;
                academy.AcademyCountry = registerDTO.AcademyCountry;
                academy.Coordinator = registerDTO.Coordinator;
                _context.Academies.Update(academy);
                _context.SaveChanges();
                return Ok("Academy updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating academy: {ex.Message}");
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
    }
}
