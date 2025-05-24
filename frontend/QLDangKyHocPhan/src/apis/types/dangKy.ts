// DTO cho request đăng ký học phần
export interface RequestDangKyDTO {
  maLopHocPhan: string;
}

// Định nghĩa ServiceResult từ backend
export interface ServiceResult {
  isSuccess: boolean;
  message?: string;
  data?: any; // Có thể mở rộng thêm nếu backend trả về dữ liệu cụ thể
}

export interface RequestChuyenLopHocPhanDTO {
  maLopHocPhanCu: string;
  maLopHocPhanMoi: string;
}

// Định nghĩa API response
export interface ApiResponse<T = any> {
  data?: T;
  status: number;
  message?: string;
  isSuccess?: boolean;
}
