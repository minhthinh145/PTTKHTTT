using QLDangKyHocPhan.Models;
using YourProjectNamespace.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface ILichSuDangKyRepository
    {
        Task<List<DangKy>> GetLichSuDangKyByMssv(string mssv);
        Task<bool> AddLichSuDangKyAsync(DangKy dangKy);
    }
}
