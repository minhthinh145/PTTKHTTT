import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { callGetLichSuDangKy } from "../apis/services/lichSuDangKy";
import type { LopHocPhanDTO, ApiResponse } from "../apis/types/lopHocPhan";
import type { DangKyDTO } from "../apis/types/lopHocPhan";

interface UseGetLichSuDangKyResult {
  lichSu: DangKyDTO[] | null;
  loading: boolean;
  error: string | null;
  refresh: () => void;
}

export const useGetLichSuDangKy = (): UseGetLichSuDangKyResult => {
  const [lichSu, setLichSu] = useState<DangKyDTO[] | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [refreshKey, setRefreshKey] = useState<number>(0);

  const fetchLichSu = async () => {
    setLoading(true);
    setError(null);

    try {
      const response: ApiResponse<DangKyDTO[]> = await callGetLichSuDangKy();
      if (response.isSuccess && response.data) {
        setLichSu(response.data);
      } else {
        const errorMessage =
          response.message || "Lỗi không xác định khi lấy lịch sử đăng ký";
        setError(errorMessage);
        toast.error(errorMessage, { position: "top-right" });
      }
    } catch (err) {
      const errorMessage = "Lỗi khi lấy lịch sử đăng ký học phần";
      setError(errorMessage);
      toast.error(errorMessage, { position: "top-right" });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchLichSu();
  }, [refreshKey]);

  const refresh = () => {
    setRefreshKey((prev) => prev + 1);
  };

  return { lichSu, loading, error, refresh };
};
