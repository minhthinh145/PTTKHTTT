﻿namespace QLDangKyHocPhan.DTOs
{
    public class LopHocPhanDTO
    {
        public string MaLopHocPhan { get; set; } = null!;
        public string? PhongHoc { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? SoLuong { get; set; }
        public string? MaHocPhan { get; set; }
        public int SoLuongDangKy { get; set; }


        // Thêm thuộc tính học phần
        public string? TenHocPhan { get; set; }
        public int? SoTinChi { get; set; }
        public string? LoaiHocPhan { get; set; }

        public string? MaGiangVien { get; set; }
        public string? TenGiangVien { get; set; }
    }

}
