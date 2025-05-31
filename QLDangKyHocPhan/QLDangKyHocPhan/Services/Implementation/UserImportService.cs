using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.DTOs.AuthDTOs;
using QLDangKyHocPhan.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLDangKyHocPhan.Services.Implementation
{
    public class UserImportService : IUserImportService
    {
        private readonly IAccountService _accountService;
        private readonly ISinhVienService _sinhVienService;
        private readonly ILogger<UserImportService> _logger;

        public UserImportService(IAccountService accountService, ISinhVienService sinhVienService, ILogger<UserImportService> logger)
        {
            _accountService = accountService;
            _sinhVienService = sinhVienService;
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
            var users = new List<ImportSinhVienDTO>();

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
                        string username = TryGetString(worksheet.Cell(row, 1));
                        string tenDangNhap = TryGetString(worksheet.Cell(row, 2));
                        string email = TryGetString(worksheet.Cell(row, 3));
                        string phone = TryGetString(worksheet.Cell(row, 4));
                        DateTime? ngaySinh = TryGetDate(worksheet.Cell(row, 5), row, _logger);
                        string password = TryGetString(worksheet.Cell(row, 6));
                        string loaiTaiKhoan = TryGetString(worksheet.Cell(row, 7)) ?? "SinhVien";
                        string maSinhVien = TryGetString(worksheet.Cell(row, 8));
                        string hoTen = TryGetString(worksheet.Cell(row, 9));
                        string gioiTinh = TryGetString(worksheet.Cell(row, 10));
                        string maKhoa = TryGetString(worksheet.Cell(row, 11));
                        string maCT = TryGetString(worksheet.Cell(row, 12));

                        if (!IsValidEmail(email))
                        {
                            _logger.LogWarning($"Dòng {row}: Email không hợp lệ [{email}], sẽ để trống.");
                            email = string.Empty;
                        }

                        phone = NormalizePhone(phone);

                        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                        {
                            _logger.LogWarning($"Bỏ qua dòng {row}: Username hoặc Password trống");
                            continue;
                        }

                        if (ngaySinh == null)
                        {
                            _logger.LogWarning($"Bỏ qua dòng {row}: Ngày sinh trống hoặc không hợp lệ");
                            continue;
                        }

                        var dto = new ImportSinhVienDTO
                        {
                            Username = username,
                            TenDangNhap = tenDangNhap,
                            Email = email,
                            PhoneNumber = phone,
                            NgaySinh = ngaySinh,
                            Password = password,
                            LoaiTaiKhoan = loaiTaiKhoan,
                            MaSinhVien = maSinhVien,
                            HoTen = hoTen,
                            GioiTinh = gioiTinh,
                            MaKhoa = maKhoa,
                            MaCT = maCT
                        };

                        users.Add(dto);
                        _logger.LogInformation($"Thêm user vào danh sách: {dto.Username}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Lỗi khi xử lý dòng {row}, bỏ qua");
                        continue;
                    }
                }

                foreach (var dto in users)
                {
                    try
                    {
                        var signUpDto = new SignUpDTO
                        {
                            Username = dto.Username,
                            TenDangNhap = dto.TenDangNhap,
                            Email = dto.Email,
                            PhoneNumber = dto.PhoneNumber,
                            DateOfBirth = dto.NgaySinh,
                            Password = dto.Password,
                            LoaiTaiKhoan = dto.LoaiTaiKhoan
                        };
                        var result = await _accountService.SignUpAsync(signUpDto);

                        if (result.Succeeded)
                        {
                            var sinhVienDto = new SinhVienDTO
                            {
                                MaSinhVien = dto.MaSinhVien,
                                HoTen = dto.HoTen,
                                NgaySinh = dto.NgaySinh ?? DateTime.MinValue,
                                GioiTinh = dto.GioiTinh,
                                Email = dto.Email,
                                MaKhoa = dto.MaKhoa,
                                MaCT = dto.MaCT
                            };
                            var resultSinhVien = await _sinhVienService.AddSinhVienAsync(sinhVienDto);
                            _logger.LogInformation($"Kết quả thêm sinh viên: {resultSinhVien.IsSuccess} - {resultSinhVien.Message}");
                        }
                        results.Add((signUpDto, result));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Lỗi khi import dòng: {dto.Username}");
                        results.Add((new SignUpDTO { Username = dto.Username }, IdentityResult.Failed(new IdentityError { Description = ex.Message })));
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

        // Helper: Lấy string, ép kiểu về chuỗi, bỏ ký tự trắng
        private static string TryGetString(IXLCell cell)
        {
            if (cell == null || cell.IsEmpty())
                return string.Empty;
            try
            {
                switch (cell.DataType)
                {
                    case XLDataType.Text:
                        return cell.GetString()?.Trim() ?? string.Empty;
                    case XLDataType.Number:
                        try
                        {
                            return cell.GetDouble().ToString(CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    case XLDataType.Boolean:
                        return cell.GetBoolean() ? "true" : "false";
                    case XLDataType.DateTime:
                        try
                        {
                            return cell.GetDateTime().ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    default:
                        try
                        {
                            return cell.Value.ToString().Trim();
                        }
                        catch
                        {
                            return string.Empty;
                        }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        // Helper: Lấy ngày tháng, ép kiểu an toàn
        private static DateTime? TryGetDate(IXLCell cell, int row, ILogger logger)
        {
            if (cell == null || cell.IsEmpty())
                return null;
            try
            {
                if (cell.DataType == XLDataType.DateTime)
                    return cell.GetDateTime();
                if (cell.DataType == XLDataType.Number)
                {
                    double oaValue = cell.GetDouble();
                    if (oaValue >= 0 && oaValue < 2958466)
                    {
                        try
                        {
                            return DateTime.FromOADate(oaValue);
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex, $"Dòng {row}: Giá trị ngày tháng không hợp lệ (OLE Automation date lỗi): {oaValue}. Giá trị gốc: {cell.Value.ToString()}");
                            return null;
                        }
                    }
                    else
                    {
                        logger.LogWarning($"Dòng {row}: Giá trị ngày tháng không hợp lệ (OLE Automation date out of range): {oaValue}. Giá trị gốc: {cell.Value.ToString()}");
                        return null;
                    }
                }
                var str = cell.GetString();
                if (!string.IsNullOrWhiteSpace(str))
                {
                    // Thử parse theo định dạng dd/MM/yyyy trước
                    if (DateTime.TryParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
                        return dob;
                    // Nếu không được, thử parse mặc định
                    if (DateTime.TryParse(str, out dob))
                        return dob;
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Dòng {row}: Không thể chuyển đổi giá trị ngày tháng: {cell.Value.ToString()}");
            }
            return null;
        }

        // Helper: Validate email
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Helper: Chuẩn hóa số điện thoại (chỉ lấy số, bỏ ký tự lạ)
        private static string NormalizePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;
            var digits = Regex.Replace(phone, @"[^\d]", "");
            if (digits.Length < 8 || digits.Length > 15) return string.Empty;
            return digits;
        }
    }
}