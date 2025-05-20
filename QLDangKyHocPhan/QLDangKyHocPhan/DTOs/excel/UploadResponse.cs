namespace QLDangKyHocPhan.DTOs.excel
{
    public class UploadResponse
    {
        public string RequestId { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Status { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<MemberDTO> Data { get; set; } = new List<MemberDTO>(); // Thêm danh sách dữ liệu
    }
}