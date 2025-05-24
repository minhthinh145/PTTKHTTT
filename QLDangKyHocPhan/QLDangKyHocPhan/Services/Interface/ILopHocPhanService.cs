using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface ILopHocPhanService
    {
        Task<ServiceResult> GetLopHocPhanByMaHocPhanAsync(string MaPH);
        Task<ServiceResult> GetAllLopHocPhanAsync();
        Task<ServiceResult> GetLopHocPhanChuaDangKyByMaHocPhanAsync(string MaPH, string mssv);
        
    }
}
