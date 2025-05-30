namespace QLDangKyHocPhan.DTOs.AuthDTOs
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string LoaiTaiKhoan { get; set; }
    }
}
