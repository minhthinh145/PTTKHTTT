using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Models;
using AutoMapper;
using QLDangKyHocPhan.DTOs;

namespace QLDangKyHocPhan.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Taikhoan, UserProfileDTO>().ReverseMap();
            CreateMap<Sinhvien, SinhVienDTO>()
                        .ForMember(dest => dest.TenKhoa, opt => opt.MapFrom(src => src.MaKhoaNavigation != null ? src.MaKhoaNavigation.TenKhoa : null))
                        .ForMember(dest => dest.TenCTDT, opt => opt.MapFrom(src => src.CTDaoTao != null ? src.CTDaoTao.TenCTDT : null));
        }
    }
}
