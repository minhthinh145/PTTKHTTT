using System.Threading.Tasks;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface ISinhVienService
    {
        Task<ServiceResult> GetSinhVienByIdAsync(string id);
        Task<ServiceResult> AddSinhVienAsync(SinhVienDTO sinhVienDto);
        Task<ServiceResult> UpdatePasswordAsync(string maSinhVien, string newPassword);
    }
}
