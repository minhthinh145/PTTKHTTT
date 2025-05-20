namespace QLDangKyHocPhan.DTOs.excel
{
    public class UploadRequest
    {
        public IFormFile? ImportFile { get; set; }
        public bool HasHeader { get; set; } = true;
    }
}
