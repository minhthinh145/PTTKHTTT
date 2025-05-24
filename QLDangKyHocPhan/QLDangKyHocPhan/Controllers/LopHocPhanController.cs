using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Services.Interface;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocPhanController : ControllerBase
    {
        private readonly ILopHocPhanService _lopHocPhanService;
        public LopHocPhanController(ILopHocPhanService lopHocPhanService)
        {
            _lopHocPhanService = lopHocPhanService;
        }
        [HttpGet("GetLopHocPhanByMaHP/{maHP}")]
        public async Task<IActionResult> GetLopHocPhanByMaHP(string maHP)
        {
            var result = await _lopHocPhanService.GetLopHocPhanByMaHocPhanAsync(maHP);
            return Ok(result);
        }
        [HttpGet("GetAllLopHocPhan")]
        public async Task<IActionResult> GetAllLopHocPhan()
        {
            var result = await _lopHocPhanService.GetAllLopHocPhanAsync();
            return Ok(result);
        }

        [HttpPost("getLop")]
        public async Task<IActionResult> GetLopHocPhanByMaLop([FromBody]RequestGetLop dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Invalid token." });
            }
            var result = await _lopHocPhanService.GetLopHocPhanChuaDangKyByMaHocPhanAsync(dto.MaHocPhan , userId);
            return Ok(result);
        }

    }
}
