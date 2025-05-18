namespace QLDangKyHocPhan.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => DateTime.UtcNow <= Expires && Revoked == null;

        public Taikhoan User { get; set; } 
    }
}
