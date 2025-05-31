import axiosInstance from "../axiosConfig";
import type { TaiKhoanDTO } from "../types/taiKhoan";

export const getAccountsByRole = async (
  role: string
): Promise<TaiKhoanDTO[]> => {
  const response = await axiosInstance.get<{ data: TaiKhoanDTO[] }>(
    `/api/phongdaotao/users-by-role`,
    { params: { role } }
  );
  // Nếu API trả về { data: [...] }
  return response.data.data;
};
