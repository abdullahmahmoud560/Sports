using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sports.DTO;
using Sports.Model;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DB db;

        public AdminController(DB db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpPost("AcceptOrReject-Players")]
        public async Task<IActionResult> AcceptOrRejectPlayers([FromBody] ActiveDTO active)
        {
            if (active == null || active.PlayerID <= 0)
            {
                return BadRequest("برجاء ادخال الحقول المطلوبة");
            }
            if(active.Statu == true)
            {
                var player = await db.Players.FindAsync(active.PlayerID);
                player!.Statu = true;
                db.Players.Update(player);
                await db.SaveChangesAsync();
                return Ok("تم قبول اللاعب بنجاح");
            }
           if(active.Statu == false && string.IsNullOrEmpty(active.Notes))
            {
                return BadRequest("برجاء ادخال ملاحظات لرفض اللاعب");
            }
            else
            {
                var player = await db.Players.FindAsync(active.PlayerID);
                player!.Statu = false;
                player.Location = "Academy";
                player.Notes = active.Notes;
                db.Players.Update(player);
                await db.SaveChangesAsync();
                return Ok("تم رفض اللاعب بنجاح");
            }
        }
    }
}
