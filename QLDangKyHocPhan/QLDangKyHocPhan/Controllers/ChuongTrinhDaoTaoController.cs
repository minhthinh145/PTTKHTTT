using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuongTrinhDaoTaoController : ControllerBase
    {
        private readonly IChuongTrinhDaoTaoService _service;

        public ChuongTrinhDaoTaoController(IChuongTrinhDaoTaoService service)
        {
            _service = service;
        }

        [HttpPost("getctdt")]
        [Authorize]
        public async Task<IActionResult> GetChuongTrinhDaoTaoById(SinhVienDTO sv)
        {
            var result = await _service.GetChuongTrinhDaoTaoByIdAsync(sv.MaCT);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("getchitietctdt")]
        public async Task<IActionResult> GetChiTietByMaCT(SinhVienDTO sv)
        {
            var result = await _service.GetChiTietByIdAsync(sv.MaCT);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
