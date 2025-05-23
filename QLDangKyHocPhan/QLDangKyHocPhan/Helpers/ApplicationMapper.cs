using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Models;
using AutoMapper;
using QLDangKyHocPhan.DTOs;
using YourProjectNamespace.Models;

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
            CreateMap<Hocphan,HocPhanDTO>().ReverseMap();
            CreateMap<CHITIET_CTDT,DanhSachMonDTO>().ReverseMap();
            CreateMap<Lophocphan, LopHocPhanDTO>().ReverseMap();
            CreateMap<DangKy, DangKyDTO>().ReverseMap();
        }
    }
}
