using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;
using QLDangKyHocPhan.Services.SendEmail;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class SinhVienService : ISinhVienService
    {
        private readonly ISinhVienRepository _repo;
        private readonly IMapper _mapper;
        private readonly ISendEmail _sendEmail;
        private readonly UserManager<Taikhoan> _userManager;

        public SinhVienService (ISinhVienRepository repo, IMapper mapper,ISendEmail email , UserManager<Taikhoan> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;

            _sendEmail = email; 
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

        public async Task<ServiceResult> AddSinhVienAsync(SinhVienDTO sinhVienDto)
        {
            try
            {
                var sinhvien = _mapper.Map<Sinhvien>(sinhVienDto);
                await _repo.AddSinhVienAsync(sinhvien);
                return ServiceResult.Success("Thêm sinh viên thành công", sinhVienDto);
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Thêm sinh viên thất bại: " + ex.Message);
            }
        }

        public async Task<ServiceResult> UpdatePasswordAsync(string maSinhVien, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(maSinhVien);
            if (user == null)
                return ServiceResult.Failure("Không tìm thấy tài khoản.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                return ServiceResult.Failure("Đổi mật khẩu thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            // Gửi email thông báo mật khẩu mới
            if (!string.IsNullOrEmpty(user.Email))
            {
                await _sendEmail.SendEmailAsync(user.Email, newPassword);
            }

            return ServiceResult.Success("Đổi mật khẩu thành công.");
        }
    }
}
