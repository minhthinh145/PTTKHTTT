namespace QLDangKyHocPhan.Services.SendEmail
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string toEmail, string newPassword);

    }
}
