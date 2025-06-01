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
        <div className="overflow-auto rounded border border-gray-300 text-center max-h-screen">
          <table className="min-w-full text-sm text-center">
            <thead className="bg-[#053C65] text-white sticky top-0">
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
