using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Services.Interface;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLDangKyHocPhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private readonly IGiangVienService _giangVienService;

        public GiangVienController(IGiangVienService giangVienService)
        {
            _giangVienService = giangVienService;
        }

        // GET: api/GiangVien/lophocphan/{maGiangVien}
        [HttpGet("lophocphan")]
        public async Task<IActionResult> GetLopHocPhanByGiangVien()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var list = await _giangVienService.GetLopHocPhanByGiangVienIdAsync(userid);
           
            return Ok(list);
        }
        [HttpPost("sinhvien-by-lophocphan")]
        public async Task<IActionResult> GetSinhVienByMaLopHocPhan([FromBody] RequestDangKyDTO req)
        {
            var result = await _giangVienService.GetSinhVienByMaLopHocPhanAsync(req.MaLopHocPhan);
            return Ok(result);
        }

    }
}
