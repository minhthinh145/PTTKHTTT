using AutoMapper;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class ChuongTrinhDaoTaoService : IChuongTrinhDaoTaoService
    {
        private readonly IChuongTrinhDaoTaoRepository _repo;
        private readonly IMapper _mapper;

        public ChuongTrinhDaoTaoService(IChuongTrinhDaoTaoRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ServiceResult> GetChuongTrinhDaoTaoByIdAsync(string id)
        {
            var result = await _repo.GetChuongTrinhDaoTaoById(id);
            if (!result.IsSuccess) 
            {
                return ServiceResult.Failure(result.Message);
            }
            return ServiceResult.Success(result.Message, result.Data);
        }
    }
}
