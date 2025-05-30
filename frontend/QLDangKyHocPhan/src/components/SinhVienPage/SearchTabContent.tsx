// SearchTabContent.tsx
import React from "react";
import type { LopHocPhanDTO } from "../../apis/types/lopHocPhan";
import { HienThiLopCard } from "./HienThiLopCard";

interface SearchTabContentProps {
  lopHocPhan?: LopHocPhanDTO[];
}

export const SearchTabContent: React.FC<SearchTabContentProps> = ({
  lopHocPhan,
}) => {
  return (
    <>
      <h2 className="text-2xl font-bold mb-4">TRA CỨU MÔN HỌC</h2>
      <div className="bg-white p-6 rounded shadow">
        <div className="flex flex-wrap gap-4 mb-4">
          <select className="border px-4 py-2 rounded w-60 cursor-pointer">
            <option>Loại tra cứu</option>
            <option>Mã học phần</option>
            <option>Tên học phần</option>
          </select>
          <input
            type="text"
            placeholder="Nhập thông tin môn học..."
            className="border px-4 py-2 rounded flex-1 min-w-[200px]"
          />
          <button className="cursor-pointer bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-500">
            Tìm kiếm
          </button>
        </div>

        <div className="border border-gray-300 rounded max-h-[500px] overflow-auto">
          <table className="min-w-full text-sm text-left">
            <thead className="bg-[#053C65] text-white sticky top-0 z-10">
              <tr>
                <th className="px-4 py-2">STT</th>
                <th className="px-4 py-2">Mã HP</th>
                <th className="px-4 py-2">Mã LHP</th>
                <th className="px-4 py-2">Tên HP</th>
                <th className="px-4 py-2">STC</th>
                <th className="px-4 py-2">Loại</th>
                <th className="px-4 py-2">Thông tin</th>
                <th className="px-4 py-2">Giảng viên</th>
                <th className="px-4 py-2">Giới hạn</th>
                <th className="px-4 py-2">Từ ngày</th>
                <th className="px-4 py-2">Đến ngày</th>
              </tr>
            </thead>
            <tbody>
              <HienThiLopCard lopHocPhan={lopHocPhan} />
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
};

export default SearchTabContent;
