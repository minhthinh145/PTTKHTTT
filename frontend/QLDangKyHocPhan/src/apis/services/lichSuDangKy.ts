import axiosInstance from "../axiosConfig";
import type { ApiResponse } from "../types/lopHocPhan";
import type { DangKyDTO } from "../types/lopHocPhan";

export const callGetLichSuDangKy = async (): Promise<
  ApiResponse<DangKyDTO[]>
> => {
  try {
    const response = await axiosInstance.get<ApiResponse<DangKyDTO[]>>(
      "/api/lichsudangky/getlichsudangky"
    );

    return {
      data: response.data.data,
      message: response.data.message,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Lỗi khi lấy lịch sử đăng ký học phần",
      isSuccess: false,
    };
  }
};
