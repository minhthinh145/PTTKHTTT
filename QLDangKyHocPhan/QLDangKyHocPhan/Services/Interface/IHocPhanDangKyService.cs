using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IHocPhanDangKyService
    {
        Task<ServiceResult> GetLopHocPhangDaDangKyByMSSV(string mssv);
    }
}
