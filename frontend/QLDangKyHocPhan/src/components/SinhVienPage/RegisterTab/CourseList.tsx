import React, { useState } from "react";
import { useGetLopHocPhanByMaHocPhan } from "../../../hooks/SinhVien/HocPhan/useGetLopHocPhanByMaHocPhan";
import DangKyCard from "./DangKyCard";
import { IoChevronForward } from "react-icons/io5";

interface CourseCardProps {
  tenHocPhan: string;
  maHocPhan: string;
  onAction: (maLopHocPhan: string) => void | Promise<void>; // Support both sync and async
  actionType: "register" | "chuyenLop"; // Indicate action type
}

const CourseCard: React.FC<CourseCardProps> = ({
  tenHocPhan,
  maHocPhan,
  onAction,
  actionType,
}) => {
  const [expanded, setExpanded] = useState(false);
  const { lopHocPhan, loading, error } = useGetLopHocPhanByMaHocPhan(maHocPhan); // Fetch only when expanded

  return (
    <div className="border border-gray-200 rounded-lg p-5 mb-6 shadow-md hover:shadow-lg transition-shadow duration-200">
      <div
        className="flex justify-between items-center cursor-pointer select-none"
        onClick={() => setExpanded(!expanded)}
      >
        <h3 className="font-semibold text-xl text-gray-900">{tenHocPhan}</h3>
        <button
          aria-label={expanded ? "Thu gọn" : "Mở rộng"}
          className="text-blue-600 transform transition-transform duration-200 cursor-pointer hover:text-blue-800 focus:outline-none"
        >
          <IoChevronForward
            className="w-6 h-6"
            style={{ transform: expanded ? "rotate(90deg)" : "rotate(0deg)" }}
          />
        </button>
      </div>

      {expanded && (
        <div className="mt-5">
          {loading && <p className="text-gray-500">Đang tải lớp học phần...</p>}
          {error && <p className="text-red-600">{error}</p>}
          {!loading && !error && (
            <table className="w-full text-sm border-collapse mt-4 text-center">
              <thead className="bg-gray-100 text-gray-800">
                <tr>
                  <th className="px-4 py-2 border-b-2 border-gray-300">#</th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Mã lớp
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Tên học phần
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Số TC
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">Loại</th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Phòng
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Giảng viên
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Sĩ số
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Số lượng đã đăng ký
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Bắt đầu
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Kết thúc
                  </th>
                  <th className="px-4 py-2 border-b-2 border-gray-300">
                    Thao tác
                  </th>
                </tr>
              </thead>
              <tbody>
                <DangKyCard lopHocPhan={lopHocPhan || []} onAction={onAction} />
              </tbody>
            </table>
          )}
        </div>
      )}
    </div>
  );
};

export default CourseCard;
