using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Models;
using AutoMapper;

namespace QLDangKyHocPhan.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Taikhoan, UserProfileDTO>().ReverseMap();

        }
    }
}
