using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Taikhoan> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ISinhVienService _sinhVien;
        private readonly IGiangVienService _giangVien;

        public AccountService(UserManager<Taikhoan> userManager, IMapper mapper, IAuthService authService, ISinhVienService sinhVien, IGiangVienService giangvien)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;
            _sinhVien = sinhVien;
            _giangVien = giangvien;
        }

        public async Task<ServiceResult> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return ServiceResult.Failure("Không tìm thấy người dùng");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (isPasswordValid)
            {
                return ServiceResult.Success("Mật khẩu hợp lệ");
            }
            else
            {
                return ServiceResult.Failure("Mật khẩu không hợp lệ");
            }
        }

        public async Task<UserProfileDTO> FindUserById(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if(user == null)
            {
                return null!;
            }
            return _mapper.Map<UserProfileDTO>(user);
        }

        public async Task<ServiceResult> GetProfileByUserAccount(UserProfileDTO userProfile)
        {
           if(userProfile.LoaiTaiKhoan == "SinhVien")
            {
                var result = await _sinhVien.GetSinhVienByIdAsync(userProfile.TenDangNhap);
                return result;
            }
           if(userProfile.LoaiTaiKhoan == "GiangVien")
            {
               var result = await _giangVien.GetGiangVienByIdAsync(userProfile.TenDangNhap);
                if (result != null)
                {
                    return result;
                }

            }
            return ServiceResult.Failure("Không tìm thấy thông tin người dùng");
        }

        public async Task<TokenResponseDTO?> SignInAsync(SignInDTO signin)
        {
            var user = await _userManager.FindByIdAsync(signin.TenDangNhap);
            if (user == null)
            {
                return null;
            }

            var result = await _authService.GenerateTokensAsync(user);
            if (!result.IsSuccess || result.Data is not TokenResponseDTO tokenData)
            {
                return null;
            }

            return tokenData;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpDTO signup)
        {
           var user = new Taikhoan
           {
               Id = signup.TenDangNhap,
               TenDangNhap = signup.TenDangNhap,
               UserName = signup.Username,
               Email = signup.Email,
               PhoneNumber = signup.PhoneNumber,
               DateOfBirth = signup.DateOfBirth,
               LoaiTaiKhoan = signup.LoaiTaiKhoan ?? "SinhVien",
           };
            var result = await _userManager.CreateAsync(user, signup.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, signup.LoaiTaiKhoan);
            }
            return result;
        }

        public async Task<UserProfileDTO> UpdateUserById(string userid, UserProfileDTO user)
        {
            var userApp = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userid);

            if (userApp == null)
            {
                return null!;
            }
            _mapper.Map(user, userApp);

            var result = await _userManager.UpdateAsync(userApp);

            return user;
        }
    }
}
