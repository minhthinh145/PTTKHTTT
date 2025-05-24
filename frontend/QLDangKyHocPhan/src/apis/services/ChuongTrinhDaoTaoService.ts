import axiosInstance from "../axiosConfig";
import type { ApiResponse } from "../types/auth";
import type {
  CTDAOTAO,
  ServiceResult,
  ChiTietCTDTDTO,
} from "../types/ChuongTrinhDaoTao";
import type { User } from "../types/user"; // interface User tương ứng SinhVienDTO

export const callGetCTDT = async (
  user: User
): Promise<ApiResponse<CTDAOTAO>> => {
  try {
    const response = await axiosInstance.post<ServiceResult<CTDAOTAO>>(
      "/api/ChuongTrinhDaoTao/getctdt",
      user, // Gửi nguyên object User, axios tự stringify JSON
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    return {
      data: response.data.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message:
        error.response?.data?.message || "Không thể lấy chương trình đào tạo",
      isSuccess: false,
    };
  }
};

export const callGetChiTietCTDT = async (
  user: User
): Promise<ApiResponse<ChiTietCTDTDTO[]>> => {
  try {
    const response = await axiosInstance.post<ServiceResult<ChiTietCTDTDTO[]>>(
      "/api/ChuongTrinhDaoTao/getchitietctdt",
      user,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    return {
      data: response.data.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Không thể lấy chi tiết CTĐT",
      isSuccess: false,
    };
  }
};
