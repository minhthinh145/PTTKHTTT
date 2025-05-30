using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IGiangVienService
    {
        //get giang vien by id
        Task<ServiceResult> GetGiangVienByIdAsync(string id);
        Task<ServiceResult> GetLopHocPhanByGiangVienIdAsync(string maGiangVien);
        Task<ServiceResult> GetSinhVienByMaLopHocPhanAsync(string maLopHocPhan);

    }
}
