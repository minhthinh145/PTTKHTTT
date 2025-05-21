using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLDangKyHocPhan.Models
{
    public class CHITIET_CTDT
    {
        [Key]
        [StringLength(10)]
        public string MaCT_CTDT { get; set; } = null!;

        [StringLength(10)]
        [ForeignKey("CTDAOTAO")]
        public string MaCT { get; set; } = null!;

        [StringLength(10)]
        [ForeignKey("Hocphan")]
        public string MaHocPhan { get; set; } = null!;

        public virtual CTDAOTAO CTDAOTAO { get; set; } = null!;
        public virtual Hocphan Hocphan { get; set; } = null!;
    }
}
