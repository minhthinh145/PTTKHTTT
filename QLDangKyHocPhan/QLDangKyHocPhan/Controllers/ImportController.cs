using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Services.Interface;

namespace QLDangKyHocPhan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IUserImportService _importService;
        private readonly ILogger<ImportController> _logger;

        public ImportController(IUserImportService importService, ILogger<ImportController> logger)
        {
            _logger = logger;
            _logger.LogInformation("Khởi tạo ImportController");
            _importService = importService;
        }

        [HttpPost("import-users")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportUsers(IFormFile file)
        {
            _logger.LogInformation("Nhận yêu cầu import-users");

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("File không hợp lệ hoặc không được cung cấp");
                return BadRequest("File không hợp lệ hoặc không được cung cấp");
            }

            try
            {
                _logger.LogInformation($"Xử lý file: {file.FileName}, kích thước: {file.Length} bytes");
                var results = await _importService.ImportUsersFromExcelAsync(file);

                var response = results.Select(r => new
                {
                    r.User.Username,
                    Success = r.Result.Succeeded,
                    Errors = r.Result.Errors
                });

                _logger.LogInformation("Import thành công");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý file");
                return StatusCode(500, $"Lỗi xử lý file: {ex.Message}");
            }
        }

        [HttpPost("test-upload")]
        [Consumes("multipart/form-data")]
        public IActionResult TestUpload(IFormFile file)
        {
            _logger.LogInformation("Nhận yêu cầu test-upload");

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("File không hợp lệ hoặc không được cung cấp");
                return BadRequest("File không hợp lệ hoặc không được cung cấp");
            }

            return Ok($"Nhận file: {file.FileName}, kích thước: {file.Length} bytes");
        }
    }
}