using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.Models;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class HocPhanDangKyRepository : IHocPhanDangKyRepository
    {
        private QlDangKyHocPhanContext _context;

        public HocPhanDangKyRepository(QlDangKyHocPhanContext context)
        {
            _context = context;            
        }
        public async Task<bool> AddHocPhanDangKyAsync(HocPhanDangKy hocPhanDangKy)
        {
            await _context.HocPhanDangKys.AddAsync(hocPhanDangKy);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteHocPhanDangKyAsync(HocPhanDangKy hocPhanDangKy)
        {
            var entity = await _context.HocPhanDangKys
                .FirstOrDefaultAsync(x => x.MaSinhVien == hocPhanDangKy.MaSinhVien
                                       && x.MaLopHocPhan == hocPhanDangKy.MaLopHocPhan);

            if (entity == null)
                return false;

            _context.HocPhanDangKys.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
