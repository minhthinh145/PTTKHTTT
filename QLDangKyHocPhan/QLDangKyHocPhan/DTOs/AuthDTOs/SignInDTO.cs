using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.DTOs.AuthDTOs
{
    public class SignInDTO
    {
        [Required, EmailAddress]
        public string StudentId { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
