﻿namespace QLDangKyHocPhan.DTOs
{
    public class MemberDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public string LoaiTaiKhoan { get; set; }
    }
}