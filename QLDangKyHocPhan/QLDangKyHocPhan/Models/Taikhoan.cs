using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;

namespace QLDangKyHocPhan.Models;

public partial class Taikhoan : IdentityUser
{
    public string TenDangNhap { get; set; }
    public string LoaiTaiKhoan { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public virtual ICollection<Sinhvien> Sinhviens { get; set; }
    public virtual ICollection<Giangvien> Giangviens { get; set; }
}
