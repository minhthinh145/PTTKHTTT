using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IDangKyHocPhanRepository
    {
        Task<bool> DangKyHocPhanAsync(string mssv, string maLopHocPhan);
        Task<bool> HuyDangKyHocPhanAsync(string mssv, string maLopHocPhan);
        Task<bool> ChuyenLopDangKyAsync(string mssv, string maLopHocPhan, string maLopHocPhanMoi);

    }
}
