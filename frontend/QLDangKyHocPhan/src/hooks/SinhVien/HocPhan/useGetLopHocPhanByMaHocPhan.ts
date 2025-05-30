import { useState, useEffect } from "react";
import type { LopHocPhanDTO } from "../../../apis/types/lopHocPhan";
import type { ApiResponse } from "../../..//apis/types/lopHocPhan";
import { callGetLopHocPhanByMaHocPhan } from "../../..//apis/services/lopHocPhan";

interface UseGetLopHocPhanByMaHocPhanResult {
  lopHocPhan: LopHocPhanDTO[] | null;
  loading: boolean;
  error: string | null;
}

export const useGetLopHocPhanByMaHocPhan = (
  maHocPhan: string | null
): UseGetLopHocPhanByMaHocPhanResult => {
  const [lopHocPhan, setLopHocPhan] = useState<LopHocPhanDTO[] | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!maHocPhan) {
      setLopHocPhan(null);
      setError(null);
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      setLoading(true);
      setError(null);

      try {
        const response: ApiResponse<LopHocPhanDTO[]> =
          await callGetLopHocPhanByMaHocPhan(maHocPhan);

        if (response.isSuccess) {
          setLopHocPhan(response.data ? response.data : null);
        } else {
          setError(response.message || "Lỗi khi lấy dữ liệu lớp học phần");
          setLopHocPhan(null);
        }
      } catch (err) {
        setError("Lỗi mạng hoặc không thể kết nối tới server");
        setLopHocPhan(null);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [maHocPhan]);

  return { lopHocPhan, loading, error };
};
