import axiosInstance from "../axiosConfig";
import type { UpdatePasswordRequest } from "../types/auth";
import type { SinhVienProfile } from "../types/user";
import type { ApiResponse } from "../types/auth";

export const updatePassword = async (data: UpdatePasswordRequest) => {
  return axiosInstance.post("/api/PhongDaoTao/update-password", data);
};

export const getSinhVienByMa = async (
  maSinhVien: string
): Promise<ApiResponse<SinhVienProfile>> => {
  try {
    const response = await axiosInstance.get<SinhVienProfile>(
      `/api/SinhVien/${maSinhVien}`
    );
    return {
      data: response.data,
      status: response.status,
      isSuccess: response.status >= 200 && response.status < 300,
    };
  } catch (error: any) {
    return {
      status: error.response?.status || 500,
      message: error.response?.data?.message || "Không tìm thấy sinh viên",
    };
  }
};
