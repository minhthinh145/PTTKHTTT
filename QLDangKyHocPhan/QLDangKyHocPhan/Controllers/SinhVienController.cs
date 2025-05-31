using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class SinhVienController : ControllerBase
{
    private readonly ISinhVienService _sinhVienService;

    public SinhVienController(ISinhVienService sinhVienService)
    {
        _sinhVienService = sinhVienService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _sinhVienService.GetSinhVienByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Data);
        return NotFound(result.Message);
    }
}