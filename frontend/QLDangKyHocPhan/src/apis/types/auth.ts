export interface SignInDTO{
    tenDangNhap : string;
    password : string;
}

export interface TokenResponseDTO{
    accessToken : string;
    refreshToken : string;
}

export interface ApiResponse<T> {
  data?: T;
  message?: string;
  status: number;
}

export interface ErrorResponse {
  message: string;
}