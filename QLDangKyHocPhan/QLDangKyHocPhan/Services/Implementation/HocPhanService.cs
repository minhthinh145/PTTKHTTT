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

        public async Task<ServiceResult> GetAllHocPhan()
        {
            var listHP = await _repo.GetAllHocPhanAsync();
            if(listHP == null || listHP.Count == 0)
            {
                return ServiceResult.Failure("Không tìm thấy học phần nào! Vui lòng làm mới lại trang");
            }
            var listdto = _mapper.Map<List<HocPhanDTO>>(listHP);
            return ServiceResult.Success("Lấy danh sách học phần thành công", data: listdto);
        }

        public async Task<ServiceResult> GetHocPhanByMaHocPhanAsync(string maHP)
        {
            var hocphan = await _repo.GetHocPhanByMaHocPhanAsync(maHP);
            if(hocphan == null)
            {
                return ServiceResult.Failure($"Không tìm thấy học phần với mã học phần :{maHP}");
            }
            var dto = _mapper.Map<HocPhanDTO>(hocphan);
            return ServiceResult.Success("Đã tìm thấy học phần", data: dto);
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
