using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Services.Interface;
using System.Security.Claims;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyController : ControllerBase
    {
        private readonly IDangKyHocPhanService _service;

        public DangKyController(IDangKyHocPhanService service)
        {
            _service = service;
        }

        // POST: api/DangKyHocPhan/dangky
        [HttpPost("dangky")]
        public async Task<IActionResult> DangKyHocPhan([FromBody] RequestDangKyDTO request)
        {
            var mssv = GetUserIdByToken();
            if (string.IsNullOrEmpty(mssv))
                return Unauthorized("Không thể xác định sinh viên");

            var result = await _service.DangKyHocPhanAsync(mssv, request.MaLopHocPhan);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // POST: api/DangKyHocPhan/huy
        [HttpPost("huy")]
        public async Task<IActionResult> HuyDangKyHocPhan([FromBody] RequestDangKyDTO request)
        {
            var mssv = GetUserIdByToken();
            if (string.IsNullOrEmpty(mssv))
                return Unauthorized("Không thể xác định sinh viên");

            var result = await _service.HuyDangKyHocPhanAsync(mssv, request.MaLopHocPhan);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        private string GetUserIdByToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            return null;
        }
    }
}
