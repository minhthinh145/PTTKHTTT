using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class DangKyHocPhanService : IDangKyHocPhanService
    {
        private readonly IDangKyHocPhanRepository _dangKyRepo;

        public DangKyHocPhanService(IDangKyHocPhanRepository dangKyRepo)
        {
            _dangKyRepo = dangKyRepo;
        }

        public async Task<ServiceResult> ChuyenLopDangKyAsync(string mssv, string maLopHocPhan, string maLopHocPhanMoi)
        {
            var success = await _dangKyRepo.ChuyenLopDangKyAsync(mssv, maLopHocPhan, maLopHocPhanMoi);
            if (!success)
                return ServiceResult.Failure("Chuyển lớp đăng ký thất bại!");
            return ServiceResult.Success("Chuyển lớp đăng ký thành công!");

        }

        public async Task<ServiceResult> DangKyHocPhanAsync(string mssv, string maLopHocPhan)
        {
            var success = await _dangKyRepo.DangKyHocPhanAsync(mssv, maLopHocPhan);
            if (!success)
                return ServiceResult.Failure("Đăng ký học phần thất bại!");

            return ServiceResult.Success("Đăng ký học phần thành công!");
        }

        public async Task<ServiceResult> HuyDangKyHocPhanAsync(string mssv, string maLopHocPhan)
        {
            var success = await _dangKyRepo.HuyDangKyHocPhanAsync(mssv, maLopHocPhan);
            if (!success)
                return ServiceResult.Failure("Hủy đăng ký học phần thất bại!");

            return ServiceResult.Success("Hủy đăng ký học phần thành công!");
        }
    }
}
