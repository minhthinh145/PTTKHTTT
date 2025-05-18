using System;
using System.Collections.Generic;

namespace QLDangKyHocPhan.Models;

public partial class Giangvien
{
    public string MaGiangVien { get; set; }
    public string HoTen { get; set; }
    public string DiaChi { get; set; }
    public string LopHoc { get; set; }
    public string Email { get; set; }
    public string TaiKhoanDangKy { get; set; }
    public virtual Taikhoan TaiKhoanDangKyNavigation { get; set; }
    public virtual ICollection<Lophocphan> Lophocphans { get; set; }
}
