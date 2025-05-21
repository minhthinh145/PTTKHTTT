using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface ISinhVienRepository
    {
        Task<Sinhvien> GetSinhvienByIdAsync(string id);
    }
}
