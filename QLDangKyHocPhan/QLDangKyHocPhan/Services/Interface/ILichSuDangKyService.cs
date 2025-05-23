using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface ILichSuDangKyService
    {
        Task<ServiceResult> GetLichSuDangKyByMSSVAsync(string mssv);
    }
}
