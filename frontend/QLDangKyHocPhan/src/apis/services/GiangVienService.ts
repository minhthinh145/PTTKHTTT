import axiosInstance from "../axiosConfig";
import type { ApiResponse } from "../types/lopHocPhan";
import type { LopHocPhanDTO } from "../types/lopHocPhan";

export const getLopHocPhanByGiangVien = async (): Promise<
  ApiResponse<LopHocPhanDTO[]>
> => {
  try {
    const response = await axiosInstance.get<any>("/api/GiangVien/lophocphan");

    // Lấy mảng từ response.data.data["$values"]
    const dataArray = response.data?.data?.["$values"] || [];

    return {
      data: dataArray,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
      message: response.data?.message || "",
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Không thể lấy danh sách lớp học phần",
      isSuccess: false,
    };
  }
};
