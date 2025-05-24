using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IHocPhanService
    {
        Task<ServiceResult> GetHocPhanChuaDangKyAsync(string maChuongTrinhDaoTao, string maSinhVien);
        Task<ServiceResult> GetHocPhanByMaHocPhanAsync(string maHP);
        Task<ServiceResult> GetAllHocPhan();
    }
}
