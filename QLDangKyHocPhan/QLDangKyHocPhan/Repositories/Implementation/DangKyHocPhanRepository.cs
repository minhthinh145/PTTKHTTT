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

        public DangKyHocPhanRepository(QlDangKyHocPhanContext context , ILopHocPhanRepository lhpRepo , IHocPhanDangKyRepository hpRepo , ILichSuDangKyRepository lsRepo)
        {
            _context = context;
            _lhpRepo = lhpRepo;
            _hpRepo = hpRepo;
            _lsRepo = lsRepo;

        }
        public async Task<bool> DangKyHocPhanAsync(string mssv, string maLopHocPhan)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Ghi lịch sử đăng ký
                var addDangKy = new DangKy
                {
                    LoaiDangKy = "Đăng ký",
                    MaLopHP = maLopHocPhan,
                    MaSinhVien = mssv,
                    NgayThucHien = DateTime.Now
                };
                await _lsRepo.AddLichSuDangKyAsync(addDangKy);

                // Step 2: Ghi vào bảng HocPhanDangKy
                var addHocPhanDangKy = new HocPhanDangKy
                {
                    MaLopHocPhan = maLopHocPhan,
                    MaSinhVien = mssv
                };
                await _hpRepo.AddHocPhanDangKyAsync(addHocPhanDangKy);

                // Step 3: Tăng số lượng lớp
                var updateResult = await _lhpRepo.UpdateSiSoPlusOneAsync(maLopHocPhan);
                if (!updateResult)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

        public async Task<bool> HuyDangKyHocPhanAsync(string mssv, string maLopHocPhan)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Ghi lịch sử hủy đăng ký
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
                    await transaction.RollbackAsync();
                    return false;
                }

                // Step 2: Xóa khỏi bảng HocPhanDangKy
                var deleted = await _hpRepo.DeleteHocPhanDangKyAsync(new HocPhanDangKy
                {
                    MaLopHocPhan = maLopHocPhan,
                    MaSinhVien = mssv
                });

                if (!deleted)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // Step 3: Giảm sĩ số lớp
                var updated = await _lhpRepo.UpdateSiSoMinusOneAsync(maLopHocPhan);
                if (!updated)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
 