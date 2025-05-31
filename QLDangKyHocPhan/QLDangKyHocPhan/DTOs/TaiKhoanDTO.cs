namespace QLDangKyHocPhan.DTOs
{
    public class TaiKhoanDTO
    {
        public string Id { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public SinhVienDTO? SinhVien { get; set; }
        public GiangVienDTO? GiangVien { get; set; }
        public PhongDaoTaoDTO? PhongDaoTao { get; set; }
    }
}