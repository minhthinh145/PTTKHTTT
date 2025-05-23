using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IChuongTrinhDaoTaoService
    {
        Task<ServiceResult> GetChuongTrinhDaoTaoByIdAsync(string id);
        Task<ServiceResult> GetChiTietByIdAsync(string id);
    }
}
