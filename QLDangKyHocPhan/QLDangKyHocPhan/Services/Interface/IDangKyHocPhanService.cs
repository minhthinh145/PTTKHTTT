using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IDangKyHocPhanService
    {
        Task<ServiceResult> DangKyHocPhanAsync(string mssv, string maLopHocPhan);
        Task<ServiceResult> HuyDangKyHocPhanAsync(string mssv, string maLopHocPhan);
    }
}
