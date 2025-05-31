import React, { useState } from "react";
import type { LopHocPhanDTO } from "../../apis/types/lopHocPhan";
import { HienThiLopCard } from "./HienThiLopCard";

interface SearchTabContentProps {
  lopHocPhan?: LopHocPhanDTO[];
}

export const SearchTabContent: React.FC<SearchTabContentProps> = ({
  lopHocPhan = [],
}) => {
  const [searchType, setSearchType] = useState("all");
  const [keyword, setKeyword] = useState("");
  const [filtered, setFiltered] = useState<LopHocPhanDTO[]>(lopHocPhan);

  React.useEffect(() => {
    setFiltered(lopHocPhan);
  }, [lopHocPhan]);

  const handleSearch = (e?: React.FormEvent) => {
    if (e) e.preventDefault();
    let result = lopHocPhan;
    if (keyword.trim()) {
      if (searchType === "maHocPhan") {
        result = lopHocPhan.filter((l) =>
          l.maHocPhan?.toLowerCase().includes(keyword.toLowerCase())
        );
      } else if (searchType === "tenHocPhan") {
        result = lopHocPhan.filter((l) =>
          l.tenHocPhan?.toLowerCase().includes(keyword.toLowerCase())
        );
      } else {
        result = lopHocPhan.filter(
          (l) =>
            l.maHocPhan?.toLowerCase().includes(keyword.toLowerCase()) ||
            l.tenHocPhan?.toLowerCase().includes(keyword.toLowerCase())
        );
      }
    }
    setFiltered(result);
  };

  return (
    <>
      <h2 className="text-2xl font-bold mb-4">TRA CỨU MÔN HỌC</h2>
      <div className="bg-white p-6 rounded shadow flex flex-col h-[calc(100vh-150px)]">
        <form className="flex flex-wrap gap-4 mb-4" onSubmit={handleSearch}>
          <select
            className="border px-4 py-2 rounded w-60 cursor-pointer"
            value={searchType}
            onChange={(e) => setSearchType(e.target.value)}
          >
            <option value="maHocPhan">Mã học phần</option>
            <option value="tenHocPhan">Tên học phần</option>
          </select>
          <input
            type="text"
            placeholder="Nhập thông tin môn học..."
            className="border px-4 py-2 rounded flex-1 min-w-[200px]"
            value={keyword}
            onChange={(e) => setKeyword(e.target.value)}
          />
          <button
            type="submit"
            className="cursor-pointer bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-500"
          >
            Tìm kiếm
          </button>
        </form>

        <div className="flex-1 border border-gray-300 rounded overflow-auto">
          <table className="min-w-full text-sm text-center">
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
              <HienThiLopCard lopHocPhan={filtered} />
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
};

export default SearchTabContent;
