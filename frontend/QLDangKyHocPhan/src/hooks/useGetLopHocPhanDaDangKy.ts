import { useEffect, useState } from "react";
import type { LopHocPhanDTO } from "../apis/types/lopHocPhan";
import { callGetLopHocPhanDaDangKy } from "../apis/services/hocPhanDangKyService";

export const useGetLopHocPhanDaDangKy = () => {
  const [state, setState] = useState<{
    data: LopHocPhanDTO[] | null;
    loading: boolean;
    error: string | null;
  }>({
    data: null,
    loading: false,
    error: null,
  });

  const fetchDaDangKy = async () => {
    setState({ data: null, loading: true, error: null });
    try {
      const result = await callGetLopHocPhanDaDangKy(); // API của bạn
      if (result.isSuccess && result.data) {
        setState({ data: result.data, loading: false, error: null });
      } else {
        setState({
          data: null,
          loading: false,
          error: result.message || "Lỗi",
        });
      }
    } catch (e: any) {
      setState({ data: null, loading: false, error: e.message || "Lỗi" });
    }
  };

  useEffect(() => {
    fetchDaDangKy();
  }, []);

  return {
    data: state.data,
    loading: state.loading,
    error: state.error,
    refresh: fetchDaDangKy, // export refresh
  };
};
