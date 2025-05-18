using System;
using System.Collections.Generic;

namespace QLDangKyHocPhan.Models;

public partial class Hocphan
{
    public string MaHocPhan { get; set; } = null!;

    public string? TenHocPhan { get; set; }

    public int? SoTc { get; set; }

    public string? DkthucHanh { get; set; }

    public virtual ICollection<Lophocphan> Lophocphans { get; set; } = new List<Lophocphan>();
}
