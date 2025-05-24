import axiosInstance from "../axiosConfig";
import type { User, ApiResponse } from "../types/user";

// Định nghĩa type cho response từ API
interface ServiceResult<T> {
  isSuccess: boolean;
  message: string;
  data: T;
}

export const getProfile = async (
  accessToken: string
): Promise<ApiResponse<User>> => {
  try {
    const response = await axiosInstance.get<ServiceResult<User>>(
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
