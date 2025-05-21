using System;
using System.Collections.Generic;

namespace QLDangKyHocPhan.Models;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string? TenKhoa { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Sinhvien> Sinhviens { get; set; } = new List<Sinhvien>();
    public virtual ICollection<CTDAOTAO> CTDAOTAOs { get; set; } = new List<CTDAOTAO>();
    public virtual ICollection<Hocphan> Hocphans { get; set; } = new List<Hocphan>(); // Thêm mối quan hệ với Hocphan

}
