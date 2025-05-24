using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using YourProjectNamespace.Models;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class DangKyHocPhanRepository : IDangKyHocPhanRepository
    {
        private readonly QlDangKyHocPhanContext _context;
        private readonly ILopHocPhanRepository _lhpRepo;
        private readonly IHocPhanDangKyRepository _hpRepo;
        private readonly ILichSuDangKyRepository _lsRepo;

        public DangKyHocPhanRepository(
            QlDangKyHocPhanContext context,
            ILopHocPhanRepository lhpRepo,
            IHocPhanDangKyRepository hpRepo,
            ILichSuDangKyRepository lsRepo)
        {
            _context = context;
            _lhpRepo = lhpRepo;
            _hpRepo = hpRepo;
            _lsRepo = lsRepo;
        }

        public async Task<bool> ChuyenLopDangKyAsync(string mssv, string maLopHocPhan, string maLopHocPhanMoi)
        {
            using var transaction = _context.Database.CurrentTransaction ?? await _context.Database.BeginTransactionAsync();

            try
            {
                // Bước 1: Kiểm tra đầu vào
                if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(maLopHocPhan) || string.IsNullOrEmpty(maLopHocPhanMoi))
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Bước 2: Kiểm tra lớp mới có còn chỗ không
                var lopHocPhanMoi = await _context.Lophocphans
                    .Where(lhp => lhp.MaLopHocPhan == maLopHocPhanMoi)
                    .Select(lhp => new { lhp.SoLuong, lhp.SoLuongDangKy, lhp.MaHocPhan })
                    .FirstOrDefaultAsync();
                if (lopHocPhanMoi == null || lopHocPhanMoi.SoLuongDangKy >= lopHocPhanMoi.SoLuong)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false; // Lớp không tồn tại hoặc đã đầy
                }

                // Bước 3: Kiểm tra lớp cũ và lớp mới thuộc cùng mã học phần
                var lopHocPhanCu = await _context.Lophocphans
                    .Where(lhp => lhp.MaLopHocPhan == maLopHocPhan)
                    .Select(lhp => lhp.MaHocPhan)
                    .FirstOrDefaultAsync();
                if (lopHocPhanCu == null || lopHocPhanCu != lopHocPhanMoi.MaHocPhan)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false; // Lớp cũ không tồn tại hoặc khác mã học phần
                }

                // Bước 4: Hủy đăng ký lớp cũ
                var huySuccess = await _hpRepo.DeleteHocPhanDangKyAsync(new HocPhanDangKy
                {
                    MaLopHocPhan = maLopHocPhan,
                    MaSinhVien = mssv
                });
                if (!huySuccess)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Bước 5: Giảm sĩ số lớp cũ
                var giamSiSoOk = await _lhpRepo.UpdateSiSoMinusOneAsync(maLopHocPhan);
                if (!giamSiSoOk)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Bước 6: Ghi lịch sử hủy lớp cũ
                var huyLichSu = new DangKy
                {
                    MaSinhVien = mssv,
                    MaLopHP = maLopHocPhan,
                    LoaiDangKy = "Hủy để chuyển lớp",
                    NgayThucHien = DateTime.Now
                };
                await _lsRepo.AddLichSuDangKyAsync(huyLichSu);

                // Bước 7: Đăng ký lớp mới (tái sử dụng giao dịch)
                var dangKyMoiOk = await DangKyHocPhanAsync(mssv, maLopHocPhanMoi);
                if (!dangKyMoiOk)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Bước 8: Ghi lịch sử chuyển lớp
                var chuyenLichSu = new DangKy
                {
                    MaSinhVien = mssv,
                    MaLopHP = maLopHocPhanMoi,
                    LoaiDangKy = "Chuyển lớp",
                    NgayThucHien = DateTime.Now
                };
                await _lsRepo.AddLichSuDangKyAsync(chuyenLichSu);

                // Cam kết giao dịch nếu tự khởi tạo
                if (_context.Database.CurrentTransaction == transaction)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction == transaction)
                {
                    await transaction.RollbackAsync();
                }
                throw new Exception($"Lỗi khi chuyển lớp: {ex.Message}", ex);
            }
        }

        public async Task<bool> DangKyHocPhanAsync(string mssv, string maLopHocPhan)
        {
            // Kiểm tra giao dịch hiện có, nếu không thì tạo mới
            var transaction = _context.Database.CurrentTransaction ?? await _context.Database.BeginTransactionAsync();

            try
            {
                // Bước 1: Kiểm tra lớp học phần có còn chỗ không
                var lopHocPhan = await _context.Lophocphans
                    .Where(lhp => lhp.MaLopHocPhan == maLopHocPhan)
                    .Select(lhp => new { lhp.SoLuong, lhp.SoLuongDangKy })
                    .FirstOrDefaultAsync();
                if (lopHocPhan == null || lopHocPhan.SoLuongDangKy >= lopHocPhan.SoLuong)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false; // Lớp không tồn tại hoặc đã đầy
                }

                // Bước 2: Ghi lịch sử đăng ký
                var addDangKy = new DangKy
                {
                    LoaiDangKy = "Đăng ký",
                    MaLopHP = maLopHocPhan,
                    MaSinhVien = mssv,
                    NgayThucHien = DateTime.Now
                };
                await _lsRepo.AddLichSuDangKyAsync(addDangKy);

                // Bước 3: Ghi vào bảng HocPhanDangKy
                var addHocPhanDangKy = new HocPhanDangKy
                {
                    MaLopHocPhan = maLopHocPhan,
                    MaSinhVien = mssv
                };
                await _hpRepo.AddHocPhanDangKyAsync(addHocPhanDangKy);

                // Bước 4: Tăng sĩ số lớp
                var updateResult = await _lhpRepo.UpdateSiSoPlusOneAsync(maLopHocPhan);
                if (!updateResult)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Cam kết giao dịch nếu tự khởi tạo
                if (_context.Database.CurrentTransaction == transaction)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction == transaction)
                {
                    await transaction.RollbackAsync();
                }
                throw new Exception($"Lỗi khi đăng ký học phần: {ex.Message}", ex);
            }
        }

        public async Task<bool> HuyDangKyHocPhanAsync(string mssv, string maLopHocPhan)
        {
            // Kiểm tra giao dịch hiện có, nếu không thì tạo mới
            var transaction = _context.Database.CurrentTransaction ?? await _context.Database.BeginTransactionAsync();

            try
            {
                // Bước 1: Ghi lịch sử hủy đăng ký
                var lichSu = new DangKy
                {
                    LoaiDangKy = "Hủy đăng ký",
                    MaLopHP = maLopHocPhan,
                    MaSinhVien = mssv,
                    NgayThucHien = DateTime.Now
                };
                var added = await _lsRepo.AddLichSuDangKyAsync(lichSu);
                if (!added)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Bước 2: Xóa khỏi bảng HocPhanDangKy
                var deleted = await _hpRepo.DeleteHocPhanDangKyAsync(new HocPhanDangKy
                {
                    MaLopHocPhan = maLopHocPhan,
                    MaSinhVien = mssv
                });
                if (!deleted)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Bước 3: Giảm sĩ số lớp
                var updated = await _lhpRepo.UpdateSiSoMinusOneAsync(maLopHocPhan);
                if (!updated)
                {
                    if (_context.Database.CurrentTransaction == transaction)
                    {
                        await transaction.RollbackAsync();
                    }
                    return false;
                }

                // Cam kết giao dịch nếu tự khởi tạo
                if (_context.Database.CurrentTransaction == transaction)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction == transaction)
                {
                    await transaction.RollbackAsync();
                }
                throw new Exception($"Lỗi khi hủy đăng ký học phần: {ex.Message}", ex);
            }
        }
    }
}