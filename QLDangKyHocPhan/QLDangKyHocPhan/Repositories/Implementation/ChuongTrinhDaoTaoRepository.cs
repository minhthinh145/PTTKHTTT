using Microsoft.EntityFrameworkCore;
using QLDangKyHocPhan.Contexts;
using QLDangKyHocPhan.DTOs;
using QLDangKyHocPhan.Helpers;
using QLDangKyHocPhan.Repositories.Interface;

namespace QLDangKyHocPhan.Repositories.Implementation
{
    public class ChuongTrinhDaoTaoRepository :  IChuongTrinhDaoTaoRepository
    {
        private readonly QlDangKyHocPhanContext _context;

        public ChuongTrinhDaoTaoRepository(QlDangKyHocPhanContext context) 
        {
            _context = context;
        }

        public async Task<List<ChiTietCTDTDTO>> GetChiTietById(string MaCT)
        {
            var chiTietList = await _context.CHITIET_CTDTs
        .Where(ct => ct.MaCT == MaCT)
        .Include(ct => ct.Hocphan)
        .Select(ct => new ChiTietCTDTDTO
        {
            MaCT_CTDT = ct.MaCT_CTDT,
            MaCT = ct.MaCT,
            MaHocPhan = ct.MaHocPhan,
            TenHocPhan = ct.Hocphan.TenHocPhan,
            SoTinChi = ct.Hocphan.SoTc
        })
        .ToListAsync();

            return chiTietList;
        }

        public async Task<ServiceResult> GetChuongTrinhDaoTaoById(string MaCT)
        {
            var result = await _context.CTDAOTAOs.FirstOrDefaultAsync(p => p.MaCT == MaCT);
            if (result == null) 
            {
                return ServiceResult.Failure("Không tìm thấy chương trình đào tạo! Vui lòng làm mới lại trang");
            }
            return ServiceResult.Success("Đã lấy được chương trình đào tạo", data: result);
        }
    }
}
