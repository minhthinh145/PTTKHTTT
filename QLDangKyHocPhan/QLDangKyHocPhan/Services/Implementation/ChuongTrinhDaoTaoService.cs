using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.DTOs;
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

        public async Task<ServiceResult> GetChiTietByIdAsync(string MaCT)
        {
            try
            {
                var data = await _repo.GetChiTietById(MaCT);
                if (data == null || !data.Any())
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = "Không tìm thấy chi tiết CTĐT"
                    };
                }

                return new ServiceResult
                {
                    IsSuccess = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult        
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


    }
}
