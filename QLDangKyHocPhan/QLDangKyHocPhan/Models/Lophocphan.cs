using System;
using System.Collections.Generic;

namespace QLDangKyHocPhan.Models;

public partial class Lophocphan
{
    public string MaLopHocPhan { get; set; } = null!;

    public string? PhongHoc { get; set; }

    public string? NgayHoc { get; set; }

    public int? SoLuong { get; set; }

    public string? MaHocPhan { get; set; }

    public string? MaGiangVien { get; set; }

    public virtual Giangvien? MaGiangVienNavigation { get; set; }

    public virtual Hocphan? MaHocPhanNavigation { get; set; }
}
