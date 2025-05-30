import React from "react";
import type { LopHocPhanDTO } from "../../apis/types/lopHocPhan";
interface HienThiLopCardProps {
  lopHocPhan?: LopHocPhanDTO[];
}

export const HienThiLopCard: React.FC<HienThiLopCardProps> = ({
  lopHocPhan,
}) => {
  if (!lopHocPhan || lopHocPhan.length === 0) {
    return (
      <tr className="bg-gray-50 text-center">
        <td className="px-4 py-2" colSpan={11}>
          Chưa có dữ liệu
        </td>
      </tr>
    );
  }

  return (
    <>
      {lopHocPhan.map((item, index) => (
        <tr key={index} className={index % 2 === 0 ? "bg-gray-50" : ""}>
          <td className="px-4 py-2 text-center border border-gray-300">
            {index + 1}
          </td>
          <td className="px-4 py-2 border border-gray-300">
            {item.maHocPhan || "-"}
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
        </tr>
      ))}
    </>
  );
};
