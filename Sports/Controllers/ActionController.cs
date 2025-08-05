using Microsoft.AspNetCore.Mvc;
using Sports.DTO;
using Sports.Model;

namespace Sports.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly DB _context;
        private readonly EmailService _emailService;
        public ActionController(DB context , EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet("Approve/{id}")]
        public async Task<IActionResult> ApproveAcademy(int id)
        {
            var academy = await _context.Academies.FindAsync(id);
            if (academy == null)
                return NotFound("الأكاديمية غير موجودة");
            if (academy.Statue)
                return BadRequest("الأكاديمية تم قبولها مسبقاً");
            academy.Statue = true;
            _context.Academies.Update(academy);
            await _context.SaveChangesAsync();
            var body = $@"
<!DOCTYPE html>
<html lang=""ar"" dir=""rtl"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>قبول طلب تسجيل الأكاديمية</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            padding: 20px;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: auto;
            background-color: #ffffff;
            border-radius: 10px;
            padding: 30px;
            border: 1px solid #ddd;
        }}
        h2 {{
            color: #4CAF50;
            font-size: 20px;
            text-align: center;
        }}
        p {{
            font-size: 14px;
            line-height: 1.8;
            text-align: center;
        }}
        a.button {{
            background-color: #4CAF50;
            color: white;
            padding: 12px 24px;
            text-decoration: none;
            font-size: 14px;
            border-radius: 6px;
            display: inline-block;
            margin-top: 20px;
        }}
        @media (max-width: 600px) {{
            .container {{
                padding: 15px;
            }}
            a.button {{
                width: 100%;
                display: block;
                padding: 10px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h2>🎉 تم قبول الأكاديمية</h2>

        <p><strong>السادة إدارة أكاديمية {academy.AcademyName} المحترمين،</strong></p>

        <p>نود إبلاغكم بأنه قد تم قبول طلب تسجيل الأكاديمية الخاصة بكم بنجاح في منصة <strong>خليجية كواترو</strong>.</p>

        <hr style=""margin: 30px 0;"">

        <p>يمكنكم الآن تسجيل الدخول إلى المنصة وإدارة الأكاديمية واللاعبين بكل سهولة.</p>

        <div style=""text-align: center;"">
            <a href=""https://academy.quattrogcc.ae/Login"" target=""_blank"" class=""button"">
                تسجيل الدخول
            </a>
        </div>

        <hr style=""margin: 30px 0;"">

        <p>
            إذا كان لديكم أي استفسار، لا تترددوا في التواصل مع فريق الدعم عبر<br>
            <a href=""mailto:support@quattrogcc.ae"">support@quattrogcc.ae</a>
        </p>

        <p>مع تحيات فريق <strong>خليجية كواترو</strong></p>
    </div>
</body>
</html>";

            var sendEmail = await _emailService.SendEmailAsync(academy.AcademyEmail, "تم قبول طلب تسجيل الأكاديمية", body);
            if(!sendEmail.Equals("تم إرسال البريد الإلكتروني بنجاح"))
            {
                return BadRequest("خطأ في إرسال البريد الإلكتروني");
            }
            return Ok("تم قبول الأكاديمية بنجاح");
        }

        [HttpGet("Reject/{id}")]
        public async Task<IActionResult> RejectAcademy(int id)
        {
            var academy = await _context.Academies.FindAsync(id);
            if (academy == null)
                return NotFound("الأكاديمية غير موجودة");

            if (academy.Statue)
                return BadRequest("الأكاديمية تم قبولها مسبقاً ولا يمكن رفضها");

            var Body = $@"
<!DOCTYPE html>
<html lang=""ar"" dir=""rtl"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>رفض طلب تسجيل الأكاديمية</title>
  <style>
    body {{
      font-family: 'Segoe UI', Tahoma, sans-serif;
      margin: 0;
      padding: 0;
      background-color: #f1f3f6;
      color: #333;
    }}
    .container {{
      max-width: 600px;
      margin: 30px auto;
      background: #ffffff;
      border-radius: 10px;
      padding: 30px;
      box-shadow: 0 4px 10px rgba(0,0,0,0.07);
      text-align: right;
    }}
    .header {{
      text-align: center;
      padding-bottom: 15px;
      border-bottom: 1px solid #eee;
    }}
    .header h2 {{
      color: #e74c3c;
      font-size: 20px;
      margin: 0;
    }}
    .content {{
      margin-top: 20px;
      font-size: 15px;
      line-height: 1.8;
    }}
    .content p {{
      margin: 10px 0;
    }}
    .contact {{
      background-color: #fdf2f2;
      border: 1px solid #f5c6cb;
      padding: 12px;
      border-radius: 6px;
      margin-top: 15px;
    }}
    .footer {{
      text-align: center;
      font-size: 13px;
      color: #888;
      margin-top: 30px;
    }}
    .footer a {{
      color: #3498db;
      text-decoration: none;
    }}
    @media (max-width: 600px) {{
      .container {{
        padding: 20px;
      }}
      .header h2 {{
        font-size: 18px;
      }}
      .content {{
        font-size: 14px;
      }}
    }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">
      <h2>❌ رفض طلب تسجيل الأكاديمية</h2>
    </div>

    <div class=""content"">
      <p>السادة إدارة أكاديمية <strong>{academy.AcademyName}</strong> المحترمين،</p>
      <p>نأسف لإبلاغكم بأنه لم يتم قبول طلب تسجيل الأكاديمية الخاصة بكم في منصة <strong>خليجية كواترو</strong>.</p>

      <div class=""contact"">
        <p>📞 للاستفسار، يرجى التواصل معنا عبر الرقم:</p>
        <p style=""font-weight: bold; font-size: 15px;"">00971551902200</p>
      </div>

      <p>نتمنى لكم التوفيق، ونرحب دائمًا بتواصلكم معنا.</p>
      <p>مع خالص التحية،<br><strong>فريق خليجية كواترو</strong></p>
    </div>

    <div class=""footer"">
      <p>الدعم: <a href=""mailto:support@quattrogcc.ae"">support@quattrogcc.ae</a></p>
      <p><a href=""https://academy.quattrogcc.ae/Login"" target=""_blank"">خليجية كواترو</a></p>
    </div>
  </div>
</body>
</html>
";

            await _emailService.SendEmailAsync(academy.AcademyEmail, "رفض طلب تسجيل الأكاديمية", Body);
            _context.Academies.Remove(academy);
            await _context.SaveChangesAsync();

            return Ok("تم رفض الطلب وحذف الأكاديمية من قاعدة البيانات");
        }

//        [HttpGet("approve-Player/{id}")]
//        public async Task<IActionResult> ApprovePlayer(int id)
//        {
//            var player = await _context.Players.FindAsync(id);
//            if (player == null)
//                return NotFound("اللاعب غير موجود");
//            if (player.Statu)
//                return Ok("تم قبول اللاعب مسبقا");
//            player.Statu = true;
//            await _context.SaveChangesAsync();

//            var acdemy = await _context.Academies.FindAsync(player.AcademyId);
//            var body = $@"
//<!DOCTYPE html>
//<!DOCTYPE html>
//<html lang=""ar"" dir=""rtl"">
//<head>
//    <meta charset=""UTF-8"">
//    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
//    <title>تم قبول اللاعب</title>
//</head>
//<body style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f6f8; color: #333; margin: 0; padding: 20px;"">
//    <div style=""max-width: 700px; margin: auto; background-color: #ffffff; border-radius: 10px; padding: 25px; box-shadow: 0 2px 8px rgba(0,0,0,0.05); direction: rtl;"">
//        <h2 style=""font-size: 18px; color: #27ae60; margin-bottom: 10px; text-align: right;"">✅ تم قبول اللاعب الجديد</h2>

//        <p style=""font-size: 14px; margin: 10px 0; text-align: right;"">
//            اسم الأكاديمية: <strong>{acdemy!.AcademyName}</strong>
//        </p>

//        <p style=""font-size: 14px; margin: 10px 0; text-align: right;"">
//            تمت الموافقة على تسجيل اللاعب <strong>{player.PLayerName}</strong> بنجاح ضمن الأكاديمية الخاصة بكم.
//        </p>
//        <p style=""font-size: 14px; margin: 10px 0; text-align: right;"">
//            يمكنكم الآن إدارة ملف اللاعب من خلال لوحة التحكم الخاصة بالأكاديمية.
//        </p>

//        <div style=""font-size: 13px; color: #777; margin-top: 30px; text-align: center;"">
//            <p>مع تحيات فريق <strong>خليجية كواترو</strong></p>
//            <p>
//                <a href=""https://khalej-kotro.vercel.app"" target=""_blank"" style=""color: #3498db;"">خليجية كواترو</a><br/>
//                الدعم: <a href=""mailto:support@quattrogcc.ae"" style=""color: #3498db;"">support@quattrogcc.ae</a>
//            </p>
//        </div>
//    </div>
//</body>
//</html>
//";
//            var sendEmail = await _emailService.SendEmailAsync(acdemy!.AcademyEmail, "تم قبول تسجيل اللاعب", body);
//            if (!sendEmail.Equals("تم إرسال البريد الإلكتروني بنجاح"))
//            {
//                return BadRequest("حدث خطأ أثناء إرسال البريد الإلكتروني");
//            }
//            return Ok("تم قبول اللاعب وتم إرسال إشعار للأكاديمية");
//        }


//        [HttpGet("reject-Player/{id}")]
//        public async Task<IActionResult> RejectPlayer(int id)
//        {
//            var player = await _context.Players.FindAsync(id);
//            if (player == null)
//                return NotFound("اللاعب غير موجود");

//            var acdemy = await _context.Academies.FindAsync(player.AcademyId);

//            var body = $@"
//<!DOCTYPE html>
//<html lang=""ar"" dir=""rtl"">
//<head>
//  <meta charset=""UTF-8"">
//  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
//  <title>رفض تسجيل اللاعب</title>
//  <style>
//    body {{
//      font-family: 'Segoe UI', Tahoma, sans-serif;
//      margin: 0;
//      padding: 0;
//      background-color: #f1f3f6;
//      color: #333;
//    }}
//    .container {{
//      max-width: 600px;
//      margin: 30px auto;
//      background: #ffffff;
//      border-radius: 10px;
//      padding: 30px;
//      box-shadow: 0 4px 10px rgba(0,0,0,0.07);
//      text-align: right;
//    }}
//    .header {{
//      text-align: center;
//      padding-bottom: 15px;
//      border-bottom: 1px solid #eee;
//    }}
//    .header h2 {{
//      color: #e74c3c;
//      font-size: 20px;
//      margin: 0;
//    }}
//    .content {{
//      margin-top: 20px;
//      font-size: 15px;
//      line-height: 1.8;
//    }}
//    .content p {{
//      margin: 10px 0;
//    }}
//    .contact {{
//      background-color: #fdf2f2;
//      border: 1px solid #f5c6cb;
//      padding: 12px;
//      border-radius: 6px;
//      margin-top: 15px;
//    }}
//    .footer {{
//      text-align: center;
//      font-size: 13px;
//      color: #888;
//      margin-top: 30px;
//    }}
//    .footer a {{
//      color: #3498db;
//      text-decoration: none;
//    }}
//    @media (max-width: 600px) {{
//      .container {{
//        padding: 20px;
//      }}
//      .header h2 {{
//        font-size: 18px;
//      }}
//      .content {{
//        font-size: 14px;
//      }}
//    }}
//  </style>
//</head>
//<body>
//  <div class=""container"">
//    <div class=""header"">
//      <h2>❌ تم رفض تسجيل اللاعب</h2>
//    </div>

//    <div class=""content"">
//      <p>نأسف لإبلاغكم بأنه تم <strong style=""color: red;"">رفض</strong> طلب تسجيل اللاعب <strong>{player.PLayerName}</strong> من أكاديمية <strong>{acdemy!.AcademyName}</strong>.</p>

//      <div class=""contact"">
//        <p>📞 للاستفسار، يرجى التواصل معنا عبر الرقم:</p>
//        <p style=""font-weight: bold; font-size: 15px;"">00971551902200</p>
//      </div>

//      <p>نتمنى لكم التوفيق، ونرحب دائمًا بتواصلكم معنا.</p>
//      <p>مع خالص التحية،<br><strong>فريق خليجية كواترو</strong></p>
//    </div>

//    <div class=""footer"">
//      <p>الدعم: <a href=""mailto:support@quattrogcc.ae"">support@quattrogcc.ae</a></p>
//      <p><a href=""https://khalej-kotro.vercel.app"" target=""_blank"">خليجية كواترو</a></p>
//    </div>
//  </div>
//</body>
//</html>
//";
//            await _emailService.SendEmailAsync(acdemy!.AcademyEmail, "رفض تسجيل لاعب جديد", body);

//            _context.Players.Remove(player);
//            await _context.SaveChangesAsync();

//            return Ok("تم رفض الطلب وإرسال إشعار للأكاديمية");
//        }
    }
}
