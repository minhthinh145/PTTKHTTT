import React from "react";
import type { LopHocPhanDTO } from "../../../../apis/types/lopHocPhan";

interface HocPhanDangKyCardProps {
  lopHocPhan: LopHocPhanDTO;
  index: number;
  onCancel?: (maLopHocPhan: string) => void;
  onChangeClass?: (maLopHocPhan: string) => void;
}

const HocPhanDangKyCard: React.FC<HocPhanDangKyCardProps> = ({
  lopHocPhan,
  index,
  onCancel,
  onChangeClass,
}) => {
  return (
    <tr className="border-b">
      <td className="px-4 py-2">{index}</td>
      <td className="px-4 py-2">{lopHocPhan.maLopHocPhan}</td>
      <td className="px-4 py-2">{lopHocPhan.tenHocPhan ?? "Chưa có tên"}</td>
      <td className="px-4 py-2">{lopHocPhan.soTinChi ?? "-"}</td>
      <td className="px-4 py-2">{lopHocPhan.loaiHocPhan ?? "-"}</td>
      <td className="px-4 py-2">{lopHocPhan.phongHoc ?? "-"}</td>
      <td className="px-4 py-2">{lopHocPhan.tenGiangVien ?? "-"}</td>
      <td className="px-4 py-2">
        {lopHocPhan.ngayBatDau
          ? new Date(lopHocPhan.ngayBatDau).toLocaleDateString()
          : "-"}
      </td>
      <td className="px-4 py-2">
        {lopHocPhan.ngayKetThuc
          ? new Date(lopHocPhan.ngayKetThuc).toLocaleDateString()
          : "-"}
      </td>
      <td className="px-4 py-2 space-x-2">
        <button
          className="px-2 py-1 bg-red-500 text-white rounded hover:bg-red-600 transition"
          onClick={() => onCancel?.(lopHocPhan.maLopHocPhan)}
        >
          Hủy
        </button>
        <button
          className="px-2 py-1 bg-yellow-500 text-white rounded hover:bg-yellow-600 transition"
          onClick={() => onChangeClass?.(lopHocPhan.maLopHocPhan)}
        >
          Chuyển lớp
        </button>
      </td>
    </tr>
  );
};

export default HocPhanDangKyCard;
