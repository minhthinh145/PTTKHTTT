import React, { useState } from "react";
import type { CTDAOTAO } from "../../apis/types/ChuongTrinhDaoTao";
import SelectOption from "./RegisterTab/SelectOption";
import type { HocPhanDTO } from "../../apis/types/HocPhan";
import CourseCard from "./RegisterTab/CourseList";
import { useDangKyHocPhan } from "../../hooks/SinhVien/DKHP/useDangKyHocPhan";
import { useHuyDangKyHocPhan } from "../../hooks/SinhVien/DKHP/useHuyDangKyHocPhan"; // import hook hủy đăng ký
import type {
  RequestChuyenLopHocPhanDTO,
  RequestDangKyDTO,
} from "../../apis/types/dangKy";
import type { SinhVienProfile } from "../../apis/types/user";
import { useGetLopHocPhanDaDangKy } from "../../hooks/SinhVien/HocPhan/useGetLopHocPhanDaDangKy";
import HocPhanDaDangKyTable from "./RegisterTab/HocPhanDaDangKyTable";
import { useChuyenLopHocPhan } from "../../hooks/SinhVien/DKHP/useChuyenLopHocPhan";

interface Props {
  ctdt: CTDAOTAO | null;
  user: SinhVienProfile | null;
  hocPhanChuaDangKy?: HocPhanDTO[]; // <-- thêm dòng này
}

