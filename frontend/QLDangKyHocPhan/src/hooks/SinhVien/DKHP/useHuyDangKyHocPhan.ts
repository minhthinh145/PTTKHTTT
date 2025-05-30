import { useState } from "react";
import { huyDangKyHocPhan } from "../../../apis/services/DangKyService";
import type {
  RequestDangKyDTO,
  ApiResponse,
  ServiceResult,
} from "../../../apis/types/dangKy";

interface HuyDangKyState {
  response: ApiResponse<ServiceResult> | null;
  loading: boolean;
  error: string | null;
}

export const useHuyDangKyHocPhan = (refresh: () => Promise<void>) => {
  const [state, setState] = useState<HuyDangKyState>({
    response: null,
    loading: false,
    error: null,
  });

  const huyDangKy = async (
    huyData: RequestDangKyDTO
  ): Promise<ApiResponse<ServiceResult>> => {
    setState({ response: null, loading: true, error: null });

    try {
      const result = await huyDangKyHocPhan(huyData);
      setState({ response: result, loading: false, error: null });

      if (result.isSuccess) {
        await refresh();
      }

      return result;
    } catch (error: any) {
      const errorMessage =
        error?.response?.data?.message ||
        error.message ||
        "Hủy đăng ký thất bại";
      setState({ response: null, loading: false, error: errorMessage });
      throw new Error(errorMessage);
    }
  };

  return {
    huyDangKy,
    response: state.response,
    loading: state.loading,
    error: state.error,
  };
};
