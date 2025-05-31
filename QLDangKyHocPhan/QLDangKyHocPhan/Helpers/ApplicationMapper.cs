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
                        .ForMember(dest => dest.TenCTDT, opt => opt.MapFrom(src => src.CTDaoTao != null ? src.CTDaoTao.TenCTDT : null))
                        .ForMember(dest => dest.MaKhoa, opt => opt.MapFrom(src => src.MaKhoaNavigation.MaKhoa))
                        .ForMember(dest => dest.MaCT, opt => opt.MapFrom(src => src.CTDaoTao.MaCT))
                        .ForMember(dest => dest.MaSinhVien, opt => opt.MapFrom(src => src.TaiKhoanId))
                        .ReverseMap();
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
            CreateMap<Giangvien, GiangVienDTO>().ReverseMap();
            CreateMap<Taikhoan, PhongDaoTaoDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<Taikhoan, TaiKhoanDTO>()
    .ForMember(dest => dest.SinhVien, opt => opt.MapFrom(src => src.Sinhvien))
    .ForMember(dest => dest.GiangVien, opt => opt.MapFrom(src => src.Giangvien));
            CreateMap<Taikhoan, Sinhvien>()
    .ForMember(dest => dest.MaSinhVien, opt => opt.MapFrom(src => src.UserName)); 

            CreateMap<ImportSinhVienDTO, Sinhvien>()
                .ForMember(dest => dest.MaSinhVien, opt => opt.MapFrom(src => src.MaSinhVien))
                .ForMember(dest => dest.MaKhoa, opt => opt.MapFrom(src => src.MaKhoa))

                .ForMember(dest => dest.MaCT, opt => opt.MapFrom(src => src.MaCT));
        }
    }
}
