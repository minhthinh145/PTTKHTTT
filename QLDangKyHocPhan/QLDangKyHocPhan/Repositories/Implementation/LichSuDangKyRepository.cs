using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using YourProjectNamespace.Models;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class LichSuDangKyRepository : ILichSuDangKyRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public LichSuDangKyRepository(QlDangKyHocPhanContext context) 
        {
            _context = context;
        }

        public async Task<bool> AddLichSuDangKyAsync(DangKy dangKy)
        {
            await _context.DangKys.AddAsync(dangKy);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<List<DangKy>> GetLichSuDangKyByMssv(string mssv)
        {
            try
            {
                if (string.IsNullOrEmpty(mssv))
                {
                    throw new ArgumentException("MSSV không được để trống hoặc null.");
                }

                var lichSuDangKy = await _context.DangKys
                    .Where(dk => dk.MaSinhVien == mssv)
                    .Include(dk => dk.LopHocPhan) // Load thông tin lớp học phần kèm theo
                                    .ThenInclude(lhp => lhp.Hocphan) //
                    .OrderByDescending(dk => dk.NgayThucHien)
                    .ToListAsync();

                return lichSuDangKy;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy lịch sử đăng ký cho MSSV {mssv}: {ex.Message}", ex);
            }
        }


    }
}
