namespace QLDangKyHocPhan.DTOs
{
    public class HocPhanDTO
    {
        public string MaHocPhan { get; set; } = null!;

        public string? TenHocPhan { get; set; }

        public int? SoTc { get; set; }

        public int? HocKy { get; set; }
        public string LoaiHocPhan { get; set; } = "Bắt buộc";
    }
}
