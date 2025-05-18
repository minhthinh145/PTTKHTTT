using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public RefreshTokenRepository(QlDangKyHocPhanContext context) 
        {
            _context = context;
        }

        public async Task<RefreshToken> FindByTokenAsync(string token)
        {

            return await _context.RefreshTokens
                 .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task RevokeAsync(string token)
        {
            var refreshToken = await FindByTokenAsync(token);
            if (refreshToken != null)
            {
                refreshToken.Revoked = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(string userId, string token, DateTime created, DateTime expires)
        {
            var refreshToken = new RefreshToken
            {
                Token = token,
                Created = created,
                Expires = expires,
                UserId = userId
            };
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
