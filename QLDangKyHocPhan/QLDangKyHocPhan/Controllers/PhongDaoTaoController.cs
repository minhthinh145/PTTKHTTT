using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Services.Implementation;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongDaoTaoController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISinhVienService _sinhVienService;
        public PhongDaoTaoController(IAccountService accountService , ISinhVienService si)
        {
            _accountService = accountService;
            _sinhVienService = si;
        }

        [HttpGet("users-by-role")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string role)
        {
            var result = await _accountService.GetUsersByRoleAsync(role);
            return Ok(result);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            var result = await _sinhVienService.UpdatePasswordAsync(request.MaSinhVien, request.NewPassword);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
