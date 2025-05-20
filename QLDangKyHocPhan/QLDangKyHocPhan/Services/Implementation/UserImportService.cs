using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class UserImportService : IUserImportService
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<UserImportService> _logger;

        public UserImportService(IAccountService accountService, ILogger<UserImportService> logger)
        {
            _accountService = accountService;
            _logger = logger;
            _logger.LogInformation("Khởi tạo UserImportService");
        }

        public async Task<List<(SignUpDTO User, IdentityResult Result)>> ImportUsersFromExcelAsync(IFormFile file)
        {
            _logger.LogInformation("Bắt đầu xử lý file Excel");

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("File không hợp lệ hoặc không được cung cấp");
                throw new ArgumentException("File không hợp lệ hoặc không được cung cấp");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Chỉ hỗ trợ file .xlsx");
                throw new ArgumentException("Chỉ hỗ trợ file .xlsx");
            }

            var results = new List<(SignUpDTO, IdentityResult)>();
            var users = new List<SignUpDTO>();

            try
            {
                using var stream = file.OpenReadStream();
                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1); // Lấy worksheet đầu tiên

                if (worksheet == null)
                {
                    _logger.LogError("Không tìm thấy sheet trong file Excel");
                    throw new Exception("Không tìm thấy sheet trong file Excel");
                }

                var rowCount = worksheet.RowsUsed().Count();

                for (int row = 2; row <= rowCount; row++) // Bỏ dòng tiêu đề
                {
                    try
                    {
                        var dto = new SignUpDTO
                        {
                            Username = worksheet.Cell(row, 1).GetString()?.Trim(),
                            TenDangNhap = worksheet.Cell(row, 2).GetString()?.Trim(),
                            Email = worksheet.Cell(row, 3).GetString()?.Trim(),
                            PhoneNumber = worksheet.Cell(row, 4).GetString()?.Trim(),
                            DateOfBirth = DateTime.TryParse(worksheet.Cell(row, 5).GetString(), out var dob) ? dob : null,
                            Password = worksheet.Cell(row, 6).GetString()?.Trim(),
                            LoaiTaiKhoan = worksheet.Cell(row, 7).GetString()?.Trim() ?? "SinhVien"
                        };

                        if (!string.IsNullOrEmpty(dto.Username) && !string.IsNullOrEmpty(dto.Password))
                        {
                            users.Add(dto);
                            _logger.LogInformation($"Thêm user vào danh sách: {dto.Username}");
                        }
                        else
                        {
                            _logger.LogWarning($"Bỏ qua dòng {row}: Username hoặc Password trống");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Lỗi khi xử lý dòng {row}, bỏ qua");
                        continue;
                    }
                }

                foreach (var user in users)
                {
                    try
                    {
                        var result = await _accountService.SignUpAsync(user);
                        results.Add((user, result));
                        _logger.LogInformation($"Đăng ký user {user.Username}: {(result.Succeeded ? "Thành công" : "Thất bại")}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Lỗi khi đăng ký user {user.Username}");
                        results.Add((user, IdentityResult.Failed(new IdentityError { Description = $"Lỗi khi đăng ký: {ex.Message}" })));
                    }
                }

                _logger.LogInformation($"Hoàn thành xử lý file Excel, tổng cộng {results.Count} user");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đọc file Excel");
                throw new Exception("Lỗi khi đọc file Excel", ex);
            }
        }
    }
}