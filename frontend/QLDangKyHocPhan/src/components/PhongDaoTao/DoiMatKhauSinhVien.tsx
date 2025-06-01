import { useState, useCallback } from "react";
import {
  updatePassword,
  getSinhVienByMa,
} from "../../apis/services/sinhVienService";
import type { UpdatePasswordRequest } from "../../apis/types/auth";
import type { SinhVienProfile } from "../../apis/types/user";
import debounce from "lodash.debounce";

const DoiMatKhauSinhVien = () => {
  const [form, setForm] = useState<UpdatePasswordRequest>({
    maSinhVien: "",
    newPassword: "",
  });
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<string | null>(null);
  const [sinhVien, setSinhVien] = useState<SinhVienProfile | null>(null);
  const [svError, setSvError] = useState<string | null>(null);

  // Dùng useCallback để giữ debounce instance giữa các lần render
  const fetchSinhVien = useCallback(
    debounce(async (maSinhVien: string) => {
      setSinhVien(null);
      setSvError(null);
      if (!maSinhVien) return;
      const res = await getSinhVienByMa(maSinhVien);
      if (res.isSuccess && res.data) {
        setSinhVien(res.data);
      } else {
        setSvError(res.message || "Không tìm thấy sinh viên này.");
      }
    }, 500),
    []
  );

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    if (e.target.name === "maSinhVien") {
      fetchSinhVien(e.target.value);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setResult(null);
    try {
      await updatePassword(form);
      setResult("Đổi mật khẩu thành công!");
    } catch (err: any) {
      setResult(
        err.response?.data?.message ||
          "Đổi mật khẩu thất bại. Vui lòng kiểm tra lại."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="max-w-3xl mx-auto mt-10 p-8 bg-gradient-to-br from-blue-50 to-white rounded-xl shadow-lg border border-blue-100"
    >
      <h2 className="text-2xl font-bold mb-6 text-blue-800 text-center tracking-wide">
        Đổi mật khẩu sinh viên
      </h2>
      <div className="mb-5">
        <label className="block font-semibold mb-2 text-blue-900">
          Mã sinh viên
        </label>
        <input
          type="text"
          name="maSinhVien"
          value={form.maSinhVien}
          onChange={handleChange}
          className="border border-blue-300 px-4 py-2 rounded-lg w-full focus:ring-2 focus:ring-blue-400 outline-none transition"
          required
          placeholder="Nhập mã sinh viên..."
        />
        {svError && <div className="text-red-600 text-sm mt-1">{svError}</div>}
      </div>
      {sinhVien && (
        <div className="mb-5 p-4 bg-blue-50 rounded-lg border border-blue-200 shadow-sm">
          <div className="grid grid-cols-2 gap-x-6 gap-y-2 text-blue-900 text-sm min-w-0">
            <div className="truncate" title={sinhVien.hoTen}>
              <span className="font-semibold">Họ tên:</span>{" "}
              <span className="break-words">{sinhVien.hoTen}</span>
            </div>
            <div className="truncate" title={sinhVien.email}>
              <span className="font-semibold">Email:</span>{" "}
              <span className="break-words">{sinhVien.email}</span>
            </div>
            <div>
              <span className="font-semibold">Giới tính:</span>{" "}
              {sinhVien.gioiTinh}
            </div>
            <div>
              <span className="font-semibold">Ngày sinh:</span>{" "}
              {new Date(sinhVien.ngaySinh).toLocaleDateString()}
            </div>
            <div
              className="truncate"
              title={sinhVien.tenKhoa + " (" + sinhVien.maKhoa + ")"}
            >
              <span className="font-semibold">Khoa:</span>{" "}
              <span className="break-words">{sinhVien.tenKhoa}</span> (
              {sinhVien.maKhoa})
            </div>
            <div
              className="truncate"
              title={sinhVien.tenCTDT + " (" + sinhVien.maCT + ")"}
            >
              <span className="font-semibold">Chương trình:</span>{" "}
              <span className="break-words">{sinhVien.tenCTDT}</span> (
              {sinhVien.maCT})
            </div>
          </div>
        </div>
      )}
      <div className="mb-6">
        <label className="block font-semibold mb-2 text-blue-900">
          Mật khẩu mới
        </label>
        <input
          type="text"
          name="newPassword"
          value={form.newPassword}
          onChange={handleChange}
          className="border border-blue-300 px-4 py-2 rounded-lg w-full focus:ring-2 focus:ring-blue-400 outline-none transition"
          required
          disabled={!sinhVien}
          placeholder="Nhập mật khẩu mới..."
        />
      </div>
      <button
        type="submit"
        className="w-full bg-blue-600 hover:bg-blue-700 text-white py-2 rounded-lg font-semibold text-lg transition disabled:opacity-60 cursor-pointer"
        disabled={loading || !sinhVien}
      >
        {loading ? "Đang cập nhật..." : "Đổi mật khẩu"}
      </button>
      {result && (
        <div className="mt-5 font-medium text-center text-green-600">
          {result}
        </div>
      )}
    </form>
  );
};

export default DoiMatKhauSinhVien;