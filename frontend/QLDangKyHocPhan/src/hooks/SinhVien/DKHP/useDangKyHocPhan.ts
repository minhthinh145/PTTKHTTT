import { useState, useEffect } from "react";
import { dangKyHocPhan } from "../../../apis/services/DangKyService";
import { callGetHocPhanChuaDangKy } from "../../../apis/services/hocPhanService";
import type {
  RequestDangKyDTO,
  ApiResponse,
  ServiceResult,
} from "../../../apis/types/dangKy";
import type { HocPhanDTO } from "../../../apis/types/HocPhan";
import type { SinhVienProfile } from "../../../apis/types/user";

interface DangKyState {
  response: ApiResponse<ServiceResult> | null;
  hocPhanChuaDangKy: HocPhanDTO[] | null;
  loading: boolean;
  error: string | null;
}

export const useDangKyHocPhan = (user: SinhVienProfile) => {
  const [state, setState] = useState<DangKyState>({
    response: null,
    hocPhanChuaDangKy: null,
    loading: false,
    error: null,
  });

  // Hàm làm mới danh sách học phần
  const fetchHocPhan = async () => {
    setState((prev) => ({ ...prev, loading: true, error: null }));
    const result = await callGetHocPhanChuaDangKy(user);
    if (result.isSuccess && result.data !== undefined) {
      setState((prev) => ({
        ...prev,
        hocPhanChuaDangKy: result.data as HocPhanDTO[],
        loading: false,
        error: null,
      }));
    } else {
      setState((prev) => ({
        ...prev,
        hocPhanChuaDangKy: null, // Explicitly set to null on failure
        loading: false,
        error: result.message || "Lỗi khi tải danh sách học phần",
      }));
    }
  };

  // Gọi lần đầu khi hook được sử dụng
  useEffect(() => {
    fetchHocPhan();
  }, [user]);

  const dangKy = async (dangKyData: RequestDangKyDTO) => {
    setState((prev) => ({
      ...prev,
      response: null,
      loading: true,
      error: null,
    }));

    try {
      const result = await dangKyHocPhan(dangKyData);
      setState((prev) => ({
        ...prev,
        response: result,
        loading: false,
        error: null,
      }));
      if (result.isSuccess) {
        await fetchHocPhan(); // Làm mới danh sách sau khi đăng ký thành công
      }
      return result;
    } catch (error: any) {
      const errorMessage = error.message || "Đăng ký thất bại";
      setState((prev) => ({
        ...prev,
        response: null,
        loading: false,
        error: errorMessage,
      }));
      throw new Error(errorMessage);
    }
  };

  return {
    dangKy,
    response: state.response,
    hocPhanChuaDangKy: state.hocPhanChuaDangKy,
    loading: state.loading,
    error: state.error,
    refresh: fetchHocPhan,
  };
};
