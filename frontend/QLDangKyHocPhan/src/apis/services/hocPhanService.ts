import axiosInstance from "../axiosConfig";
import type { ApiResponse } from "../types/auth";
import type { HocPhanDTO } from "../types/HocPhan";
import type { SinhVienProfile } from "../../apis/types/user";

export const callGetHocPhanChuaDangKy = async (
  user: SinhVienProfile
): Promise<ApiResponse<HocPhanDTO[]>> => {
  try {
    const response = await axiosInstance.post<ApiResponse<HocPhanDTO[]>>(
      "/api/hocphan/hpdangky",
      user, 
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
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
        error.response?.data?.message ||
        "Lỗi khi lấy danh sách học phần chưa đăng ký",
      isSuccess: false,
    };
  }
};
