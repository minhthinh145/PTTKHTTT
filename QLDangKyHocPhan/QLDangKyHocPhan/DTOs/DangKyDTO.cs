using QLDangKyHocPhan.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.DTOs
{
    public class DangKyDTO
    {

        public string MaHocPhan { get; set; }
        public string MaLopHP { get; set; }
        public string TenHocPhan { get; set; }
        public DateTime NgayThucHien { get; set; }

        public string LoaiDangKy { get; set; }  // 'DangKy' hoặc 'HuyDangKy'

    }
}
