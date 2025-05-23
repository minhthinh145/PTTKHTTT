using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.Services.Interface;
using System.Security.Claims;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuDangKyController : ControllerBase
    {
        private readonly ILichSuDangKyService _service;

        public LichSuDangKyController(ILichSuDangKyService service)
        {
            _service = service;
        }

        //get lich su dang ky
        [HttpGet("getlichsudangky")]
        public async Task<IActionResult> GetLichSuDangKy()
        {
            var userIdClaim = GetUserIdFromToken();
            var result = await _service.GetLichSuDangKyByMSSVAsync(userIdClaim);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }
         
        private string GetUserIdFromToken()
        {
            var result = User.FindFirst(ClaimTypes.NameIdentifier);
            if(result != null)
            {
                return result.Value;
            }
            return string.Empty;
        }
    }
}
