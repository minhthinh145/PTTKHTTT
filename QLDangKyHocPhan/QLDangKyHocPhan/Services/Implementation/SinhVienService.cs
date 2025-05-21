using AutoMapper;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class SinhVienService : ISinhVienService
    {
        private readonly ISinhVienRepository _repo;
        private readonly IMapper _mapper;

        public SinhVienService (ISinhVienRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ServiceResult> GetSinhVienByIdAsync(string id)
        {
            var sv = await _repo.GetSinhvienByIdAsync(id);
            if (sv == null)
            {
                return ServiceResult.Failure("Không tìm thấy sinh viên với Id: " + id);
            }

            var svDto = _mapper.Map<SinhVienDTO>(sv);
            return ServiceResult.Success(data: svDto);
        }

    }
}
