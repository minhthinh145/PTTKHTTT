using QLDangKyHocPhan.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourProjectNamespace.Models
{
    public class DangKy
    {
        [Key]
        public int MaDangKy { get; set; }

        [Required]
        [ForeignKey("SinhVien")]
        public string MaSinhVien { get; set; }

        [Required]
        [ForeignKey("LopHocPhan")]
        public string MaLopHP { get; set; }

        [Required]
        public DateTime NgayThucHien { get; set; }

        [Required]
        [MaxLength(20)]
        public string LoaiDangKy { get; set; }  // 'DangKy' hoặc 'HuyDangKy'

        // Navigation properties
        public Sinhvien SinhVien { get; set; }
        public Lophocphan LopHocPhan { get; set; }
    }
}
