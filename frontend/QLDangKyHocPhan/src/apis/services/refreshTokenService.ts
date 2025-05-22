import axiosInstance from "../axiosConfig";
import type {
  RefreshTokenRequestDTO,
  TokenResponseDTO,
  ApiResponse,
} from "../types/auth";

export const callRefreshToken = async (
  refreshTokenData: RefreshTokenRequestDTO
): Promise<ApiResponse<TokenResponseDTO>> => {
  try {
    const response = await axiosInstance.post<TokenResponseDTO>(
      "/api/auth/refresh-token",
      refreshTokenData
    );

    return {
      data: response.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300, // thêm trường này cho rõ
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Failed to refresh token",
    };
  }
};
