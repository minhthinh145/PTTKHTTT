
using DocumentFormat.OpenXml.Vml;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;



namespace QLDangKyHocPhan.Services.SendEmail
{

    public class SendEmailService : ISendEmail
    {
        private readonly IConfiguration _configuration;
        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string newPassword)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("QLĐăngKýHọcPhần", _configuration["EmailSettings:From"]));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Mật khẩu mới của bạn";

            message.Body = new TextPart("plain")
            {
                Text = $"Mật khẩu mới của bạn là: {newPassword}\nVui lòng đổi lại mật khẩu sau khi đăng nhập và không chia sẻ mật khẩu này với bất kỳ ai."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _configuration["EmailSettings:SmtpServer"],
                int.Parse(_configuration["EmailSettings:Port"]),
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]
            );
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
