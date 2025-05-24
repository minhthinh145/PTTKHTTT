import axiosInstance from "../axiosConfig";
import type { LopHocPhanDTO } from "../types/lopHocPhan";
import type { ApiResponse } from "../types/lopHocPhan";

export const callGetLopHocPhanDaDangKy = async (): Promise<
  ApiResponse<LopHocPhanDTO[]>
> => {
  try {
    const response = await axiosInstance.get<ApiResponse<LopHocPhanDTO[]>>(
      "/api/hocphandangky/GetAll"
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
        "Lỗi khi lấy danh sách lớp học phần đã đăng ký",
      isSuccess: false,
    };
  }
};
