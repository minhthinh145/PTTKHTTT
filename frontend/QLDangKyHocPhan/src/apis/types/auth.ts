export interface SignInDTO {
  tenDangNhap: string;
  password: string;
}

export interface TokenResponseDTO {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshTokenRequestDTO {
  refreshToken: string;
}

export interface ApiResponse<T> {
  data?: T | null;
  message?: string;
  status: number;
  isSuccess?: boolean;
}

export interface ErrorResponse {
  message: string;
}

export interface SignOutRequestDTO {
  RefreshToken: string; // chữ hoa R và T như backend
}
