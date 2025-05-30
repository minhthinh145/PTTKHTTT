import axiosInstance from "../axiosConfig";
import type { SinhVienProfile, ApiResponse } from "../types/user";
import type { GiangVienProfile } from "../types/giangvien";

// Định nghĩa type cho response từ API
interface ServiceResult<T> {
  isSuccess: boolean;
  message: string;
  data: T;
}

export const getProfile = async (
  accessToken: string
): Promise<ApiResponse<SinhVienProfile>> => {
  try {
    const response = await axiosInstance.get<ServiceResult<SinhVienProfile>>(
      "/api/auth/profile",
      {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      }
    );
    return {
      data: response.data.data, // Trả về User
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    console.error("getProfile error:", error.response?.data || error);
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Failed to get profile",
    };
  }
};

export const getGiangVienProfile = async (
  accessToken: string
): Promise<ApiResponse<GiangVienProfile>> => {
  try {
    const response = await axiosInstance.get<ServiceResult<GiangVienProfile>>(
      "/api/auth/profile",
      {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      }
    );

    return {
      data: response.data.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    console.error("getGiangVienProfile error:", error.response?.data || error);
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Không thể lấy thông tin giảng viên",
      isSuccess: false,
    };
  }
};
