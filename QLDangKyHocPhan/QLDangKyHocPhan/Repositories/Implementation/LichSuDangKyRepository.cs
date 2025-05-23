using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
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
            return await _context.DangKys
                .Where(dk => dk.MaSinhVien == mssv)
                .OrderByDescending(dk => dk.NgayThucHien)
                .ToListAsync();
        }


    }
}
