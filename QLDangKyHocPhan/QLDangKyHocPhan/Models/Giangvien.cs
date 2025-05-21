using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDangKyHocPhan.Models;

public partial class Giangvien
{
    public string MaGiangVien { get; set; }
    public string HoTen { get; set; }
    public string DiaChi { get; set; }
    public string LopHoc { get; set; }
    public string Email { get; set; }
    public string TaiKhoanId { get; set; }

    [ForeignKey("TaiKhoanId")]
    public virtual Taikhoan TaiKhoan { get; set; }
    public virtual ICollection<Lophocphan> Lophocphans { get; set; }
}
