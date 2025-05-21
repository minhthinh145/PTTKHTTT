using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IChuongTrinhDaoTaoRepository
    {
        Task<ServiceResult> GetChuongTrinhDaoTaoById(string MaCT);
    }
}
