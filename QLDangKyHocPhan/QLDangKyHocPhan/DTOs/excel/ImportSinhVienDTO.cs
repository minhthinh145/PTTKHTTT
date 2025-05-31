namespace QLDangKyHocPhan.DTOs
{
    public class ImportSinhVienDTO
    {
        // Thông tin tài khoản & sinh viên
        public string Username { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? NgaySinh { get; set; } // Dùng chung cho cả tài khoản và sinh viên
        public string Password { get; set; }
        public string LoaiTaiKhoan { get; set; } = "SinhVien";

        public string MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string MaKhoa { get; set; }
        public string MaCT { get; set; }
    }
}