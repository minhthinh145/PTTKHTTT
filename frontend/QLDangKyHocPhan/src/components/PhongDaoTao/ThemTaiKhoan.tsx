import { useRef } from "react";
import { AiOutlineUpload, AiOutlineClose } from "react-icons/ai";
import { useImportTaiKhoan } from "../../hooks/useImportTaiKhoan";
import type { ImportSinhVienRow } from "../../hooks/useImportTaiKhoan";

// Khai báo header là tuple các key của ImportSinhVienRow
const REQUIRED_HEADER = [
  "Username",
  "TenDangNhap",
  "Email",
  "PhoneNumber",
  "NgaySinh",
  "Password",
  "LoaiTaiKhoan",
  "MaSinhVien",
  "HoTen",
  "GioiTinh",
  "MaKhoa",
  "MaCT",
] as const;

type HeaderKey = (typeof REQUIRED_HEADER)[number];

const ThemTaiKhoan = () => {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const {
    rows,
    error,
    validateResult,
    importResult,
    loading,
    handleFileChange,
    handleCancel,
    handleSave,
  } = useImportTaiKhoan();

  return (
    <div>
      <h2 className="font-bold mb-4 text-xl flex items-center gap-2">
        <AiOutlineUpload className="text-blue-500" size={28} />
        Thêm tài khoản từ Excel
      </h2>
      <div className="mb-4 flex items-center gap-2">
        <label className="block mb-2 text-gray-700 font-medium">
          Chọn file Excel (.xlsx, .xls) đúng định dạng:
        </label>
        <input
          type="file"
          accept=".xlsx, .xls"
          onChange={handleFileChange}
          className="border rounded px-3 py-2 w-full max-w-xs"
          ref={fileInputRef}
        />
        {rows.length > 0 && (
          <button
            className="ml-2 text-red-500 hover:text-red-700"
            onClick={() => handleCancel(fileInputRef)}
            title="Huỷ file"
          >
            <AiOutlineClose size={22} />
          </button>
        )}
      </div>
      {rows.length > 0 && (
        <button
          className="mb-4 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 font-semibold"
          onClick={handleSave}
          disabled={loading}
        >
          {loading ? "Đang lưu..." : "Lưu"}
        </button>
      )}
      {error && <div className="mb-2 text-red-600 font-medium">{error}</div>}
      {validateResult && (
        <div className="mb-2 text-green-600 font-medium">{validateResult}</div>
      )}
      {importResult && (
        <div className="mb-2 text-blue-600 font-medium">
          <div>Kết quả import:</div>
          <pre className="text-xs bg-gray-100 p-2 rounded max-h-40 overflow-auto">
            {JSON.stringify(importResult, null, 2)}
          </pre>
        </div>
      )}
      {rows.length > 0 && (
        <div className="overflow-x-auto rounded shadow border border-gray-200 min-w-0">
          <table className="min-w-full w-full text-center ml-auto">
            <thead>
              <tr className="bg-blue-100">
                {REQUIRED_HEADER.map((key) => (
                  <th className="border px-3 py-2 font-semibold" key={key}>
                    {key}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {rows.map((row, i) => (
                <tr key={i} className="hover:bg-blue-50 transition-colors">
                  {REQUIRED_HEADER.map((key) => (
                    <td className="border px-3 py-2" key={key}>
                      {row[key as keyof ImportSinhVienRow] ?? ""}
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
      {rows.length === 0 && (
        <div className="text-gray-500 mt-4">
          Chưa có dữ liệu, hãy chọn file Excel để xem trước.
          <br />
          <span className="text-xs">
            Định dạng cột: {REQUIRED_HEADER.join(" - ")}
          </span>
        </div>
      )}
    </div>
  );
};

export default ThemTaiKhoan;
