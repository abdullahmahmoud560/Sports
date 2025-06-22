using System.Globalization;
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
    public class CampController : ControllerBase
    {
        public DB _context;

        public CampController(DB context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("Add-Camps")]
        public async Task<IActionResult> addCamps(CampDTO campDTO)
        {
            try 
            {
                var academyId = int.Parse(User.FindFirstValue("Id")!);
                if (campDTO == null)
                {
                    return BadRequest("جميع الحقول مطلوبة");
                }
                Camp camp = new Camp
                {
                    task = campDTO.task,
                    Date = campDTO.Date,
                    Time = campDTO.Time,
                    AcademyId = academyId,
                };
                _context.camps.Add(camp);
                await _context.SaveChangesAsync();
                return Ok("تم إضافة المعسكر بنجاح");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding camp: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("Get-All-Camps")]
        public async Task<IActionResult> GetAllCamps()
        {
            try
            {
                var AcademyId = int.Parse(User.FindFirstValue("Id")!);
                var camps = await _context.camps.Where(x=>x.AcademyId == AcademyId).ToListAsync();
                List<GetCampDTO> getCampDTOs = new List<GetCampDTO>();
                var culture = new CultureInfo("ar-EG");

                if (camps.Any())
                {
                    foreach (var camp in camps)
                    {
                        // تأكد أن camp.Date و camp.Time مش null
                        var formattedDate = camp.Date?.ToString("dddd d MMMM", culture);
                        var formattedDateString = camp.Date?.ToString("yyyy-MM-dd");

                        getCampDTOs.Add(new GetCampDTO
                        {
                            Id = camp.Id,
                            label = formattedDate ?? "",
                            date = formattedDateString ?? "",
                            activity = camp.task ?? "",
                            time = camp.Time ?? ""
                        });
                    }

                    return Ok(getCampDTOs);
                }

                return Ok(new string[] {});
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("updateCamp/{id}")]
        public async Task<IActionResult> UpdateCamp(int id, [FromBody] CampDTO campDTO)
        {
            var AcademyId = int.Parse(User.FindFirstValue("Id")!);
            var camp = await _context.camps.Where(x=>x.AcademyId == AcademyId && x.Id == id).FirstOrDefaultAsync();

            if (camp == null)
            {
                return NotFound("المعسكر غير موجود");
            }

            camp.task = campDTO.task;
            camp.Date = campDTO.Date;
            camp.Time = campDTO.Time;

            await _context.SaveChangesAsync();

            return Ok("تم تحديث المعسكر بنجاح");
        }

        [Authorize]
        [HttpDelete("deleteCamp/{id}")]
        public async Task<IActionResult> DeleteCamp(int id)
        {
            var academyId = int.Parse(User.FindFirstValue("Id")!);
            var camp = await _context.camps.Where(x=>x.AcademyId == academyId && x.Id == id).FirstOrDefaultAsync();

            if (camp == null)
            {
                return NotFound("المعسكر غير موجود");
            }

            _context.camps.Remove(camp);
            await _context.SaveChangesAsync();

            return Ok("تم حذف المعسكر بنجاح");
        }

    }
}
