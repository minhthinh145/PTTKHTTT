using Microsoft.AspNetCore.Identity;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Taikhoan> _userManager;

        public AccountRepository(UserManager<Taikhoan> userManager , RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task AddToRoleAsync(Taikhoan user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> CheckPasswordAsync(Taikhoan user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);

        }

        public async Task CreateRoleAsync(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> CreateUserAsync(Taikhoan user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<Taikhoan?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Taikhoan?> FindByUserIDAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async  Task<IList<string>> GetUserRolesAsync(Taikhoan user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
