using QLDangKyHocPhan.Models;

namespace QLDangKyHocPhan.Repositories.Interface
{
    public interface IRefreshTokenRepository
    {
        Task SaveAsync(string userId, string token, DateTime created, DateTime expires);
        Task<RefreshToken> FindByTokenAsync(string token);
        Task RevokeAsync(string token);
    }
}
