using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.DTOs.AuthDTOs
{
    public class SignUpDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string TenDangNhap { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Required]
        public string PhoneNumber { get; set; }
         
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required] 
        public string LoaiTaiKhoan {  get; set; } = "SinhVien"; // Giá trị mặc định là "SinhVien"

    }
}
