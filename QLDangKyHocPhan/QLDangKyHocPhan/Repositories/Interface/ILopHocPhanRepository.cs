using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface ILopHocPhanRepository
    {
        Task<List<Lophocphan>> GetLopHocPhanByMaHP(string maHP);
        Task<List<Lophocphan>> GetLopHocPhanChuaDangKyAsync();
        Task<List<Lophocphan>> GetLopHocPhanChuaDangKyByMaHP(string maHP , string mssv);
        Task<bool> UpdateSiSoPlusOneAsync(string maLopHocPhan);
        Task<bool> UpdateSiSoMinusOneAsync(string maLopHocPhan);
    }
}
