using System.IO;

namespace QLDangKyHocPhan.DTOs.excel
{
    public class ExcelDataImportRequest
    {
        public string RequestId { get; set; }
        public bool HasHeader { get; set; }
        public MemoryStream FileData { get; set; } = new MemoryStream();

        public ExcelDataImportRequest(string requestId)
        {
            RequestId = requestId;
        }
    }
}