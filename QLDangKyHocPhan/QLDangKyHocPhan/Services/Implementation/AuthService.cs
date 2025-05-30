using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using QLDangKyHocPhan.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Taikhoan> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public AuthService(UserManager<Taikhoan> userManager, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ServiceResult> GenerateAccessTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ServiceResult.Failure("User not found.");

            return await GenerateAccessTokenForUserAsync(user);
        }

        public async Task<ServiceResult> GenerateAccessTokenForUserAsync(Taikhoan user)
        {
            if (user == null)
                return ServiceResult.Failure("User is null.");

            try
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authKey = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddMinutes(55),
                    Issuer = _configuration["JWT:ValidIssuer"],
                    Audience = _configuration["JWT:ValidAudience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authKey), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = _tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = _tokenHandler.WriteToken(token);

                return ServiceResult.Success("Access token generated.", accessToken);
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Failed to generate access token: " + ex.Message);
            }
        }

        public async Task<ServiceResult> GenerateTokensAsync(Taikhoan user)
        {
            var accessTokenResult = await GenerateAccessTokenForUserAsync(user);
            if (!accessTokenResult.IsSuccess)
                return accessTokenResult;

            var accessToken = accessTokenResult.Data?.ToString();
            var refreshToken = Guid.NewGuid().ToString();
            var expires = DateTime.UtcNow.AddDays(7);

            await SaveRefreshTokenAsync(user.Id, refreshToken, expires);

            var tokenData = new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                LoaiTaiKhoan = user.LoaiTaiKhoan
            };

            return ServiceResult.Success("Tokens generated successfully.", tokenData);
        }

        public async Task<ServiceResult> RefreshAccessTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.FindByTokenAsync(refreshToken);
            if (storedToken == null || !storedToken.IsActive)
            {
                return ServiceResult.Failure("Invalid or expired refresh token.");
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                return ServiceResult.Failure("User not found.");
            }

            return await GenerateAccessTokenForUserAsync(user);
        }

        public async Task<ServiceResult> RevokeRefreshTokenAsync(string refreshToken)
        {
            try
            {
                await _refreshTokenRepository.RevokeAsync(refreshToken);
                return ServiceResult.Success("Refresh token revoked.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Failed to revoke token: " + ex.Message);
            }
        }

        public async Task<ServiceResult> SaveRefreshTokenAsync(string userId, string refreshToken, DateTime expires)
        {
            try
            {
                await _refreshTokenRepository.SaveAsync(userId, refreshToken, DateTime.UtcNow, expires);
                return ServiceResult.Success("Refresh token saved.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure("Failed to save refresh token: " + ex.Message);
            }
        }
    }
}
