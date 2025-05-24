import axiosInstance from "../axiosConfig";
import type {
  RequestDangKyDTO,
  ApiResponse,
  ServiceResult,
  RequestChuyenLopHocPhanDTO,
} from "../types/dangKy";

export const dangKyHocPhan = async (
  dangKyData: RequestDangKyDTO
): Promise<ApiResponse<ServiceResult>> => {
  try {
    const response = await axiosInstance.post<ServiceResult>(
      "/api/Dangky/dangky",
      dangKyData
    );

    return {
      data: response.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Failed to register course",
      isSuccess: false,
    };
  }
};

export const huyDangKyHocPhan = async (
  huyDangKyHocPhanData: RequestDangKyDTO
): Promise<ApiResponse<ServiceResult>> => {
  try {
    const response = await axiosInstance.post<ServiceResult>(
      "/api/Dangky/huy",
      huyDangKyHocPhanData
    );

    return {
      data: response.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Failed to cancel course registration",
      isSuccess: false,
    };
  }
};

export const chuyenLopHocPhan = async (
  chuyenLopHocPhanData: RequestChuyenLopHocPhanDTO
): Promise<ApiResponse<ServiceResult>> => {
  try {
    const response = await axiosInstance.post<ServiceResult>(
      "/api/Dangky/chuyen",
      chuyenLopHocPhanData
    );

    return {
      data: response.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Failed to transfer course section",
      isSuccess: false,
    };
  }
};
