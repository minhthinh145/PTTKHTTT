﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDangKyHocPhan.Models;

public partial class Sinhvien
{
    public string MaSinhVien { get; set; }
    public string HoTen { get; set; }
    public DateTime NgaySinh { get; set; }
    public string GioiTinh { get; set; }
    public string Email { get; set; }
    public string MaKhoa { get; set; }
    public virtual Khoa MaKhoaNavigation { get; set; }
    public string TaiKhoanId { get; set; }
    public virtual Taikhoan TaiKhoan { get; set; }

    [ForeignKey("CTDaoTao")]
    public string MaCT { get; set; }
    public virtual CTDAOTAO CTDaoTao { get; set; }
}

