using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Taikhoan> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountService(UserManager<Taikhoan> userManager , IMapper mapper , IAuthService authService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;
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

        public async Task<TokenResponseDTO?> SignInAsync(SignInDTO signin, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
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
