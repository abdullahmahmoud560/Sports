using MailKit.Security;
using MimeKit;

namespace Sports.DTO
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("خليجية كواترو", _configuration["EmailConfiguration:From"]));
                message.ReplyTo.Add(new MailboxAddress("دعم خليجية كواترو", "support@quattrogcc.ae"));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();

                string smtpServer = _configuration["EmailConfiguration:SmtpServer"]!;
                int smtpPort = int.TryParse(_configuration["EmailConfiguration:Port"], out int port) ? port : 587;
                string username = _configuration["EmailConfiguration:Username"]!;
                string password = _configuration["EmailConfiguration:Password"]!;
                bool useSsl = bool.TryParse(_configuration["EmailConfiguration:UseSSL"], out bool ssl) && ssl;

                await client.ConnectAsync(smtpServer, smtpPort, useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(username, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return "تم إرسال البريد الإلكتروني بنجاح";
            }
            catch (Exception ex)
            {
                return $"❌ خطأ أثناء إرسال البريد: {ex.Message}";
            }
        }
    }
}
