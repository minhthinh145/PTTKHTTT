import { useState, useCallback } from "react";
import * as XLSX from "xlsx";
import { importUsers } from "../apis/services/importServices";
import { toast } from "react-toastify";

// Header đúng thứ tự 12 cột, đồng bộ với BE
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
];

export interface ImportSinhVienRow {
  Username: string;
  TenDangNhap: string;
  Email: string;
  PhoneNumber: string;
  NgaySinh: string | null;
  Password: string;
  LoaiTaiKhoan: string;
  MaSinhVien: string;
  HoTen: string;
  GioiTinh: string;
  MaKhoa: string;
  MaCT: string;
}

function validatePassword(pw: string) {
  return (
    pw.length >= 8 &&
    /[A-Z]/.test(pw) &&
    /[a-z]/.test(pw) &&
    /[^A-Za-z0-9]/.test(pw)
  );
}

function validateRole(role: string) {
  return role === "SinhVien";
}

export const useImportTaiKhoan = () => {
  const [rows, setRows] = useState<ImportSinhVienRow[]>([]);
  const [file, setFile] = useState<File | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [validateResult, setValidateResult] = useState<string | null>(null);
  const [importResult, setImportResult] = useState<any>(null);
  const [loading, setLoading] = useState(false);

  // Đọc file và chuyển thành mảng object
  const handleFileChange = useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      setError(null);
      setValidateResult(null);
      setImportResult(null);
      const f = e.target.files?.[0];
      if (!f) return;
      setFile(f);
      const reader = new FileReader();
      reader.onload = (evt) => {
        const data = evt.target?.result;
        const workbook = XLSX.read(data, { type: "binary" });
        const sheetName = workbook.SheetNames[0];
        const worksheet = workbook.Sheets[sheetName];
        const json: any[][] = XLSX.utils.sheet_to_json(worksheet, {
          header: 1,
        });

        // Validate header
        const header = json[0];
        if (
          header.length !== REQUIRED_HEADER.length ||
          !REQUIRED_HEADER.every((h, i) => h === header[i])
        ) {
          setRows([]);
          setError(
            "File Excel phải có đúng các cột: " + REQUIRED_HEADER.join(" - ")
          );
          return;
        }

        // Map từng dòng thành object
        const dataRows: ImportSinhVienRow[] = [];
        for (let i = 1; i < json.length; ++i) {
          const row = json[i];
          if (
            !row ||
            row.every(
              (cell) => cell === undefined || cell === null || cell === ""
            )
          ) {
            continue;
          }
          dataRows.push({
            Username: row[0] || "",
            TenDangNhap: row[1] || "",
            Email: row[2] || "",
            PhoneNumber: row[3] || "",
            NgaySinh: row[4] || null,
            Password: row[5] || "",
            LoaiTaiKhoan: row[6] || "",
            MaSinhVien: row[7] || "",
            HoTen: row[8] || "",
            GioiTinh: row[9] || "",
            MaKhoa: row[10] || "",
            MaCT: row[11] || "",
          });
        }
        setRows(dataRows);
      };
      reader.readAsBinaryString(f);
    },
    []
  );

  const handleCancel = useCallback(
    (fileInputRef: React.RefObject<HTMLInputElement | null>) => {
      setRows([]);
      setFile(null);
      setError(null);
      setValidateResult(null);
      setImportResult(null);
      if (fileInputRef.current) fileInputRef.current.value = "";
    },
    []
  );

  // Validate và gửi dữ liệu
  const handleSave = useCallback(async () => {
    setError(null);
    setValidateResult(null);
    setImportResult(null);
    if (rows.length === 0 || !file) return;

    // Kiểm tra từng dòng
    let hasError = false;
    let errorMsg = "";
    rows.forEach((row, idx) => {
      if (!validatePassword(row.Password)) {
        hasError = true;
        errorMsg += `Dòng ${idx + 2}: Mật khẩu không hợp lệ. `;
      }
      if (!validateRole(row.LoaiTaiKhoan)) {
        hasError = true;
        errorMsg += `Dòng ${idx + 2}: Chỉ cho phép thêm tài khoản SinhVien. `;
      }
    });
    if (hasError) {
      setError(errorMsg);
      return;
    }

    setValidateResult("✅ Dữ liệu hợp lệ, đang gửi lên server...");
    setLoading(true);
    try {
      const result = await importUsers(file);
      setImportResult(result);
      setValidateResult("✅ Import thành công!");
      toast.success("Đã thêm account thành công!");
    } catch (err: any) {
      setError("Lỗi import: " + (err?.response?.data || err.message));
    } finally {
      setLoading(false);
    }
  }, [rows, file]);

  return {
    rows,
    error,
    validateResult,
    importResult,
    loading,
    handleFileChange,
    handleCancel,
    handleSave,
    setRows,
  };
};
