import React, { useState } from "react";
import type { LopHocPhanDTO } from "../../../apis/types/lopHocPhan";
import ChuyenLopPopup from "./ChuyenLopPopup";

interface HocPhanDaDangKyTableProps {
  hocPhanDaDangKy: LopHocPhanDTO[];
  onCancel: (maLopHocPhan: string) => void;
  onChuyenLop: (request: {
    maLopHocPhanCu: string;
    maLopHocPhanMoi: string;
  }) => Promise<void>;
}

const HocPhanDaDangKyTable: React.FC<HocPhanDaDangKyTableProps> = ({
  hocPhanDaDangKy,
  onCancel,
  onChuyenLop,
}) => {
  const [selectedHocPhan, setSelectedHocPhan] = useState<LopHocPhanDTO | null>(
    null
  );

  return (
    <div>
      <table className="w-full text-sm border-collapse">
        <thead className="bg-gray-100 text-gray-800">
          <tr>
            <th className="px-4 py-2 border-b-2 border-gray-300">#</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Mã lớp</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">
              Tên học phần
            </th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Số TC</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Loại</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Phòng</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Giảng viên</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Sĩ số</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">
              Số lượng đã đăng ký
            </th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Bắt đầu</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Kết thúc</th>
            <th className="px-4 py-2 border-b-2 border-gray-300">Thao tác</th>
          </tr>
        </thead>
        <tbody>
          {hocPhanDaDangKy.map((item, index) => (
            <tr
              key={item.maLopHocPhan}
              className={index % 2 === 0 ? "bg-gray-50" : "bg-white"}
            >
              <td className="px-4 py-2 text-center border border-gray-300">
                {index + 1}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.maLopHocPhan}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.tenHocPhan || "-"}
              </td>
              <td className="px-4 py-2 text-center border border-gray-300">
                {item.soTinChi || "-"}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.loaiHocPhan || "-"}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.phongHoc || "-"}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.tenGiangVien || "-"}
              </td>
              <td className="px-4 py-2 text-center border border-gray-300">
                {item.soLuong || "-"}
              </td>
              <td className="px-4 py-2 text-center border border-gray-300">
                {item.soLuongDangKy ?? "-"}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.ngayBatDau
                  ? new Date(item.ngayBatDau).toLocaleDateString()
                  : "-"}
              </td>
              <td className="px-4 py-2 border border-gray-300">
                {item.ngayKetThuc
                  ? new Date(item.ngayKetThuc).toLocaleDateString()
                  : "-"}
              </td>
              <td className="px-4 py-2 text-center border border-gray-300">
                <button
                  className="bg-red-600 text-white px-3 py-1 rounded-lg hover:bg-red-700 mr-2"
                  onClick={() => onCancel(item.maLopHocPhan)}
                >
                  Hủy
                </button>
                <button
                  className="bg-blue-600 text-white px-3 py-1 rounded-lg hover:bg-blue-700"
                  onClick={() => setSelectedHocPhan(item)}
                >
                  Chuyển lớp
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {selectedHocPhan && selectedHocPhan.maHocPhan && (
        <ChuyenLopPopup
          maHocPhan={selectedHocPhan.maHocPhan}
          maLopHocPhanCu={selectedHocPhan.maLopHocPhan}
          tenHocPhan={selectedHocPhan.tenHocPhan || ""}
          onChuyenLop={onChuyenLop}
          onClose={() => setSelectedHocPhan(null)}
        />
      )}
    </div>
  );
};

export default HocPhanDaDangKyTable;
