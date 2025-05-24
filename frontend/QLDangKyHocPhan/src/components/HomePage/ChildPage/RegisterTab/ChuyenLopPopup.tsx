import React from "react";
import { useGetLopHocPhanByMaHocPhan } from "../../../../hooks/useGetLopHocPhanByMaHocPhan";
import type { LopHocPhanDTO } from "../../../../apis/types/lopHocPhan";
import DangKyCard from "./DangKyCard";
import { toast } from "react-toastify";

interface ChuyenLopPopupProps {
  maHocPhan: string | null;
  maLopHocPhanCu: string;
  tenHocPhan: string;
  onChuyenLop: (request: {
    maLopHocPhanCu: string;
    maLopHocPhanMoi: string;
  }) => Promise<void>;
  onClose: () => void;
}

const ChuyenLopPopup: React.FC<ChuyenLopPopupProps> = ({
  maHocPhan,
  maLopHocPhanCu,
  tenHocPhan,
  onChuyenLop,
  onClose,
}) => {
  const { lopHocPhan, loading, error } = useGetLopHocPhanByMaHocPhan(
    maHocPhan,
  );

  const handleChuyenLop = async (maLopHocPhanMoi: string) => {
    try {
      await onChuyenLop({ maLopHocPhanCu, maLopHocPhanMoi });
      toast.success("Chuyển lớp thành công!", { position: "top-right" });
      onClose();
    } catch (err) {
      console.error("Lỗi khi chuyển lớp:", err);
      toast.error(
        "Lỗi khi chuyển lớp: " +
          (err instanceof Error ? err.message : "Unknown error"),
        { position: "top-right" }
      );
    }
  };

  if (!maHocPhan) {
    return (
      <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
        <div className="bg-white rounded-lg p-6 w-full max-w-4xl">
          <div className="flex justify-between items-center mb-4">
            <h2 className="text-xl font-semibold">Lỗi</h2>
            <button
              onClick={onClose}
              className="text-gray-600 hover:text-gray-800 text-2xl"
              aria-label="Đóng"
            >
              ×
            </button>
          </div>
          <p className="text-red-600">Không tìm thấy mã học phần.</p>
        </div>
      </div>
    );
  }

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg p-6 w-full max-w-4xl max-h-[80vh] overflow-y-auto">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-xl font-semibold">
            Chuyển lớp cho: {tenHocPhan}
          </h2>
          <button
            onClick={onClose}
            className="text-gray-600 hover:text-gray-800 text-2xl"
            aria-label="Đóng"
          >
            ×
          </button>
        </div>

        {loading && (
          <div className="animate-pulse">
            <div className="h-4 bg-gray-200 rounded w-3/4 mb-2"></div>
            <div className="h-4 bg-gray-200 rounded w-1/2"></div>
          </div>
        )}
        {error && <p className="text-red-600">{error}</p>}
        {!loading && !error && (
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
                <th className="px-4 py-2 border-b-2 border-gray-300">
                  Giảng viên
                </th>
                <th className="px-4 py-2 border-b-2 border-gray-300">Sĩ số</th>
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
              <DangKyCard
                lopHocPhan={
                  lopHocPhan?.filter(
                    (item) => item.maLopHocPhan !== maLopHocPhanCu
                  ) || []
                }
                onAction={handleChuyenLop}
                actionType="chuyenLop" // Pass actionType for button text
              />
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default ChuyenLopPopup;
