using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;
using System.Threading.Tasks;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public GiangVienRepository(QlDangKyHocPhanContext context)
        {
            _context = context;
        }

        public async Task<Giangvien?> GetGiangVienByIdAsync(string id)
        {
            return await _context.Giangviens
                .Include(gv => gv.TaiKhoan)       // Include navigation properties nếu cần
                .Include(gv => gv.Lophocphans)    // Include danh sách lớp học phần nếu cần
                .FirstOrDefaultAsync(gv => gv.MaGiangVien == id);
        }

        public async Task<List<Lophocphan>> GetLopHocPhanByMaGiangVienAsync(string maGiangVien)
        {
            var lopHocPhanList = await _context.Lophocphans
     .Include(lhp => lhp.MaGiangVienNavigation)  // đúng: Include navigation property
     .Where(lhp => lhp.MaGiangVien == maGiangVien)
     .ToListAsync();
            return lopHocPhanList;
        }
    }
}
