using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IHocPhanService
    {
        Task<ServiceResult> GetHocPhanChuaDangKyAsync(string maChuongTrinhDaoTao, string maSinhVien);
    }
}
