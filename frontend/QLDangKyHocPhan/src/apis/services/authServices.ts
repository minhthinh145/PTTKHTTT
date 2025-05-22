import axiosInstance from "../axiosConfig";
import type {
  SignInDTO,
  TokenResponseDTO,
  ApiResponse,
  SignOutRequestDTO,
} from "../types/auth";

export const signIn = async (
  signInData: SignInDTO
): Promise<ApiResponse<TokenResponseDTO>> => {
  try {
    const response = await axiosInstance.post<TokenResponseDTO>(
      "/api/auth/signin",
      signInData
    );
    return {
      data: response.data,
      status: response.status,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Failed to sign in",
    };
  }
};

export const signOut = async (
  refreshToken: string
): Promise<ApiResponse<{ message: string }>> => {
  try {
    const requestData: SignOutRequestDTO = { RefreshToken: refreshToken };
    const response = await axiosInstance.post<{ message: string }>(
      "/api/auth/signout",
      requestData
    );

    return {
      data: response.data,
      status: response.status,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Failed to sign out",
    };
  }
};
