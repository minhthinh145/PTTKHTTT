import { useState } from "react";
import { chuyenLopHocPhan } from "../apis/services/DangKyService";
import type {
  ApiResponse,
  ServiceResult,
  RequestChuyenLopHocPhanDTO,
} from "../apis/types/dangKy";

export const useChuyenLopHocPhan = (refresh: () => Promise<void>) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const chuyenLop = async (
    data: RequestChuyenLopHocPhanDTO
  ): Promise<ApiResponse<ServiceResult>> => {
    setLoading(true);
    setError(null);
    try {
      const result = await chuyenLopHocPhan(data);
      if (result.isSuccess) {
        await refresh();
      }
      return result;
    } catch (error: any) {
      const message =
        error?.response?.data?.message || error.message || "Lỗi chuyển lớp";
      setError(message);
      throw new Error(message);
    } finally {
      setLoading(false);
    }
  };

  return { chuyenLop, loading, error };
};
