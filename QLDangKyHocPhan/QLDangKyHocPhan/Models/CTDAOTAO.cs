using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.Models
{
    public class CTDAOTAO
    {
        [Key]
        [StringLength(10)]
        public string MaCT { get; set; } = null!;

        [StringLength(10)]
        [ForeignKey("Khoa")]
        public string MaKhoa { get; set; } = null!;

        [StringLength(9)] // Giả sử định dạng năm học như "2025-2026"
        public string NamHoc { get; set; } = null!;

        [StringLength(50)]
        public string? TenCTDT { get; set; }
        public virtual Khoa Khoa { get; set; } = null!;
        public virtual ICollection<CHITIET_CTDT> ChiTietCtdts { get; set; } = new List<CHITIET_CTDT>();
        public virtual ICollection<Sinhvien> SinhViens { get; set; } = new List<Sinhvien>();
    }
}
