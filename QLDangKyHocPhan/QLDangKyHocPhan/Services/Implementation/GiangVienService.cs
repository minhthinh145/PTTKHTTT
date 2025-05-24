using AutoMapper;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class GiangVienService : IGiangVienService
    {
        //inject context
        private readonly IGiangVienRepository _repo;
        private readonly IMapper _mapper;
        public GiangVienService(IGiangVienRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResult> GetGiangVienByIdAsync(string id)
        {
           var giangVien = await _repo.GetGiangVienByIdAsync(id);
            if (giangVien == null)
            {
                return ServiceResult.Failure("Giảng viên không tồn tại hoặc không có quyền truy cập thông tin này.");
            }
            return ServiceResult.Success("success",data:giangVien);

        }

        public async Task<ServiceResult> GetLopHocPhanByGiangVienIdAsync(string maGiangVien)
        {
            var lopHocPhans = await _repo.GetLopHocPhanByMaGiangVienAsync(maGiangVien);
            if (lopHocPhans == null || lopHocPhans.Count == 0)
            {
                return ServiceResult.Failure("Không tìm thấy lớp học phần cho giảng viên này.");
            }
            return ServiceResult.Success("Lấy danh sách lớp học phần thành công.", data: lopHocPhans);

        }
    }
}
