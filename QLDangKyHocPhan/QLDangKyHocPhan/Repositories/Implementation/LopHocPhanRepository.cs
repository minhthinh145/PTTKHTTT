using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class LopHocPhanRepository : ILopHocPhanRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public LopHocPhanRepository(QlDangKyHocPhanContext context)
        {
            _context = context;
        }

        public async Task<List<Lophocphan>> GetLopHocPhanByMaHP(string maHP)
        {
            var result = await _context.Lophocphans
                .Where(lhp => lhp.MaHocPhan == maHP)
                .ToListAsync();
            return result;
        }

        public async Task<List<Lophocphan>> GetLopHocPhanChuaDangKyAsync()
        {
            var result = await _context.Lophocphans
     .Include(lhp => lhp.Hocphan)
     .ToListAsync();
            return result;
        }

        public async Task<List<Lophocphan>> GetLopHocPhanChuaDangKyByMaHP(string maHP, string maSinhVien)
        {
            // Lấy danh sách mã lớp học phần mà sinh viên đã đăng ký
            var lopDaDangKy = await _context.HocPhanDangKys
                .Where(dk => dk.MaSinhVien == maSinhVien)
                .Select(dk => dk.MaLopHocPhan)
                .ToListAsync();

            // Lấy danh sách lớp học phần theo mã học phần mà sinh viên chưa đăng ký
            var lopChuaDangKy = await _context.Lophocphans
                .Include(lhp => lhp.Hocphan)
                .Where(lhp => lhp.MaHocPhan == maHP && !lopDaDangKy.Contains(lhp.MaLopHocPhan))
                .ToListAsync();

            return lopChuaDangKy;
        }

        public async Task<bool> UpdateSiSoMinusOneAsync(string maLopHocPhan)
        {
            var lop = await _context.Lophocphans.FirstOrDefaultAsync(l => l.MaLopHocPhan == maLopHocPhan);
            if (lop == null || lop.SoLuongDangKy == 0) return false;
            lop.SoLuongDangKy -= 1;
            _context.Lophocphans.Update(lop);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateSiSoPlusOneAsync(string maLopHocPhan)
        {
            var lop = await _context.Lophocphans.FirstOrDefaultAsync(l => l.MaLopHocPhan == maLopHocPhan);
            if (lop == null || lop.SoLuongDangKy >= lop.SoLuong)
                return false;

            lop.SoLuongDangKy += 1;
            _context.Lophocphans.Update(lop);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
