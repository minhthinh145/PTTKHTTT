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
            CreateMap<Lophocphan, LopHocPhanDTO>()
     .ForMember(dest => dest.TenGiangVien, opt => opt.MapFrom(src => src.MaGiangVienNavigation.HoTen))
     .ForMember(dest => dest.TenHocPhan, opt => opt.MapFrom(src => src.Hocphan.TenHocPhan))
     .ForMember(dest => dest.SoTinChi, opt => opt.MapFrom(src => src.Hocphan.SoTc))
     .ForMember(dest => dest.LoaiHocPhan, opt => opt.MapFrom(src => src.Hocphan.LoaiHocPhan));


            CreateMap<DangKy, DangKyDTO>()
                .ForMember(dest => dest.MaHocPhan, opt => opt.MapFrom(src => src.LopHocPhan.MaHocPhan))
                .ForMember(dest => dest.TenHocPhan, opt => opt.MapFrom(src => src.LopHocPhan.Hocphan.TenHocPhan))
                .ReverseMap();
        }
    }
}
