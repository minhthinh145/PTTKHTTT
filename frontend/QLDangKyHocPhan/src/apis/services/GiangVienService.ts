import axiosInstance from "../axiosConfig";
import type { ApiResponse } from "../types/lopHocPhan";
import type { LopHocPhanDTO } from "../types/lopHocPhan";
import type { SinhVienProfile } from "../types/user";

export const getLopHocPhanByGiangVien = async (): Promise<
  ApiResponse<LopHocPhanDTO[]>
> => {
  try {
    const response = await axiosInstance.get<any>("/api/GiangVien/lophocphan");

    // Lấy mảng từ response.data.data["$values"]

    return {
      data: response.data.data,
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

export const getSinhVienByMaLopHocPhan = async (
  maLopHocPhan: string
): Promise<ApiResponse<SinhVienProfile[]>> => {
  try {
    const response = await axiosInstance.post<any>(
      "/api/GiangVien/sinhvien-by-lophocphan",
      { maLopHocPhan }
    );
    return {
      data: response.data.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
      message: response.data?.message || "",
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Không thể lấy danh sách sinh viên",
      isSuccess: false,
    };
  }
};
