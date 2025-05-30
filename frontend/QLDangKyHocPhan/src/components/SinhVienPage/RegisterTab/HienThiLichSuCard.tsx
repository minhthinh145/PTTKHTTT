import React from "react";
import type { DangKyDTO } from "../../../apis/types/lopHocPhan";
interface HienThiLopCardProps {
  lopHocPhan?: DangKyDTO[];
}

export const HienThiLichSuCard: React.FC<HienThiLopCardProps> = ({
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
          <td className="px-4 py-2 border border-gray-300">{item.maLopHP}</td>
          <td className="px-4 py-2 border border-gray-300">
            {item.tenHocPhan || "-"}
          </td>
          <td className="px-4 py-2 border border-gray-300">
            {item.loaiDangKy === "Đăng ký" ? "Đăng ký" : "Hủy đăng ký"}
          </td>
          <td className="px-4 py-2 border border-gray-300">
            {item.ngayThucHien
              ? new Date(item.ngayThucHien).toLocaleString("vi-VN", {
                  day: "2-digit",
                  month: "2-digit",
                  year: "numeric",
                  hour: "2-digit",
                  minute: "2-digit",
                  second: "2-digit",
                  hour12: false,
                })
              : "-"}
          </td>
        </tr>
      ))}
    </>
  );
};
