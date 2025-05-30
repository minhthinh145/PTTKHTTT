import React from "react";
import { useGetLichSuDangKy } from "../../hooks/SinhVien/useGetLichSuDangKy"; // Adjust path
import { HienThiLopCard } from "./HienThiLopCard"; // Adjust path
import { HienThiLichSuCard } from "./RegisterTab/HienThiLichSuCard";

const HistoryTabContent: React.FC = () => {
  const { lichSu, loading, error, refresh } = useGetLichSuDangKy();
  console.log("Lịch sử đăng ký:", lichSu);

  return (
    <>
      <h2 className="text-2xl font-bold mb-4">LỊCH SỬ ĐĂNG KÝ HỌC PHẦN</h2>
      <div className="bg-white p-6 rounded shadow">
        <div className="flex flex-wrap gap-4 mb-4">
          <select className="border px-4 py-2 rounded w-60 cursor-pointer">
            <option>Năm học</option>
            {/* Add dynamic options if API available */}
          </select>
          <select className="border px-4 py-2 rounded w-60 cursor-pointer">
            <option>Học kỳ</option>
            {/* Add dynamic options if API available */}
          </select>
          <button
            className="cursor-pointer bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-600 disabled:bg-blue-400"
            onClick={refresh}
            disabled={loading}
          >
            {loading ? "Đang tải..." : "Tìm kiếm"}
          </button>
        </div>

        <div className="overflow-auto rounded border border-gray-300 text-center">
          <table className="min-w-full text-sm text-left">
            <thead className="bg-[#053C65] text-white">
              <tr>
                <th className="px-4 py-2">STT</th>
                <th className="px-4 py-2">Mã HP</th>
                <th className="px-4 py-2">Mã LHP</th>
                <th className="px-4 py-2">Tên HP</th>
                <th className="px-4 py-2">Hành động</th>
                <th className="px-4 py-2">Đến ngày</th>
              </tr>
            </thead>
            <tbody>
              <HienThiLichSuCard lopHocPhan={lichSu ?? undefined} />
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
};

export default HistoryTabContent;
