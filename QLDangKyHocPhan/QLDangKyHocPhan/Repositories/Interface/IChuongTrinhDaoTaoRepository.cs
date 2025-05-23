using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IChuongTrinhDaoTaoRepository
    {
        Task<ServiceResult> GetChuongTrinhDaoTaoById(string MaCT);
        Task<List<ChiTietCTDTDTO>> GetChiTietById(string MaCT);
    }
}
