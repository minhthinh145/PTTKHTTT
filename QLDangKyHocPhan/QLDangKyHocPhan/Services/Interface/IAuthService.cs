using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IAuthService
    {
        Task<ServiceResult> GenerateAccessTokenAsync(string userId);
        Task<ServiceResult> GenerateAccessTokenForUserAsync(Taikhoan user);
        Task<ServiceResult> GenerateTokensAsync(Taikhoan user);
        Task<ServiceResult> RefreshAccessTokenAsync(string refreshToken);
        Task<ServiceResult> SaveRefreshTokenAsync(string userId, string refreshToken, DateTime expires);
        Task<ServiceResult> RevokeRefreshTokenAsync(string refreshToken);
    }
}
