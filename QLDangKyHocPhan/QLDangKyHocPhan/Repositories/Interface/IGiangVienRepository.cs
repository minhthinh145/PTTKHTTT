using QLDangKyHocPhan.Models;
using System.Threading.Tasks;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IGiangVienRepository
    {
        Task<Giangvien?> GetGiangVienByIdAsync(string id);
        Task<List<Lophocphan>> GetLopHocPhanByMaGiangVienAsync(string maGiangVien);
    }
}
