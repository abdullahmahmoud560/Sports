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
    public class RoleController : ControllerBase
    {
        private readonly DB _context;

        public RoleController(DB context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("Add-Role")]
        public IActionResult AddRole(RoleDTO roleDTO)
        {
            try
            {
                if (roleDTO == null)
                {
                    return BadRequest("Role data is required.");
                }
                Role role = new Role
                {
                    RoleName = roleDTO.RoleName,
                    RoleDescription = roleDTO.RoleDescription,
                    AcademyId = int.Parse(User.FindFirstValue("Id")!)
                };
                _context.Roles.Add(role);
                _context.SaveChanges();
                return Ok("Role added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding role: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var roles = await _context.Roles.Where(x => x.AcademyId == AcademyId).ToListAsync();
                if (roles == null || !roles.Any())
                {
                    return Ok(new string[] {});
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving roles: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-Role/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            try
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);
                var role = await _context.Roles.Where(x=>x.AcademyId == academyId && x.Id == id).FirstOrDefaultAsync();
                if (role == null)
                {
                    return NotFound("Role not found.");
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving role: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("Delete-Role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);
                var role = await _context.Roles.Where(x => x.AcademyId == academyId && x.Id == id).FirstOrDefaultAsync();
                if (role == null)
                {
                    return NotFound("Role not found.");
                }
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return Ok("Role deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting role: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("Update-Role/{id}")]
        public async Task<IActionResult> UpdateRole(int id , RoleDTO roleDTO) 
        {
            try
            {
                if (roleDTO == null)
                {
                    return BadRequest("Invalid Role data.");
                }
                var academyId = int.Parse(User.FindFirstValue("Id")!);
                var role = await _context.Roles.Where(x => x.AcademyId == academyId && x.Id == id).FirstOrDefaultAsync();
                if (role == null)
                {
                    return NotFound("Role not found.");
                }
                role.RoleName = roleDTO.RoleName;
                role.RoleDescription = roleDTO.RoleDescription;
                await _context.SaveChangesAsync();
                return Ok("Role updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating role: {ex.Message}");
            }
        }
    }
}
