using AutoMapper;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class HocPhanService : IHocPhanService
    {
        private readonly IHocPhanRepository _repo;
        private readonly IMapper _mapper;

        public HocPhanService(IHocPhanRepository repo , IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResult> GetHocPhanChuaDangKyAsync(string maChuongTrinhDaoTao, string maSinhVien)
        {
            var hocphanModel =  await _repo.GetHocPhanChuaDangKyAsync(maChuongTrinhDaoTao, maSinhVien);
            if (hocphanModel == null || hocphanModel.Count == 0)
            {
                return ServiceResult.Failure("Không tìm thấy học phần nào! Vui lòng làm mới lại trang");
            }
            //use mapper
            var hocphanDTO = _mapper.Map<List<HocPhanDTO>>(hocphanModel);
            return ServiceResult.Success("Lấy danh sách học phần thành công",data:hocphanDTO);

        }
    }
}
