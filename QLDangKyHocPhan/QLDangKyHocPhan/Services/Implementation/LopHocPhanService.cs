using AutoMapper;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class LopHocPhanService : ILopHocPhanService
    {
        private readonly ILopHocPhanRepository _repo;
        private readonly IMapper _mapper;

        public LopHocPhanService(ILopHocPhanRepository lopHocPhanRepository, IMapper mapper)
        {
            _repo = lopHocPhanRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult> GetAllLopHocPhanAsync()
        {
            var listLopHocPhan = await _repo.GetAllLopHocPhanAsync();
            if (listLopHocPhan == null || listLopHocPhan.Count == 0)
            {
                return ServiceResult.Failure("Không tìm thấy lớp học phần nào! Vui lòng làm mới lại trang");
            }
            var dto = _mapper.Map<List<LopHocPhanDTO>>(listLopHocPhan);
            return ServiceResult.Success("Đã lấy được danh sách lớp học phần", data: dto);

        }

        public async Task<ServiceResult> GetLopHocPhanByMaHocPhanAsync(string MaPH )
        {
           var lopHocPhan = await _repo.GetLopHocPhanByMaHP(MaPH);
            if (lopHocPhan == null || lopHocPhan.Count == 0)
            {
                return ServiceResult.Failure("Không tìm thấy lớp học phần nào! Vui lòng làm mới lại trang");
            }
            var dto = _mapper.Map<List<LopHocPhanDTO>>(lopHocPhan);
            return ServiceResult.Success("Đã lấy được danh sách lớp học phần", data: dto);
        }

        public async Task<ServiceResult> GetLopHocPhanChuaDangKyByMaHocPhanAsync(string MaPH, string mssv)
        {
            var lopHocPhan = await _repo.GetLopHocPhanChuaDangKyByMaHP(MaPH, mssv);
            if (lopHocPhan == null || lopHocPhan.Count == 0)
            {
                return ServiceResult.Failure("Không tìm thấy lớp học phần nào! Vui lòng làm mới lại trang");
            }
            var dto = _mapper.Map<List<LopHocPhanDTO>>(lopHocPhan);
            return ServiceResult.Success("Đã lấy được danh sách lớp học phần", data: dto);
        }
    }
}
