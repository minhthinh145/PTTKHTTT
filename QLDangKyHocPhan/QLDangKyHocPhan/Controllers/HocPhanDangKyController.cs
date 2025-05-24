using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.Services.Interface;
using System.Security.Claims;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhanDangKyController : ControllerBase
    {
        private readonly IHocPhanDangKyService _service;

        public HocPhanDangKyController(IHocPhanDangKyService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetHocPhanDaDangKy()
        {
            var userID = GetMaSoSinhVienByToken();
            var result = await _service.GetLopHocPhangDaDangKyByMSSV(userID);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        private string GetMaSoSinhVienByToken()
        {
            var token = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không hợp lệ hoặc không có quyền truy cập.");
            }
            return token;
        }
    }
}
