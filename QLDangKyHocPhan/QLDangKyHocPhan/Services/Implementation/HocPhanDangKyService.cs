using AutoMapper;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class HocPhanDangKyService : IHocPhanDangKyService
    {
        private readonly IHocPhanDangKyRepository _repo;
        private readonly IMapper _mapper;

        public HocPhanDangKyService(IHocPhanDangKyRepository repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ServiceResult> GetLopHocPhangDaDangKyByMSSV(string mssv)
        {
           var modal = await _repo.GetLopHocPhangDaDangKyByMSSV(mssv);
            if (modal == null || !modal.Any())
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Sinh viên chưa đăng ký lớp học phần nào.",
                    Data = null
                };
            }
            var lopHocPhanDTOs = _mapper.Map<List<LopHocPhanDTO>>(modal);

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Lấy danh sách lớp học phần thành công.",
                Data = lopHocPhanDTOs
            };
        }
    }
}
