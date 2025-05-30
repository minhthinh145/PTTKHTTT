﻿using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IHocPhanRepository
    {
        Task<List<Hocphan>> GetHocPhanChuaDangKyAsync(string maChuongTrinhDaoTao, string maSinhVien);
        Task<Hocphan> GetHocPhanByMaHocPhanAsync(string maHP);
        Task<List<Hocphan>> GetAllHocPhanAsync();
    }
}
