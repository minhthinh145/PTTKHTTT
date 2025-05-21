using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDangKyHocPhan.Models;

public partial class Hocphan
{
    public string MaHocPhan { get; set; } = null!;

    public string? TenHocPhan { get; set; }

    public int? SoTc { get; set; }

    public string? DKTienQuyet { get; set; }
    public string? MaKhoa { get; set; }
 
    public int? HocKy {  get; set; }
    public string LoaiHocPhan { get; set; } = "Bắt buộc";  
    [ForeignKey("MaKhoa")]
    public virtual Khoa? Khoa { get; set; }
    public virtual ICollection<Lophocphan> Lophocphans { get; set; } = new List<Lophocphan>();
    public virtual ICollection<CHITIET_CTDT> ChiTietCtdts { get; set; } = new List<CHITIET_CTDT>();
}
