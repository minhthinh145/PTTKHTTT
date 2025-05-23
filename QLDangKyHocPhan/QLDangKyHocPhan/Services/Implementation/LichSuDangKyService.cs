using AutoMapper;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class LichSuDangKyService : ILichSuDangKyService
    {
        private readonly ILichSuDangKyRepository _repo;
        private readonly IMapper _mapper;

        public LichSuDangKyService(ILichSuDangKyRepository repo ,  IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ServiceResult> GetLichSuDangKyByMSSVAsync(string mssv)
        {
            var listDanhSach = await _repo.GetLichSuDangKyByMssv(mssv);
            if (listDanhSach == null) 
            {
                return ServiceResult.Failure("Chưa đến thời điểm đăng ký học phần");
            }
            var dto = _mapper.Map<List<DangKyDTO>>(listDanhSach);
            return ServiceResult.Success("Đã lấy danh sách môn học thành công", data: dto);
        }
    }
}
