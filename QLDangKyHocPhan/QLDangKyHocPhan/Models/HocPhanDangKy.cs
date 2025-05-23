using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.Models
{
    public class HocPhanDangKy
    {
        [Key]
        public int MaHP_DangKy { get; set; }

        [Required]
        public string MaSinhVien { get; set; }

        [Required]
        public string MaLopHocPhan { get; set; }

        // Navigation
        public Sinhvien SinhVien { get; set; }
        public Lophocphan LopHocPhan { get; set; }
    }
}
