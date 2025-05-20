using Microsoft.AspNetCore.Identity;
using QLDangKyHocPhan.DTOs.AuthDTOs;

namespace QLDangKyHocPhan.Services.Interface
{
    public interface IUserImportService
    {
        Task<List<(SignUpDTO User, IdentityResult Result)>> ImportUsersFromExcelAsync(IFormFile file);
    }
}
