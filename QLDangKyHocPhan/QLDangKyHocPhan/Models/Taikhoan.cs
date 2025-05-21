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

    public virtual Sinhvien Sinhvien { get; set; }
    public virtual Giangvien Giangvien { get; set; }
}
