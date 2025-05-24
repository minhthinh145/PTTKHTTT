import type { LopHocPhanDTO } from "../../../apis/types/lopHocPhan";
import React from "react";

interface GiangVienTableProps {
  lopHocPhan: LopHocPhanDTO[];
  loading: boolean;
  error: string | null;
}

const GiangVienTable: React.FC<GiangVienTableProps> = ({ lopHocPhan }) => {
  // Ép thành mảng chuẩn (nếu không phải mảng thì thành mảng rỗng)
  const arr = Array.isArray(lopHocPhan) ? lopHocPhan : [];

  if (arr.length === 0) {
    return <p>Không có lớp học phần nào.</p>;
  }
  return (
    <table className="w-full text-sm border-collapse mt-4">
      <thead className="bg-gray-100 text-gray-800">
        <tr>
          <th className="px-4 py-2 border-b-2 border-gray-300">#</th>
          <th className="px-4 py-2 border-b-2 border-gray-300">Mã lớp</th>
          <th className="px-4 py-2 border-b-2 border-gray-300">Tên học phần</th>
          <th className="px-4 py-2 border-b-2 border-gray-300">Số TC</th>
          <th className="px-4 py-2 border-b-2 border-gray-300">Loại</th>
          <th className="px-4 py-2 border-b-2 border-gray-300">Phòng</th>
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
        {arr.map((lhp, index) => (
          <tr key={lhp.maLopHocPhan}>
            <td className="border px-4 py-2 text-center">{index + 1}</td>
            <td className="border px-4 py-2">{lhp.maLopHocPhan}</td>
            <td className="border px-4 py-2">{lhp.tenHocPhan}</td>
            <td className="border px-4 py-2 text-center">{lhp.soTinChi}</td>
            <td className="border px-4 py-2">{lhp.loaiHocPhan}</td>
            <td className="border px-4 py-2">{lhp.phongHoc}</td>
            <td className="border px-4 py-2 text-center">{lhp.soLuong}</td>
            <td className="border px-4 py-2 text-center">
              {lhp.soLuongDangKy}
            </td>
            <td className="border px-4 py-2">{lhp.ngayBatDau}</td>
            <td className="border px-4 py-2">{lhp.ngayKetThuc}</td>
            <td className="border px-4 py-2 text-center"></td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default GiangVienTable;
