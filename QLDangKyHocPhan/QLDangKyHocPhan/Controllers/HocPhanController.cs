using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhanController : ControllerBase
    {
        private readonly IHocPhanService _service;

        public HocPhanController(IHocPhanService service)
        {
            _service = service;
        }

        [HttpGet("hpdangky")]
        public async Task<IActionResult> GetHocPhanChuaDangKy([FromBody] SinhVienDTO dto)
        {
            var result = await _service.GetHocPhanChuaDangKyAsync(dto.MaCT, dto.MaSinhVien);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(result);
        }

    }
}
