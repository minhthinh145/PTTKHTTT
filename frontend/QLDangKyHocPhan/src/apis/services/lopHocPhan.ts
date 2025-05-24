import axiosInstance from "../axiosConfig";
import type { LopHocPhanDTO } from "../types//lopHocPhan";
import type { ApiResponse } from "../types//lopHocPhan";

export const callGetAllLopHocPhan = async (): Promise<
  ApiResponse<LopHocPhanDTO[]>
> => {
  try {
    const response = await axiosInstance.get<ApiResponse<LopHocPhanDTO[]>>(
      "/api/lophocphan/getalllophocphan"
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
        error.response?.data?.message || "Lỗi khi lấy danh sách lớp học phần",
      isSuccess: false,
    };
  }
};

interface RequestGetLop {
  MaHocPhan: string;
}

export const callGetLopHocPhanByMaHocPhan = async (
  maHocPhan: string
): Promise<ApiResponse<LopHocPhanDTO[]>> => {
  try {
    const response = await axiosInstance.post<ApiResponse<LopHocPhanDTO[]>>(
      "/api/lophocphan/getLop",
      { MaHocPhan: maHocPhan },
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
        "Lỗi khi lấy lớp học phần theo mã học phần",
      isSuccess: false,
    };
  }
};
