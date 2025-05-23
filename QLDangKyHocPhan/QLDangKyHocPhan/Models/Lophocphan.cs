using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDangKyHocPhan.Models;

public partial class Lophocphan
{
    [Column(TypeName = "varchar(50)")]
    public string MaLopHocPhan { get; set; } = null!;

    public string? PhongHoc { get; set; }

    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }
    public int? SoLuong { get; set; }

    public int SoLuongDangKy { get; set; } = 0;

    public string? MaHocPhan { get; set; }

    public string? MaGiangVien { get; set; }

    public virtual Giangvien? MaGiangVienNavigation { get; set; }

    public virtual Hocphan Hocphan { get; set; } = null!; // Thêm mối quan hệ với Hocphan
}
