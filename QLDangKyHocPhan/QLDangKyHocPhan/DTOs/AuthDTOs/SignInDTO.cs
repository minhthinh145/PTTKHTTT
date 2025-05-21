using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.DTOs.AuthDTOs
{
    public class SignInDTO
    {
        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