const RegisterTabContent = ({ ctdt, user }: Props) => {
  const [selectedGroup, setSelectedGroup] =
    useState<string>("trongChuongTrinh");

  // Hook đăng ký học phần
  const {
    dangKy,
    hocPhanChuaDangKy,
    loading,
    error,
    refresh: refreshChuaDangKy,
  } = useDangKyHocPhan(user!);

  // Hook hủy đăng ký học phần, truyền vào hàm refresh của hook đăng ký để làm mới dữ liệu
  const {
    huyDangKy,
    loading: loadingHuy,
    error: errorHuy,
  } = useHuyDangKyHocPhan(refreshChuaDangKy);

  // Hook lấy danh sách học phần đã đăng ký
  const {
    data: hocPhanDaDangKy,
    loading: loadingDaDangKy,
    error: errorDaDangKy,

    refresh: refreshDaDangKy,
  } = useGetLopHocPhanDaDangKy();

  //Hook chuyển lớp
  const refreshAll = async () => {
    await Promise.all([refreshChuaDangKy(), refreshDaDangKy()]);
  };

  const {
    chuyenLop,
    loading: loadingChuyen,
    error: errorChuyen,
  } = useChuyenLopHocPhan(refreshAll);

  // Xử lý chuyển lớp học phần
  const handleChuyenLop = async (request: RequestChuyenLopHocPhanDTO) => {
    try {
      const result = await chuyenLop(request); // ✅ Truyền đúng 1 object

      if (result.isSuccess) {
        console.log("Chuyển lớp thành công:", result.data);
        await refreshAll(); // Làm mới cả 2 danh sách
      } else {
        console.log("Chuyển lớp thất bại:", result.message);
      }
    } catch (err) {
      console.error("Lỗi khi chuyển lớp:", err);
    }
  };

  // Xử lý đăng ký
  const handleRegister = async (maLopHocPhan: string) => {
    const dangKyData: RequestDangKyDTO = { maLopHocPhan };
    try {
      const result = await dangKy(dangKyData);
      if (result.isSuccess) {
        console.log("Đăng ký thành công:", result.data);
        await refreshAll(); // Làm mới cả 2 danh sách
      } else {
        console.log("Đăng ký thất bại:", result.message);
      }
    } catch (err) {
      console.error("Lỗi khi đăng ký:", err);
    }
  };

  // Xử lý hủy đăng ký
  const handleCancel = async (maLopHocPhan: string) => {
    const huyData: RequestDangKyDTO = { maLopHocPhan };
    try {
      const result = await huyDangKy(huyData);
      if (result.isSuccess) {
        console.log("Hủy đăng ký thành công:", result.data);
        // refreshAll() đã được gọi trong hook useHuyDangKyHocPhan rồi,
        // nhưng gọi lại ở đây cũng được nếu muốn đảm bảo
        await refreshAll();
      } else {
        console.log("Hủy đăng ký thất bại:", result.message);
      }
    } catch (err) {
      console.error("Lỗi khi hủy đăng ký:", err);
    }
  };
  return (
    <>
      <h2 className="text-2xl font-bold mb-4">ĐĂNG KÝ HỌC PHẦN</h2>
      <div className="bg-white rounded shadow p-6">
        <div className="flex items-center gap-4 mb-4">
          <select className="border p-2 rounded w-64 cursor-pointer">
            <option>Chương trình đào tạo</option>
          </select>
          <button className="bg-blue-700 text-white hover:bg-blue-500 transition-colors duration-200 px-4 py-2 rounded cursor-pointer">
            Làm mới
          </button>
        </div>
      </div>
      <div className="bg-white rounded-lg shadow-lg p-6 flex flex-col gap-8">
        {/* Các phần UI khác như chọn chương trình đào tạo, nhóm môn, danh sách học phần chưa đăng ký ... */}

        {/* Danh sách học phần chưa đăng ký */}
        {/* Danh sách học phần chưa đăng ký */}
        <div className="mt-8 border border-blue-300 rounded-lg bg-white p-5">
          <h3 className="text-lg font-semibold mb-3 text-blue-700 flex items-center gap-2">
            <svg
              className="w-5 h-5 text-blue-500"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              viewBox="0 0 24 24"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M12 4v16m8-8H4"
              />
            </svg>
            Học phần chưa đăng ký
            <span className="ml-2 text-xs text-gray-500 font-normal">
              {hocPhanChuaDangKy && hocPhanChuaDangKy.length > 0
                ? `(${hocPhanChuaDangKy.length} học phần)`
                : ""}
            </span>
          </h3>
          <div className="max-h-96 overflow-y-auto">
            {loading ? (
              <div className="flex items-center justify-center h-20 text-blue-600">
                <svg className="animate-spin h-5 w-5 mr-2" viewBox="0 0 24 24">
                  <circle
                    className="opacity-25"
                    cx="12"
                    cy="12"
                    r="10"
                    stroke="currentColor"
                    strokeWidth="4"
                  ></circle>
                  <path
                    className="opacity-75"
                    fill="currentColor"
                    d="M4 12a8 8 0 018-8v8z"
                  ></path>
                </svg>
                Đang tải hoặc xử lý đăng ký...
              </div>
            ) : error ? (
              <div className="text-red-600 text-center py-4">{error}</div>
            ) : !hocPhanChuaDangKy || hocPhanChuaDangKy.length === 0 ? (
              <div className="text-gray-500 text-center py-4">
                Chưa có học phần để đăng ký.
              </div>
            ) : (
              <ul className="space-y-4">
                {hocPhanChuaDangKy.map((hp) => (
                  <li key={hp.maHocPhan}>
                    <CourseCard
                      tenHocPhan={hp.tenHocPhan ?? "Không có tên học phần"}
                      maHocPhan={hp.maHocPhan}
                      onAction={handleRegister}
                      actionType="register"
                    />
                  </li>
                ))}
              </ul>
            )}
          </div>
        </div>

        {/* Danh sách học phần đã đăng ký */}
        <div className="mt-6 border border-green-600 rounded-lg p-6">
          <h3 className="text-lg font-semibold mb-4 text-green-700">
            Học phần đã đăng ký
          </h3>
          <div className="max-h-96 overflow-y-auto">
            {loadingDaDangKy && (
              <p className="text-gray-500">Đang tải danh sách đã đăng ký...</p>
            )}
            {!hocPhanDaDangKy || hocPhanDaDangKy.length === 0 ? (
              <p className="text-gray-600">Bạn chưa đăng ký học phần nào.</p>
            ) : (
              <HocPhanDaDangKyTable
                hocPhanDaDangKy={hocPhanDaDangKy}
                onCancel={handleCancel} // Truyền hàm hủy đăng ký vào table
                onChuyenLop={handleChuyenLop} // Truyền hàm chuyển lớp
              />
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default RegisterTabContent;
