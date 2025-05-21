using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public SinhVienRepository(QlDangKyHocPhanContext context)
        {
            _context = context;
        }
        public async Task<Sinhvien?> GetSinhvienByIdAsync(string id)
        {
            return await _context.Sinhviens
                   .Include(s => s.MaKhoaNavigation)
                    .Include(s => s.CTDaoTao)
                    .FirstOrDefaultAsync(s => s.MaSinhVien == id);
        }
    }
}
