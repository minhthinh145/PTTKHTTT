import { useState } from "react";
import type { CTDAOTAO } from "../../../apis/types/ChuongTrinhDaoTao";
import type { HocPhanDTO } from "../../../apis/types/HocPhan";
import CourseCard from "../../SinhVienPage/RegisterTab/CourseList";
import { useDangKyHocPhan } from "../../../hooks/SinhVien/DKHP/useDangKyHocPhan";
import { useHuyDangKyHocPhan } from "../../../hooks/SinhVien/DKHP/useHuyDangKyHocPhan"; // import hook hủy đăng ký
import type {
  RequestChuyenLopHocPhanDTO,
  RequestDangKyDTO,
} from "../../../apis/types/dangKy";
import type { SinhVienProfile } from "../../../apis/types/user";
import { useGetLopHocPhanDaDangKy } from "../../../hooks/SinhVien/HocPhan/useGetLopHocPhanDaDangKy";
import HocPhanDaDangKyTable from "../../SinhVienPage/RegisterTab/HocPhanDaDangKyTable";
import { useChuyenLopHocPhan } from "../../../hooks/SinhVien/DKHP/useChuyenLopHocPhan";
import { toast } from "react-toastify";

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
      const result = await chuyenLop(request);

      if (result.isSuccess) {
        toast.success("Chuyển lớp thành công!");
        await refreshAll(); // Làm mới cả 2 danh sách
      } else {
        toast.error("Chuyển lớp không thành công: " + result.message);
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
        await refreshAll(); // Làm mới cả 2 danh sách
      } else {
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
        toast.success("Hủy đăng ký thành công!");
        // refreshAll() đã được gọi trong hook useHuyDangKyHocPhan rồi
        await refreshAll();
      } else {
        toast.error("Hủy đăng ký không thành công: " + result.message);
      }
    } catch (err) {}
  };
  return (
    <>
      <h2 className="text-2xl font-bold mb-6 text-gray-800 border-b-2 border-blue-600 pb-2">
        ĐĂNG KÝ HỌC PHẦN
      </h2>
      <div className="bg-white rounded-lg shadow-lg p-6 flex flex-col gap-8">
        {/* Các phần UI khác như chọn chương trình đào tạo, nhóm môn, danh sách học phần chưa đăng ký ... */}

        {/* Danh sách học phần chưa đăng ký */}
        <div className="mt-6 border border-black rounded-lg p-6">
          <h3 className="text-lg font-semibold mb-4">Học phần chưa đăng ký</h3>
          <div className="max-h-96 overflow-y-auto">
            {loading && (
              <p className="text-gray-500">Đang tải hoặc xử lý đăng ký...</p>
            )}
            {error && <p className="text-red-600">{error}</p>}
            {!hocPhanChuaDangKy || hocPhanChuaDangKy.length === 0 ? (
              <p className="text-gray-600">Chưa có học phần để đăng ký.</p>
            ) : (
              hocPhanChuaDangKy.map((hp) => (
                <div key={hp.maHocPhan} className="mb-6">
                  <CourseCard
                    tenHocPhan={hp.tenHocPhan ?? "Không có tên học phần"}
                    maHocPhan={hp.maHocPhan}
                    onAction={handleRegister}
                    actionType="register"
                  />
                </div>
              ))
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
