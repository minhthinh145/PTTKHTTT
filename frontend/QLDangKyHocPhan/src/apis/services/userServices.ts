import axiosInstance from "../axiosConfig";
import type { User, ApiResponse } from "../types/user";

export const getProfile = async (
  accessToken: string
): Promise<ApiResponse<User>> => {
  try {
    const response = await axiosInstance.get<User>("/api/auth/profile", {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    });
    return {
      data: response.data,
      status: response.status,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Failed to get profile",
    };
  }
};
