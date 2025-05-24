using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IHocPhanDangKyRepository
    {
        Task<bool> AddHocPhanDangKyAsync(HocPhanDangKy hocPhanDangKy);
        Task<bool> DeleteHocPhanDangKyAsync(HocPhanDangKy hocPhanDangKy);
        Task<List<Lophocphan>> GetLopHocPhangDaDangKyByMSSV(string mssv); 
    }
}
