namespace QLDangKyHocPhan.DTOs
{
    public class DanhSachMonDTO
    {
        public string MaCT { get; set; } = null!; // Mã chương trình đào tạo

        public List<HocPhanDTO> HocPhans { get; set; } = new(); // Danh sách các học phần
    }
}
