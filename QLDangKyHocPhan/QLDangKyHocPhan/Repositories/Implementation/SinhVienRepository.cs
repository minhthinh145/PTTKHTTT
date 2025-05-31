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

        public async Task AddSinhVienAsync(Sinhvien sinhvien)
        {
            // Kiểm tra và lấy Khoa
            var khoa = await _context.Khoas.FindAsync(sinhvien.MaKhoa);
            if (khoa == null)
            {
                throw new Exception($"Khoa với mã '{sinhvien.MaKhoa}' không tồn tại.");
            }
            sinhvien.MaKhoaNavigation = khoa;

            // Kiểm tra và lấy CTDAOTAO
            var ctdt = await _context.CTDAOTAOs.FindAsync(sinhvien.MaCT);
            if (ctdt == null)
            {
                throw new Exception($"Chương trình đào tạo với mã '{sinhvien.MaCT}' không tồn tại.");
            }
            sinhvien.CTDaoTao = ctdt;

            // Gán lại khóa ngoại nếu cần
            sinhvien.MaKhoa = khoa.MaKhoa;
            sinhvien.MaCT = ctdt.MaCT;

            _context.Sinhviens.Add(sinhvien);
            await _context.SaveChangesAsync();
        }
    }
}
