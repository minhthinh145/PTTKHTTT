using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class HocPhanRepository : IHocPhanRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public HocPhanRepository(QlDangKyHocPhanContext context) 
        {
            _context = context;
        }

        public async Task<List<Hocphan>> GetAllHocPhanAsync()
        {
            return await _context.Hocphans.ToListAsync();
        }

        public async Task<Hocphan> GetHocPhanByMaHocPhanAsync(string maHP)
        {
            return await _context.Hocphans.FirstOrDefaultAsync(p => p.MaHocPhan == maHP);
        }

        public async Task<List<Hocphan>> GetHocPhanChuaDangKyAsync(string maChuongTrinhDaoTao, string maSinhVien)
        {
            // Lấy danh sách mã học phần đã đăng ký
            var hocPhanDaDangKy = await _context.HocPhanDangKys
                .Where(dk => dk.MaSinhVien == maSinhVien)
                .Include(dk => dk.LopHocPhan)
                .ThenInclude(lhp => lhp.Hocphan)
                .Select(dk => dk.LopHocPhan.Hocphan.MaHocPhan)
                .Distinct()
                .ToListAsync();

            // Lấy danh sách học phần thuộc chương trình đào tạo từ bảng CHITIET_CTDT
            var hocPhanThuocCTDT = await _context.CHITIET_CTDTs
                .Where(ct => ct.MaCT == maChuongTrinhDaoTao)
                .Select(ct => ct.Hocphan)
                .Where(hp => !hocPhanDaDangKy.Contains(hp.MaHocPhan))
                .ToListAsync();

            return hocPhanThuocCTDT;
        }


    }
}
